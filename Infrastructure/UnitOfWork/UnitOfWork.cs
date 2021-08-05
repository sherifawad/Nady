using Core.Models;
using DataBase;
using DataBase.Repository;
using DataBase.UnitOfWork;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _Context;
        private Hashtable _repositories;
        public UnitOfWork(IDatabaseContext context)
        {
            _Context = context;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        private void Dispose(bool dispose)
        {
            if (dispose)
                _Context.Dispose();
        }

        public Task CommitAsync(string userId = null)
        {
            return _Context.SaveChangesAsync(userId);
        }

        public async Task<bool> Complete(string userId = null)
        {
            return await _Context.SaveChangesAsync(userId) > 0;
        }

        public bool HasChanges()
        {
            _Context.ChangeTracker.DetectChanges();
            var changes = _Context.ChangeTracker.HasChanges();

            return changes;
        }

        public IRepository<string, T> Repository<T>() where T : BaseModel
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(T).Name;
            try
            {

                if (!_repositories.ContainsKey(type))
                {
                    var repositoryType = typeof(Repository<,>);
                    var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(new Type[] { typeof(string), typeof(T) }), _Context);
                    _repositories.Add(type, repositoryInstance);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }

            return (IRepository<string, T>)_repositories[type];

        }
    }
}
