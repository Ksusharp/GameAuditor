using AutoMapper;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;

namespace GameAuditor.AutomapperProfiles
{
    public class GameAutomapper : Profile
    {
        public GameAutomapper()
        {
            CreateMap<Game, CreateGameViewModel>().ReverseMap();
            CreateMap<Post, CreatePostViewModel>().ReverseMap();
        }
    }
}
