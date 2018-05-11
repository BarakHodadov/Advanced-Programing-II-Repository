using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService;

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
            logsList = new ObservableCollection<Log>();
            Log log1 = new Log("Info", "This is info");
            Log log2 = new Log("Warning", "This is Warning");
            Log log3 = new Log("Error", "This is Error");
            Log log4 = new Log("Info", "This is info");
            Log log5 = new Log("Warning", "This is Warning");
            Log log6 = new Log("Error", "This is Error");
            logsList.Add(log1);
            logsList.Add(log2);
            logsList.Add(log3);
            logsList.Add(log4);
            logsList.Add(log5);
            logsList.Add(log6);
        }
        
        public ICollection<Log> Logs
        {
            get { return this.logsList; }
        }

        public void setColors()
        {
            foreach (Log log in logsList)
            {

            }
        }
    } 
}
