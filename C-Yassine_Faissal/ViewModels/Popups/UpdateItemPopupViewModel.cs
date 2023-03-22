using C_Yassine_Faissal.ViewModels;
using System.Windows.Input;
using C_Yassine_Faissal.Commands;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class UpdateItemPopupViewModel : ViewModelBase
    {
        private int _bookId;
        private string _newTitle;

        public int ItemId
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

        public UpdateItemPopupViewModel()
        {
            UpdateBookCommand = new RelayCommand(UpdateItem, CanUpdateItem);
        }

        private void UpdateItem(object obj)
        {
            // Add logic to update the book here
        }

        private bool CanUpdateItem(object obj)
        {
            return _bookId > 0 && !string.IsNullOrEmpty(_newTitle);
        }
    }
}
