using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEZTA.Models
{
    public class Product
    {
        public string productSKU { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string discount { get; set; }
        public string amount { get; set; }
    }

    public class Tender
    {
        public string tenderName { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string amountLocalCurrency { get; set; }
        public string reference { get; set; }
    }

    public class Response
    {
        public string flag { get; set; }
        public string message { get; set; }
    }
    public class Order
    {
        public string orderNo { get; set; }
        public string orderDate { get; set; }
        public string referenceNo { get; set; }
        public string isReturn { get; set; }
        public string recallOrderNo { get; set; }
        public string customerAccountNo { get; set; }
        public string customerName { get; set; }
        public string shippingName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string zip { get; set; }
        public string area { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string mobile1 { get; set; }
        public string mobile2 { get; set; }
        public string email { get; set; }
        public string currency { get; set; }
        public string grossAmount { get; set; }
        public string shippingCharge { get; set; }
        public string netAmount { get; set; }
        public List<Product> products { get; set; }
        public List<Tender> tenders { get; set; }
    }
    public class CancelOrder
    {
        public string orderNo { get; set; }
        public string cancelDate { get; set; }
        public string cancelReason { get; set; }         

    }
}