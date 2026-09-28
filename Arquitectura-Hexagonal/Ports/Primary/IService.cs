

using Arquitectura_Hexagonal.Entities;

namespace Arquitectura_hexagonal.Ports.Primary
{
    public interface IService
    {
        void Register(string name, decimal price);

        IEnumerable<Products> GetAll();
    }
}