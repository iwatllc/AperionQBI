using System;
using System.Text.Json;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;

namespace AperionQB.Infrastructure.QuickBooks.Payments
{
	public class AddPayment : QuickBooksOperation
	{

		public bool addPayment(int totalAmt, int customerId, string memo)
		{
            try
            {
                ReferenceType reference = new ReferenceType();
                reference.Value = customerId.ToString();
                reference.type = "Customer.ID";

                SalesItemLineDetail detail = new SalesItemLineDetail();
                detail.Qty = 1;
                detail.AnyIntuitObject = totalAmt.ToString() + "m";
                detail.ItemElementName = ItemChoiceType.UnitPrice;

                Line line = new Line();
                line.Amount = totalAmt;
                line.AmountSpecified = true;
                line.Description = "Sample data used to put payments into intuit. Should include tax and any additional fees";
                line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                line.DetailTypeSpecified = true;
                line.Id = "10";
                line.AnyIntuitObject = detail;




                Line[] lines = { line };
                SalesItemLineDetail[] details = { detail };

                SalesReceipt receipt = new SalesReceipt();
                receipt.CustomerRef = reference;
                receipt.domain = "QBO";
                receipt.TotalAmt = totalAmt;
                receipt.Line = details;
                receipt.TotalAmtSpecified = true;
                receipt.PrivateNote = memo;
                receipt.PaymentType = PaymentTypeEnum.Cash;

                DataService service = new DataService(serviceContext);
                //Console.WriteLine("Json: " + JsonSerializer.Serialize(receipt));

                SalesReceipt result = service.Add(receipt);



                if (result != null)
                {
                    return true;
                }
            }catch (Exception e)
			{
				Console.WriteLine(e.Message + "\n" + e.StackTrace);
				return false;
			}
			return false;
		}
	}
}

