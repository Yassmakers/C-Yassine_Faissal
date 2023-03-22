using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.ViewModels;
using System.Windows.Input;
using C_Yassine_Faissal.Models;



namespace C_Yassine_Faissal.ViewModels.Popups
    {
        public class DeleteItemPopupViewModel : ViewModelBase
    {
            private string _name;

            public string Name
            {
                get => _name;
                set => SetProperty(ref _name, value);
            }

            public ICommand CreateUserCommand { get; }

            public DeleteItemPopupViewModel()
            {
                CreateUserCommand = new RelayCommand(DeleteItem, CanDeleteItem);
            }

            private void DeleteItem(object obj)
            {
                // Add logic to create a new user
            }

            private bool CanDeleteItem(object obj)
            {
                return !string.IsNullOrEmpty(Name);
            }
        }
    }