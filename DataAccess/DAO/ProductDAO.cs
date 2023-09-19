using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        public static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Product> GetProducts()
        {
            var prs = new List<Product>();
            try
            {
                using var context = new PRN221_OnPEContext();
                prs = context.Products.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return prs;
        }
        public Product GetProductByID(int id)
        {
            Product pr = null;
            try
            {
                using var context = new PRN221_OnPEContext();
                pr = context.Products.SingleOrDefault(m => m.ProductId == id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pr;
        }
        public void AddProduct(Product p)
        {
            try
            {
                Product _p = GetProductByID(p.ProductId);
                if (_p == null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Products.Add(p);
                    context.SaveChanges();
                }
                else throw new Exception("Product is exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void modify(Product p)
        {
            try
            {
                Product _p = GetProductByID(p.ProductId);
                if (_p != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Products.Update(p);
                    context.SaveChanges();
                }
                else throw new Exception("Product is not exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void delete(int id)
        {
            try
            {
                Product _p = GetProductByID(id);
                if (_p != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Products.Remove(_p);
                    context.SaveChanges();
                }
                else throw new Exception("Product is not exist!");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
