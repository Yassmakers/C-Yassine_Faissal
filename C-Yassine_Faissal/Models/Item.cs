namespace C_Yassine_Faissal.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; set; }
        public int AuthorId { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public Author Author { get; set; }
    }
}
