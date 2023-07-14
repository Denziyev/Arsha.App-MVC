using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Arsha.Core.Entities
{
    public class Product:BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string ImageHeight { get; set; }
        public string ImageWidth { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped] 
        public IFormFile? FormFile{ get; set; }
    }
}
