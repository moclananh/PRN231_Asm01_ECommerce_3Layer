using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DataAccess;
using DataAccess.IRepository;
using DataAccess.Repository;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private IMemberService repository = new MemberService();

        // GET: api/Members
        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetMembers()
        {
            return repository.GetMemberList();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public ActionResult<Member> GetMember(int id)
        {
            var member =  repository.GetMemberByID(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutMember(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {

            if (id != member.MemberId)
            {
                return BadRequest();
            }
            try
            {
                repository.UpdateMember(member);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return NoContent();
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
           repository.InsertMember(member);

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = repository.GetMemberByID(id);
            if (member == null)
            {
                return NotFound();
            }

            repository.DeleteMember(id);

            return NoContent();
        }

        /*private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }*/
    }
}
