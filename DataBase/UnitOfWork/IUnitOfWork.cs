using DataBase.Repository;
using Core.Models;
using System;
using System.Threading.Tasks;

namespace DataBase.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(string userId = null);
        Task<bool> Complete(string userId = null);
        bool HasChanges();
        IRepository<string, T> Repository<T>() where T : BaseModel;
    }
}