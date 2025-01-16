using BlogDotNet.DataAccessLayer.Context;
using BlogDotNet.DataAccessLayer.Repositories.Abstractions;
using BlogDotNet.DataAccessLayer.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.DataAccessLayer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await appDbContext.DisposeAsync();
        }

        public int Save()
        {
            return appDbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await appDbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(appDbContext);
        }
        //dbContext üzerinden include yapmak için 
        
    }
}
