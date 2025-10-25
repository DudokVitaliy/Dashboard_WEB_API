using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.DAL.Entities
{
    public class GenreEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; } = string.Empty;
        public string? NormalizedName { get; set; }
        public ICollection<GameEntity> Games { get; set; } = [];
    }
}
