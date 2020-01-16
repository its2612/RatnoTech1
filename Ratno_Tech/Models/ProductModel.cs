using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ratno_Tech.Models
{
    public class ProductModel
    {

        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string name { get; set; }

        public decimal price { get; set; }
        [Required]
        public string imagepath { get; set; }

        [Required]
        public string details { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}