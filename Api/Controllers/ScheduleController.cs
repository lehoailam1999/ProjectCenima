using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ISchedulesServices _scheduleServices;

        public ScheduleController(ISchedulesServices scheduleServices)
        {
            _scheduleServices = scheduleServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSchedule()
        {
            var listSchedule = await _scheduleServices.GetAll();
            return Ok(listSchedule);
        }
        [HttpPost]

        public async Task<IActionResult> AddNewSchedule(Request_Schedules request)
        {
            var schedule = await _scheduleServices.AddNewSchedules(request);
            return Ok(schedule);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSchedule(int id)
        {
            var schedule = await _scheduleServices.UpdateSchedules(id);
            return Ok(schedule);
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var delete = await _scheduleServices.DeleteSchedules(id);
            return Ok(delete);
        }
    }
}
