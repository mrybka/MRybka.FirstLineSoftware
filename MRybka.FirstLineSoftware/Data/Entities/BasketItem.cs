#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Data.Entities
{
    public class BasketItem
    {
        [Key]
        public int ProductId { get; set; }
        public int Amount { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get;set;}
    }
}
