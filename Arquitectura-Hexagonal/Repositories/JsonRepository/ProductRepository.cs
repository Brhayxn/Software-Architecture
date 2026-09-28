
using System.Text.Json;
using Arquitectura_Hexagonal.Entities;
using Arquitectura_Hexagonal.Ports.Secundary;

namespace Arquitectura_Hexagonal.Repositories.JsonRepository
{
    public class ProductRepository : IRepository<Products>
    {
        private readonly string _filePath;

        public ProductRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(Products product)
        {
            var products = GetAll().ToList();
            products.Add(product);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(products, options);
            File.WriteAllText(_filePath, jsonString);
        }

        public IEnumerable<Products> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Products>();
            }

            string json = File.ReadAllText(_filePath);
            var products = JsonSerializer.Deserialize<List<Products>>(json);
            return products ?? new List<Products>();
        }
    }
}