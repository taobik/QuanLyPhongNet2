﻿namespace QuanLyPhongNet.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Food
    {
        private int foodID;
        private string name;
        private string categoryName;
        private float priceUnit;
        private string unitLot;
        private int inventoryNumber;

        public int FoodID
        {
            get
            {
                return foodID;
            }

            set
            {
                foodID = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }

        public float PriceUnit
        {
            get
            {
                return priceUnit;
            }

            set
            {
                priceUnit = value;
            }
        }

        public string UnitLot
        {
            get
            {
                return unitLot;
            }

            set
            {
                unitLot = value;
            }
        }

        public int InventoryNumber
        {
            get
            {
                return inventoryNumber;
            }

            set
            {
                inventoryNumber = value;
            }
        }
    }
}
