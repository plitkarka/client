using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Plitkarka.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Plitkarka.ViewModels
{
    public class SearchViewModel : ReactiveObject
    {
        [Reactive] public bool IsSearchRequestsVisible { get; set; }

        [Reactive] public ObservableCollection<string> SearchRequests { get; set; }

        [Reactive] public ObservableCollection<SearchRecommendation> SearchRecommendations { get; set; }

        [Reactive] public string SearchRequest { get; set; }

        public ReactiveCommand<string, Unit> SearchCommand { get; set; }

        public SearchViewModel()
        {
            SearchRequests = new ObservableCollection<string>();
            SearchRecommendations = new ObservableCollection<SearchRecommendation>()
            {
                new(){RecommendationTag="#психологія", Amount = 1200 },
                new(){RecommendationTag="#стрес", Amount=576},
                new(){RecommendationTag="#design", Amount=12000},
                new(){RecommendationTag="#робота", Amount=5700},
                new(){RecommendationTag="#Ukraine", Amount=113200}
            };

            SearchCommand = ReactiveCommand.Create<string>(Search);
        }

        private void Search(string searchRequest)
        {
            if (!string.IsNullOrEmpty(searchRequest))
            {
                SearchRequests.Insert(0,searchRequest);
                IsSearchRequestsVisible = SearchRequests.Any();
            }

            SearchRequest = string.Empty;
        }
    }
}

