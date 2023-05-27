using System.Collections.ObjectModel;
using System.Reactive;
using Plitkarka.Infrastructure.Interfaces;
using Plitkarka.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.ViewModels
{
    public class SelectedPostViewModel : ReactiveObject
    {
        [Reactive] public Comment UsersComment { get; set; }
        [Reactive] public bool IsDataLoaded { get; set; }

        [Reactive] public Post Post { get; set; }

        [Reactive] public ObservableCollection<Comment> Comments { get; set; }

        public ReactiveCommand<Unit, Unit> GoBackCommand { get; }

        public ReactiveCommand<Comment, Unit> LikeCommentCommand { get; }

        public SelectedPostViewModel(INavigationService navigationService, IMessagingService messagingService)
        {
            Comments = new ObservableCollection<Comment>
            {
                new() {AuthorName = "olena.kosak", CommentContent="Дуже важлива тема! Дякую за допис", CommentDate=DateTime.Now, LikesCount=10},
                new() {AuthorName = "vikaaa", CommentContent="Дуже цікаво, буду мати на увазі!", CommentDate=DateTime.Now, LikesCount=0},
                new() {AuthorName="sashkaaa", CommentContent="Ну хз, мені і так норм, понапридумують тут проблем....",CommentDate=DateTime.Now, LikesCount=2}
            };
            
            GoBackCommand = ReactiveCommand.Create(() =>
            {
                navigationService.GoBackAsync();
            });

            LikeCommentCommand = ReactiveCommand.Create<Comment>(LikeComment);

            messagingService.Subscribe<FeedDashboardViewModel, Post>(this, "SelectedPost", (sender, post) => OnPostSelected(sender, post));
        }

        private void OnPostSelected(FeedDashboardViewModel sender, Post post)
        {
            Post = post;
            IsDataLoaded = true;
        }

        private void LikeComment(Comment comment)
        {
            comment.IsLiked = !comment.IsLiked;
            comment.LikesCount += (comment.IsLiked ? 1 : -1);
        }
    }
}