using System;
using System.Collections.Generic;
using System.Linq;
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
		throw new NotImplementedException();
	}

	public void Delete(T entity)
	{
		throw new NotImplementedException();
	}

	public List<T> GetAll()
	{
		throw new NotImplementedException();
	}

	public T? GetById(Guid id)
	{
		throw new NotImplementedException();
	}

	public void Update(T entity)
	{
		throw new NotImplementedException();
	}
}
