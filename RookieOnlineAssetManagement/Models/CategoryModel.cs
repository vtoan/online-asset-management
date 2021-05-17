using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class CategoryModel
    {
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name cannot null")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Prefix cannot null")]
        public string ShortName { get; set; }
    }
}
