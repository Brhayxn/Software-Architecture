namespace Arquitectura_Hexagonal.Entities
{
    public class Products
    {
        public Guid id { get; set; }
        private string _name { get; set; }
        private decimal _price { get; set; }

        public string Name
        {
            get => _name; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }
                _name = value;
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative.");
                }
                _price = value;
            }
        }

        public Products(Guid id, string name, decimal price)
        {
            this.id = id;
            _name = name;
            _price = price;
        }
    }
    


}