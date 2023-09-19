using BusinessObject.DataAccess;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Authenticate : IAuthenticate
    {
        public Member Login(string username, string password) => AuthenticateDAO.Instance.Login(username, password);

        public void Register(Member member) => AuthenticateDAO.Instance.Register(member);
    }
}
