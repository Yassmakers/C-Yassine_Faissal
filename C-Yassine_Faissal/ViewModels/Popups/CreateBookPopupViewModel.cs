using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels;
using System.Windows.Input;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class CreateBookPopupViewModel : ViewModelBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ICommand CreateBookCommand { get; }

        public CreateBookPopupViewModel()
        {
            CreateBookCommand = new RelayCommand(CreateBook, CanCreateBook);
        }

        private void CreateBook(object obj)
        {
            // Add logic to create a new book
        }

        private bool CanCreateBook(object obj)
        {
            return !string.IsNullOrEmpty(Title);
        }
    }
}
