using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Models.SQLiteModels
{
    [SQLite.Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public Guid UserId { get; set; }

        [Unique, MaxLength(128)]
        public string? FirebaseUID { get; set; }

        [MaxLength(100)]
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

}
