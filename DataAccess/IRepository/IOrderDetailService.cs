using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetOrderDetails();

        void UpdateOD(OrderDetail o);

        void DeleteOD(int idO, int idP);

        void AddOD(OrderDetail o);

        OrderDetail GetOrderDetail(int idO, int idP);
    }
}
