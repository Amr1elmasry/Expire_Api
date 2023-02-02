using System.Linq.Expressions;

namespace Expire_Api.Interface
{
    public interface IBaseRepository<T>
    {
        Task<T> FindById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithData();
        Task<int> Count();
        Task<T> Find(Expression<Func<T, bool>> criteria);
        Task<T> FindWithData(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> FindAllWithData(Expression<Func<T, bool>> criteria);

        List<string> GetCollections(Type entityType);

        void CommitChanges();

    }
}
