using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ImageService.Logging.Modal;
using ImageService.Server;
using System.Configuration;
using ImageService.Logging;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Communication;

namespace ImageService
{
    public partial class ImageService : ServiceBase
    {
        public event EventHandler CloseService;

        private int eventId = 1;

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        public ImageService(string[] args)
        {
            InitializeComponent();

            string sourceName = "ImageServiceSource";
            string logName = "ImageServiceLog";
            if (args.Count() > 0)
            {
                sourceName = args[0];
            }
            if (args.Count() > 1)
            {
                logName = args[1];
            }

            eventLog1 = new EventLog();
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, logName);
            }
            eventLog1.Source = sourceName;
            eventLog1.Log = logName;


        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In OnStart");

            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds 
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);


            //creating the server.
            int thumbnailSize = int.Parse(AppConfigReader.Instance.GetValueByKey("ThumbnailSize"));
            string outputDir = AppConfigReader.Instance.GetValueByKey("OutputDir");

            string handler = AppConfigReader.Instance.GetValueByKey("Handler");
            string[] handlerDirs = { handler };
            if (handler.Contains(";"))
            {
                handlerDirs = AppConfigReader.Instance.GetValueByKey("Handler").Split(';');
            }

            LoggingService logger = new LoggingService();
            logger.MessageRecieved += LogWriteEntry;

            Logger logs = new Logger();
            logger.MessageRecieved += logs.addLog;

            ImageController imageController = new ImageController(new ImageServiceModal(outputDir, thumbnailSize), logs);
            List<string> dirsList = new List<string>(handlerDirs);
            ImageServer server = new ImageServer(imageController, logger, dirsList);
            server.CreateHandlers();

            CloseService += delegate
            {
                server.OnCloseServer(this, EventArgs.Empty);
            };

            TCPServer tcpserver = new TCPServer("127.0.0.1", 8000, new ClientHandler());
            tcpserver.OnCommandRecieved += imageController.ExecuteCommand;
            tcpserver.Start();
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue");
        }
        protected override void OnPause()
        {
            eventLog1.WriteEntry("In OnPause");
        }
        protected override void OnStop()
        {
            // Update the service state to Stop Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In onStop");

            // Update the service state to Stop.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            CloseService?.Invoke(this, EventArgs.Empty);
        }

        private void LogWriteEntry(object source, MessageRecievedEventArgs e)
        {
            EventLogEntryType msgType;
            switch (e.Status)
            {
                case MessageTypeEnum.WARNING:
                    msgType = EventLogEntryType.Warning;
                    break;
                case MessageTypeEnum.FAIL:
                    msgType = EventLogEntryType.Error;
                    break;
                default:
                    msgType = EventLogEntryType.Information;
                    break;
            }
            eventLog1.WriteEntry(e.Message, msgType);
        }

        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            Console.WriteLine("Enter any key to continue");
            Console.ReadLine();
            OnStop();
        }
    }
}
