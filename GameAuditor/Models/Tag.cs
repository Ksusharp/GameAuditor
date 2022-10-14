using GameAuditor.Models.Interfaces;

namespace GameAuditor.Models
{
    public enum Tags
    {
        Games, GameDev, Studios, Movie
    }
    public class Tag : EntityBase
    {
        public Tags Tags { get; set; }
    }
}
