using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using System.IO;
using System.Xml.Linq;
using ClosedXML.Excel;

namespace auto
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection con;
        private string connString = "Host=127.0.0.1;Username=postgres;Password=123;Database=AutoPartsStore";

        public Form1()
        {
            InitializeComponent();
            con = new NpgsqlConnection(connString);
            con.Open();
            LoadClients();
            LoadProducts();
            LoadOrders();
            LoadWarehouse();
            LoadProductsToCheckList();
        }

        private void LoadClients()
        {
            try
            {
                string sql = "SELECT client_id as \"ID\", name as \"Имя\", phone as \"Телефон\", " +
                            "address as \"Адрес\", email as \"Email\" FROM clients";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewClients.DataSource = dt;
                dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}");
            }
        }

        private void LoadProducts()
        {
            try
            {
                string sql = @"SELECT product_id as ""ID"", name as ""Название"", 
                      price as ""Цена"", category as ""Категория""
                      FROM products";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewProducts.DataSource = dt;
                dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrders()
        {
            try
            {
                string sql = @"SELECT o.order_id as ""ID"", c.name as ""Клиент"", 
                      o.order_date as ""Дата"", o.status as ""Статус"", 
                      o.total_amount as ""Сумма""
                      FROM orders o
                      JOIN clients c ON o.client_id = c.client_id
                      ORDER BY o.order_date DESC";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewOrders.DataSource = dt;
                dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWarehouse()
        {
            try
            {
                string sql = @"SELECT w.warehouse_id as ""ID"", w.product_id as ""product_id"", p.name as ""Товар"", 
                      w.quantity as ""Количество"", p.price as ""Цена"",
                      (w.quantity * p.price) as ""Общая стоимость"",
                      w.last_restock_date as ""Дата поступления""
                      FROM warehouse w
                      JOIN products p ON w.product_id = p.product_id
                      ORDER BY p.name";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewWarehouse.DataSource = dt;
                dataGridViewWarehouse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных склада: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadReportData()
        {
            try
            {
                string sql = @"
            SELECT 
                o.order_id AS ""ID заказа"",
                c.name AS ""Клиент"",
                o.order_date AS ""Дата заказа"",
                o.status AS ""Статус"",
                o.total_amount AS ""Сумма"",
                STRING_AGG(p.name, ', ') AS ""Товары""
            FROM orders o
            JOIN clients c ON o.client_id = c.client_id
            JOIN order_items oi ON o.order_id = oi.order_id
            JOIN products p ON oi.product_id = p.product_id
            WHERE o.status IN ('new', 'processing', 'pending') 
            GROUP BY o.order_id, c.name, o.order_date, o.status, o.total_amount
            
            ORDER BY o.order_date DESC";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewReport.DataSource = dt;
                dataGridViewReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отчета: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProductsToCheckList()
        {
            try
            {
                string sql = "SELECT product_id, name FROM products";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                checkedListBoxProducts.Items.Clear();
                while (reader.Read())
                {
                    checkedListBoxProducts.Items.Add(
                        new KeyValuePair<int, string>(
                            reader.GetInt32(0),
                            reader.GetString(1)
                        ),
                        false
                    );
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (con != null && con.State == ConnectionState.Open)
                con.Close();
        }

        // Обработчик для кнопки "Добавить клиента"
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new AddClientForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        string sql = @"INSERT INTO clients (name, phone, address, email) 
                                     VALUES (@name, @phone, @address, @email)";

                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("name", form.ClientName);
                            cmd.Parameters.AddWithValue("phone", form.Phone);
                            cmd.Parameters.AddWithValue("address", form.Address);
                            cmd.Parameters.AddWithValue("email", form.Email);

                            cmd.ExecuteNonQuery();
                        }

                        LoadClients();
                        MessageBox.Show("Клиент успешно добавлен!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Обработчик для кнопки "Редактировать клиента"
        private void btnEditClient_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для редактирования!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int clientId = (int)dataGridViewClients.SelectedRows[0].Cells["ID"].Value;

                string sql = "SELECT * FROM clients WHERE client_id = @id";
                DataTable dt = new DataTable();

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("id", clientId);
                    new NpgsqlDataAdapter(cmd).Fill(dt);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Клиент не найден!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = dt.Rows[0];
                using (var form = new EditClientForm(
                    row["name"].ToString(),
                    row["phone"].ToString(),
                    row["address"].ToString(),
                    row["email"].ToString()))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        sql = @"UPDATE clients SET 
                              name = @name, 
                              phone = @phone, 
                              address = @address, 
                              email = @email 
                              WHERE client_id = @id";

                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("name", form.ClientName);
                            cmd.Parameters.AddWithValue("phone", form.Phone);
                            cmd.Parameters.AddWithValue("address", form.Address);
                            cmd.Parameters.AddWithValue("email", form.Email);
                            cmd.Parameters.AddWithValue("id", clientId);

                            cmd.ExecuteNonQuery();
                        }

                        LoadClients();
                        MessageBox.Show("Данные клиента обновлены!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании клиента: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Класс формы для редактирования клиента
        public class AddClientForm : Form
        {
            public string ClientName { get; protected set; }
            public string Phone { get; protected set; }
            public string Address { get; protected set; }
            public string Email { get; protected set; }
            protected TextBox txtName;
            protected TextBox txtPhone;
            protected TextBox txtAddress;
            protected TextBox txtEmail;

            public AddClientForm()
            {
                InitializeForm("Добавление нового клиента");
            }

            protected void InitializeForm(string title)
            {
                this.Text = title;
                this.Size = new Size(600, 300);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;

                // Создание элементов управления
                var lblName = new Label { Text = "ФИО:", Location = new Point(20, 20), Width = 100 };
                txtName = new TextBox { Location = new Point(120, 20), Width = 300 };

                var lblPhone = new Label { Text = "Телефон:", Location = new Point(20, 50), Width = 100 };
                txtPhone = new TextBox { Location = new Point(120, 50), Width = 300 };

                var lblAddress = new Label { Text = "Адрес:", Location = new Point(20, 80), Width = 100 };
                txtAddress = new TextBox { Location = new Point(120, 80), Width = 300 };

                var lblEmail = new Label { Text = "Email:", Location = new Point(20, 110), Width = 100 };
                txtEmail = new TextBox { Location = new Point(120, 110), Width = 300 };

                var btnOk = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(120, 150),
                    Size = new Size(100, 50)
                };
                var btnCancel = new Button
                {
                    Text = "Отмена",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(300, 150),
                    Size = new Size(100, 50)
                };
                btnOk.Click += (s, e) =>
                {
                    if (ValidateInput())
                    {
                        ClientName = txtName.Text;
                        Phone = txtPhone.Text;
                        Address = txtAddress.Text;
                        Email = txtEmail.Text;
                    }
          
                };

                this.Controls.AddRange(new Control[] {
            lblName, txtName,
            lblPhone, txtPhone,
            lblAddress, txtAddress,
            lblEmail, txtEmail,
            btnOk, btnCancel
        });
            }
            protected virtual bool ValidateInput()
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите ФИО клиента!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
        }

        public class EditClientForm : AddClientForm
        {
            public EditClientForm(string name, string phone, string address, string email)
            {
                this.Text = "Редактирование клиента";
                txtName.Text = name;
                txtPhone.Text = phone;
                txtAddress.Text = address;
                txtEmail.Text = email;
            }
        }
        // Обработчик для кнопки "Удалить клиента"
   
        private void btnDeleteClient_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для удаления!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Вы действительно хотите удалить этого клиента и все связанные данные?",
                                             "Подтверждение удаления",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                int clientId = (int)dataGridViewClients.SelectedRows[0].Cells["ID"].Value;

                string sql = "DELETE FROM clients WHERE client_id = @id";

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("id", clientId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        LoadClients();
                        MessageBox.Show("Клиент и все связанные данные успешно удалены!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Клиент не найден!", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении клиента: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public class ProductEditForm : Form
        {
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
           

            private TextBox txtName, txtCategory;
            private NumericUpDown numPrice;

            public ProductEditForm()
            {
                InitializeForm("Добавление товара");
            }

            public ProductEditForm(string name, decimal price, string category)
                : this()
            {
                this.Text = "Редактирование товара";
                txtName.Text = name;
                numPrice.Value = price;
                txtCategory.Text = category;
      
            }

            private void InitializeForm(string title)
            {
                this.Text = title;
                this.Size = new Size(450, 300);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;

                // Создание элементов управления
                var lblName = new Label { Text = "Название:", Location = new Point(20, 20), Width = 100 };
                txtName = new TextBox { Location = new Point(130, 20), Width = 280 };

                var lblPrice = new Label { Text = "Цена:", Location = new Point(20, 60), Width = 100 };
                numPrice = new NumericUpDown
                {
                    Location = new Point(130, 60),
                    Width = 120,
                    DecimalPlaces = 2,
                    Minimum = 0,
                    Maximum = 1000000
                };

                var lblCategory = new Label { Text = "Категория:", Location = new Point(20, 100), Width = 100 };
                txtCategory = new TextBox { Location = new Point(130, 100), Width = 280 };

              
       

                var btnOk = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(150, 160),
                    Size = new Size(80, 50)
                };
                var btnCancel = new Button
                {
                    Text = "Отмена",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(300, 160),
                    Size = new Size(100, 50)
                };

                btnOk.Click += (s, e) =>
                {
                    if (ValidateInput())
                    {
                        ProductName = txtName.Text;
                        Price = numPrice.Value;
                        Category = txtCategory.Text;
                       
                    }

                };

                this.Controls.AddRange(new Control[] {
            lblName, txtName,
            lblPrice, numPrice,
            lblCategory, txtCategory,
            
            btnOk, btnCancel
        });
            }

            private bool ValidateInput()
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название товара!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (numPrice.Value <= 0)
                {
                    MessageBox.Show("Цена должна быть больше нуля!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
        }
        // Обработчик добавления товара
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new ProductEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        string sql = @"INSERT INTO products (name, price, category) 
                             VALUES (@name, @price, @category)";

                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("name", form.ProductName);
                            cmd.Parameters.AddWithValue("price", form.Price);
                            cmd.Parameters.AddWithValue("category", form.Category);
                  

                            cmd.ExecuteNonQuery();
                        }

                        LoadProducts();
                        MessageBox.Show("Товар успешно добавлен!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик редактирования товара
        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар для редактирования!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int productId = (int)dataGridViewProducts.SelectedRows[0].Cells["ID"].Value;

                string sql = "SELECT * FROM products WHERE product_id = @id";
                DataTable dt = new DataTable();

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("id", productId);
                    new NpgsqlDataAdapter(cmd).Fill(dt);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Товар не найден!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = dt.Rows[0];
                using (var form = new ProductEditForm(
                    row["name"].ToString(),
                    Convert.ToDecimal(row["price"]),
                    row["category"].ToString()))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        sql = @"UPDATE products SET 
                      name = @name, 
                      price = @price, 
                      category = @category 
                      WHERE product_id = @id";

                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("name", form.ProductName);
                            cmd.Parameters.AddWithValue("price", form.Price);
                            cmd.Parameters.AddWithValue("category", form.Category);
                            cmd.Parameters.AddWithValue("id", productId);

                            cmd.ExecuteNonQuery();
                        }

                        LoadProducts();
                        MessageBox.Show("Данные товара обновлены!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании товара: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик удаления товара
        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар для удаления!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить этот товар и все связанные данные?",
                                             "Подтверждение удаления",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                int productId = (int)dataGridViewProducts.SelectedRows[0].Cells["ID"].Value;

                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        
                        string deleteOrderItemsSql = "DELETE FROM order_items WHERE product_id = @id";

                        
                        string deleteWarehouseSql = "DELETE FROM warehouse WHERE product_id = @id";

                        
                        string deleteProductSql = "DELETE FROM products WHERE product_id = @id";

                        using (var cmd = new NpgsqlCommand(deleteOrderItemsSql, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("id", productId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new NpgsqlCommand(deleteWarehouseSql, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("id", productId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new NpgsqlCommand(deleteProductSql, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("id", productId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Товар не найден!", "Ошибка",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        transaction.Commit();
                        LoadProducts();
                        MessageBox.Show("Товар и все связанные данные успешно удалены!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public class OrderEditForm : Form
        {
            public int? ClientId { get; private set; }
            public DateTime OrderDate { get; private set; }
            public string Status { get; private set; }
            public decimal TotalAmount { get; private set; }
            public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

            private ComboBox cmbClients;
            private DateTimePicker dtpOrderDate;
            private ComboBox cmbStatus;
            private DataGridView dgvOrderItems;
            private Button btnAddItem, btnRemoveItem;
            private NumericUpDown numQuantity;
            private ComboBox cmbProducts;
            private Label lblTotal;

            public class OrderItem
            {
                public int ProductId { get; set; }
                public string ProductName { get; set; }
                public decimal Price { get; set; }
                public int Quantity { get; set; }
                public decimal Total => Price * Quantity;
            }

            public OrderEditForm(NpgsqlConnection connection, int? orderId = null)
            {
                InitializeForm(connection, orderId);
            }

            private void InitializeForm(NpgsqlConnection connection, int? orderId)
            {
                this.Text = orderId.HasValue ? "Редактирование заказа" : "Новый заказ";
                this.Size = new Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;

                // Основные элементы формы
                var lblClient = new Label { Text = "Клиент:", Location = new Point(20, 20), Width = 100 };
                cmbClients = new ComboBox { Location = new Point(130, 20), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

                var lblDate = new Label { Text = "Дата заказа:", Location = new Point(20, 60), Width = 100 };
                dtpOrderDate = new DateTimePicker { Location = new Point(130, 60), Width = 150, Format = DateTimePickerFormat.Short };

                var lblStatus = new Label { Text = "Статус:", Location = new Point(20, 100), Width = 100 };
                cmbStatus = new ComboBox { Location = new Point(130, 100), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
                cmbStatus.Items.AddRange(new[] { "new", "processing", "paid", "delivered", "canceled" });

                dgvOrderItems = new DataGridView
                {
                    Location = new Point(20, 140),
                    Width = 740,
                    Height = 300,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect
                };
                dgvOrderItems.Columns.Add("ProductName", "Товар");
                dgvOrderItems.Columns.Add("Price", "Цена");
                dgvOrderItems.Columns.Add("Quantity", "Количество");
                dgvOrderItems.Columns.Add("Total", "Сумма");

                
                var lblProduct = new Label { Text = "Товар:", Location = new Point(20, 450), Width = 100 };
                cmbProducts = new ComboBox { Location = new Point(140, 450), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

                var lblQuantity = new Label { Text = "Количество:", Location = new Point(20, 490), Width = 120 };
                numQuantity = new NumericUpDown { Location = new Point(140, 490), Width = 100, Minimum = 1, Maximum = 1000 };

                btnAddItem = new Button { Text = "Добавить", Location = new Point(250, 490), Width = 100, Height = 40 };
                btnRemoveItem = new Button { Text = "Удалить", Location = new Point(340, 490), Width = 100, Height = 40 };

                lblTotal = new Label { Text = "Итого: 0.00", Location = new Point(400, 450), Width = 200, TextAlign = ContentAlignment.MiddleRight };

                var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(500, 490), Width = 80, Height = 40 };
                var btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Location = new Point(590, 490), Width = 100, Height = 40 };

                LoadClients(connection);
                LoadProducts(connection);

                if (orderId.HasValue)
                {
                    LoadOrderData(connection, orderId.Value);
                }
                else
                {
                    dtpOrderDate.Value = DateTime.Now;
                    cmbStatus.SelectedItem = "new";
                }

                btnAddItem.Click += (s, e) => AddOrderItem();
                btnRemoveItem.Click += (s, e) => RemoveOrderItem();
                btnOk.Click += (s, e) => SaveOrder();

                this.Controls.AddRange(new Control[] {
            lblClient, cmbClients,
            lblDate, dtpOrderDate,
            lblStatus, cmbStatus,
            dgvOrderItems,
            lblProduct, cmbProducts,
            lblQuantity, numQuantity,
            btnAddItem, btnRemoveItem,
            lblTotal,
            btnOk, btnCancel
        });
            }

            private void LoadClients(NpgsqlConnection connection)
            {
                string sql = "SELECT client_id, name FROM clients ORDER BY name";
                var dt = new DataTable();
                new NpgsqlDataAdapter(sql, connection).Fill(dt);

                cmbClients.DisplayMember = "name";
                cmbClients.ValueMember = "client_id";
                cmbClients.DataSource = dt;
            }

            private void LoadProducts(NpgsqlConnection connection)
            {
                string sql = "SELECT product_id, name, price FROM products ORDER BY name";
                var dt = new DataTable();
                new NpgsqlDataAdapter(sql, connection).Fill(dt);

                cmbProducts.DisplayMember = "name";
                cmbProducts.ValueMember = "product_id";
                cmbProducts.DataSource = dt;
            }

            private void LoadOrderData(NpgsqlConnection connection, int orderId)
            {
                string sql = "SELECT client_id, order_date, status FROM orders WHERE order_id = @id";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("id", orderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ClientId = reader.GetInt32(0);
                            cmbClients.SelectedValue = ClientId;
                            dtpOrderDate.Value = reader.GetDateTime(1);
                            cmbStatus.SelectedItem = reader.GetString(2);
                        }
                    }
                }

                sql = @"SELECT oi.product_id, p.name, oi.price, oi.quantity 
               FROM order_items oi
               JOIN products p ON oi.product_id = p.product_id
               WHERE oi.order_id = @id";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("id", orderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new OrderItem
                            {
                                ProductId = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Quantity = reader.GetInt32(3)
                            };
                            OrderItems.Add(item);
                            dgvOrderItems.Rows.Add(item.ProductName, item.Price, item.Quantity, item.Total);
                        }
                    }
                }
                UpdateTotal();
            }

            private void AddOrderItem()
            {
                if (cmbProducts.SelectedItem == null) return;

                var productRow = (cmbProducts.SelectedItem as DataRowView)?.Row as DataRow;
                if (productRow == null) return;

                var item = new OrderItem
                {
                    ProductId = Convert.ToInt32(productRow["product_id"]),
                    ProductName = productRow["name"].ToString(),
                    Price = Convert.ToDecimal(productRow["price"]),
                    Quantity = (int)numQuantity.Value
                };

                OrderItems.Add(item);
                dgvOrderItems.Rows.Add(item.ProductName, item.Price, item.Quantity, item.Total);
                UpdateTotal();
            }

            private void RemoveOrderItem()
            {
                if (dgvOrderItems.SelectedRows.Count == 0) return;

                int index = dgvOrderItems.SelectedRows[0].Index;
                OrderItems.RemoveAt(index);
                dgvOrderItems.Rows.RemoveAt(index);
                UpdateTotal();
            }

            private void UpdateTotal()
            {
                decimal total = OrderItems.Sum(i => i.Total);
                lblTotal.Text = $"Итого: {total:N2}";
                TotalAmount = total;
            }

            private void SaveOrder()
            {
                if (cmbClients.SelectedItem == null)
                {
                    MessageBox.Show("Выберите клиента!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (OrderItems.Count == 0)
                {
                    MessageBox.Show("Добавьте хотя бы один товар в заказ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClientId = (int)cmbClients.SelectedValue;
                OrderDate = dtpOrderDate.Value;
                Status = cmbStatus.SelectedItem.ToString();
            }
        }
        // Обработчик кнопки "Новый заказ"
        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new OrderEditForm(con))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        using (var transaction = con.BeginTransaction())
                        {
                            try
                            {
                                
                                string sql = @"INSERT INTO orders (client_id, order_date, status, total_amount)
                                     VALUES (@clientId, @orderDate, @status, @totalAmount)
                                     RETURNING order_id";

                                int orderId;
                                using (var cmd = new NpgsqlCommand(sql, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("clientId", form.ClientId.Value);
                                    cmd.Parameters.AddWithValue("orderDate", form.OrderDate);
                                    cmd.Parameters.AddWithValue("status", form.Status);
                                    cmd.Parameters.AddWithValue("totalAmount", form.TotalAmount);

                                    orderId = Convert.ToInt32(cmd.ExecuteScalar());
                                }

                               
                                sql = @"INSERT INTO order_items (order_id, product_id, quantity, price)
                               VALUES (@orderId, @productId, @quantity, @price)";

                                foreach (var item in form.OrderItems)
                                {
                                    using (var cmd = new NpgsqlCommand(sql, con, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("orderId", orderId);
                                        cmd.Parameters.AddWithValue("productId", item.ProductId);
                                        cmd.Parameters.AddWithValue("quantity", item.Quantity);
                                        cmd.Parameters.AddWithValue("price", item.Price);

                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                transaction.Commit();
                                LoadOrders();
                                MessageBox.Show("Заказ успешно создан!", "Успех",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeliverOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заказ для доставки!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int orderId = (int)dataGridViewOrders.SelectedRows[0].Cells["ID"].Value;
                string currentStatus = dataGridViewOrders.SelectedRows[0].Cells["Статус"].Value.ToString();

                if (currentStatus != "paid")
                {
                    MessageBox.Show("Можно доставить только оплаченные заказы!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show("Подтвердите доставку заказа?",
                                                 "Подтверждение доставки",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                // Обновляем статус заказа
                string updateOrderSql = "UPDATE orders SET status = 'delivered' WHERE order_id = @id";

                // Добавляем запись о доставке
                string insertDeliverySql = @"INSERT INTO deliveries (order_id, delivery_date, status)
                                   VALUES (@orderId, @deliveryDate, 'delivered')";

                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new NpgsqlCommand(updateOrderSql, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("id", orderId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new NpgsqlCommand(insertDeliverySql, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("orderId", orderId);
                            cmd.Parameters.AddWithValue("deliveryDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        LoadOrders();
                        MessageBox.Show("Заказ успешно доставлен!", "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при доставке заказа: {ex.Message}", "Ошибка",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Обработчик кнопки "Просмотр заказа"
        private void btnViewOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заказ для редактирования!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int orderId = (int)dataGridViewOrders.SelectedRows[0].Cells["ID"].Value;
                string currentStatus = dataGridViewOrders.SelectedRows[0].Cells["Статус"].Value.ToString();

                if (currentStatus == "delivered" || currentStatus == "paid")
                {
                    MessageBox.Show("Нельзя редактировать доставленные или оплаченные заказы!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var form = new OrderEditForm(con, orderId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        using (var transaction = con.BeginTransaction())
                        {
                            try
                            {
                                
                                string updateOrderSql = @"UPDATE orders SET 
                                               client_id = @clientId,
                                               order_date = @orderDate,
                                               status = @status,
                                               total_amount = @totalAmount
                                               WHERE order_id = @orderId";

                                using (var cmd = new NpgsqlCommand(updateOrderSql, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@clientId", form.ClientId.Value);
                                    cmd.Parameters.AddWithValue("@orderDate", form.OrderDate);
                                    cmd.Parameters.AddWithValue("@status", form.Status);
                                    cmd.Parameters.AddWithValue("@totalAmount", form.TotalAmount);
                                    cmd.Parameters.AddWithValue("@orderId", orderId);

                                    cmd.ExecuteNonQuery();
                                }

                         
                                string deleteItemsSql = "DELETE FROM order_items WHERE order_id = @orderId";
                                using (var cmd = new NpgsqlCommand(deleteItemsSql, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("orderId", orderId);
                                    cmd.ExecuteNonQuery();
                                }

                           
                                string insertItemsSql = @"INSERT INTO order_items 
                                                (order_id, product_id, quantity, price)
                                                VALUES (@orderId, @productId, @quantity, @price)";

                                foreach (var item in form.OrderItems)
                                {
                                    using (var cmd = new NpgsqlCommand(insertItemsSql, con, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("orderId", orderId);
                                        cmd.Parameters.AddWithValue("productId", item.ProductId);
                                        cmd.Parameters.AddWithValue("quantity", item.Quantity);
                                        cmd.Parameters.AddWithValue("price", item.Price);

                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                transaction.Commit();
                                LoadOrders();
                                MessageBox.Show("Заказ успешно обновлен!", "Успех",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"Ошибка при обновлении заказа: {ex.Message}", "Ошибка",
                                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании заказа: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик кнопки "Оплатить заказ"
        private void btnPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заказ для оплаты!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int orderId = (int)dataGridViewOrders.SelectedRows[0].Cells["ID"].Value;
                string currentStatus = dataGridViewOrders.SelectedRows[0].Cells["Статус"].Value.ToString();

                if (currentStatus == "paid")
                {
                    MessageBox.Show("Этот заказ уже оплачен!", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirmResult = MessageBox.Show("Вы уверены, что хотите отметить заказ как оплаченный?",
                                                 "Подтверждение оплаты",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                string sql = "UPDATE orders SET status = 'paid' WHERE order_id = @id";
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("id", orderId);
                    cmd.ExecuteNonQuery();
                }

                LoadOrders();
                MessageBox.Show("Заказ успешно оплачен!", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оплате заказа: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик кнопки "Экспорт в Excel"
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заказ для экспорта!", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int orderId = (int)dataGridViewOrders.SelectedRows[0].Cells["ID"].Value;

              
                string sql = @"SELECT o.order_id, c.name as client_name, o.order_date, o.status, o.total_amount,
                     p.name as product_name, oi.quantity, oi.price, (oi.quantity * oi.price) as item_total
                     FROM orders o
                     JOIN clients c ON o.client_id = c.client_id
                     JOIN order_items oi ON o.order_id = oi.order_id
                     JOIN products p ON oi.product_id = p.product_id
                     WHERE o.order_id = @id";

                DataTable dt = new DataTable();
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("id", orderId);
                    new NpgsqlDataAdapter(cmd).Fill(dt);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Заказ не найден!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Заказ_{orderId}_{DateTime.Now:yyyyMMdd}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Заказ");

                       
                        worksheet.Cell(1, 1).Value = "Номер заказа:";
                        worksheet.Cell(1, 2).Value = dt.Rows[0]["order_id"].ToString();
                        worksheet.Cell(2, 1).Value = "Клиент:";
                        worksheet.Cell(2, 2).Value = dt.Rows[0]["client_name"].ToString();
                        worksheet.Cell(3, 1).Value = "Дата заказа:";
                        worksheet.Cell(3, 2).Value = Convert.ToDateTime(dt.Rows[0]["order_date"]).ToString("dd.MM.yyyy");
                        worksheet.Cell(4, 1).Value = "Статус:";
                        worksheet.Cell(4, 2).Value = dt.Rows[0]["status"].ToString();
                        worksheet.Cell(5, 1).Value = "Общая сумма:";
                        worksheet.Cell(5, 2).Value = Convert.ToDecimal(dt.Rows[0]["total_amount"]).ToString("N2");

                        
                        worksheet.Cell(7, 1).Value = "Товары в заказе:";
                        worksheet.Cell(8, 1).Value = "Товар";
                        worksheet.Cell(8, 2).Value = "Количество";
                        worksheet.Cell(8, 3).Value = "Цена";
                        worksheet.Cell(8, 4).Value = "Сумма";

                        int row = 9;
                        foreach (DataRow dr in dt.Rows)
                        {
                            worksheet.Cell(row, 1).Value = dr["product_name"].ToString();
                            worksheet.Cell(row, 2).Value = Convert.ToInt32(dr["quantity"]);
                            worksheet.Cell(row, 3).Value = Convert.ToDecimal(dr["price"]).ToString("N2");
                            worksheet.Cell(row, 4).Value = Convert.ToDecimal(dr["item_total"]).ToString("N2");
                            row++;
                        }

                     
                        var headerRange = worksheet.Range("A8:D8");
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                        var totalRange = worksheet.Range($"A5:B5");
                        totalRange.Style.Font.Bold = true;

                     
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show($"Файл сохранен: {saveDialog.FileName}", "Экспорт завершен",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте заказа: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportToExcel()
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Files|*.xlsx";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Отчет");
                            // Заголовки столбцов
                            for (int j = 0; j < dataGridViewReport.Columns.Count; j++)
                            {
                                worksheet.Cell(1, j + 1).Value = dataGridViewReport.Columns[j].HeaderText;
                            }
                            // Данные
                            for (int i = 0; i < dataGridViewReport.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridViewReport.Columns.Count; j++)
                                {
                                    worksheet.Cell(i + 2, j + 1).Value =
                                        dataGridViewReport.Rows[i].Cells[j].Value?.ToString();
                                }
                            }
                            workbook.SaveAs(saveDialog.FileName);
                            MessageBox.Show("Отчет сохранен в Excel!", "Успех",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                LoadReportData(); 
                ExportToExcel();  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeliveryReport_Click(object sender, EventArgs e)
        {
            try
            {
                
                List<int> selectedProductIds = new List<int>();
                foreach (var item in checkedListBoxProducts.CheckedItems)
                {
                    var product = (KeyValuePair<int, string>)item;
                    selectedProductIds.Add(product.Key);
                }

                if (selectedProductIds.Count == 0)
                {
                    MessageBox.Show("Выберите товары для отчёта!", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = $@"
            SELECT 
                p.product_id AS ""ID товара"",
                p.name AS ""Товар"",
                SUM(CASE WHEN o.status = 'delivered' THEN oi.quantity * oi.price ELSE 0 END) AS ""Доставленная сумма"",
                SUM(oi.quantity * oi.price) AS ""Заказанная сумма"",
                ROUND(
                    (SUM(CASE WHEN o.status = 'delivered' THEN oi.quantity * oi.price ELSE 0 END) * 100.0) 
                    / NULLIF(SUM(oi.quantity * oi.price), 0), 
                    2
                ) AS ""Процент доставки (%)""
            FROM orders o
            JOIN order_items oi ON o.order_id = oi.order_id
            JOIN products p ON oi.product_id = p.product_id
            WHERE 
                o.order_date BETWEEN @startDate AND @endDate
                AND p.product_id IN ({string.Join(",", selectedProductIds)})
            GROUP BY p.product_id, p.name
            ORDER BY p.name";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@startDate", dateTimePickerStart.Value.Date);
                cmd.Parameters.AddWithValue("@endDate", dateTimePickerEnd.Value.Date);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridViewReport2.DataSource = dt;
                dataGridViewReport2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения.", "Информация",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public class WarehouseForm : Form
        {
            public int? SupplierId { get; private set; }
            public DateTime InvoiceDate { get; private set; }
            public List<WarehouseItem> Items { get; private set; } = new List<WarehouseItem>();

            private ComboBox cmbSuppliers;
            private DateTimePicker dtpInvoiceDate;
            private DataGridView dgvItems;
            private ComboBox cmbProducts;
            private NumericUpDown numQuantity;
            private NumericUpDown numPrice;
            private Button btnAddItem, btnRemoveItem;

            public class WarehouseItem
            {
                public int ProductId { get; set; }
                public string ProductName { get; set; }
                public int Quantity { get; set; }
                public decimal Price { get; set; }
            }

            public WarehouseForm(NpgsqlConnection connection)
            {
                InitializeForm(connection);
            }

            private void InitializeForm(NpgsqlConnection connection)
            {
                this.Text = "Новая накладная";
                this.Size = new Size(700, 540);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;

                // Основные элементы
                var lblSupplier = new Label { Text = "Поставщик:", Location = new Point(20, 20), Width = 100 };
                cmbSuppliers = new ComboBox { Location = new Point(130, 20), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

                var lblDate = new Label { Text = "Дата накладной:", Location = new Point(20, 60), Width = 100 };
                dtpInvoiceDate = new DateTimePicker { Location = new Point(130, 60), Width = 150, Format = DateTimePickerFormat.Short };

                dgvItems = new DataGridView
                {
                    Location = new Point(20, 100),
                    Width = 650,
                    Height = 250,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect
                };
                dgvItems.Columns.Add("ProductName", "Товар");
                dgvItems.Columns.Add("Quantity", "Количество");
                dgvItems.Columns.Add("Price", "Цена за единицу");
                dgvItems.Columns.Add("Total", "Общая стоимость");

                var lblProduct = new Label { Text = "Товар:", Location = new Point(20, 360), Width = 100 };
                cmbProducts = new ComboBox { Location = new Point(130, 360), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

                var lblQuantity = new Label { Text = "Количество:", Location = new Point(20, 400), Width = 110 };
                numQuantity = new NumericUpDown { Location = new Point(130, 400), Width = 100, Minimum = 1, Maximum = 10000 };

                var lblPrice = new Label { Text = "Цена:", Location = new Point(250, 400), Width = 80 };
                numPrice = new NumericUpDown
                {
                    Location = new Point(330, 400),
                    Width = 100,
                    Minimum = 0,
                    Maximum = 1000000,
                    DecimalPlaces = 2
                };

                btnAddItem = new Button { Text = "Добавить", Location = new Point(480, 360), Width = 130, Height = 40 };
                btnRemoveItem = new Button { Text = "Удалить", Location = new Point(480, 400), Width = 130, Height = 40 };

                var btnOk = new Button { Text = "Сохранить", DialogResult = DialogResult.OK, Location = new Point(25, 440), Width = 120, Height = 40};
     

                LoadSuppliers(connection);
                LoadProducts(connection);
                dtpInvoiceDate.Value = DateTime.Now;

                btnAddItem.Click += (s, e) => AddItem();
                btnRemoveItem.Click += (s, e) => RemoveItem();
                btnOk.Click += (s, e) => SaveForm();

                this.Controls.AddRange(new Control[] {
            lblSupplier, cmbSuppliers,
            lblDate, dtpInvoiceDate,
            dgvItems,
            lblProduct, cmbProducts,
            lblQuantity, numQuantity,
            lblPrice, numPrice,
            btnAddItem, btnRemoveItem,
            btnOk
        });
            }

            private void LoadSuppliers(NpgsqlConnection connection)
            {
                string sql = "SELECT supplier_id, name FROM suppliers ORDER BY name";
                var dt = new DataTable();
                new NpgsqlDataAdapter(sql, connection).Fill(dt);

                cmbSuppliers.DisplayMember = "name";
                cmbSuppliers.ValueMember = "supplier_id";
                cmbSuppliers.DataSource = dt;
            }

            private void LoadProducts(NpgsqlConnection connection)
            {
                string sql = "SELECT product_id, name FROM products ORDER BY name";
                var dt = new DataTable();
                new NpgsqlDataAdapter(sql, connection).Fill(dt);

                cmbProducts.DisplayMember = "name";
                cmbProducts.ValueMember = "product_id";
                cmbProducts.DataSource = dt;
            }

            private void AddItem()
            {
                if (cmbProducts.SelectedItem == null) return;

                var productRow = (cmbProducts.SelectedItem as DataRowView)?.Row as DataRow;
                if (productRow == null) return;

                var item = new WarehouseItem
                {
                    ProductId = Convert.ToInt32(productRow["product_id"]),
                    ProductName = productRow["name"].ToString(),
                    Quantity = (int)numQuantity.Value,
                    Price = numPrice.Value
                };

                Items.Add(item);
                dgvItems.Rows.Add(item.ProductName, item.Quantity, item.Price.ToString("N2"), (item.Quantity * item.Price).ToString("N2"));
            }

            private void RemoveItem()
            {
                if (dgvItems.SelectedRows.Count == 0) return;
                int index = dgvItems.SelectedRows[0].Index;
                Items.RemoveAt(index);
                dgvItems.Rows.RemoveAt(index);
            }

            private void SaveForm()
            {
                if (cmbSuppliers.SelectedItem == null)
                {
                    MessageBox.Show("Выберите поставщика!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Items.Count == 0)
                {
                    MessageBox.Show("Добавьте хотя бы один товар!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SupplierId = (int)cmbSuppliers.SelectedValue;
                InvoiceDate = dtpInvoiceDate.Value;
            }
        }

        // Обработчик кнопки "Новая накладная"
        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new WarehouseForm(con))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        using (var transaction = con.BeginTransaction())
                        {
                            try
                            {
                                
                                string invoiceSql = @"INSERT INTO invoices 
                                            (supplier_id, invoice_date, total_amount)
                                            VALUES (@supplierId, @invoiceDate, @totalAmount)
                                            RETURNING invoice_id";

                                decimal totalAmount = form.Items.Sum(i => i.Quantity * i.Price);
                                int invoiceId;

                                using (var cmd = new NpgsqlCommand(invoiceSql, con, transaction))
                                {
                                    cmd.Parameters.AddWithValue("supplierId", form.SupplierId.Value);
                                    cmd.Parameters.AddWithValue("invoiceDate", form.InvoiceDate);
                                    cmd.Parameters.AddWithValue("totalAmount", totalAmount);

                                    invoiceId = Convert.ToInt32(cmd.ExecuteScalar());
                                }

                                string itemsSql = @"INSERT INTO invoice_items 
                                          (invoice_id, product_id, quantity, price)
                                          VALUES (@invoiceId, @productId, @quantity, @price)";

                                string warehouseSql = @"INSERT INTO warehouse 
                                              (product_id, quantity, last_restock_date)
                                              VALUES (@productId, @quantity, @restockDate)
                                              ON CONFLICT (product_id) 
                                              DO UPDATE SET 
                                                  quantity = warehouse.quantity + @quantity,
                                                  last_restock_date = @restockDate";

                                foreach (var item in form.Items)
                                {
                                    
                                    using (var cmd = new NpgsqlCommand(itemsSql, con, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("invoiceId", invoiceId);
                                        cmd.Parameters.AddWithValue("productId", item.ProductId);
                                        cmd.Parameters.AddWithValue("quantity", item.Quantity);
                                        cmd.Parameters.AddWithValue("price", item.Price);

                                        cmd.ExecuteNonQuery();
                                    }

                                  
                                    using (var cmd = new NpgsqlCommand(warehouseSql, con, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("productId", item.ProductId);
                                        cmd.Parameters.AddWithValue("quantity", item.Quantity);
                                        cmd.Parameters.AddWithValue("restockDate", form.InvoiceDate);

                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                transaction.Commit();
                                LoadWarehouse();
                                MessageBox.Show("Накладная успешно создана!", "Успех",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"Ошибка при создании накладной: {ex.Message}", "Ошибка",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик кнопки "Списать товары"
        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (dataGridViewWarehouse.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар для списания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int warehouseId = (int)dataGridViewWarehouse.SelectedRows[0].Cells["ID"].Value;
                int productId = Convert.ToInt32(dataGridViewWarehouse.SelectedRows[0].Cells["product_id"].Value); // Исправлено
                string productName = dataGridViewWarehouse.SelectedRows[0].Cells["Товар"].Value.ToString();
                int currentQuantity = Convert.ToInt32(dataGridViewWarehouse.SelectedRows[0].Cells["Количество"].Value);

                using (var form = new Form())
                {
                    form.Text = "Списание товара";
                    form.Size = new Size(300, 180);
                    form.FormBorderStyle = FormBorderStyle.FixedDialog;
                    form.MaximizeBox = false;
                    form.MinimizeBox = false;
                    form.StartPosition = FormStartPosition.CenterParent;

                    var lblProduct = new Label { Text = $"Товар: {productName}", Location = new Point(20, 20), Width = 250 };
                    var lblCurrent = new Label { Text = $"Доступно: {currentQuantity}", Location = new Point(20, 50), Width = 100 };
                    var lblQuantity = new Label { Text = "Количество для списания:", Location = new Point(20, 80), Width = 150 };

                    var numQuantity = new NumericUpDown
                    {
                        Location = new Point(180, 80),
                        Width = 80,
                        Minimum = 1,
                        Maximum = currentQuantity
                    };

                    var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(80, 120), Width = 80 };
                    var btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Location = new Point(180, 120), Width = 80 };

                    form.Controls.AddRange(new Control[] {
                lblProduct, lblCurrent, lblQuantity,
                numQuantity, btnOk, btnCancel
            });

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        int quantityToWriteOff = (int)numQuantity.Value;

                        string sql = @"UPDATE warehouse SET 
                              quantity = quantity - @quantity
                              WHERE warehouse_id = @id";

                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("quantity", quantityToWriteOff);
                            cmd.Parameters.AddWithValue("id", warehouseId);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                               
                                


                                LoadWarehouse();
                                MessageBox.Show("Товар успешно списан!", "Успех",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Не удалось списать товар!", "Ошибка",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при списании товара: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик кнопки "Обновить остатки"
        private void btnRefreshWarehouse_Click(object sender, EventArgs e)
        {
            LoadWarehouse();
        }


    }
}