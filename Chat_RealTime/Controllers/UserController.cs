using AutoMapper;
using Chat_DAL;
using Chat_DAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat_RealTime.Controllers
{

    public class UserController : BaseContoller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ChatUser>>> GetUsers()
        {
            return await _unitOfWork.Users.GetAllAsunc();
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatUser>> GetUser(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }
    }
}
