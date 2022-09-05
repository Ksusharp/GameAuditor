using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAuditor.Models.Interfaces
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}
