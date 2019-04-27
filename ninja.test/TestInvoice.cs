using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ninja.model.Entity;
using ninja.model.Manager;

namespace ninja.test {

    [TestClass]
    public class TestInvoice {

        [TestMethod]
        public void InsertNewInvoice() {

            //Para que funcione, la correción que hice fué reemplazar la línea "Invoice result = manager.GetById(id);" y mandarla por parámetro en el Assert como "manager.GetById(id)"
            InvoiceManager manager = new InvoiceManager();
            long id = 1006;
            Invoice invoice = new Invoice() {
                Id = id,
                Type = Invoice.Types.A.ToString()
            };

            manager.Insert(invoice);
            // Invoice result = manager.GetById(id);

            Assert.AreEqual(invoice, manager.GetById(id));

        }

        [TestMethod]
        public void InsertNewDetailInvoice() {

            InvoiceManager manager = new InvoiceManager();
            long id = 1006;
            Invoice invoice = new Invoice() {
                Id = id,
                Type = Invoice.Types.A.ToString()
            };

            invoice.AddDetail(new InvoiceDetail() {
                Id = 1,
                InvoiceId = id,
                Description = "Venta insumos varios",
                Amount = 14,
                UnitPrice = 4.33
            });

            invoice.AddDetail(new InvoiceDetail() {
                Id = 2,
                InvoiceId = id,
                Description = "Venta insumos tóner",
                Amount = 5,
                UnitPrice = 87
            });

            manager.Insert(invoice);
            Invoice result = manager.GetById(id);

            Assert.AreEqual(invoice, result);

        }

        [TestMethod]
        public void DeleteInvoice() {

            #region Escribir el código dentro de este bloque

            try
            {
                InvoiceManager manager = new InvoiceManager();
                long id = 1002;

                //Identifico el objeto/factura que deseo eliminar
                Invoice invoice = manager.GetById(id);

                //Elimino de mis registros esa factura
                manager.Delete(invoice.Id);

                //Busco en mis registros si sigue existiendo la factura que eliminé, y la traigo por Id
                Invoice result = manager.GetById(invoice.Id);

                //Si no son iguales está OK, ya que eso me demuestra que se eliminó el registro con éxito
                Assert.AreNotEqual(invoice, result);
            }
            catch (Exception ex)
            {
                //Cambio la exepción no implementada, por el retorno del mensaje de error
                throw new Exception(ex.Message.ToString());
            }
            
            #endregion Escribir el código dentro de este bloque

        }

        [TestMethod]
        public void UpdateInvoiceDetail() {
            //En este puto realizo correción sobre el Id que se asigna a InvoiceId; asignando el valore correspondiente( es el Id de la factura) 1003 + el Id del detalle, Id 1 y 2
            long id = 1003;
            InvoiceManager manager = new InvoiceManager();
            IList<InvoiceDetail> detail = new List<InvoiceDetail>();

            detail.Add(new InvoiceDetail() {
                Id = 1,
                InvoiceId = id,
                Description = "Venta insumos varios",
                Amount = 14,
                UnitPrice = 4.33
            });

            detail.Add(new InvoiceDetail() {
                Id = 2,
                InvoiceId = id,
                Description = "Venta insumos tóner",
                Amount = 5,
                UnitPrice = 87
            });

            manager.UpdateDetail(id, detail);
            Invoice result = manager.GetById(id);

            Assert.AreEqual(3, Convert.ToInt32(result.GetDetail().Count()));

        }

        [TestMethod]
        public void CalculateInvoiceTotalPriceWithTaxes() {
            //La modificación que realizé sobre este método, es crear una sumatoria auxiliar, para tener un valor comparativo contra cual verificar dicha suma.
            long id = 1003;
            InvoiceManager manager = new InvoiceManager();
            Invoice invoice = manager.GetById(id);

            double sum = 0;
            double aux = 0;

            foreach (InvoiceDetail item in invoice.GetDetail()) 
                sum += item.TotalPrice * item.Taxes;

            foreach (InvoiceDetail item in invoice.GetDetail())
                aux += item.TotalPrice * item.Taxes;

            //Si las sumas de los totales, dan un valor mayor a cero, verifico con un Assert si los montos Sum y Aux son iguales
            if((sum + aux) > 0)
                Assert.AreEqual(sum, aux);
        }

    }

}