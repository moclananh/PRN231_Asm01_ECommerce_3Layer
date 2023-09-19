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
    public class OrderService : IOrderService
    {
        public void AddOrder(Order o) => OrderDAO.Instance.AddOrder(o);


        public void DeleteOrder(int idO) => OrderDAO.Instance.Delete(idO);


        public Order GetOrder(int id) => OrderDAO.Instance.GetOrderByID(id);


        public List<Order> GetOrders() => OrderDAO.Instance.GetOrders();

        public void UpdateOrder(Order o) => OrderDAO.Instance.Modify(o);
    }
}
