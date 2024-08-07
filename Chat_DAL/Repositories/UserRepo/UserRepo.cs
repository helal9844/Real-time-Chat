using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat_DAL
{
    public class UserRepo : GenericRepo<ChatUser>, IUserRepo
    {
        private readonly IMapper _mapper;

        public UserRepo(AppDbContext context,IMapper mapper) : base(context)
        {
            _mapper = mapper;

        }


        public async Task<ChatUser> GetMemberAsync(string userName)
        {
            /*.ProjectTo<MemberDTO>(
                            _mapper.ConfigurationProvider).*/
            return await _context.Users.Where(x => x.UserName == userName).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ChatUser>> GetMembersAsync()
        {
            /*            .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)*/
            return await _context.Users.ToListAsync();
        }

        public async Task<List<ChatUser>> GetUsersWithPhotoAsync()
        {
		    return await _context.Users.Include(p=>p.Photos).ToListAsync();

        }

        public async Task<ChatUser> GetUserWithUserNameandPhotoAsync(string userName)
        {
            return await _context.Users.Include(p => p.Photos).Where(p=>p.UserName == userName).SingleOrDefaultAsync();
        }
    }
}
