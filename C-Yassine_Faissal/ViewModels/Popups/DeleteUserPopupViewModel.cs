using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class DeleteUserPopupViewModel : ViewModelBase
    {
        private readonly LibraryContext _libraryContext;

        private string _searchEmail;
        public string SearchEmail
        {
            get { return _searchEmail; }
            set { _searchEmail = value; OnPropertyChanged(); }
        }

        public ICommand SearchAndDeleteCommand { get; }

        public DeleteUserPopupViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            SearchAndDeleteCommand = new RelayCommand(obj => SearchAndDeleteUser());
        }

        private void SearchAndDeleteUser()
        {
            var userToDelete = _libraryContext.Users.FirstOrDefault(u => u.Email == SearchEmail);

            if (userToDelete != null)
            {
                _libraryContext.Users.Remove(userToDelete);
                _libraryContext.SaveChanges();
                MessageBox.Show("User deleted successfully.");
            }
            else
            {
                MessageBox.Show("User not found");
            }
        }
    }
}
