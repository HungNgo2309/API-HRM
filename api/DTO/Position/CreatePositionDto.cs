using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Position
{
    public class CreatePositionDto
    {
        public int Role { get; set; }
        public String Name {get;set;}=string.Empty;
    }
}