using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Entities
{
    public class Employee:BaseModel
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }


        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public List<SocialNetwork>? SocialNetworks { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
