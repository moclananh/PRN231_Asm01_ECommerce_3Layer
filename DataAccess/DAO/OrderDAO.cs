using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        public static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Order> GetOrders()
        {
            var ods = new List<Order>();
            try
            {
                using var context = new PRN221_OnPEContext();
                ods = context.Orders.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ods;
        }
        public Order GetOrderByID(int id)
        {
            Order or = null;
            try
            {
                using var context = new PRN221_OnPEContext();
                or = context.Orders.SingleOrDefault(m => m.OrderId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return or;
        }
        public void AddOrder(Order order)
        {
            try
            {
                Order _o = GetOrderByID(order.OrderId);
                if (_o == null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                else throw new Exception("Order is exist!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Modify(Order o)
        {
            try
            {
                Order _o = GetOrderByID(o.OrderId);
                if (_o != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Orders.Update(o);
                    context.SaveChanges();
                }
                else throw new Exception("Order is not exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                Order _o = GetOrderByID(id);
                if (_o != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Orders.Remove(_o);
                    context.SaveChanges();
                }
                else throw new Exception("Order is not exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
