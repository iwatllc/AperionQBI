using System;
using System.Text.Json;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
	public class AddPayment : QuickBooksOperation
	{
        /**
         * Example found here: https://github.com/IntuitDeveloper/SampleApp-CRUD-.Net/blob/master/SampleApp_CRUD_.Net/SampleApp_CRUD_.Net/Helper/QBOHelper.cs#L264
         */
        public int addPayment(decimal totalAmt, int customerId, string lineItemDescription, string memo)
		{
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

                salesReceipt.Balance = new Decimal(0.00);
                salesReceipt.BalanceSpecified = true;

                salesReceipt.TxnDate = DateTime.UtcNow.Date;
                salesReceipt.TxnDateSpecified = true;
                salesReceipt.CustomerRef = new ReferenceType
                {
                    name = "Customer.ID",
                    Value = customerId.ToString()
                }; ;
                


                List<Line> lineList = new List<Line>();
                Line line = new Line();
                line.LineNum = "1";
                line.Description = lineItemDescription;
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

                DataService service = new DataService(serviceContext);

                SalesReceipt result = service.Add(salesReceipt);

                if (result != null)
                {
                    return Int32.Parse(result.Id);
                }
            }catch (Exception e)
			{
				Console.WriteLine(e.Message + "\n" + e.StackTrace);
				return -1;
			}
			return -1;
		}
	}
}

