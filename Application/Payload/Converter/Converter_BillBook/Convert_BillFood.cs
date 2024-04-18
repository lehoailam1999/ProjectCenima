using Application.Payload.DataResponse;
using Domain.Entities;
using Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter.Converter_BillBook
{
    public class Convert_BillFood
    {
        private readonly IBaseRepositories<Food> _baseRepositories;

        public Convert_BillFood(IBaseRepositories<Food> baseRepositories)
        {
            _baseRepositories = baseRepositories;
        }

        public Response_BillFood EntityToDTO(BillFood billFood)
        {
            string nameFood = _baseRepositories.SingleOrDefault(x => x.Id == billFood.FoodId).NameofFood;
            Response_BillFood response = new Response_BillFood()
            {
                Quantity = billFood.Quantity,
                NameFood = nameFood

            };
            return response;
        }
    }
}
