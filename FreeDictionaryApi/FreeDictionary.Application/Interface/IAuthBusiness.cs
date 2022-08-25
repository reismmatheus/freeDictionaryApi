using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Interface
{
    public interface IAuthBusiness
    {
        Task Singup();
        Task Singin();
    }
}
