namespace C_Yassine_Faissal.Models
{
    public class Item
    {
        // Properties voor het item-model, in overeenstemming met de projectvereisten
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // We gebruiken de ItemType enumeratie om de verschillende itemtypen te onderscheiden
        public ItemType ItemType { get; set; }
        public int AuthorId { get; set; }
        // We gebruiken de ItemStatus enumeratie om de status van een item bij te houden
        public ItemStatus ItemStatus { get; set; }
        // Navigatieproperty om de relatie tussen item en auteur te definiëren
        public Author Author { get; set; }
    }
}
