using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }

        [Required(ErrorMessage = "Se debe especificar la orden.")]
        public long IdOrden { set; get; }
        public Orden Orden { set; get; }

        public int AccountingDate { set; get; }
        
        [StringLength(20, ErrorMessage = "El número de orden de compra es demasiado largo.")]
        public string BuyOrder { set; get; }

        [StringLength(30, ErrorMessage = "El número de tarjeta de crédito es demasiado largo.")]
        public String CardNumber { set; get; }
        
        [StringLength(255, ErrorMessage = "La fecha de expiración de la tarjeta es demaciado larga.")]
        public String CardExpirationDate { set; get; }
        public int AuthorizationCode { set; get; }
        public string PaymentTypeCode { set; get; }
        public int ResponseCode { set; get; }
        public int SharedNumber { set; get; }
        public decimal Ammount { set; get; }

        [StringLength(30, ErrorMessage = "El código de comercio es demaciado largo.")]
        public string CommerceCode { set; get; }

        [StringLength(30, ErrorMessage = "La fecha de transacción es demaciado larga.")]
        public string TransactionDate { set; get; }

        [StringLength(10, ErrorMessage = "El código VCI no debe superar los 10 carácteres.")]
        public string VCI { set; get; }

        [StringLength(255, ErrorMessage = "El token de WebPay no debe sobrepasar los 255 carácteres.")]
        public string Token { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set; get; }
        public DateTime? Deleted_at { set; get; }

        public Transaction()
        {
            this.Updated_at = DateTime.Today;
        }
    }
    /*
    class WebPayTransactionDbContext: DbContext
    {
        public DbSet<WebPayTransaction> WebPayTransactions { set; get; }
    }
    */
}