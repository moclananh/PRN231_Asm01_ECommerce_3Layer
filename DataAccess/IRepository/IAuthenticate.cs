using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IAuthenticate
    {
        Member Login(string username, string password);
        void Register(Member member);
    }
}
