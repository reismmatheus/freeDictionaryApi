using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Model
{
    public class UserModel
    {
        public class UserMeModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
        public class UserWordAdded
        {
            public string Word { get; set; }
            public DateTime Added { get; set; }
        }
    }
}
