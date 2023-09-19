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
    public class OrderDetailService : IOrderDetailService
    {
        public void AddOD(OrderDetail o) => OrderDetailDAO.Instance.AddOderDetail(o);


        public void DeleteOD(int idO, int idP) => OrderDetailDAO.Instance.Delete(idO, idP);


        public OrderDetail GetOrderDetail(int idO, int idP) => OrderDetailDAO.Instance.GetOrderDetail(idO, idP);

        public IEnumerable<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrders();

        public void UpdateOD(OrderDetail o) => OrderDetailDAO.Instance.Modify(o);
    }
}
