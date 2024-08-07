using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chat_DAL
{
    public interface IUserRepo:IGenericRepo<ChatUser>
    {
        Task<List<ChatUser>> GetUsersWithPhotoAsync();
        Task<ChatUser> GetUserWithUserNameandPhotoAsync(string userName);
        Task<IEnumerable<ChatUser>> GetMembersAsync();
        Task<ChatUser> GetMemberAsync(string userName);


    }
}
