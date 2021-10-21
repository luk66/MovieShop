using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Cast
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Gender { get; set; }

        public string? TmdbURL { get; set; }

        public string? ProfilePath { get; set; }

        //Navigation property
        public ICollection<MovieCast> Movies { get; set; }
    }
}
