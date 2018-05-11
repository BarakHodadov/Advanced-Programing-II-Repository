using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Controller.Handlers;

namespace ImageServiceGUI.Model
{
    class SettingsModel : INotifyPropertyChanged
    {
        #region members
        private ICollection<string> handlersList;
        private string outputDir;
        private string sourceName;
        private string logName;
        private string thumbSize;
        #endregion

        #region event
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public SettingsModel()
        {
            handlersList = new ObservableCollection<string>();
            handlersList.Add("handler1");
            handlersList.Add("handler2");
            handlersList.Add("handler3");
        }

        #region Properties
        public ICollection<string> Handlers
        {
            get { return this.handlersList; }
            set { this.handlersList = value; }
        }

        public string OutputDir
        {
            get { return this.outputDir; }
            set { this.outputDir = value; }
        }

        public string SourceName
        {
            get { return this.sourceName; }
            set { this.sourceName = value; }
        }

        public string LogeName
        {
            get { return this.logName; }
            set { this.logName = value; }
        }

        public string ThumbSize
        {
            get { return this.thumbSize; }
            set { this.thumbSize = value; }
        }
        #endregion

        private string m_SelectedHandler;
        public string SelectedHandler
        {
            get { return this.m_SelectedHandler; }
            set
            {
                this.m_SelectedHandler = value;
                OnPropertyChanged("SelectedHandler");
            }
        }

        public void RemoveHandler(string handler)
        {
            this.handlersList.Remove(handler);
        }
    }
}
