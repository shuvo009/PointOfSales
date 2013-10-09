namespace whostpos.Domain.Interface
{
    public interface ICommonRepository<T>
    {
        void ClearChanges(T entity);
    }
}
