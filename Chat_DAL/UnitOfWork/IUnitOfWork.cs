using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_DAL.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepo Users { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
