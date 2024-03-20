using Chat_DAL;

namespace Chat_BL;

public interface ITokenService
{
    string CreateToken(ChatUser user);
}
