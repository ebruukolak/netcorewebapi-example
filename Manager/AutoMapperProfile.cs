using AutoMapper;
using Entity;
using Manager.DTO;

namespace Manager
{
    public class AutoMapperProfile:Profile
    {
       public AutoMapperProfile()
       {
           CreateMap<Users,UserDTO>();
           CreateMap<UserDTO,Users>();
       }
    }
}