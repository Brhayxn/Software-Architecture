
using Arquitectura_Hexagonal.Ports.Secundary;
using Arquitectura_Hexagonal.Entities;
using System.Xml.Linq;

namespace Arquitectura_Hexagonal.Repositories.XMLRepository
{
    public class XmlProductRepository : IRepository<Products>
    {
        private readonly string _filePath;
        public XmlProductRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                // Create an empty XML file if it doesn't exist
                var emptyXml = new XDocument(new XElement("Products"));
                emptyXml.Save(_filePath);
            }
        }
        
        public void Save(Products product)
        {
            // Implementation for adding a product to XML storage
            var doc = XDocument.Load(_filePath);
            doc.Root.Add(new XElement("Product",
                new XElement("Id", product.id),
                new XElement("Name", product.Name),
                new XElement("Price", product.Price)
            ));
            doc.Save(_filePath);
        }


        public IEnumerable<Products> GetAll()
        {
            // Implementation for retrieving all products from XML storage
            var doc = XDocument.Load(_filePath);

            var products = new List<Products>();

            foreach (var element in doc.Root.Elements("Product"))
            {
                var product = new Products
                (
                    Guid.Parse(element.Element("Id").Value),
                    element.Element("Name").Value,
                    decimal.Parse(element.Element("Price").Value)
                );
                products.Add(product);
            }

            return products;
        }
    }
}