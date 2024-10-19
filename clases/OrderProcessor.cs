using interfaces;
using objetos;

namespace logic{

    public class OrderProcessor
    {
        private readonly IInventoryService _inventoryService;
        private readonly IEmailService _emailService;

        public OrderProcessor(IInventoryService inventoryService, IEmailService emailService)
        {
            _inventoryService = inventoryService;
            _emailService = emailService;
        }

       public void ProcesarPedido(Pedido pedido)
{
    foreach (var articulo in pedido.Articulos)
    {
        if (articulo.Cantidad > 20) 
        {
            throw new Exception($"El artículo: {articulo.Nombre} excede el límite permitido");
        }

        if (!_inventoryService.VerificarInventario(articulo.Id, articulo.Cantidad))
        {
            throw new Exception($"Inventario insuficiente para el artículo: {articulo.Nombre}");
        }

        _inventoryService.ActualizarInventario(articulo.Id, articulo.Cantidad);
    }

    _emailService.EnviarConfirmacion(pedido);
}
    }

}