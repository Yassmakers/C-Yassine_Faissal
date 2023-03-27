using C_Yassine_Faissal.Commands;
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
    // Constructor: Initialiseert de data context, stelt de CreateUserCommand in en vult de lijst met gebruikersrollen
    public CreateUserPopupViewModel(LibraryContext libraryContext)
    {
        _libraryContext = libraryContext;
        // Initialiseren van de CreateUserCommand met een actie om de CreateUser-functie aan te roepen
        CreateUserCommand = new RelayCommand(CreateUser);
        // Initialiseren van de lijst met gebruikersrollen
        Roles = new ObservableCollection<UserRole>
        {
            UserRole.Admin,
            UserRole.Employee,
            UserRole.Guest
        };
    }
    public string Password { get; set; }
    public string UserName { get; set; }
    // Creëert een nieuwe gebruiker met de opgegeven gegevens en voegt deze toe aan de database
    private void CreateUser(object obj)
    {
        // Creëer een nieuw User-object met de ingevoerde gegevens van de gebruiker
        var newUser = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            UserName = UserName, 
            Role = SelectedRole,
            Password = Password
        };

        // Voeg de nieuwe gebruiker toe aan de DbSet van Users in de LibraryContext
        _libraryContext.Users.Add(newUser);
        // Probeer de wijzigingen op te slaan; toon een foutbericht als er een uitzondering optreedt
        try
        {
            _libraryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while saving the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        // Als het opslaan succesvol is, toon een informatiebericht en wis de invoervelden
        MessageBox.Show($"User '{FirstName} {LastName}' has been created successfully.", "User Created", MessageBoxButton.OK, MessageBoxImage.Information);

        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        SelectedRole = UserRole.Guest;
    }
}
