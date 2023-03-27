using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    // Dit ViewModel behandelt de logica achter het bijwerken van een Item object.
    public class UpdateItemPopupViewModel 
        {
        // De data context van de Library.
        private readonly LibraryContext _libraryContext;
        
        // ObservableCollection eigenschappen voor de verschillende lijsten.
            public ObservableCollection<Author> Authors { get; set; }
            public ObservableCollection<ItemType> ItemTypes { get; set; }
            public ObservableCollection<ItemStatus> ItemStatuses { get; set; }
            public ItemStatus SelectedItemStatus { get; set; }
        // Constructor: Initialiseert de data context, laadt de auteurs-, itemtypes- en itemstatuslijsten, en stelt het te bewerken Item in
        public UpdateItemPopupViewModel(LibraryContext libraryContext)
            {
                _libraryContext = libraryContext;
                Authors = new ObservableCollection<Author>(_libraryContext.Authors.ToList());
                ItemTypes = new ObservableCollection<ItemType>(Enum.GetValues(typeof(ItemType)).Cast<ItemType>());
                ItemStatuses = new ObservableCollection<ItemStatus>(Enum.GetValues(typeof(ItemStatus)).Cast<ItemStatus>());
            }




        // Update het geselecteerde Item object.
        public void UpdateItem(Item selectedItem)
            {
                selectedItem.ItemStatus = SelectedItemStatus;
            }
        // Laad het geselecteerde Item object.
        public void LoadItem(Item selectedItem)
            {
                SelectedItemStatus = selectedItem.ItemStatus;
            }
        
        // Zoek een Item object op titel.
        public Item FindItemByTitle(string title)
        {
            return _libraryContext.Items.Include(i => i.Author).FirstOrDefault(i => i.Title == title);
        }

        // Verwijder een Item object asynchroon.
        public async Task DeleteItemAsync(Item item)
        {
            _libraryContext.Items.Remove(item);
            await _libraryContext.SaveChangesAsync();
        }
        // Update het geselecteerde Item object in de database

        public async Task UpdateItemAsync(Item item)
        {
            _libraryContext.Update(item);
            await _libraryContext.SaveChangesAsync();
        }
    }
}