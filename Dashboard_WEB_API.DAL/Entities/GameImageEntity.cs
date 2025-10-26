using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.DAL.Entities
{
    public class GameImageEntity : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public required string ImagePath { get; set; }
        public bool IsMain { get; set; } = false;
        public required string? GameId { get; set; }
        public GameEntity? Game { get; set; }
    }
}
