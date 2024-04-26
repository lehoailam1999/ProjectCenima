using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_Promotion
    {
        public Response_Promotion EntityToDTO(Promotion promotion)
        {
            Response_Promotion response = new Response_Promotion()
            {
                Id = promotion.Id,
                Percent = promotion.Percent,
                Description = promotion.Description,
                Quantity = promotion.Quantity,
                Type = promotion.Type,
                StartTime = promotion.StartTime,
                EndTime = promotion.EndTime,
                Name = promotion.Name,
                IsActive = promotion.IsActive,
                RankCustomerId = promotion.RankCustomerId,
            };
            return response;
        }
        public List<Response_Promotion> EntityToListDTO(List<Promotion> listpromotion)
        {

            return listpromotion.Select(item => new Response_Promotion
            {
                Id = item.Id,
                Percent = item.Percent,
                Description = item.Description,
                Quantity = item.Quantity,
                Type = item.Type,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                Name = item.Name,
                IsActive = item.IsActive,
                RankCustomerId = item.RankCustomerId,
            }).ToList();
        }
    }
}
