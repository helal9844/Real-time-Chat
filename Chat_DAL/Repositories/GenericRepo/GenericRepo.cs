using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Chat_DAL;

public class GenericRepo<T>:IGenericRepo<T> where T : class
{
    protected readonly AppDbContext _context;
	public GenericRepo(AppDbContext context)
	{
		_context = context;
	}

	public void Add(T entity)
	{
		_context.Set<T>().Add(entity);	
	}

	public void Delete(T entity)
	{
		_context.Set<T>().Remove(entity); 
	}

	public List<T> GetAll()
	{
		return _context.Set<T>().ToList(); 
	}

	public T? GetById(int id)
	{
		return _context.Set<T>().Find(id);
	}

	public void Update(T entity)
	{
		_context.Set<T>().Update(entity);	
	}
	public T Find(Expression<Func<T,bool>> expression)
	{
		return _context.Set<T>().SingleOrDefault(expression);
	}
	public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
	{
		return await _context.Set<T>().AnyAsync(expression);
	}

	public async Task<List<T>> GetAllAsunc()
	{
        return await _context.Set<T>().ToListAsync();
    }

	public async Task<T?> GetByIdAsync(int id)
	{
		return await _context.Set<T>().FindAsync(id);
    }

	public async Task<T?> SingleOrDefualtAsync(Expression<Func<T, bool>> expression)
	{
		return await _context.Set<T>().SingleOrDefaultAsync(expression);
	}
}
