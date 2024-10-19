using NUnit.Framework;
using NSubstitute;
using objetos;
using System;
using interfaces;
using logic;

namespace objetos.Tests
{
    [TestFixture]
    public class OrderProcessorTests
    {
        private IInventoryService _inventoryService;
        private IEmailService _emailService;
        private OrderProcessor _orderProcessor;

        [SetUp]
        public void Setup()
        {
            _inventoryService = Substitute.For<IInventoryService>();
            _emailService = Substitute.For<IEmailService>();
            _orderProcessor = new OrderProcessor(_inventoryService, _emailService);
        }

        [Test]
        public void ProcesarPedido_InventarioSuficiente_EnvioDeConfirmacion()
        {
            // Arrange
            var pedido = new Pedido("Cliente 1");
            var articulo = new Articulo(1, "Articulo 1", 10, 2);
            pedido.AgregarArticulo(articulo);

            _inventoryService.VerificarInventario(articulo.Id, articulo.Cantidad).Returns(true);

            // Act
            _orderProcessor.ProcesarPedido(pedido);

            // Assert
            _inventoryService.Received(1).ActualizarInventario(articulo.Id, articulo.Cantidad);
            _emailService.Received(1).EnviarConfirmacion(pedido);
        }

        [Test]
        public void ProcesarPedido_InventarioInsuficiente_LanzaExcepcion()
        {
            // Arrange
            var pedido = new Pedido("Cliente 2");
            var articulo = new Articulo(2, "Articulo 2", 20, 5);
            pedido.AgregarArticulo(articulo);

            _inventoryService.VerificarInventario(articulo.Id, articulo.Cantidad).Returns(false);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _orderProcessor.ProcesarPedido(pedido));
            Assert.That(ex.Message, Is.EqualTo("Inventario insuficiente para el artículo: Articulo 2"));
        }

        [Test]
        public void ProcesarPedido_ActualizaInventario()
        {
            // Arrange
            var pedido = new Pedido("Cliente 3");
            var articulo = new Articulo(3, "Articulo 3", 30, 3);
            pedido.AgregarArticulo(articulo);

            _inventoryService.VerificarInventario(articulo.Id, articulo.Cantidad).Returns(true);

            // Act
            _orderProcessor.ProcesarPedido(pedido);

            // Assert
            _inventoryService.Received(1).ActualizarInventario(articulo.Id, articulo.Cantidad);
            _emailService.Received(1).EnviarConfirmacion(pedido);
        }

        [Test]
        public void ProcesarPedido_ExcedeLimiteArticulos_LanzaExcepcion()
        {
            // Arrange
            var pedido = new Pedido("Cliente 4");
            var articulo = new Articulo(4, "Articulo 4", 40, 1000); // Excede el límite permitido
            pedido.AgregarArticulo(articulo);

            _inventoryService.VerificarInventario(articulo.Id, articulo.Cantidad).Returns(true);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _orderProcessor.ProcesarPedido(pedido));
            Assert.That(ex.Message, Is.EqualTo("El artículo: Articulo 4 excede el límite permitido"));
        }
    }
}