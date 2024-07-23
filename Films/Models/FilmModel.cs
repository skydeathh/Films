using Newtonsoft.Json;

namespace Films.Models
{
    public class FilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }
        public string Countries { get; set; }
        public string Genres { get; set; }
    }
}
