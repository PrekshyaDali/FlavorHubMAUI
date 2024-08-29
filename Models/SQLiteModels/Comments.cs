using SQLite;

namespace FlavorHub.Models.SQLiteModels
{
    [SQLite.Table("Comments")]
    public class Comments
    {
        [PrimaryKey]
        public Guid CommentId { get; set; } = Guid.NewGuid();
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }

        [MaxLength(1000)]
        public string? CommentText { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UserName { get; set; }
        public string? UserProfileImage { get; set; }
    }
}
