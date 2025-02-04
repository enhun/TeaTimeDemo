using System.Linq.Expressions;
using TeaTimeDemo.Models;

namespace TeaTimeDemo.DataAccess.Repository.IRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string includeProperties);
        Product Get(Expression<Func<Product, bool>> filter);
        Product Get(Expression<Func<Product, bool>> filter, string includeProperties);
        void Add(Product entity);
        void Remove(Product entity);
        void Update(Product entity);
        void Save();
    }
}