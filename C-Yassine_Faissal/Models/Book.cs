using System.Collections.Generic;

namespace C_Yassine_Faissal.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        // Other properties as needed
    }
}
