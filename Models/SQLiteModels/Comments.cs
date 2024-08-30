using SQLite;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> c432d41c467b3bc301cc3aecd3a6bce4d12219b8

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
        public DateTime CreatedDate  { get; set; } = DateTime.Now;
        public string? UserName { get; set; }
        public string? UserProfileImage { get; set; }
    }
}
