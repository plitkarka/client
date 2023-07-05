using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.Models;

public class Profile : ReactiveObject
{
    public int UserId { get; set; }

    [Reactive] public string Nickname { get; set; }
    
    [Reactive] public string Name { get; set; }
    
    [Reactive] public int Followers { get; set; }
    
    [Reactive] public int Following { get; set; }
    
    [Reactive] public string Bio { get; set; }
    
    [Reactive] public string Link { get; set; }

    [Reactive] public string PhotoUrl { get; set; }
}