using System;
using System.ComponentModel.DataAnnotations;

namespace InternalSystems.Models
{
    public class AccessModel
    {       
        public int Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
    }
}

