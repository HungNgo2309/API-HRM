using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Position;
using api.Models;

namespace api.Mappers
{
    public static class PositionMapper
    {
        public static PositionDto ToPositionDto(this Position position)
        {
            return new PositionDto{
                ID=position.ID,
                Role=position.Role,
                Name=position.Name,
                Staffs =position.Staffs.Select(s=>s.ToStaffDto()).ToList()
            };
        }
        public static Position FromCreateToDto(this CreatePositionDto positionDto)
        {
            return new Position{
                Role=positionDto.Role,
                Name=positionDto.Name
            };
        }
    }
}