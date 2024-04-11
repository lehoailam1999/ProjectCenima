using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;

        public RoomController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoom(int pageSize=1,int pageNumber=1)
        {
            var listRoom = await _roomServices.GetAll(pageSize,pageNumber);
            return Ok(listRoom);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewRoom(Request_Room request)
        {
            var room = await _roomServices.AddNewRoom(request);
            return Ok(room);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRoom(int id)
        {
            var room = await _roomServices.UpdateRoom(id);
            return Ok(room);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var delete = await _roomServices.DeleteRoom(id);
            return Ok(delete);
        }
    }
}
