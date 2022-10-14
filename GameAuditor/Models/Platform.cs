using GameAuditor.Models.Interfaces;

namespace GameAuditor.Models
{
    public enum Platforms
    {
        PC, Xbox, PlayStation, Mobile
    }
    public class Platform : EntityBase
    {
        public Platforms Platforms { get; set; }
    }
}
