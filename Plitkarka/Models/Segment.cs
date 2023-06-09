using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.Models;

public class Segment : ReactiveObject
{
    [Reactive] public string SegmentName { get; set; }

    [Reactive] public bool IsSelected { get; set; }
    
    public Segment(string segmentName)
    {
        SegmentName = segmentName;
    }
}