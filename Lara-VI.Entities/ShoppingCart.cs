using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lara_VI.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public int ByPeace { get; set; }
        [NotMapped]
        public double ByWeight { get; set; }
    }
}
