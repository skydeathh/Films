using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Films.Models
{
    public class KinopoiskAPI
    {
        private const string _url = "https://kinopoiskapiunofficial.tech/api/v2.2";
        private const string _apiKey = "YOUR_API_KEY";
        public KinopoiskAPI() { }

        public async Task<List<FilmModel>> GetFilmsAsync()
        {
            var url = _url + "/films";

            using (var client = new HttpClient())
            {
                var films = new List<FilmModel>();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    films.Add(new FilmModel() { Name = "Check your internet connection", Id = 0 });
                    return films;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseContent);


                foreach (var item in json["items"])
                {
                    films.Add(MapFilmJson(item.ToObject<JObject>()));
                }

                return films;
            }
        }

        public async Task<List<FilmModel>> GetFilmsByIdAsync(List<int> ids)
        {
            var url = _url + "/films/";
            var films = new List<FilmModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);

                foreach (var id in ids)
                {
                    var response = await client.GetAsync(url + id.ToString());

                    if (!response.IsSuccessStatusCode)
                    {

                        return new List<FilmModel>() { new FilmModel() { Name = "Check your internet connection", Id = 0 } };
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseContent);

                    films.Add(MapFilmJson(json));
                }

                return films;
            }
        }

        public async Task<FilmModel> GetFilmInfoByIdAsync(int id)
        {
            var url = _url + "/films/";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);

                var response = await client.GetAsync(url + id.ToString());

                if (!response.IsSuccessStatusCode)
                {
                    return new FilmModel() { Name = "Check your internet connection", Id = 0 };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseContent);

                return MapFilmWithDescriptionJson(json);
            }
        }

        private FilmModel MapFilmJson(JObject json)
        {
            return new FilmModel
            {
                Id = (int)json["kinopoiskId"],
                Name = CheckName(json),
                Year = (int)json["year"],
                ImageUrl = json["posterUrlPreview"].ToString(),
                Rating = json["ratingKinopoisk"].ToString()
            };
        }

        private FilmModel MapFilmWithDescriptionJson(JObject json)
        {
            var countries = string.Join(", ", json["countries"].ToObject<JArray>().Select(c => c["country"].ToString()));
            var genres = string.Join(", ", json["genres"].ToObject<JArray>().Select(g => g["genre"].ToString()));

            return new FilmModel
            {
                Id = (int)json["kinopoiskId"],
                Name = CheckName(json),
                Year = (int)json["year"],
                ImageUrl = json["posterUrl"].ToString(),
                Rating = json["ratingKinopoisk"].ToString(),
                Description = json["description"].ToString(),
                Countries = countries,
                Genres = genres
            };
        }

        private string CheckName(JObject json)
        {
            string name;

            if (json["nameOriginal"].ToString() != "")
                name = json["nameOriginal"].ToString();
            else
                name = json["nameRu"].ToString();

            return name;
        }
    }
}
