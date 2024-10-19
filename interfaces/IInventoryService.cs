using objetos;
namespace interfaces{

public interface IInventoryService{
    bool VerificarInventario(int productoId, int cantidad);
    void ActualizarInventario(int productoId, int cantidad);
}
}

