using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_Food
    {
        public Response_Food EntityToDTO(Food food)
        {
            Response_Food response = new Response_Food()
            {
                Id=food.Id,
                Price = food.Price,
                Description = food.Description,
                Image = food.Image,
                NameofFood = food.NameofFood
            };
            return response;
        }
        public List<Response_Food> EntityToListDTO(List<Food> listFood)
        {
            return listFood.Select(item => new Response_Food
            {
                Id = item.Id,
                Price = item.Price,
                Description = item.Description,
                Image = item.Image,
                NameofFood = item.NameofFood
            }).ToList();
        }
    }
}
