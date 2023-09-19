using BusinessObject.DataAccess;
using BusinessObject.DTOs;
using DataAccess.DAO;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StatisticalService : IStatisticalService
    {
        public List<Order> GetStatisticalByDate(DateTime startDate, DateTime endDate) => StatisticalDAO.Instance.Statistical( startDate,  endDate);
    }
}
