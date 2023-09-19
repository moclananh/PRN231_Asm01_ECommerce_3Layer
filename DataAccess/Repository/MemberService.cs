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
    public class MemberService : IMemberService
    {
        public void DeleteMember(int MemberId) => MemberDAO.Instance.DeleteMember(MemberId);

        public Member GetMemberByID(int MemberId) => MemberDAO.Instance.GetMemberByID(MemberId);

        public List<Member> GetMemberList()
        => MemberDAO.Instance.GetMemberList_2();

        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMemberList();


        public void InsertMember(Member member) => MemberDAO.Instance.InsertMember(member);



        public void UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);


    }
}
