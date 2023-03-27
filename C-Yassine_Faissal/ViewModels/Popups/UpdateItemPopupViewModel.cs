using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace C_Yassine_Faissal.ViewModels.Popups
{
        public class UpdateItemPopupViewModel 
        {
            private readonly LibraryContext _libraryContext;

            public ObservableCollection<Author> Authors { get; set; }
            public ObservableCollection<ItemType> ItemTypes { get; set; }
            public ObservableCollection<ItemStatus> ItemStatuses { get; set; }
            public ItemStatus SelectedItemStatus { get; set; }
            public UpdateItemPopupViewModel(LibraryContext libraryContext)
            {
                _libraryContext = libraryContext;
                Authors = new ObservableCollection<Author>(_libraryContext.Authors.ToList());
                ItemTypes = new ObservableCollection<ItemType>(Enum.GetValues(typeof(ItemType)).Cast<ItemType>());
                ItemStatuses = new ObservableCollection<ItemStatus>(Enum.GetValues(typeof(ItemStatus)).Cast<ItemStatus>());
            }


       



        public void UpdateItem(Item selectedItem)
            {
                selectedItem.ItemStatus = SelectedItemStatus;
            }
            public void LoadItem(Item selectedItem)
            {
                SelectedItemStatus = selectedItem.ItemStatus;
            }

        public Item FindItemByTitle(string title)
        {
            return _libraryContext.Items.Include(i => i.Author).FirstOrDefault(i => i.Title == title);
        }

        // delete functie
        public async Task DeleteItemAsync(Item item)
        {
            _libraryContext.Items.Remove(item);
            await _libraryContext.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            _libraryContext.Update(item);
            await _libraryContext.SaveChangesAsync();
        }
    }
}