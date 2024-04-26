using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface ISchedulesServices
    {
        Task<ResponseObject<List<Response_Schedules>>> GetAll();
        Task<ResponseObject<List<Response_Schedules>>> GetAllById(int idMovie);
        Task<ResponseObject<Response_Schedules>> AddNewSchedules(Request_Schedules request);
        Task<string> DeleteSchedules(int id);
        Task<ResponseObject<Response_Schedules>> UpdateSchedules(int id);
    }
}
