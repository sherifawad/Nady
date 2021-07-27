using DataBase.Repository;
using Core.Models;
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