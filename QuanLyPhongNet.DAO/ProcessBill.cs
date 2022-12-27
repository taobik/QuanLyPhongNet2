namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessBill
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;
        string x = "Data Source=msi;Initial Catalog=QuanLyPhongNet;Integrated Security=True";
        public ProcessBill()
        {
            objReader = new QuanLyPhongNETDataContext(x);
            objWriter = new QuanLyPhongNETDataContext(x);
        }

        public void CreateNewBill(int billID,string userName,DateTime foundedDate,float priceTotal)
        {
            Bill bill = new Bill();
            bill.BillID = billID;
            bill.FoundedUser = userName;
            bill.FoundedDate = foundedDate;
            bill.PriceTotal = priceTotal;
            objReader.Bills.InsertOnSubmit(bill);
            objReader.SubmitChanges();
        }
        public List<QuanLyPhongNet.DTO.Bill> LoadAllBills()
        {
            return (from bill in objReader.Bills
                    select new QuanLyPhongNet.DTO.Bill
                    {
                        BillID =bill.BillID,
                        UserName=bill.FoundedUser,
                        FoundedDate=bill.FoundedDate.Value,
                        PriceTotal=(float)bill.PriceTotal
                    }).ToList();
        }
    }
}
