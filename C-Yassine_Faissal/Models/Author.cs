// Author.cs in de Models map
using System.Collections.Generic;

namespace C_Yassine_Faissal.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}

