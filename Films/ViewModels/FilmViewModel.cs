using Films.Models;
using Films.Services;
using Films.Utilities;

namespace Films.ViewModels
{
    public class FilmViewModel : ViewModelBase
    {

        private FilmModel _model;
        public FilmModel FilmModel
        {
            get => _model;
            set { _model = value; OnPropertyChanged(); }
        }

        public FilmViewModel(ISharedDataService sharedData, KinopoiskAPI api)
        {
            if (sharedData.GetLastFilmId() != 0)
            {
                InitializeAsync(sharedData, api);
            }
        }

        private async void InitializeAsync(ISharedDataService sharedData, KinopoiskAPI api)
        {
            FilmModel = await api.GetFilmInfoByIdAsync(sharedData.GetLastFilmId());
        }
    }
}
