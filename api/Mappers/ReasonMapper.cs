using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ReasonCategory;
using api.Models;

namespace api.Mappers
{
    public static class ReasonMapper
    {
        public static ReasonDto ToReasonDto (this ReasonCategory reasonCategory)
        {
            return new ReasonDto{
                ID=reasonCategory.ID,
                Name=reasonCategory.Name
            };
        }
        public static ReasonCategory FromCreateToDto(this ReasonChangeDto reasonChangeDto)
        {
            return new ReasonCategory{
                Name=reasonChangeDto.Name,
            };
        }
    }
}