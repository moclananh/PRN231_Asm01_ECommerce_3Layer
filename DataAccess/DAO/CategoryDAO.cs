using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        public static readonly object instanceLock = new object();
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Category> GetListCategories()
        {
            var cates = new List<Category>();
            try
            {
                using var context = new PRN221_OnPEContext();
                cates = context.Categories.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cates;
        }

        //-------------------------------------------------------------
        public Category GetCategoryByID(int CategoryId)
        {
            Category cat = null;
            try
            {
                using var context = new PRN221_OnPEContext();
                cat = context.Categories.SingleOrDefault(c => c.CategoryId == CategoryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cat;
        }
        //-------------------------------------------------------------
        public void Insert(Category cat)
        {
            try
            {
                Category _cat = GetCategoryByID(cat.CategoryId);
                if (_cat == null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Categories.Add(cat);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cat is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------------------------
        public void Update(Category cat)
        {
            try
            {
                Category _cat = GetCategoryByID(cat.CategoryId);
                if (_cat != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Categories.Update(cat);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cat does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-------------------------------------------------------------
        public void Delete(int CategoryId)
        {
            try
            {
                Category cat = GetCategoryByID(CategoryId);
                if (cat != null)
                {
                    using var context = new PRN221_OnPEContext();
                    context.Categories.Remove(cat);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cat does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
