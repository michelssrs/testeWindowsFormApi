using System;

namespace teste.FormApi
{
    public class orderObj
    {
        public Int32 userId { get; set; }
        public Int32 orderId { get; set; }
        public Int32 productId { get; set; }
    }

    public class orderListObj
    {
        public String productName { get; set; }
        public DateTime productDate { get; set; }
        public DateTime orderDate { get; set; }
    }
}
