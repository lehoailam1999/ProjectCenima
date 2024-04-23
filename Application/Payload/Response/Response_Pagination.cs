using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Response
{
    public class Response_Pagination<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int ToTalItem { get; set; }
        public int ToTalPage { get; set; }
        public List<T> Data { get; set; }

        public Response_Pagination(int status, string message, int pageSize, int pageNumber, int toTalItem, int toTalPage,List<T> data)
        {
            Status = status;
            Message = message;
            PageSize = pageSize;
            PageNumber = pageNumber;
            ToTalItem = toTalItem;
            ToTalPage = toTalPage;
            Data = data;
        }

        public Response_Pagination()
        {
        }

        public Response_Pagination<T> ResponseSuccess(string message, int pageNumber, int pageSize, List<T> data)
        {
            int totalItems = data.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            int startIndex = (pageNumber - 1) * pageSize;
            List<T> pageData = data.Skip(startIndex).Take(pageSize).ToList();

            return new Response_Pagination<T>()
            {
                Status = StatusCodes.Status200OK,
                Message = message,
                PageSize = pageSize,
                PageNumber = pageNumber,
                ToTalItem = totalItems,
                ToTalPage = totalPages,
                Data = pageData
            };

        }
        public Response_Pagination<T> ResponseError(int status, string message)
        {
            return new Response_Pagination<T>()
            {
                Status = status,
                Message = message,
                PageSize = 0,
                PageNumber = 0,
                ToTalItem = 0,
                ToTalPage = 0,
                Data = null
            };
        }

    }
}
