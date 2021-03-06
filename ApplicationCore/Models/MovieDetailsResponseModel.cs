using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieDetailsResponseModel
    {
        // viewModel: huge view to contains multiple models
        //properties of movie
        // list<cast>
        // list<genres>

        public MovieDetailsResponseModel()
        {
            Casts = new List<CastResponseModel>();
            Genres = new List<GenreModel>();
            Trailers = new List<TrailerResponseModel>();
            Reviews = new List<UserReviewResponseModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }

        public decimal? Rating { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; }
        public string TmdbUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }

        public bool? IsPurchased { get; set; }

        public bool? IsReviewed { get; set; }

        public bool? IsFavorited { get; set; }
        
        //public int FavoritesCount { get; set; }
        //list of cast
        public List<CastResponseModel> Casts { get; set; }
        // list of genres
        public List<GenreModel> Genres { get; set; }
        // list of user reviews
        public List<UserReviewResponseModel> Reviews { get; set; }
        // list of trailers
        public List<TrailerResponseModel> Trailers { get; set; }
    }

    public class CastResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }
        public string Character { get; set; }
    }
    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MovieReviewResponseModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
        public string Name { get; set; }
    }
    public class UserReviewResponseModel
    {
        public int UserId { get; set; }
        public List<MovieReviewResponseModel> MovieReviews { get; set; }
    }
    public class TrailerResponseModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
    }
}
