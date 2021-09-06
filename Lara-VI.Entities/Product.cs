using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lara_VI.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string  ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        
        public string Image { get; set; }

        public bool Popularity { get; set; }

        public bool ByWeight { get; set; }

        public bool ByPeace { get; set; }
        [NotMapped]
        public int TempByPeace { get; set; }
        [NotMapped]
        public double TempByWeight { get; set; }

        

    }
}
