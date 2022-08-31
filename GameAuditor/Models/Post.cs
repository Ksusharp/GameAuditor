using GameAuditor.Models.Interfaces;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace GameAuditor.Models
{
    public class Post : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public  string Tags { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set;} = DateTime.Now;
    }
}
