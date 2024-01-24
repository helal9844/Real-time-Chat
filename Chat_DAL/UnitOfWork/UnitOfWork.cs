using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }





        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
           _context.SaveChanges();
        }
    }
}
