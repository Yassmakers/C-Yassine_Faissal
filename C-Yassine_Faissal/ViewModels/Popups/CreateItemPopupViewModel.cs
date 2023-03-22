using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels;
using System.Windows.Input;
using C_Yassine_Faissal.Commands; 

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class CreateItemPopupViewModel : ViewModelBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ICommand CreateBookCommand { get; }

        public CreateItemPopupViewModel()
        {
            CreateBookCommand = new RelayCommand(CreateItem, CanCreateItem);
        }

        private void CreateItem(object obj)
        {
            // Add logic to create a new item
        }

        private bool CanCreateItem(object obj)
        {
            return !string.IsNullOrEmpty(Title);
        }
    }
}
