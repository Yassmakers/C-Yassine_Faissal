// Author.cs in de Models map
using System.Collections.Generic;

namespace C_Yassine_Faissal.Models
{
    // Deze klasse representeert een auteur en voldoet aan de projectvereisten voor het
    // hebben van auteurs in het model. 
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // We hebben een ICollection<Item> property toegevoegd 
        // om de relatie tussen auteurs en items te beschrijven.
        public ICollection<Item> Items { get; set; }
    }
}

