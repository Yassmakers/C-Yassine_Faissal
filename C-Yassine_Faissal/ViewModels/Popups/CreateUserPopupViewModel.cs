using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels;
using System.Windows.Input;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class CreateUserPopupViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICommand CreateUserCommand { get; }

        public CreateUserPopupViewModel()
        {
            CreateUserCommand = new RelayCommand(CreateUser, CanCreateUser);
        }

        private void CreateUser(object obj)
        {
            // Add logic to create a new user
        }

        private bool CanCreateUser(object obj)
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}