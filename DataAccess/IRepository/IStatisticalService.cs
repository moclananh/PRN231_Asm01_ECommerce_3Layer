using BusinessObject.DataAccess;
using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IStatisticalService
    {
        List<Order> GetStatisticalByDate(DateTime startDate, DateTime endDate);
    }
}
