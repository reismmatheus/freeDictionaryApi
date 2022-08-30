using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Model
{
    public class AuthModel
    {
        public class SinginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class SinginResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Token { get; set; }
        }
        public class SingupModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class SingupResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Token { get; set; }
        }
    }
}
