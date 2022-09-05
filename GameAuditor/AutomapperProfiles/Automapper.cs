using AutoMapper;
using GameAuditor.Models;
using GameAuditor.Models.ViewModels;

namespace GameAuditor.AutomapperProfiles
{
    public class GameAutomapper
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Game, CreateGameViewModel>());
            var mapper = config.CreateMapper();
        }
    }
}
