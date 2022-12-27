﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongNet.Common;
using QuanLyPhongNet.BUS;

namespace QuanLyPhongNet
{
    public partial class CategoryManageGUI : Form
    {
        private Parameters parameter;
        private NetRoomReader objReader;
        private NetRoomWriter objWriter;
        private const int TAB_FOOD = 0;
        private const int TAB_DRINK = 1;
        private const int TAB_CARD = 2;
        private const int TAB_CATEGORY = 3;

        public CategoryManageGUI()
        {
            InitializeComponent();
            parameter = new Parameters();
            objReader = new NetRoomReader();
            objWriter = new NetRoomWriter();
        }


        private void CategoryManageGUILoadEventHandler(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            LoadSourceToDRGV();
            LoadSourceToCBO();
            grbInformation.ForeColor = Color.Blue;
        }

        private void SearchEventHandler(object sender, EventArgs e)
        {

        }

        private void AddEventHandler(object sender, EventArgs e)
        {
            AddProcess();
        }

        private void UpdateEventHandler(object sender, EventArgs e)
        {
            UpdateProcess();
        }

        private void DeleteEventHandler(object sender, EventArgs e)
        {
            DeleteProcess();
        }

        private void ExitEventHandler(object sender, EventArgs e)
        {
            OptionGUI frmOption = new OptionGUI();
            this.Hide();
            frmOption.ShowDialog();     
        }

        private void SearchMouseHoverEventHandler(object sender, EventArgs e)
        {
            picSearch.BorderStyle = BorderStyle.Fixed3D;

        }

        private void SearchMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picSearch.BorderStyle = BorderStyle.None;
        }

        private void AddMouseHoverEventHandler(object sender, EventArgs e)
        {
            picAdd.BorderStyle = BorderStyle.Fixed3D;
        }

        private void AddMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picAdd.BorderStyle = BorderStyle.None;
        }

        private void UpdateMouseHoverEventHandler(object sender, EventArgs e)
        {
            picUpdate.BorderStyle = BorderStyle.Fixed3D;
        }

        private void UpdateMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picUpdate.BorderStyle = BorderStyle.None;
        }

        private void DeleteMouseHoverEventHandler(object sender, EventArgs e)
        {
            picDelete.BorderStyle = BorderStyle.Fixed3D;
        }

        private void DeleteMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picDelete.BorderStyle = BorderStyle.None;
        }

        private void ExitMouseHoverEventHandler(object sender, EventArgs e)
        {
            picExit.BorderStyle = BorderStyle.Fixed3D;
        }

        private void ExitMouseLeaveEventHandler(object sender, EventArgs e)
        {
            picExit.BorderStyle = BorderStyle.None;
        }

        private void ResetControl(TabPage currentTAB)
        {
            foreach (Control c in currentTAB.Controls)
            {
                if (c is TextBox)
                    (c as TextBox).ResetText();
                else if (c is ComboBox)
                {
                    (c as ComboBox).Text = "--Lựa Chọn--";
                    (c as ComboBox).ForeColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// Check all input control is null or empty.
        /// </summary>
        /// <param name="currentTAB">current tabpag.</param>
        /// <returns>true or false.</returns>
        private bool CheckValidInput(TabPage currentTAB)
        {
            int countFilled = currentTAB.Controls.OfType<TextBox>().Count(x => string.IsNullOrEmpty(x.Text));
            if (countFilled > 0)
                return false;
            return true;
        }

        private void DgvInformationCellClickEventHandler(object sender, DataGridViewCellEventArgs e)
        {
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    {
                        txtFoodName.Text = drgvInformation.CurrentRow.Cells[1].Value.ToString();
                        cboFoodCategory.Text = drgvInformation.CurrentRow.Cells[2].Value.ToString();
                        txtPriceUnitOfFood.Text = drgvInformation.CurrentRow.Cells[3].Value.ToString();
                        txtInventoryNumberOfFood.Text = drgvInformation.CurrentRow.Cells[5].Value.ToString();
                        txtUnitLotOfFood.Text = drgvInformation.CurrentRow.Cells[4].Value.ToString();
                        break;
                    }
                case TAB_DRINK:
                    {
                        txtDrinkName.Text = drgvInformation.CurrentRow.Cells[1].Value.ToString();
                        cboDrinkCategory.Text = drgvInformation.CurrentRow.Cells[2].Value.ToString();
                        txtPriceUnitOfDrink.Text = drgvInformation.CurrentRow.Cells[3].Value.ToString();
                        txtInventoryNumberOfDrink.Text = drgvInformation.CurrentRow.Cells[5].Value.ToString();
                        txtUnitLotOfDrink.Text = drgvInformation.CurrentRow.Cells[4].Value.ToString();
                        break;
                    }
                case TAB_CARD:
                    {
                        txtCardName.Text = drgvInformation.CurrentRow.Cells[1].Value.ToString();
                        cboCardCategory.Text = drgvInformation.CurrentRow.Cells[2].Value.ToString();
                        txtPriceUnitOfCard.Text = drgvInformation.CurrentRow.Cells[3].Value.ToString();
                        txtInventoryNumberOfCard.Text = drgvInformation.CurrentRow.Cells[5].Value.ToString();
                        txtUnitLotOfCard.Text = drgvInformation.CurrentRow.Cells[4].Value.ToString();
                        break;
                    }
                case TAB_CATEGORY:
                    {
                        txtCategoryName.Text = drgvInformation.CurrentRow.Cells[0].Value.ToString();
                        break;
                    }
            }
        }
            

        private void DgvInformationCellDoubleClickEventHandler(object sender, DataGridViewCellEventArgs e)
        {
            TabPage tb = tab.SelectedTab;
            ResetControl(tb);
            drgvInformation.ClearSelection();
        }

        private void LoadSourceToDRGV()
        {
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    drgvInformation.DataSource = objReader.GetAllFoods();
                    drgvInformation.Columns[0].HeaderText = "Mã Định Danh";
                    drgvInformation.Columns[0].Width = 100;
                    drgvInformation.Columns[1].HeaderText = "Tên Món Ăn";
                    drgvInformation.Columns[2].HeaderText = "Thuộc Loại";
                    drgvInformation.Columns[3].HeaderText = "Đơn Giá";
                    drgvInformation.Columns[4].HeaderText = "Đơn Vị Tính";
                    drgvInformation.Columns[5].HeaderText = "Số Lượng Tồn";
                    break;
                case TAB_DRINK:
                    drgvInformation.DataSource = objReader.GetAllDrinks();
                    drgvInformation.Columns[0].HeaderText = "Mã Định Danh";
                    drgvInformation.Columns[0].Width = 100;
                    drgvInformation.Columns[1].HeaderText = "Tên Nước Uống";
                    drgvInformation.Columns[2].HeaderText = "Thuộc Loại";
                    drgvInformation.Columns[3].HeaderText = "Đơn Giá";
                    drgvInformation.Columns[4].HeaderText = "Đơn Vị Tính";
                    drgvInformation.Columns[5].HeaderText = "Số Lượng Tồn";
                    break;
                case TAB_CARD:
                    drgvInformation.DataSource = objReader.GetAllCards();
                    drgvInformation.Columns[0].HeaderText = "Mã Định Danh";
                    drgvInformation.Columns[0].Width = 100;
                    drgvInformation.Columns[1].HeaderText = "Tên Thẻ";
                    drgvInformation.Columns[2].HeaderText = "Thuộc Loại";
                    drgvInformation.Columns[3].HeaderText = "Đơn Giá";
                    drgvInformation.Columns[4].HeaderText = "Đơn Vị Tính";
                    drgvInformation.Columns[5].HeaderText = "Số Lượng Tồn";
                    break;
                case TAB_CATEGORY:
                    drgvInformation.DataSource = objReader.GetAllCategorys();
                    drgvInformation.Columns[0].HeaderText = "Tên Loại Danh Mục";               
                    break;
            }
            drgvInformation.Refresh();
            LoadSourceToCBO();
            drgvInformation.ClearSelection();
        }

        private void LoadSourceToCBO()
        {
            cboFoodCategory.DataSource = objReader.GetAllCategorys();
            cboFoodCategory.DisplayMember = "CategoryName";
            cboFoodCategory.ValueMember = "CategoryName";
            cboDrinkCategory.DataSource = objReader.GetAllCategorys();
            cboDrinkCategory.DisplayMember = "CategoryName";
            cboDrinkCategory.ValueMember = "CategoryName";
            cboCardCategory.DataSource = objReader.GetAllCategorys();
            cboCardCategory.DisplayMember = "CategoryName";
            cboCardCategory.ValueMember = "CategoryName";
        }

        private void TabSelectedIndexChangedEventHandler(object sender, EventArgs e)
        {
            LoadSourceToCBO();
            LoadSourceToDRGV();
            TabPage tb = tab.SelectedTab;
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    grbInformation.ForeColor = Color.Blue;
                    break;
                case TAB_DRINK:
                    grbInformation.ForeColor = Color.Chocolate;
                    break;
                case TAB_CARD:
                    grbInformation.ForeColor = Color.Indigo;
                    break;
                case TAB_CATEGORY:
                    grbInformation.ForeColor = Color.DarkRed;
                    break;
            }
        }

        private void AddProcess()
        {
            TabPage tp = tab.SelectedTab;
            if (!CheckValidInput(tp))
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin!");
                return;
            }
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    objWriter.InsertFood(txtFoodName.Text, cboFoodCategory.Text, float.Parse(txtPriceUnitOfFood.Text), txtUnitLotOfFood.Text, int.Parse(txtInventoryNumberOfFood.Text));
                    break;
                case TAB_DRINK:
                    objWriter.InsertDrink(txtDrinkName.Text, cboDrinkCategory.Text, float.Parse(txtPriceUnitOfDrink.Text), txtUnitLotOfDrink.Text, int.Parse(txtInventoryNumberOfDrink.Text));
                    break;
                case TAB_CARD:
                    objWriter.InsertCard(txtCardName.Text, cboCardCategory.Text, float.Parse(txtPriceUnitOfCard.Text),txtUnitLotOfCard.Text, int.Parse(txtInventoryNumberOfCard.Text));
                    break;
                case TAB_CATEGORY:
                    objWriter.InsertCategory(txtCategoryName.Text);
                    break;
            }
            ResetControl(tp);
            LoadSourceToDRGV();
            LoadSourceToCBO();
            MessageBox.Show("Thêm thành công!");
        }

        public void UpdateProcess()
        {
            TabPage tp = tab.SelectedTab;
            switch (tab.SelectedIndex)
            {
                case TAB_FOOD:
                    objWriter.UpdateFood(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()), txtFoodName.Text, cboFoodCategory.Text, float.Parse(txtPriceUnitOfFood.Text), txtUnitLotOfFood.Text, int.Parse(txtInventoryNumberOfFood.Text));
                    break;
                case TAB_DRINK:
                    objWriter.UpdateDrink(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()), txtDrinkName.Text, cboDrinkCategory.Text, float.Parse(txtPriceUnitOfDrink.Text), txtUnitLotOfDrink.Text, int.Parse(txtInventoryNumberOfDrink.Text));
                    break;
                case TAB_CARD:
                    objWriter.UpdateCard(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()), txtCardName.Text, cboCardCategory.Text, float.Parse(txtPriceUnitOfCard.Text), txtUnitLotOfCard.Text, int.Parse(txtInventoryNumberOfCard.Text));
                    break;
                case TAB_CATEGORY:
                    objWriter.UpdateCategory(txtCategoryName.Text);
                    break;
            }
            ResetControl(tp);
            LoadSourceToCBO();
            LoadSourceToDRGV();
            MessageBox.Show("Sửa thành công");
        }

        public void DeleteProcess()
        {
            if (MessageBox.Show("Bạn muốn xóa?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TabPage tp = tab.SelectedTab;
                switch (tab.SelectedIndex)
                {
                    case TAB_FOOD:
                        objWriter.DeleteFood(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()));
                        break;
                    case TAB_DRINK:
                        objWriter.DeleteDrink(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()));
                        break;
                    case TAB_CARD:
                        objWriter.DeleteCard(int.Parse(drgvInformation.CurrentRow.Cells[0].Value.ToString()));
                        break;
                    case TAB_CATEGORY:
                        objWriter.DeleteCategory(drgvInformation.CurrentRow.Cells[0].Value.ToString());
                        break;
                }
                ResetControl(tp);
                LoadSourceToDRGV();
                LoadSourceToCBO();
                MessageBox.Show("Xóa thành công!");
            }
        }

        private void PriceUnitOfFoodKeyPressEventHandler(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void InventoryNumberOfFoodKeyPressEventHandler(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void drgvInformation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
