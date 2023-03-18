using C_Yassine_Faissal.ViewModels;
using System.Windows.Input;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class UpdateBookPopupViewModel : ViewModelBase
    {
        private int _bookId;
        private string _newTitle;

        public int BookId
        {
            get => _bookId;
            set => SetProperty(ref _bookId, value);
        }

        public string NewTitle
        {
            get => _newTitle;
            set => SetProperty(ref _newTitle, value);
        }

        public ICommand UpdateBookCommand { get; }

        public UpdateBookPopupViewModel()
        {
            UpdateBookCommand = new RelayCommand(UpdateBook, CanUpdateBook);
        }

        private void UpdateBook(object obj)
        {
            // Add logic to update the book here
        }

        private bool CanUpdateBook(object obj)
        {
            return _bookId > 0 && !string.IsNullOrEmpty(_newTitle);
        }
    }
}
