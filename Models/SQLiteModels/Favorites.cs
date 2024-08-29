using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Models.SQLiteModels
{
    [SQLite.Table("Favorites")]
    public  class Favorites
    {
        [PrimaryKey]
        public Guid FavoritesId { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public Guid RecipeId { get; set; }

        public DateTime CreatedDate {  get; set; } = DateTime.Now;
    }
}
