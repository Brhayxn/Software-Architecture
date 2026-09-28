
// dotnet add package Microsoft.Extensions.DependencyInjection
using Arquitectura_hexagonal.Ports.Primary;
using Arquitectura_Hexagonal.Entities;
using Arquitectura_Hexagonal.Ports.Secundary;
using Arquitectura_Hexagonal.Repositories.JsonRepository;
using Arquitectura_Hexagonal.Services;
using Microsoft.Extensions.DependencyInjection;
using Arquitectura_Hexagonal.Presentation.CLI;

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");
var services = new ServiceCollection();
services.AddSingleton<IRepository<Products>, ProductRepository>(provider => new ProductRepository(filePath));
services.AddTransient<IService, ProductService>();

var serviceProvider = services.BuildServiceProvider();
var productService = serviceProvider.GetRequiredService<IService>();

var cli = new Cli(productService);
cli.Run();
