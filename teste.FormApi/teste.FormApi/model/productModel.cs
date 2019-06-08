using System;

namespace teste.FormApi
{
    public class productObj
    {
        public Int32 productId { get; set; }

        public Int32 productAmount { get; set; }
        public String productNome { get; set; }
        public Decimal productPrice { get; set; }
        public DateTime productDate { get; set; }
    }

    public class productGridObj
    {
        public Int32 productId { get; set; }
        public String productNome { get; set; }
        public Decimal productPrice { get; set; }

    }
}
