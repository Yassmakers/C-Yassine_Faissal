﻿using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.ViewModels;
using System.Windows.Input;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.Data;
using System.Threading.Tasks;
using System.Linq;
using static C_Yassine_Faissal.Commands.RelayCommand;


namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class DeleteItemPopupViewModel : ViewModelBase
    {
        private readonly LibraryContext _libraryContext;

        public DeleteItemPopupViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task DeleteItemByTitleAsync(string title)
        {
            var item = _libraryContext.Items.FirstOrDefault(i => i.Title == title);
            if (item != null)
            {
                _libraryContext.Items.Remove(item);
                await _libraryContext.SaveChangesAsync();
            }
        }
    }
}