using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IMemberService
    {
        IEnumerable<Member> GetMembers();
        List<Member> GetMemberList();
        Member GetMemberByID(int MemberId);
        void InsertMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int MemberId);
    }
}
