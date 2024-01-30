using AutoMapper;
using Chat_BL;
using Chat_DAL;
using Chat_DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Text;

 
namespace Chat_RealTime.Controllers
{
    public class CredentialsController : BaseContoller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        public CredentialsController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub, IChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }
        [HttpPost("Register")]
        public  async Task<ActionResult<ChatUser>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username))
            {
                return BadRequest("Invalid UserName !!");
            }

            using var hmac = new HMACSHA512();
            var user = new ChatUser
            {
                UserName = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ChatUser>> Login(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.SingleOrDefualtAsync(u => u.UserName == loginDTO.Username.ToLower());

            if (user == null) return Unauthorized("Invalid UserName");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i< computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return user;

        }
        private async Task<bool> UserExists(string userName)
        {
            return await _unitOfWork.Users.AnyAsync(u=>u.UserName == userName.ToLower());
        }

    }
}
