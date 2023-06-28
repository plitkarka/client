namespace Plitkarka.Client.Models.User;

public record UserUpdateProfile
{
    public string? Login { get; set; }  

    public string? Name { get; set; }  

    public string? Description { get; set; }  

    public string? Link { get; set; }  
}
