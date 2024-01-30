using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DAL;

public interface IGenericRepo<T> where T : class
{
    List<T> GetAll();
    Task<List<T>> GetAllAsunc();
    T? GetById(int id);
    Task<T?> GetByIdAsync(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> SingleOrDefualtAsync(Expression<Func<T, bool>> expression); 
    T? Find(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);


}
