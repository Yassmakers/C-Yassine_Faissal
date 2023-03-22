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
        private string _email;
        private string _password;

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

        public ICommand LoginCommand { get; }

        private LibraryContext _context;
        public LoginViewModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            string dbPath = System.IO.Path.Combine(AppContext.BaseDirectory, "library.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            _context = new LibraryContext(optionsBuilder.Options);
            _context.Database.EnsureCreated(); // Only call EnsureCreated()

            LoginCommand = new RelayCommand(obj => Login(), obj => CanLogin(obj));
        }



        public Action CloseAction { get; set; }

        private void Login()
        {
            {

                var user = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if (user != null)
                {

                    // Gebruiker succesvol gevalideerd, navigeer naar hoofdscherm
                    var mainView = new MainWindow();
                    mainView.Show();
                    CloseAction?.Invoke(); 

                }
                else
                {
                    // Toon foutmelding
                    MessageBox.Show("Invalid email or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

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
