using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Entities
{
    public class SocialNetwork : BaseModel
    {
        public string Icon { get; set; }
        public string Link { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
