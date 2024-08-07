
using AutoMapper;
using Chat_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DAL;

public class AutoMapperProfile :Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ChatUser,MemberDTO>()
            .ForMember(dest=>dest.PhotoUrl,opt=>opt.MapFrom(src=>
                src.Photos.FirstOrDefault(x=>x.IsMain).Url))
            .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>
                src.DateOfBirth.CalculateAge()));
        CreateMap<Photo, PhotoDTO>().ReverseMap();

    }
}
