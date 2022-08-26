using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Domain
{
    public class FavoriteWord : Entity
    {
        public Guid UserId { get; set; }
        public string Word { get; set; }
        public virtual User User { get; set; }
    }
}
