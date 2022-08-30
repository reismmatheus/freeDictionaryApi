using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Domain
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<FavoriteWord> FavoriteWords { get; set; }
        public virtual ICollection<HistoryWord> HistoryWords { get; set; }
    }
}
