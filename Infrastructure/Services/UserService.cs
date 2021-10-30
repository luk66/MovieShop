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

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            throw new NotImplementedException();
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
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
