
using Arquitectura_hexagonal.Ports.Primary;

namespace Arquitectura_Hexagonal.Presentation.CLI
{
    public class Cli
    {
        private readonly IService _productService;

        public Cli(IService productService)
        {
            _productService = productService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Register Product");
                Console.WriteLine("2. List Products");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterProduct();
                        break;
                    case "2":
                        ListProducts();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void RegisterProduct()
        {
            Console.Write("Enter product name: ");
            var name = Console.ReadLine();

            Console.Write("Enter product price: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price))
            {
                Console.WriteLine("Invalid price. Please enter a valid decimal number.");
                return;
            }

            try
            {
                _productService.Register(name, price);
                Console.WriteLine("Product registered successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ListProducts()
        {
            var products = _productService.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.id}, Name: {product.Name}, Price: {product.Price}");
            }
        }
    }
}