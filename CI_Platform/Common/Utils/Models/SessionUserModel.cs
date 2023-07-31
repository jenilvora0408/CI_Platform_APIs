namespace Common.Utils.Models;
public class SessionUserModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Type { get; set; }
}