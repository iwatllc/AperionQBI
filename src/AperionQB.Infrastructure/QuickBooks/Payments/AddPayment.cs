﻿using System.Security.Cryptography.Xml;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using AperionQB.Application.Interfaces;
using AperionQB.Infrastructure.Logging;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
    public class AddPayment : QuickBooksOperation
    {
        private Object l = new Object();


        private Logger logger;
        public AddPayment(IApplicationDbContext context, IInfoHandler handler) : base(context, handler) { logger = new Logger(); }

        /**
         * Example found here: https://github.com/IntuitDeveloper/SampleApp-CRUD-.Net/blob/master/SampleApp_CRUD_.Net/SampleApp_CRUD_.Net/Helper/QBOHelper.cs#L264
         */
        public int addPayment(decimal totalAmt, int customerId, string bzbPaymentTypeID, string memo, string invoiceIdentifier)
        {
            int intuitID = -1;
            try
            {
                intuitID = _context.QBPaymentTypeMappings.ToList().Where((type) => (type.bzbPaymentTypeID == Int32.Parse(bzbPaymentTypeID))).First().intuitPaymentTypeID.Value;
            }catch (Exception e)
            {
                logger.log(DateTime.Now + ": Failed to find a Payment Type Mapping entry for BzB Payment Type ID " + bzbPaymentTypeID + ".");
            }
            try
            {
                
                SalesReceipt salesReceipt = new SalesReceipt();

                salesReceipt.TotalAmt = totalAmt;
                salesReceipt.TotalAmtSpecified = true;
                salesReceipt.PrivateNote = memo;

                salesReceipt.ApplyTaxAfterDiscount = false;
                salesReceipt.ApplyTaxAfterDiscountSpecified = true;

                salesReceipt.PrintStatus = PrintStatusEnum.NeedToPrint;
                salesReceipt.PrintStatusSpecified = true;
                salesReceipt.EmailStatus = EmailStatusEnum.NotSet;
                salesReceipt.EmailStatusSpecified = true;
                salesReceipt.PaymentRefNum = invoiceIdentifier;
        
                salesReceipt.Balance = new Decimal(0.00);
                salesReceipt.BalanceSpecified = true;

                salesReceipt.TxnDate = DateTime.UtcNow.Date;
                salesReceipt.TxnDateSpecified = true;
                salesReceipt.CustomerRef = new ReferenceType
                {
                    name = "Customer.ID",
                    Value = customerId.ToString()
                }; ;
                
                Console.WriteLine("Sending invoice with intuit payment id: " + intuitID.ToString());

                if(intuitID != -1)
                {
                    salesReceipt.PaymentMethodRef = new ReferenceType { Value = intuitID.ToString()};
                }

                List<Line> lineList = new List<Line>();
                Line line = new Line();
                line.LineNum = "1";
                line.Description = memo;
                line.Amount = totalAmt;
                line.AmountSpecified = true;


                line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                line.DetailTypeSpecified = true;

                line.AnyIntuitObject = new SalesItemLineDetail()
                {
                    Qty = 1,
                    QtySpecified = true,

                };


                lineList.Add(line);
                salesReceipt.Line = lineList.ToArray();
                SalesReceipt result = null;

                lock (l)
                {
                    DataService service = new DataService(serviceContext);
                    result = service.Add(salesReceipt);
                }

                if (result != null)
                {
                    return Int32.Parse(result.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                return -1;
            }
            return -1;
        }
    }
}
