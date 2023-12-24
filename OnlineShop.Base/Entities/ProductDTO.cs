using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Base.Entities
{
    [DisplayName("Product")]
    public class ProductDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public Category Category { get; set; } = default!;
    }
}
