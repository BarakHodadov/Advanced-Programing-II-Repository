using ImageService;
using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    /*
     * In order to implement this class we used the following resources
     * a given code example from Dor
     * this link - https://stackoverflow.com/questions/12422945/how-to-bind-wpf-button-to-a-command-in-viewmodelbase?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
     */
    class SettingViewModel : INotifyPropertyChanged
    {
        #region event
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private LogModel log;
        public ICollection<Log> logsList
        {
            get { return this.log.Logs; }
        }
        public LogModel Log
        {
            set { this.log = value; }
            get { return this.log; }
        }

        // constructor
        public SettingViewModel()
        {
            this.log = new LogModel();
            log.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }
    }
}
