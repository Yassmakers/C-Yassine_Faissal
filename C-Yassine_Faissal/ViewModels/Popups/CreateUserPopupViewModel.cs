﻿using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

public class CreateUserPopupViewModel : ViewModelBase
{
    private LibraryContext _libraryContext;

    public ICommand CreateUserCommand { get; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole SelectedRole { get; set; }

    public ObservableCollection<UserRole> Roles { get; set; }

    public CreateUserPopupViewModel(LibraryContext libraryContext)
    {
        _libraryContext = libraryContext;

        CreateUserCommand = new RelayCommand(CreateUser);

        Roles = new ObservableCollection<UserRole>
        {
            UserRole.Admin,
            UserRole.Employee,
            UserRole.Guest
        };
    }
    public string Password { get; set; }
    public string UserName { get; set; }
    private void CreateUser(object obj)
    {
        var newUser = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            UserName = UserName, 
            Role = SelectedRole,
            Password = Password
        };


        _libraryContext.Users.Add(newUser);

        try
        {
            _libraryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while saving the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBox.Show($"User '{FirstName} {LastName}' has been created successfully.", "User Created", MessageBoxButton.OK, MessageBoxImage.Information);

        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        SelectedRole = UserRole.Guest;
    }
}
