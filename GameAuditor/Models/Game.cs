using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace GameAuditor.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }

    }
}
