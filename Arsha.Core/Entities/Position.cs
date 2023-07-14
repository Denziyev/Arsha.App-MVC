using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Entities
{
    public class Position:BaseModel
    {
        public string Name { get; set; }
        List<Employee>? Employees { get; set; }
    }
}
