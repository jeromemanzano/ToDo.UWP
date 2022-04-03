using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Mvvm.Input;

namespace ToDo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _newItem;
        public string NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged();
                AddItemCommand.NotifyCanExecuteChanged();
            }
        }
        public ObservableCollection<string> ToDoList { get; } = new ObservableCollection<string>();

        public IRelayCommand AddItemCommand { get; }

        public MainViewModel()
        {
            AddItemCommand = new RelayCommand(ExecuteAddItem, CanAddItem);
        }

        private bool CanAddItem()
        {
            return !string.IsNullOrEmpty(NewItem);
        }

        private void ExecuteAddItem()
        {
            ToDoList.Add(NewItem);
            NewItem = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
