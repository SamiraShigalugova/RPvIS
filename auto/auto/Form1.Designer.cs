namespace auto
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button2 = new Button();
            button4 = new Button();
            button1 = new Button();
            dataGridViewClients = new DataGridView();
            tabPage2 = new TabPage();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            dataGridViewProducts = new DataGridView();
            tabPage3 = new TabPage();
            button14 = new Button();
            button13 = new Button();
            button12 = new Button();
            button11 = new Button();
            button10 = new Button();
            dataGridViewOrders = new DataGridView();
            tabPage4 = new TabPage();
            button18 = new Button();
            button17 = new Button();
            button16 = new Button();
            dataGridViewWarehouse = new DataGridView();
            tabPage5 = new TabPage();
            groupBox2 = new GroupBox();
            btnDeliveryReport = new Button();
            dataGridViewReport2 = new DataGridView();
            checkedListBoxProducts = new CheckedListBox();
            dateTimePickerEnd = new DateTimePicker();
            dateTimePickerStart = new DateTimePicker();
            groupBox1 = new GroupBox();
            dataGridViewReport = new DataGridView();
            button20 = new Button();
            dtpUnpaidDate = new DateTimePicker();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClients).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewWarehouse).BeginInit();
            tabPage5.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport2).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Location = new Point(29, 27);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(911, 631);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(dataGridViewClients);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(903, 593);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Клиенты";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(644, 314);
            button2.Name = "button2";
            button2.Size = new Size(240, 34);
            button2.TabIndex = 5;
            button2.Text = "Удалить клиента";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnDeleteClient_Click;
            // 
            // button4
            // 
            button4.Location = new Point(16, 391);
            button4.Name = "button4";
            button4.Size = new Size(868, 34);
            button4.TabIndex = 4;
            button4.Text = "Редактировать клиента";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnEditClient_Click;
            // 
            // button1
            // 
            button1.Location = new Point(16, 314);
            button1.Name = "button1";
            button1.Size = new Size(571, 34);
            button1.TabIndex = 1;
            button1.Text = "Добавить нового клиента";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridViewClients
            // 
            dataGridViewClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClients.Location = new Point(16, 17);
            dataGridViewClients.Name = "dataGridViewClients";
            dataGridViewClients.RowHeadersWidth = 62;
            dataGridViewClients.Size = new Size(868, 242);
            dataGridViewClients.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button7);
            tabPage2.Controls.Add(button6);
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(dataGridViewProducts);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(903, 593);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Товары";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(17, 433);
            button7.Name = "button7";
            button7.Size = new Size(864, 34);
            button7.TabIndex = 3;
            button7.Text = "Редактировать";
            button7.UseVisualStyleBackColor = true;
            button7.Click += btnEditProduct_Click;
            // 
            // button6
            // 
            button6.Location = new Point(427, 487);
            button6.Name = "button6";
            button6.Size = new Size(454, 34);
            button6.TabIndex = 2;
            button6.Text = "Удалить";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btnDeleteProduct_Click;
            // 
            // button5
            // 
            button5.Location = new Point(17, 487);
            button5.Name = "button5";
            button5.Size = new Size(390, 34);
            button5.TabIndex = 1;
            button5.Text = "Добавить";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnAddProduct_Click;
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewProducts.Location = new Point(17, 20);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.RowHeadersWidth = 62;
            dataGridViewProducts.Size = new Size(864, 379);
            dataGridViewProducts.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(button14);
            tabPage3.Controls.Add(button13);
            tabPage3.Controls.Add(button12);
            tabPage3.Controls.Add(button11);
            tabPage3.Controls.Add(button10);
            tabPage3.Controls.Add(dataGridViewOrders);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(903, 593);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Заказы";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            button14.Location = new Point(243, 445);
            button14.Name = "button14";
            button14.Size = new Size(637, 34);
            button14.TabIndex = 5;
            button14.Text = "Экспорт в Excel";
            button14.UseVisualStyleBackColor = true;
            button14.Click += btnExportToExcel_Click;
            // 
            // button13
            // 
            button13.Location = new Point(243, 380);
            button13.Name = "button13";
            button13.Size = new Size(637, 34);
            button13.TabIndex = 4;
            button13.Text = "Доставить";
            button13.UseVisualStyleBackColor = true;
            button13.Click += btnDeliverOrder_Click;
            // 
            // button12
            // 
            button12.Location = new Point(22, 498);
            button12.Name = "button12";
            button12.Size = new Size(177, 34);
            button12.TabIndex = 3;
            button12.Text = "Оплатить";
            button12.UseVisualStyleBackColor = true;
            button12.Click += btnPayOrder_Click;
            // 
            // button11
            // 
            button11.Location = new Point(22, 435);
            button11.Name = "button11";
            button11.Size = new Size(177, 34);
            button11.TabIndex = 2;
            button11.Text = "Просмотр";
            button11.UseVisualStyleBackColor = true;
            button11.Click += btnViewOrder_Click;
            // 
            // button10
            // 
            button10.Location = new Point(22, 380);
            button10.Name = "button10";
            button10.Size = new Size(177, 34);
            button10.TabIndex = 1;
            button10.Text = "Новый заказ";
            button10.UseVisualStyleBackColor = true;
            button10.Click += btnNewOrder_Click;
            // 
            // dataGridViewOrders
            // 
            dataGridViewOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewOrders.Location = new Point(22, 22);
            dataGridViewOrders.Name = "dataGridViewOrders";
            dataGridViewOrders.RowHeadersWidth = 62;
            dataGridViewOrders.Size = new Size(858, 330);
            dataGridViewOrders.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(button18);
            tabPage4.Controls.Add(button17);
            tabPage4.Controls.Add(button16);
            tabPage4.Controls.Add(dataGridViewWarehouse);
            tabPage4.Location = new Point(4, 34);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(903, 593);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Склад";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // button18
            // 
            button18.Location = new Point(242, 436);
            button18.Name = "button18";
            button18.Size = new Size(642, 34);
            button18.TabIndex = 3;
            button18.Text = "Списать";
            button18.UseVisualStyleBackColor = true;
            button18.Click += btnWriteOff_Click;
            // 
            // button17
            // 
            button17.Location = new Point(19, 488);
            button17.Name = "button17";
            button17.Size = new Size(865, 34);
            button17.TabIndex = 2;
            button17.Text = "Обновить остатки";
            button17.UseVisualStyleBackColor = true;
            button17.Click += btnRefreshWarehouse_Click;
            // 
            // button16
            // 
            button16.Location = new Point(19, 436);
            button16.Name = "button16";
            button16.Size = new Size(182, 34);
            button16.TabIndex = 1;
            button16.Text = "Новая накладная";
            button16.UseVisualStyleBackColor = true;
            button16.Click += btnNewInvoice_Click;
            // 
            // dataGridViewWarehouse
            // 
            dataGridViewWarehouse.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewWarehouse.Location = new Point(24, 18);
            dataGridViewWarehouse.Name = "dataGridViewWarehouse";
            dataGridViewWarehouse.RowHeadersWidth = 62;
            dataGridViewWarehouse.Size = new Size(860, 390);
            dataGridViewWarehouse.TabIndex = 0;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(groupBox2);
            tabPage5.Controls.Add(groupBox1);
            tabPage5.Location = new Point(4, 34);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(903, 593);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Отчеты";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnDeliveryReport);
            groupBox2.Controls.Add(dataGridViewReport2);
            groupBox2.Controls.Add(checkedListBoxProducts);
            groupBox2.Controls.Add(dateTimePickerEnd);
            groupBox2.Controls.Add(dateTimePickerStart);
            groupBox2.Location = new Point(34, 306);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(840, 267);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Соотношение заказов";
            // 
            // btnDeliveryReport
            // 
            btnDeliveryReport.Location = new Point(258, 197);
            btnDeliveryReport.Name = "btnDeliveryReport";
            btnDeliveryReport.Size = new Size(154, 34);
            btnDeliveryReport.TabIndex = 4;
            btnDeliveryReport.Text = "Создать отчет";
            btnDeliveryReport.UseVisualStyleBackColor = true;
            btnDeliveryReport.Click += btnDeliveryReport_Click;
            // 
            // dataGridViewReport2
            // 
            dataGridViewReport2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReport2.Location = new Point(456, 30);
            dataGridViewReport2.Name = "dataGridViewReport2";
            dataGridViewReport2.RowHeadersWidth = 62;
            dataGridViewReport2.Size = new Size(360, 225);
            dataGridViewReport2.TabIndex = 3;
            // 
            // checkedListBoxProducts
            // 
            checkedListBoxProducts.FormattingEnabled = true;
            checkedListBoxProducts.Location = new Point(26, 143);
            checkedListBoxProducts.Name = "checkedListBoxProducts";
            checkedListBoxProducts.Size = new Size(217, 88);
            checkedListBoxProducts.TabIndex = 2;
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Location = new Point(26, 82);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(217, 31);
            dateTimePickerEnd.TabIndex = 1;
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.Location = new Point(25, 36);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new Size(218, 31);
            dateTimePickerStart.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dataGridViewReport);
            groupBox1.Controls.Add(button20);
            groupBox1.Controls.Add(dtpUnpaidDate);
            groupBox1.Location = new Point(34, 28);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(840, 255);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Неоплаченные товары";
            // 
            // dataGridViewReport
            // 
            dataGridViewReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReport.Location = new Point(270, 24);
            dataGridViewReport.Name = "dataGridViewReport";
            dataGridViewReport.RowHeadersWidth = 62;
            dataGridViewReport.Size = new Size(546, 197);
            dataGridViewReport.TabIndex = 2;
            // 
            // button20
            // 
            button20.Location = new Point(21, 90);
            button20.Name = "button20";
            button20.Size = new Size(222, 34);
            button20.TabIndex = 1;
            button20.Text = "Создать отчет";
            button20.UseVisualStyleBackColor = true;
            button20.Click += btnGenerateReport_Click;
            // 
            // dtpUnpaidDate
            // 
            dtpUnpaidDate.Location = new Point(21, 42);
            dtpUnpaidDate.Name = "dtpUnpaidDate";
            dtpUnpaidDate.Size = new Size(222, 31);
            dtpUnpaidDate.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(952, 714);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewClients).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).EndInit();
            tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewWarehouse).EndInit();
            tabPage5.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport2).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Button button4;
        private Button button1;
        private DataGridView dataGridViewClients;
        private TabPage tabPage5;
        private Button button7;
        private Button button6;
        private Button button5;
        private DataGridView dataGridViewProducts;
        private Button button14;
        private Button button13;
        private Button button11;
        private DataGridView dataGridViewOrders;
        private Button button18;
        private Button button17;
        private Button button16;
        private DataGridView dataGridViewWarehouse;
        private GroupBox groupBox2;
        private DateTimePicker dateTimePickerEnd;
        private DateTimePicker dateTimePickerStart;
        private GroupBox groupBox1;
        private DataGridView dataGridViewReport;
        private Button button20;
        private DateTimePicker dtpUnpaidDate;
        private Button button21;
        private DataGridView dataGridViewReport2;
        private CheckedListBox clbProducts;
        private Button button2;
        private Button button12;
        private Button button10;
        private CheckedListBox checkedListBoxProducts;
        private Button btnDeliveryReport;
    }
}
