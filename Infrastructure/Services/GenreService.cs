using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreRepository;
        public GenreService(IAsyncRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();
            var genreModel = new List<GenreModel>();

            foreach (var genre in genres)
            {
                genreModel.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }

            return genreModel;
            
        }
    }
}
