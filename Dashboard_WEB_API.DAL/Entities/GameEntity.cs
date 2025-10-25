namespace Dashboard_WEB_API.DAL.Entities
{
    public class GameEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }
        public ICollection<GenreEntity> Genres { get; set; } = [];
        public ICollection<GameImageEntity> Images { get; set; } = [];

    }
}
