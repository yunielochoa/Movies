using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RentAMovie.Models;

namespace RentAMovie.ViewModels
{
    public class MovieFromViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Range(1, 20)]
        [Required]
        [Display(Name = "Number in Stock")]
        public int? Stock { get; set; }

       [Display(Name = "Genre")]
        public int? GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public MovieFromViewModel()
        {
            Id = 0;
        }

        public MovieFromViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            GenreId = movie.GenreId;
            Stock = movie.Stock;
        }

    }
}