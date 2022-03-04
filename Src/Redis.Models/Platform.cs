namespace Redis.Models;

public class Platform
{
    public Platform()
    {
        Id = $"platform:{Guid.NewGuid()}";
        Name = string.Empty;
    }
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }

}
