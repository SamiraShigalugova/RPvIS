namespace tourfirma
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button8 = new Button();
            tabPage6 = new TabPage();
            cmbParametricQuery = new ComboBox();
            cmbAggregateQuery = new ComboBox();
            button7 = new Button();
            button6 = new Button();
            dataGridViewResult = new DataGridView();
            button5 = new Button();
            label2 = new Label();
            button4 = new Button();
            label1 = new Label();
            tabPage5 = new TabPage();
            dataGridView6 = new DataGridView();
            tabPage4 = new TabPage();
            dataGridView5 = new DataGridView();
            tabPage3 = new TabPage();
            dataGridView4 = new DataGridView();
            tabPage2 = new TabPage();
            dataGridView3 = new DataGridView();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).BeginInit();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView6).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView5).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartPie).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartBar).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.DarkSeaGreen;
            button1.Location = new Point(34, 483);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(213, 80);
            button1.TabIndex = 1;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Location = new Point(741, 483);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(213, 80);
            button2.TabIndex = 2;
            button2.Text = "Изменить";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.RosyBrown;
            button3.Location = new Point(1096, 483);
            button3.Margin = new Padding(4, 5, 4, 5);
            button3.Name = "button3";
            button3.Size = new Size(213, 80);
            button3.TabIndex = 3;
            button3.Text = "Удалить";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.Khaki;
            button8.Location = new Point(397, 483);
            button8.Name = "button8";
            button8.Size = new Size(213, 80);
            button8.TabIndex = 5;
            button8.Text = "Триггер";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(cmbParametricQuery);
            tabPage6.Controls.Add(cmbAggregateQuery);
            tabPage6.Controls.Add(button7);
            tabPage6.Controls.Add(button6);
            tabPage6.Controls.Add(dataGridViewResult);
            tabPage6.Controls.Add(button5);
            tabPage6.Controls.Add(label2);
            tabPage6.Controls.Add(button4);
            tabPage6.Controls.Add(label1);
            tabPage6.Location = new Point(4, 34);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(1267, 324);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Запросы";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // cmbParametricQuery
            // 
            cmbParametricQuery.FormattingEnabled = true;
            cmbParametricQuery.Location = new Point(23, 228);
            cmbParametricQuery.Name = "cmbParametricQuery";
            cmbParametricQuery.Size = new Size(182, 33);
            cmbParametricQuery.TabIndex = 11;
            // 
            // cmbAggregateQuery
            // 
            cmbAggregateQuery.FormattingEnabled = true;
            cmbAggregateQuery.Location = new Point(24, 69);
            cmbAggregateQuery.Name = "cmbAggregateQuery";
            cmbAggregateQuery.Size = new Size(182, 33);
            cmbAggregateQuery.TabIndex = 10;
            // 
            // button7
            // 
            button7.BackColor = Color.PaleGreen;
            button7.Location = new Point(276, 98);
            button7.Name = "button7";
            button7.Size = new Size(177, 34);
            button7.TabIndex = 9;
            button7.Text = "Импорт из MS Excel";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.PaleGreen;
            button6.Location = new Point(276, 30);
            button6.Name = "button6";
            button6.Size = new Size(177, 38);
            button6.TabIndex = 8;
            button6.Text = "Экспорт в MS Excel";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // dataGridViewResult
            // 
            dataGridViewResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResult.Location = new Point(499, 30);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.RowHeadersWidth = 62;
            dataGridViewResult.RowTemplate.Height = 33;
            dataGridViewResult.Size = new Size(713, 260);
            dataGridViewResult.TabIndex = 7;
            // 
            // button5
            // 
            button5.Location = new Point(23, 272);
            button5.Name = "button5";
            button5.Size = new Size(112, 34);
            button5.TabIndex = 5;
            button5.Text = "Выполнить";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 182);
            label2.Name = "label2";
            label2.Size = new Size(247, 25);
            label2.TabIndex = 3;
            label2.Text = "Параметризованный запрос";
            // 
            // button4
            // 
            button4.Location = new Point(23, 117);
            button4.Name = "button4";
            button4.Size = new Size(112, 34);
            button4.TabIndex = 2;
            button4.Text = "Выполнить";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 18);
            label1.Name = "label1";
            label1.Size = new Size(214, 25);
            label1.TabIndex = 0;
            label1.Text = "Агрегированный запрос";
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(dataGridView6);
            tabPage5.Location = new Point(4, 34);
            tabPage5.Margin = new Padding(4, 5, 4, 5);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(4, 5, 4, 5);
            tabPage5.Size = new Size(1267, 324);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Оплата";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView6
            // 
            dataGridView6.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView6.Location = new Point(21, 22);
            dataGridView6.Margin = new Padding(4, 5, 4, 5);
            dataGridView6.Name = "dataGridView6";
            dataGridView6.RowHeadersWidth = 62;
            dataGridView6.RowTemplate.Height = 25;
            dataGridView6.Size = new Size(1213, 278);
            dataGridView6.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(dataGridView5);
            tabPage4.Location = new Point(4, 34);
            tabPage4.Margin = new Padding(4, 5, 4, 5);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(4, 5, 4, 5);
            tabPage4.Size = new Size(1267, 324);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Сезоны";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView5
            // 
            dataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView5.Location = new Point(21, 22);
            dataGridView5.Margin = new Padding(4, 5, 4, 5);
            dataGridView5.Name = "dataGridView5";
            dataGridView5.RowHeadersWidth = 62;
            dataGridView5.RowTemplate.Height = 25;
            dataGridView5.Size = new Size(1223, 276);
            dataGridView5.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(dataGridView4);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Margin = new Padding(4, 5, 4, 5);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(4, 5, 4, 5);
            tabPage3.Size = new Size(1267, 324);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Туры";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(25, 25);
            dataGridView4.Margin = new Padding(4, 5, 4, 5);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 62;
            dataGridView4.RowTemplate.Height = 25;
            dataGridView4.Size = new Size(1089, 275);
            dataGridView4.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView3);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Margin = new Padding(4, 5, 4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 5, 4, 5);
            tabPage2.Size = new Size(1267, 324);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Информация о туристах";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(30, 32);
            dataGridView3.Margin = new Padding(4, 5, 4, 5);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 62;
            dataGridView3.RowTemplate.Height = 25;
            dataGridView3.Size = new Size(1205, 264);
            dataGridView3.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(34, 50);
            tabControl1.Margin = new Padding(4, 5, 4, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1275, 362);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(chartBar);
            tabPage1.Controls.Add(chartPie);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1267, 324);
            tabPage1.TabIndex = 6;
            tabPage1.Text = "Диаграммы";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // chartPie
            // 
            chartArea2.Name = "ChartArea1";
            chartPie.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartPie.Legends.Add(legend2);
            chartPie.Location = new Point(23, 15);
            chartPie.Name = "chartPie";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chartPie.Series.Add(series2);
            chartPie.Size = new Size(318, 275);
            chartPie.TabIndex = 0;
            chartPie.Text = "chart1";
            // 
            // chartBar
            // 
            chartArea1.Name = "ChartArea1";
            chartBar.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartBar.Legends.Add(legend1);
            chartBar.Location = new Point(378, 15);
            chartBar.Name = "chartBar";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartBar.Series.Add(series1);
            chartBar.Size = new Size(712, 248);
            chartBar.TabIndex = 1;
            chartBar.Text = "chart1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1369, 663);
            Controls.Add(button8);
            Controls.Add(tabControl1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).EndInit();
            tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView6).EndInit();
            tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView5).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartPie).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartBar).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button8;
        private TabPage tabPage6;
        private ComboBox cmbParametricQuery;
        private ComboBox cmbAggregateQuery;
        private Button button7;
        private Button button6;
        private DataGridView dataGridViewResult;
        private Button button5;
        private Label label2;
        private Button button4;
        private Label label1;
        private TabPage tabPage5;
        private DataGridView dataGridView6;
        private TabPage tabPage4;
        private DataGridView dataGridView5;
        private TabPage tabPage3;
        private DataGridView dataGridView4;
        private TabPage tabPage2;
        private DataGridView dataGridView3;
        private TabControl tabControl1;
        private Panel panel1;
        private TabPage tabPage1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBar;
    }
}