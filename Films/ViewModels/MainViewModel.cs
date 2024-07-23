using Films.Models;
using Films.Services;
using Films.Utilities;
using System.Windows.Input;

namespace Films.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(); }
        }

        private readonly KinopoiskAPI _api;
        private readonly ISharedDataService _sharedDataModel;

        public ICommand HomeCommand { get; }
        public ICommand FilmCommand { get; }
        public ICommand FavoritesCommand { get; }

        public MainViewModel(INavigationService navigationService, KinopoiskAPI api, ISharedDataService sharedData)
        {
            _api = api;
            _sharedDataModel = sharedData;

            NavigationService = navigationService;

            HomeCommand = new RelayCommand(Home);
            FilmCommand = new RelayCommand(Film);
            FavoritesCommand = new RelayCommand(Favorites);

           NavigationService.NavigateTo<HomeViewModel>();
        }

        private void Film(object viewModel)
        {
            NavigationService.NavigateTo<FilmViewModel>();
        }

        private void Favorites(object viewModel)
        {
            NavigationService.NavigateTo<FavoritesViewModel>();
        }

        private void Home(object viewModel)
        {
            NavigationService.NavigateTo<HomeViewModel>();
        }
    }
}
