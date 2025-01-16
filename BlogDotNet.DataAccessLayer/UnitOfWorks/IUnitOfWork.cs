using BlogDotNet.CoreLayer.Entities;
using BlogDotNet.DataAccessLayer.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.DataAccessLayer.UnitOfWorks
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();
        Task<int> SaveAsync();
        int Save();
    }
}
