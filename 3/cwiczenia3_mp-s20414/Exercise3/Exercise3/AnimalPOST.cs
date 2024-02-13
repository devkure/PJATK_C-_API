using System.ComponentModel.DataAnnotations;
public class AnimalPOST
{
    [Required]
    public int ID { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Name { set; get; } = string.Empty;

    public string Description { set; get; } = null;

    public string Category { set; get; } = string.Empty;

    public string Area { set; get; } = string.Empty;
}
