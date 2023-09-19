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
    public class CategoryService : ICategoryService
    {
        public void Delete(int CategoryId) => CategoryDAO.Instance.Delete(CategoryId);

        public List<Category> GetAllCategories() => CategoryDAO.Instance.GetListCategories();
        public Category GetCategoryByID(int CategoryId) => CategoryDAO.Instance.GetCategoryByID(CategoryId);

        public void Insert(Category category) => CategoryDAO.Instance.Insert(category);

        public void Update(Category category) => CategoryDAO.Instance.Update(category);
    }
}
