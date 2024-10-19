using objetos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace objetos{

public class Pedido
{

    
    public int Id { get; set; }
    public string ClienteNombre { get; set; }
    public List<Articulo> Articulos { get; set; } = new List<Articulo>();
    public string Estado { get; set; } // Ej: Pendiente, Procesado, Cancelado, etc.
    public DateTime FechaCreacion { get; set; }

    public Pedido(string clienteNombre)
    {
        ClienteNombre = clienteNombre;
        Estado = "Pendiente";
        FechaCreacion = DateTime.Now;
    }

    public decimal CalcularTotal()
    {
        return Articulos.Sum(a => a.Precio * a.Cantidad);
    }

    public void AgregarArticulo(Articulo articulo)
    {
        Articulos.Add(articulo);
    }

    public void RemoverArticulo(Articulo articulo)
    {
        Articulos.Remove(articulo);
    }
}
}