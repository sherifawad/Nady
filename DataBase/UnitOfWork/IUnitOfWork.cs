using DataBase.Models;
using DataBase.Repository;
using System;
using System.Threading.Tasks;

namespace DataBase.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        Task<bool> Complete();
        bool HasChanges();
        IRepository<string, T> Repository<T>() where T : BaseModel;
    }
}