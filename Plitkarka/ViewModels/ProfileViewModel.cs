using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.ViewModels
{
    public class Profile
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class Segment : ReactiveObject
    {
        [Reactive] public string SegmentName { get; set; }

        [Reactive] public bool IsSelected { get; set; }

        public string UnderlineColor => "Blue";

        [Reactive] public string SegmentContent { get; set; }

        public Segment(string segmentName)
        {
            SegmentName = segmentName;
        }
    }

    public class ProfileViewModel : ReactiveObject
    {

        [Reactive] public ObservableCollection<Segment> Segments { get; set; }

        [Reactive] public ObservableCollection<Profile> Recommendations { get; set; }

        public ReactiveCommand<int,Unit> FollowCommand { get; }

        public ReactiveCommand<Segment,Unit> ChangeContentCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenEditPageCommand { get; }

        public ProfileViewModel()
		{
            Recommendations = new ObservableCollection<Profile>
            {
                new Profile { UserId = 1, Nickname = "user1", PhotoUrl = "https://example.com/user1.jpg" },
                new Profile { UserId = 2, Nickname = "user2", PhotoUrl = "https://example.com/user2.jpg" },
                new Profile { UserId = 3, Nickname = "user3", PhotoUrl = "https://example.com/user2.jpg" },
                new Profile { UserId = 4, Nickname = "user4", PhotoUrl = "https://example.com/user2.jpg" },

                // Add more profiles as needed
            };

            Segments = new ObservableCollection<Segment>
            {
                new Segment("Плітки"),
                new Segment("Репліти"),
                new Segment("Медіа"),
                new Segment("Вподобайки"),
            };


            ChangeContentCommand = ReactiveCommand.Create<Segment>(ChangeContent);

            FollowCommand = ReactiveCommand.CreateFromTask<int>(async _ =>
            {
                int userId = 123;
                await FollowUser(userId);
            });

            //OpenEditPageCommand = ReactiveCommand.Create(async () =>);

            Segments[0].IsSelected = true;

        }

        private void ChangeContent(Segment segment)
        {
            foreach (var seg in Segments)
            {
                seg.IsSelected = (seg == segment);
                seg.SegmentContent = seg.SegmentName;
            }
        }

        private async Task FollowUser(int userId)
        {
            await Task.Delay(1000);
        }
    }
}

