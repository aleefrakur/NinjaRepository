using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ninja.model.Entity;
using ninja.model.Mock;

namespace ninja.model.Manager {

    public class InvoiceManager {

        private InvoiceMock _mock;

        public InvoiceManager() {

            this._mock = InvoiceMock.GetInstance();

        }

        public IList<Invoice> GetAll() {

            return this._mock.GetAll();

        }

        public Invoice GetById(long id) {

            return this._mock.GetById(id);

        }

        public void Insert(Invoice item) {

            this._mock.Insert(item);

        }

        public void Delete(long id) {

            Invoice invoice = this.GetById(id);
            this._mock.Delete(invoice);

        }

        public Boolean Exists(long id) {

            return this._mock.Exists(id);

        }

        public void UpdateDetail(long id, IList<InvoiceDetail> detail) {

            /*
              Este método tiene que reemplazar todos los items del detalle de la factura
              por los recibidos por parámetro
             */

            #region Escribir el código dentro de este bloque

                foreach (InvoiceDetail record in detail.Where(x => x.InvoiceId == id))
                {
                    bool exists = this._mock.Exists(record.InvoiceId);

                    if (exists)
                        this._mock.GetById(record.InvoiceId).AddDetail(record);
            }
           
            #endregion Escribir el código dentro de este bloque

        }

    }

}