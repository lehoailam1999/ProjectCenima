using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter.Converter_BillBook
{
    public class Convert_BillFood
    {
        public Response_BillFood EntityToDTO(BillFood billFood)
        {
            Response_BillFood response = new Response_BillFood()
            {
                Quantity = billFood.Quantity,
                FoodId = billFood.FoodId

            };
            return response;
        }
    }
}
