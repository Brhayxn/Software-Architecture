namespace Arquitectura_Hexagonal.Ports.Secundary
{
    public interface IRepository<T>
    {
        void Save(T entity);

        IEnumerable<T> GetAll();
    }
}