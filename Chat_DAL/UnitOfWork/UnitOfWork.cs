using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepo(_context,_mapper);
        }

        public IUserRepo Users { set; get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
           _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
