using Films.Models;
using Films.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Films.Services;

namespace Films.ViewModels
{
    public class FavoritesViewModel : ViewModelBase
    {
        private readonly KinopoiskAPI _api;
        private readonly ISharedDataService _sharedData;

        private ObservableCollection<FilmModel> _filmModels = new ObservableCollection<FilmModel>();
        public ObservableCollection<FilmModel> FilmModels
        {
            get => _filmModels;
            set { _filmModels = value; OnPropertyChanged(); }
        }

        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(); }
        }

        public ICommand RemoveFromFavoritesCommand { get; }
        public ICommand NavigateToFilmCommand { get; }

        public FavoritesViewModel(INavigationService navigationService, ISharedDataService sharedData, KinopoiskAPI api)
        {
            _sharedData = sharedData;
            _api = api;

            NavigationService = navigationService;

            RemoveFromFavoritesCommand = new RelayCommand(RemoveFromFavorites);
            NavigateToFilmCommand = new RelayCommand(NavigateToFilmViewModel);

            GetFavoriteFilms();
        }

        private async Task GetFavoriteFilms()
        {
            var films = await _api.GetFilmsByIdAsync(_sharedData.GetSharedIds());

            FilmModels = new ObservableCollection<FilmModel>(films);
        }

        private void RemoveFromFavorites(object selectedFilm)
        {
            if (selectedFilm != null)
            {
                var filmModel = selectedFilm as FilmModel;
                _sharedData.RemoveSharedId(filmModel.Id);

                RemoveFilmById(filmModel.Id);
            }
        }

        private void RemoveFilmById(int id)
        {
            var filmToRemove = FilmModels.FirstOrDefault(film => film.Id == id);
            if (filmToRemove != null)
            {
                FilmModels.Remove(filmToRemove);
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
