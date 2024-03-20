using AutoMapper;
using Chat_DAL;
using Chat_DAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat_RealTime.Controllers
{
    public class ErrorController : BaseContoller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ErrorController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet("Auth")]
        public ActionResult<string> GetSecurity()
        {
            return "Secrect Key";
        }
        [HttpGet("not-found")]
        public ActionResult<ChatUser> GetNotFound()
        {
            var user = _unitOfWork.Users.Find(p=>p.Id == -1);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var user = _unitOfWork.Users.Find(p=>p.Id == -1);
            var userReturn = user.ToString();
            return userReturn;
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Bad Request");
        }
    }
}
