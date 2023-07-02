using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Entities
{
    public class Category:BaseModel
    {
        [Required]
        public string Name { get;set; }

        public List<Product>? products { get; set; }
    }
}
