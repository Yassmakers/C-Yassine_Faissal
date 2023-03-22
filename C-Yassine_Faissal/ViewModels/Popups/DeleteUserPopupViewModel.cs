using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using C_Yassine_Faissal.Commands;


namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class DeleteUserPopupViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICommand CreateUserCommand { get; }

        public DeleteUserPopupViewModel()
        {
            CreateUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
        }

        private void DeleteUser(object obj)
        {
            // Add logic to create a new user
        }

        private bool CanDeleteUser(object obj)
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}
