using System.Linq.Expressions;
using TeaTimeDemo.DataAccess.Data;
using TeaTimeDemo.DataAccess.Repository.IRepository;
using TeaTimeDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace TeaTimeDemo.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public IEnumerable<Product> GetAll(string includeProperties)
        {
            IQueryable<Product> query = _db.Products;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }
            return query.ToList();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            return _db.Products.FirstOrDefault(filter);
        }

        // 修改這個方法：實作包含關聯資料的 Get 方法
        public Product Get(Expression<Func<Product, bool>> filter, string includeProperties)
        {
            IQueryable<Product> query = _db.Products;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return query.FirstOrDefault(filter);
        }

        public void Add(Product entity)
        {
            _db.Products.Add(entity);
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Size = obj.Size;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }

        public void Remove(Product entity)
        {
            _db.Products.Remove(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}