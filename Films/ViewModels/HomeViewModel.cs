using Films.Models;
using Films.Services;
using Films.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Films.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private KinopoiskAPI _api;
        private readonly ISharedDataService _sharedData;

        private ObservableCollection<FilmModel> _filmModels = new ObservableCollection<FilmModel>();
        public ObservableCollection<FilmModel> FilmModels
        {
            get => _filmModels;
            set { _filmModels = value; OnPropertyChanged(nameof(FilmModels)); }
        }

        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(); }
        }

        public ICommand GetFilmsCommand { get; }
        public ICommand AddToFavoritesCommand { get; }
        public ICommand NavigateToFilmCommand { get; }

        public HomeViewModel(INavigationService navigationService, ISharedDataService sharedData, KinopoiskAPI api)
        {
            _api = api;
            _sharedData = sharedData;

            NavigationService = navigationService;
            FilmModels = new ObservableCollection<FilmModel>();

            NavigateToFilmCommand = new RelayCommand(NavigateToFilmViewModel);
            GetFilmsCommand = new RelayCommand(async (o) => await GetFilms());
            AddToFavoritesCommand = new RelayCommand(AddToFavorites);

            GetFilmsCommand.Execute(null);
        }

        private async Task GetFilms()
        {
            var films = await _api.GetFilmsAsync();

            FilmModels = new ObservableCollection<FilmModel>(films);
        }

        private void AddToFavorites(object selectedFilm)
        {
            if (selectedFilm != null)
            {
                var film = selectedFilm as FilmModel;
                _sharedData.AddSharedId(film.Id);
            }
        }

        private void NavigateToFilmViewModel(object selectedFilm)
        {
            if (selectedFilm != null)
            {
                var film = selectedFilm as FilmModel;
                _sharedData.SetLastFilmId(film.Id);
                NavigationService.NavigateTo<FilmViewModel>();
            }
        }
    }
}