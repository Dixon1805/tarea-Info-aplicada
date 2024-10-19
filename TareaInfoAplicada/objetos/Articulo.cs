namespace objetos
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public Articulo(int id, string nombre, decimal precio, int cantidad)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
        }
    }
}