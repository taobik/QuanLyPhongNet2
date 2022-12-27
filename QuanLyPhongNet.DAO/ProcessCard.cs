namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using QuanLyPhongNet.Common;

    public class ProcessCard
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;
        string x = "Data Source=msi;Initial Catalog=QuanLyPhongNet;Integrated Security=True";
        public ProcessCard()
        {
            objReader = new QuanLyPhongNETDataContext(x);
            objWriter = new QuanLyPhongNETDataContext(x);
        }

        public List<QuanLyPhongNet.DTO.Card> LoadAllCards()
        {
            return (from card in objReader.TheCards
                    select new QuanLyPhongNet.DTO.Card
                    {
                        CardID = card.CardID,
                        Name = card.CardName,
                        CategoryName = card.CategoryName,
                        PriceUnit = (float)card.PriceUnit,
                        UnitLot = card.UnitLot,
                        InventoryNumber = (int)card.InventoryNumber,
                    }).ToList();
        }

        public void InsertCard(string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                objWriter.TheCards.InsertOnSubmit(new TheCard
                {
                    CardName = name,
                    CategoryName = categoryName,
                    PriceUnit = priceUnit,
                    UnitLot = unitLot,
                    InventoryNumber = inventoryNumber
                });
                objWriter.SubmitChanges();
            }
        }

        public void UpdateCard(int cardID,string name, string categoryName, float priceUnit, string unitLot, int inventoryNumber)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                TheCard objUpdate;
                objUpdate = objWriter.TheCards.FirstOrDefault(x => x.CardID == cardID);
                objUpdate.CardName = name;
                objUpdate.CategoryName = categoryName;
                objUpdate.PriceUnit = priceUnit;
                objUpdate.UnitLot = unitLot;
                objUpdate.InventoryNumber = inventoryNumber;
                objWriter.SubmitChanges();
            }
        }

        public void DeleteCard(int cardID)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                var objDelete = objWriter.TheCards.Single(x => x.CardID == cardID);
                objWriter.TheCards.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }    
    }
}
