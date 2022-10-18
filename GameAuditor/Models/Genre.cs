using GameAuditor.Models.Interfaces;

namespace GameAuditor.Models
{
    public enum Genres
    {
        Strategy, Sport, RPG, Simulator, Horror, Action, Puzzle, MMORPG, Shooter, Fighting, Platform, Stealth
    }
    public class Genre : EntityBase
    {
        public Genres Genres { get; set; }
    }
}
