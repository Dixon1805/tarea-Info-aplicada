using objetos;

Console.WriteLine("Hello, World!");

Pedido pedido = new Pedido("Juan Perez");
pedido.AgregarArticulo(new Articulo(1, "Producto 1", 100, 1));

Console.WriteLine($"Total: {pedido.CalcularTotal()}");