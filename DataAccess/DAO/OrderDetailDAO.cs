using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        public static readonly object instanceLock = new object();
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetOrders()
        {
            var ods = new List<OrderDetail>();
            try
            {
                using var context = new PRN221_OnPEContext();
                ods = context.OrderDetails.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ods;
        }
        public OrderDetail GetOrderDetail(int idO, int idP)
        {
            OrderDetail od = null;
            try
            {
                using var context = new PRN221_OnPEContext();
                od = context.OrderDetails.SingleOrDefault(m => m.OrderId == idO && m.ProductId == idP);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return od;
        }
        public void AddOderDetail(OrderDetail o)
        {
            try
            {
                OrderDetail _o = GetOrderDetail(o.OrderId, o.ProductId);
                if (_o == null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.OrderDetails.Add(o);
                    context.SaveChanges();
                }
                else throw new Exception("This order detail is exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Modify(OrderDetail o)
        {
            try
            {
                OrderDetail _o = GetOrderDetail(o.OrderId, o.ProductId);
                if (_o != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.OrderDetails.Update(o);
                    context.SaveChanges();
                }
                else throw new Exception("This order detail is not exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int idO, int idP)
        {
            try
            {
                OrderDetail _o = GetOrderDetail(idO, idP);
                if (_o != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.OrderDetails.Remove(_o);
                    context.SaveChanges();
                }
                else throw new Exception("This order detail is not exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
