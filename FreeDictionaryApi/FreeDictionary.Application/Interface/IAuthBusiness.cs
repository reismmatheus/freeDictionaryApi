using FreeDictionary.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Interface
{
    public interface IAuthBusiness
    {
        Task<(AuthModel, bool)> Singin(SinginModel model);
        Task<(AuthModel, bool)> Singup(SingupModel model);
    }
}
