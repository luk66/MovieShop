using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCore.Models.FavoriteResponseModel;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieReository;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IAsyncRepository<Review> _reviewRepository;


        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IMovieRepository movieReository,
                           IAsyncRepository<Favorite> favoriteRepository, IAsyncRepository<Review> reviewRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _movieReository = movieReository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check wheter email exists in the database
            // 
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if(dbUser != null)
            {
                throw new Exception("Email already exists, please login");
            }
            //generate a random unique salt
            var salt = GetSalt();
            
            //create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            //save the user object to db
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };

            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }


        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            //get the salt and hashedpassword from database for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null)
            {
                throw null;
            }

            // hash the user entered password with salt from the database
            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            
            //check the hashedpassword with database hashed password
            if(hashedPassword == dbUser.HashedPassword)
            {
                //user entered the correct password
                var userLoginReponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginReponseModel;
            }
            return null;
        }

        private string GetSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);

        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;

        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            //TODO
            var existFav = await _favoriteRepository.Get(f => f.MovieId == favoriteRequest.MovieId && f.UserId == favoriteRequest.UserId);

            if (existFav != null)
            {
                throw new Exception($"All already favorited");
            }
            var newFav = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };

            await _favoriteRepository.Add(newFav);

        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            //TODO
            var existFav = await _favoriteRepository.Get(f => f.MovieId == favoriteRequest.MovieId && f.UserId == favoriteRequest.UserId);
            if(existFav == null)
            {
                throw new Exception($"Have not favorited yet");
            }
            await _favoriteRepository.Delete(existFav.FirstOrDefault());
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            // TODO
            var favoriteMovies = await _favoriteRepository.Get(f => f.UserId == id);
            var fMovies = new List<FavoriteMovieResponseModel>();
            foreach(var movie in favoriteMovies)
            {
                fMovies.Add(new FavoriteMovieResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                    
                });
            }

            var favoriteResponse = new FavoriteResponseModel
            {
                FavoriteMovies = fMovies
            };

            return favoriteResponse;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var isMoviePurchased = await IsMoviePurchased(purchaseRequest, userId);
            if (isMoviePurchased == true)
            {
                return false;
            }
            var movieDetails = await _movieReository.GetMovieById(purchaseRequest.MovieId);
            var purchase = new Purchase
            {
                UserId = userId,
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = purchaseRequest.PurchaseNumber.Value,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime.Value,
                TotalPrice = movieDetails.Price.Value
            };

            await _purchaseRepository.Add(purchase);
            return true;
            
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var isMoviePurchased = await _purchaseRepository.Exists(p => p.UserId == userId && p.MovieId == purchaseRequest.MovieId);
            return isMoviePurchased;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var purchases =await _purchaseRepository.GetAllPurchasesForUser(id);
            var purchasedMovies = new List<MovieCardResponseModel>();
            //if (!purchases.Any())
            //{
            //    throw new Exception($"No Purchase Found for this User id {id}");
            //}
            foreach (var purchase in purchases)
            {
                purchasedMovies.Add(new MovieCardResponseModel
                {
                    Id = purchase.Movie.Id,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl
                });
            }
            var userPurchases = new PurchaseResponseModel
            {
                UserId = id,
                TotalMoviesPurchased = purchasedMovies.Count,
                PurchasedMovies = purchasedMovies
            };
            return userPurchases;
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {   //TODO
            var purchase = await _purchaseRepository.GetPurchaseDetails(userId, movieId);

            var purchaseDetails = new PurchaseDetailsResponseModel
            {
                Id = purchase.Id,
                UserId = purchase.UserId,
                PurchaseNumber = purchase.PurchaseNumber,
                TotalPrice = purchase.TotalPrice,
                PurchaseDateTime = purchase.PurchaseDateTime,
                MovieId = purchase.Movie.Id,
                Title = purchase.Movie.Title,
                PosterUrl = purchase.Movie.PosterUrl,
                ReleaseDate = purchase.Movie.ReleaseDate.GetValueOrDefault()
            };
            return purchaseDetails;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            //TODO

            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };

            await _reviewRepository.Add(review);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            //TODO
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };
            await _reviewRepository.Update(review);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            //TODO
            var review = await _reviewRepository.Get(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.Delete(review.FirstOrDefault());
        }

        public async Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var reviews = await _userRepository.GetReviewsByUser(id);
            if (reviews == null)
            {
                throw new Exception($"No Reviews Found for this User id {id}");
            }
            var movieReviews = new List<MovieReviewResponseModel>();
            foreach(var review in reviews)
            {
                movieReviews.Add(new MovieReviewResponseModel
                {
                    UserId = id,
                    MovieId = review.MovieId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                    Name = review.User.FirstName
                });
            }
            var userReviews = new UserReviewResponseModel
            {
                UserId = id,
                MovieReviews = movieReviews
            };
            return userReviews;
        }
    }
}
