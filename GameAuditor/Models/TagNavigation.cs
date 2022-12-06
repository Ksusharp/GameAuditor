using GameAuditor.Models.Interfaces;

namespace GameAuditor.Models
{
    public class TagNavigation : EntityBase
    {
        public Guid? PostId { get; set; }

        public Post Post { get; set; }

        public Guid? TagId { get; set; }

        public PostTag PostTag { get; set; }
    }
}
