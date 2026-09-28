using Arquitectura_hexagonal.Ports.Primary;
using Arquitectura_Hexagonal.Entities;
using Arquitectura_Hexagonal.Ports.Secundary;


namespace Arquitectura_Hexagonal.Services
{

    public class ProductService : IService
    {
        private readonly IRepository<Products> _repository;

        public ProductService(IRepository<Products> repository)
        {
            _repository = repository;
        }

        public void Register(string name, decimal price)
        {
            var product = new Products(Guid.NewGuid(), name, price);
            _repository.Save(product);
        }

        public IEnumerable<Products> GetAll()
        {
            return _repository.GetAll();
        }
    }
}