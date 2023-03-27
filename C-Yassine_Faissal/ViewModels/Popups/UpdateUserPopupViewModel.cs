using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System;
using static C_Yassine_Faissal.Commands.RelayCommand;
using System.ComponentModel;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    // Dit ViewModel behandelt de logica achter het bijwerken van een User object.
    public class UpdateUserPopupViewModel : ViewModelBase
    {
        // De data context van de Library.
        private readonly LibraryContext _libraryContext;
        private User _selectedUser;

        private string _searchEmail;
        public string SearchEmail
        {
            get { return _searchEmail; }
            set { _searchEmail = value; OnPropertyChanged(); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private UserRole _selectedRole;
        public UserRole SelectedRole
        {
            get { return _selectedRole; }
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        public ObservableCollection<UserRole> Roles { get; } = new ObservableCollection<UserRole>
        {
             UserRole.Admin,
             UserRole.Employee,
             UserRole.Guest
        };

        // ICommand eigenschappen voor de verschillende acties.
        public ICommand SearchCommand { get; }
        public ICommand SaveCommand { get; }
        // Constructor: Initialiseert de data context en ICommand objecten.
        public UpdateUserPopupViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            SearchCommand = new RelayCommand(obj => SearchUser());
            SaveCommand = new RelayCommand(obj => SaveUser(), obj => CanSaveUser());
        }
        
        // Zoek een User object op basis van e-mailadres.
        private void SearchUser()
        {
            _selectedUser = _libraryContext.Users.FirstOrDefault(u => u.Email == SearchEmail);
            if (_selectedUser != null)
            {
                FirstName = _selectedUser.FirstName;
                LastName = _selectedUser.LastName;
                Email = _selectedUser.Email;
                SelectedRole = _selectedUser.Role;
            }
            else
            {
                MessageBox.Show("User not found");
            }
        }
        
        // Controleer of een User object kan worden opgeslagen.
        private bool CanSaveUser()
        {
            return _selectedUser != null && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Email);
        }

        // Sla het geselecteerde User object op.
        private void SaveUser()
        {
            if (_selectedUser != null)
            {
                _selectedUser.FirstName = FirstName;
                _selectedUser.LastName = LastName;
                _selectedUser.Email = Email;
                _selectedUser.Role = SelectedRole;

                _libraryContext.SaveChanges();
                MessageBox.Show("User updated successfully.");
            }
            else
            {
                MessageBox.Show("Please search for a user before updating");
            }
        }
    }
}
