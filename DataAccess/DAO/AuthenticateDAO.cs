using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AuthenticateDAO
    {
        private static AuthenticateDAO instance = null;
        public static readonly object instanceLock = new object();
        public static AuthenticateDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AuthenticateDAO();
                    }
                    return instance;
                }
            }
        }

        public Member Login(string email, string pass)
        {
            Member member = null;
            try
            {
                using var context = new PRN221_OnPEContext();
                member = context.Members.SingleOrDefault(s => s.Email == email && s.Password == pass);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return (member);
        }

        public void Register(Member member)
        {
            try
            {
                using var context = new PRN221_OnPEContext();
                var check = context.Members.FirstOrDefault(s => s.Email == member.Email);
                if (check == null)
                {
                    context.Add(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
