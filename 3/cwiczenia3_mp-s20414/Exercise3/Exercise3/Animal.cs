using System.ComponentModel.DataAnnotations;

public class Animal
{
    public int ID { get; set; }

    public string Name { set; get; } = string.Empty;

    public string Description { set; get; } = null;

    public string Category { set; get; } = string.Empty;

    public string Area { set; get; } = string.Empty;
}
