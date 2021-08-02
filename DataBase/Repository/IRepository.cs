using Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public interface IRepository<TKey, T> where T : BaseModel
    {

        Task<T> AddItemAsync(T item, bool save = false) ;
        Task<bool> UpdateItemAsync(T item, bool save = false) ;
        Task<bool> InsertOrUpdate(TKey id, T item, bool save = false) ;
        Task<bool> DeleteItemAsync(T item, bool save = false);
        Task<T> FindAsync(TKey id);
        Task<List<T>> GetAllAsync(bool forceRefresh = false);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = "", bool track = false);
        Task<T> Getlast();
        Task<List<T>> Get(Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool track = false) ;
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        bool IsTracked(T item) ;
    }
}
