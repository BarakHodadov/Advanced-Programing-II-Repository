using ImageServiceGUI.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceGUI.ViewModel
{
    /*
     * In order to implement this class we used the following resources
     * a given code example from Dor
     * this link - https://stackoverflow.com/questions/12422945/how-to-bind-wpf-button-to-a-command-in-viewmodelbase?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
     */
    class SettingsViewModel : INotifyPropertyChanged
    {
        #region event
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        // members
        private SettingsModel settingsModel;
        public ICommand RemoveCommand { get; set; }

        #region properties for display
        public ICollection<string> handlersList
        {
            get { return this.settingsModel.Handlers; }
        }

        public string OutputDir
        {
            get { return this.settingsModel.OutputDir; }
        }

        public string SourceName
        {
            get { return this.settingsModel.SourceName; }
        }

        public string LogeName
        {
            get { return this.settingsModel.LogeName; }
        }

        public string ThumbSize
        {
            get { return this.settingsModel.ThumbSize; }
        }
        #endregion

        // constructor
        public SettingsViewModel()
        {
            this.settingsModel = new SettingsModel(); // creaet new model
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove); // crete the remove command. set action and time to act
            PropertyChanged += RemoveHandler;
            settingsModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public void RemoveHandler(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command?.RaiseCanExecuteChanged();
        }

        private void OnRemove(object obj)
        {
            this.settingsModel.RemoveHandler(this.SelectedHandler); // remove from the list
            this.settingsModel.SelectedHandler = null; // initialize the next choise to null
        }

        private bool CanRemove(object obj)
        {
            // if an item was selected (it is not null and there is an item) return true
            return !string.IsNullOrEmpty(this.SelectedHandler);
        }

        public string SelectedHandler
        {
            get { return this.settingsModel.SelectedHandler; }
            set
            {
                this.settingsModel.SelectedHandler = value;
                NotifyPropertyChanged("SelectedHandler");
            }
        }
    }
}
