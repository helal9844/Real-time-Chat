using AutoMapper;
using Chat_BL;
using Chat_DAL;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat_RealTime.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var users = await _unitOfWork.Users.GetUsersWithPhotoAsync();
            var usersMap = _mapper.Map<IEnumerable<MemberDTO>>(users);
            return Ok(usersMap);
        }
        [HttpGet("{userName}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string userName)
        {
            var user = await _unitOfWork.Users.GetUserWithUserNameandPhotoAsync(userName);
            var userMap = _mapper.Map<MemberDTO>(user);
            return userMap;
        }
    }
}
