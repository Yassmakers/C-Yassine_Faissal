using System.Collections.Generic;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Item> Items { get; set; }
}