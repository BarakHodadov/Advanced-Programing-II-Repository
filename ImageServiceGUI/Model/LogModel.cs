using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService;
using ImageService.Infrastructure;
using ImageServiceGUI.GUIClient;

namespace ImageServiceGUI.Model
{
    class LogModel : INotifyPropertyChanged
    {
        #region event
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private ICollection<Log> logsList;

        public LogModel()
        {
            GUITCPClient client = GUITCPClient.Instance;
            client.Connect();
            string logs = client.sendrecieve(client.makeData(CommandEnum.LogCommand));
            string[] logsFromCommand = logs.Remove(logs.Length - 1).Split(';'); // remove the last ; from the logs list and split them
            logsList = new ObservableCollection<Log>();

            string type, message;
            foreach (string log in logsFromCommand)
            {
                type = log.Split('#')[0];
                message = log.Split('#')[1];
                logsList.Add(new Log(type, message));
            }
        }

        public ICollection<Log> Logs
        {
            get { return this.logsList; }
        }
    } 
}
