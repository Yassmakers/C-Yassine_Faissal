using System.Windows;
using System.Windows.Input;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.Views;
using Microsoft.Extensions.Options;
using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.ViewModels;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Sqlite;




namespace C_Yassine_Faissal.ViewModels
{

    public class LoginViewModel : ViewModelBase
    {
        // Velden voor het opslaan van e-mail en wachtwoord.
        private string _email;
        private string _password;

        // Eigenschappen voor het ophalen en instellen van e-mail en wachtwoord.
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        // ICommand eigenschappen voor het inloggen en inloggen als gast.
        public ICommand LoginCommand { get; }
        public ICommand GuestLoginCommand { get; }

        // Instantie van LibraryContext om met de database te communiceren.
        private LibraryContext _context;

        // Constructor voor het initialiseren van commando's en de datacontext.
        public LoginViewModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            string dbPath = @"C:\Users\symon\source\repos\C-Yassine_Faissal\C-Yassine_Faissal\bin\Debug\net6.0-windows\library.db";
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            _context = new LibraryContext(optionsBuilder.Options);

            // Initialiseer de RelayCommand voor het inloggen en inloggen als gast.
            LoginCommand = new RelayCommand(obj => Login(), obj => CanLogin(obj));
            GuestLoginCommand = new RelayCommand(obj => GuestLogin());
        }

        // Methode om in te loggen als gast.
        private void GuestLogin()
        {
            // Navigeer naar de main window als gast
            var mainView = new MainWindow(false, false);
            mainView.Show();
            CloseAction?.Invoke();
        }

        // Actie om het venster te sluiten.
        public Action CloseAction { get; set; }

        // Methode om in te loggen met e-mail en wachtwoord.
        private void Login()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (user != null)
            {
                bool isAdmin = user.Role == UserRole.Admin;
                bool isEmployee = user.Role == UserRole.Employee;

                var mainView = new MainWindow(isAdmin, isEmployee); // Use the new constructor
                CloseAction?.Invoke();
                mainView.Show();
            }
            else
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Methode om te controleren of inloggen mogelijk is.
        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }
        // Methode om eigenschappen in te stellen en PropertyChanged event te activeren.
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
