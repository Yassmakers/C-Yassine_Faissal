﻿public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Author Author { get; set; }
    public int AuthorId { get; set; }
}