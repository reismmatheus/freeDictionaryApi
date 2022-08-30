using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeDictionary.Application.Model.AuthModel;

namespace FreeDictionary.Application.Interface
{
    public interface IAuthBusiness
    {
        Task<SinginResponse> Singim(SinginModel model);
        Task<SingupModel> Singup(SingupModel model);
    }
}
