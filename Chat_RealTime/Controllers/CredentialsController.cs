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
        private readonly ITokenService _tokenService;

        public CredentialsController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub, IChatHub> hubContext,ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public  async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username))
            {
                return BadRequest("Invalid UserName !!");
            }

            using var hmac = new HMACSHA256();
            var user = new ChatUser
            {
                UserName = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.SingleOrDefualtAsync(u => u.UserName == loginDTO.Username.ToLower());

            if (user == null) return Unauthorized("Invalid UserName");

            using var hmac = new HMACSHA256(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(loginDTO.Password));

            for (int i = 0; i< computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }
        private async Task<bool> UserExists(string userName)
        {
            return await _unitOfWork.Users.AnyAsync(u=>u.UserName == userName.ToLower());
        }

    }
}
