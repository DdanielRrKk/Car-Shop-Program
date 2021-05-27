using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZadanieDeset
{
    public partial class Form1 : Form
    {
        /*
        140 Пежо306 1986 2380 4812296560 Георги Китов 1 01.7.2012
        157 Опел Рекорд 1991 2630 4812296560 Георги Китов - -
        161 Мицубиши 1994 6860 6001174825 Петър Григоров 3 03.08.2012
        213 Нисан джип 1994 12600 4812296560 Георги Китов 2 01.08.2012
        222 Лада Самара 1990 700 3808143562 Калин Томов - -
        235 Шкода 1985 500 3808143562 Калин Томов 5 04.08.2012
        236 Део Тико 1992 800 5106066843 Иван Петров - -
        361 Мерцедес 1980 1900 4210072993 Веселина Гюрова - -
        362 Ауди 1990 2100 4210072993 Веселина Гюрова 4 03.08.2012
        374 Ситроен 1983 1200 6001174825 Петър Григоров - -
         */

        private string GetSQLCOnnection()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Daniel\source\repos\ZadanieDeset\ZadanieDeset\DB.mdf;Integrated Security=True";
        }

        private void DatabaseData()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);
                dgv.DataSource = new BindingSource(table, null);
            }
        }

        // tochka 1
        private void CenaOver1000()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi WHERE Cena > 1000 ";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);
                dgv.DataSource = new BindingSource(table, null);
            }
        }

        // tochka 2
        private void NomerMarkaEGN()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"SELECT Reg_nomer, Cena, EGN FROM Razpisi";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);
                dgv.DataSource = new BindingSource(table, null);
            }
        }

        // tochka 3 - 1
        private void SortMarka()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                DataTable table = new DataTable();

                var source = dgv.DataSource;
                while (source is BindingSource)
                    source = ((BindingSource)source).DataSource;

                if (source is DataTable)
                    table = (DataTable)source;

                if (!table.Columns.Contains("Marka"))
                {
                    MessageBox.Show($"Таблицата не съдържа такава колона !", "Внимание!", MessageBoxButtons.OK);
                    return;
                }

                DataView dv = table.DefaultView;
                dv.Sort = "Marka asc";

                dgv.DataSource = new BindingSource(dv.ToTable(), null);
            }
        }

        // tochka 3 - 2
        private void SortDataProdajba()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                DataTable table = new DataTable();

                var source = dgv.DataSource;
                while (source is BindingSource)
                    source = ((BindingSource)source).DataSource;

                if (source is DataTable)
                    table = (DataTable)source;

                if (!table.Columns.Contains("Data_prodajba"))
                {
                    MessageBox.Show($"Таблицата не съдържа такава колона !", "Внимание!", MessageBoxButtons.OK);
                    return;
                }

                DataView dv = table.DefaultView;
                dv.Sort = "Data_prodajba asc";

                dgv.DataSource = new BindingSource(dv.ToTable(), null);
            }
        }

        // tochka 4
        private void FindEGN()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi WHERE EGN = '4812296560'";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);

                dgv.DataSource = new BindingSource(table, null);
            }
        }

        // tochka 5
        private void SpravkaZaMarkaGodina()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                //string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi WHERE Godina < 1990 AND Marka LIKE 'М%'";
                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi WHERE Godina < 1990";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);

                for (int i = 0; i < table.Rows.Count; i++)
                    if (!table.Rows[i][1].ToString().StartsWith("М"))
                    {
                        table.Rows.RemoveAt(i);
                        i--;
                    }

                dgv.DataSource = new BindingSource(table, null);
            }
        }

        // tochka 6
        private void BroiNeprodadeni()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi WHERE Godina < 1990 AND Data_prodajba IS NULL";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);
                MessageBox.Show($"Броя на автомобилите е {table.Rows.Count} !", "Внимание!", MessageBoxButtons.OK);

                dgv.DataSource = new BindingSource(table, null);
            }
        }

        // tochka 7
        private void Komisionna()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query1 = @"UPDATE Razpisi SET Komisionna = Cena + (Cena * 0.1)";
                SqlCommand cmd1 = new SqlCommand(query1, connection);
                connection.Open();
                cmd1.ExecuteNonQuery();
                connection.Close();

                string query = @"SELECT * FROM Razpisi";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);

                int kom = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                    kom += Convert.ToInt32(table.Rows[i][8].ToString());
                MessageBox.Show($"Печалбата от комисионната при продаване на всички автомобили ще бъде {kom} !", "Внимание!", MessageBoxButtons.OK);

                dgv.DataSource = new BindingSource(table, null);
            }
        }

        // tochka 8
        private void Pechalba()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi WHERE Data_prodajba IS NOT NULL";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);

                int pechalba = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                    pechalba += Convert.ToInt32(table.Rows[i][3].ToString());
                MessageBox.Show($"Печалбата от продажбите на всички автомобили е {pechalba} !", "Внимание!", MessageBoxButtons.OK);

                DataView dv = table.DefaultView;
                dv.Sort = "EGN desc";

                dgv.DataSource = new BindingSource(dv.ToTable(), null);
            }
        }

        // tochka 9
        private void Dobavi()
        {
            //==========PROVERKI
            error.Text = string.Empty;
            ProveriPrazni();
            if (error.Text != string.Empty)
                return;
            ProveriPovtarqshtiseNomera();
            ProveriDanni();
            if (error.Text != string.Empty)
                return;
            //==========PROVERKI
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"INSERT INTO Razpisi (Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba) VALUES ('"+reg_nomer.Text+"', N'"+marka.Text+"', '"+godina.Text+"', '"+cena.Text+"', '"+egn.Text+"', N'"+imeFamiliq.Text+"', '"+prod_nomer.Text+"', '"+data_prod.Text+"')";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                this.populateList(1);
            }

            reg_nomer.Text = "";
            marka.Text = "";
            godina.Text = "";
            cena.Text = "";
            egn.Text = "";
            imeFamiliq.Text = "";
            prod_nomer.Text = "";
            data_prod.Text = "";
        }

        private void DeleteDatabaseData(int reg_num)
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query1 = @$"DELETE FROM Razpisi WHERE Reg_nomer = {reg_num}";
                SqlCommand cmd1 = new SqlCommand(query1, connection);
                connection.Open();
                cmd1.ExecuteNonQuery();
                connection.Close();

                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);
                dgv.DataSource = new BindingSource(table, null);
            }
        }

        private void SaveDatabaseData(int reg_num)
        {
            //==========PROVERKI
            error.Text = string.Empty;
            ProveriPrazni();
            if (error.Text != string.Empty)
                return;
            ProveriPovtarqshtiseNomera();
            ProveriDanni();
            if (error.Text != string.Empty)
                return;
            //==========PROVERKI
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query1 = @$"UPDATE Razpisi SET Marka = N'{marka.Text}', Godina = '{int.Parse(godina.Text)}', Cena = '{int.Parse(cena.Text)}', EGN = '{egn.Text}', Ime_Familiq = N'{imeFamiliq.Text}', Prod_nomer = '{int.Parse(prod_nomer.Text)}', Data_prodajba = '{Convert.ToDateTime(data_prod.Text)}' WHERE Reg_nomer = {reg_num}";
                SqlCommand cmd1 = new SqlCommand(query1, connection);
                connection.Open();
                cmd1.ExecuteNonQuery();
                connection.Close();

                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);
                dgv.DataSource = new BindingSource(table, null);
            }

            reg_nomer.ReadOnly = false;
            reg_nomer.Text = "";
            marka.Text = "";
            godina.Text = "";
            cena.Text = "";
            egn.Text = "";
            imeFamiliq.Text = "";
            prod_nomer.Text = "";
            data_prod.Text = "";
        }

        private void FindInDatabaseData()
        {
            using (SqlConnection connection = new SqlConnection(GetSQLCOnnection()))
            {
                string query = @"SELECT Reg_nomer, Marka, Godina, Cena, EGN, Ime_Familiq, Prod_nomer, Data_prodajba FROM Razpisi WHERE";
                bool check = false;

                if (reg_nomer.Text != "")
                {
                    query += $" Reg_nomer = '{reg_nomer.Text}'";
                    check = true;
                }

                if (marka.Text != "" && check == false)
                {
                    query += $" Marka = '{marka.Text}'";
                    check = true;
                }
                else if(marka.Text != "" && check == true)
                {
                    query += $" AND Marka = '{marka.Text}'";
                    check = true;
                }

                if (godina.Text != "" && check == false)
                {
                    query += $" Godina = '{godina.Text}'";
                    check = true;
                }
                else if (godina.Text != "" && check == true)
                {
                    query += $" AND Godina = '{godina.Text}'";
                    check = true;
                }

                if (cena.Text != "" && check == false)
                {
                    query += $" Cena = '{cena.Text}'";
                    check = true;
                }
                else if (cena.Text != "" && check == true)
                {
                    query += $" AND Cena = '{cena.Text}'";
                    check = true;
                }

                if (egn.Text != "" && check == false)
                {
                    query += $" EGN = '{egn.Text}'";
                    check = true;
                }
                else if (egn.Text != "" && check == true)
                {
                    query += $" AND EGN = '{egn.Text}'";
                    check = true;
                }

                if (imeFamiliq.Text != "" && check == false)
                {
                    query += $" Ime_Familiq = '{imeFamiliq.Text}'";
                    check = true;
                }
                else if (imeFamiliq.Text != "" && check == true)
                {
                    query += $" AND Ime_Familiq = '{imeFamiliq.Text}'";
                    check = true;
                }

                if (prod_nomer.Text != "" && check == false)
                {
                    query += $" Prod_nomer = '{prod_nomer.Text}'";
                    check = true;
                }
                else if (prod_nomer.Text != "" && check == true)
                {
                    query += $" AND Prod_nomer = '{prod_nomer.Text}'";
                    check = true;
                }

                if (data_prod.Text != "" && check == false)
                    query += $" Data_prodajba = '{data_prod.Text}'";
                else if (data_prod.Text != "" && check == true)
                    query += $" AND Data_prodajba = '{data_prod.Text}'";

                try
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable table = new DataTable();
                    da.Fill(table);

                    dgv.DataSource = new BindingSource(table, null);
                }
                catch(Exception ex)
                {
                    error.Text = "Данните не са валидни";
                }
            }
        }

        DataGridView dgv = new DataGridView();

        Label error = new Label();

        TextBox reg_nomer = new TextBox();
        TextBox marka = new TextBox();
        TextBox godina = new TextBox();
        TextBox cena = new TextBox();
        TextBox egn = new TextBox();
        TextBox imeFamiliq = new TextBox();
        TextBox prod_nomer = new TextBox();
        TextBox data_prod = new TextBox();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.Location = new System.Drawing.Point(16, 200);
            dgv.Size = new System.Drawing.Size(943, 300);
            this.Controls.Add(dgv);
            DatabaseData();

            this.Text = "Име Фамилия ФК Номер КСТ Група";
            this.Size = new System.Drawing.Size(990, 550);
            this.Top = 0;
            this.Left = 0;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);

            //===========RESET BUTTON SECTION
            Button bReset = new Button();
            bReset.Location = new System.Drawing.Point(10, 32);
            bReset.Height = 20;
            bReset.Width = 80;
            bReset.Text = "Reset";
            bReset.Click += new System.EventHandler(bPopulate_Click);

            error.Location = new System.Drawing.Point(120, 32);
            error.Height = 20;
            error.Width = 600;
            error.Text = "";
            error.ForeColor = Color.Red;
            //===========RESET BUTTON SECTION

            //===========ADD BUTTON SECTION
            Button bAdd = new Button();
            bAdd.Location = new System.Drawing.Point(10, 64);
            bAdd.Height = 20;
            bAdd.Width = 80;
            bAdd.Text = "Add";
            bAdd.Click += new System.EventHandler(bAdd_Click);

            reg_nomer.Location = new System.Drawing.Point(120, 64);
            reg_nomer.Height = 20;
            reg_nomer.Width = 80;
            reg_nomer.PlaceholderText = "Рег. номер";
            reg_nomer.KeyPress += new KeyPressEventHandler(Reg_nomer_KeyPress);

            marka.Location = new System.Drawing.Point(220, 64);
            marka.Height = 20;
            marka.Width = 80;
            marka.PlaceholderText = "Марка";
            marka.KeyPress += new KeyPressEventHandler(Marka_KeyPress);
            marka.TextChanged += new System.EventHandler(Marka_TextChanged);

            godina.Location = new System.Drawing.Point(320, 64);
            godina.Height = 20;
            godina.Width = 80;
            godina.PlaceholderText = "Година";
            godina.KeyPress += new KeyPressEventHandler(Godina_KeyPress);

            cena.Location = new System.Drawing.Point(420, 64);
            cena.Height = 20;
            cena.Width = 80;
            cena.PlaceholderText = "Цена";
            cena.KeyPress += new KeyPressEventHandler(Cena_KeyPress);

            egn.Location = new System.Drawing.Point(520, 64);
            egn.Height = 20;
            egn.Width = 80;
            egn.PlaceholderText = "ЕГН";
            egn.KeyPress += new KeyPressEventHandler(EGN_KeyPress);

            imeFamiliq.Location = new System.Drawing.Point(620, 64);
            imeFamiliq.Height = 20;
            imeFamiliq.Width = 130;
            imeFamiliq.PlaceholderText = "Име и фамилия";
            imeFamiliq.KeyPress += new KeyPressEventHandler(ime_KeyPress);
            imeFamiliq.TextChanged += new System.EventHandler(ime_TextChanged);

            prod_nomer.Location = new System.Drawing.Point(770, 64);
            prod_nomer.Height = 20;
            prod_nomer.Width = 80;
            prod_nomer.PlaceholderText = "Продажба №";
            prod_nomer.KeyPress += new KeyPressEventHandler(Prod_nomer_KeyPress);

            data_prod.Location = new System.Drawing.Point(870, 64);
            data_prod.Height = 20;
            data_prod.Width = 80;
            data_prod.PlaceholderText = "Дата продажба";
            data_prod.KeyPress += new KeyPressEventHandler(Data_KeyPress);
            //===========ADD BUTTON SECTION

            //===========DELETE BUTTON SECTION
            Button bDelete = new Button();
            bDelete.Location = new System.Drawing.Point(10, 96);
            bDelete.Height = 20;
            bDelete.Width = 80;
            bDelete.Text = "Delete";
            bDelete.Click += new System.EventHandler(bDelete_Click);
            //===========DELETE BUTTON SECTION

            //===========EDIT BUTTON SECTION
            Button bEdit = new Button();
            bEdit.Location = new System.Drawing.Point(10, 128);
            bEdit.Height = 20;
            bEdit.Width = 80;
            bEdit.Text = "Edit";
            bEdit.Click += new System.EventHandler(bEdit_Click);

            Button bSave = new Button();
            bSave.Location = new System.Drawing.Point(120, 128);
            bSave.Height = 20;
            bSave.Width = 80;
            bSave.Text = "Save";
            bSave.Click += new System.EventHandler(bSave_Click);
            //===========EDIT BUTTON SECTION

            //===========SEARCH BUTTON SECTION
            Button bSearch = new Button();
            bSearch.Location = new System.Drawing.Point(10, 160);
            bSearch.Height = 20;
            bSearch.Width = 80;
            bSearch.Text = "Search";
            bSearch.Click += new System.EventHandler(bSearch_Click);
            //===========SEARCH BUTTON SECTION

            this.setMenu();

            this.Controls.Add(bReset);
            this.Controls.Add(error);

            this.Controls.Add(bAdd);
            this.Controls.Add(reg_nomer);
            this.Controls.Add(marka);
            this.Controls.Add(godina);
            this.Controls.Add(cena);
            this.Controls.Add(egn);
            this.Controls.Add(imeFamiliq);
            this.Controls.Add(prod_nomer);
            this.Controls.Add(data_prod);

            this.Controls.Add(bDelete);

            this.Controls.Add(bEdit);
            this.Controls.Add(bSave);

            this.Controls.Add(bSearch);
        }

        private void bPopulate_Click(object sender, EventArgs e)
        {
            this.populateList(1);
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            this.populateList(10);
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ако продължите, ще изгубите данните в базата данни. Искате ли да продължите ?", "Внимание !", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dgv.SelectedRows[0];
                    int numb = (int)row.Cells["Reg_nomer"].Value;
                    DeleteDatabaseData(numb);
                }
                else
                    error.Text = "Не може да изтриете ред без да сте го маркирали";
            }
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            if(reg_nomer.Text.Length != 0 || marka.Text.Length != 0 || godina.Text.Length != 0 || cena.Text.Length != 0 || egn.Text.Length != 0 || imeFamiliq.Text.Length != 0)
            {
                if (MessageBox.Show("Ако продължите, ще изгубите данните в полетата. Искате ли да продължите ?", "Внимание !", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = dgv.SelectedRows[0];
                        reg_nomer.Text = "" + (int)row.Cells["Reg_nomer"].Value;
                        marka.Text = (string)row.Cells["Marka"].Value;
                        godina.Text = "" + (int)row.Cells["Godina"].Value;
                        cena.Text = "" + (int)row.Cells["Cena"].Value;
                        egn.Text = (string)row.Cells["EGN"].Value;
                        imeFamiliq.Text = (string)row.Cells["Ime_Familiq"].Value;
                        prod_nomer.Text = "" + (int)row.Cells["Prod_nomer"].Value;
                        DateTime time = (DateTime)row.Cells["Data_prodajba"].Value;
                        data_prod.Text = "" + time.ToShortDateString();

                        reg_nomer.ReadOnly = true;
                    }
                    else
                        error.Text = "Не може да редактирате ред без да сте го маркирали";
                }
            }
            else
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dgv.SelectedRows[0];
                    reg_nomer.Text = "" + (int)row.Cells["Reg_nomer"].Value;
                    marka.Text = (string)row.Cells["Marka"].Value;
                    godina.Text = "" + (int)row.Cells["Godina"].Value;
                    cena.Text = "" + (int)row.Cells["Cena"].Value;
                    egn.Text = (string)row.Cells["EGN"].Value;
                    imeFamiliq.Text = (string)row.Cells["Ime_Familiq"].Value;
                    prod_nomer.Text = "" + (int)row.Cells["Prod_nomer"].Value;
                    DateTime time = (DateTime)row.Cells["Data_prodajba"].Value;
                    data_prod.Text = "" + time.ToShortDateString();

                    reg_nomer.ReadOnly = true;
                }
                else
                    error.Text = "Не може да редактирате ред без да сте го маркирали";
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveDatabaseData(int.Parse(reg_nomer.Text));
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            if (reg_nomer.Text.Length == 0 && marka.Text.Length == 0 && godina.Text.Length == 0 && cena.Text.Length == 0 && egn.Text.Length == 0 && imeFamiliq.Text.Length == 0)
            {
                error.Text = "Не може да търсите в базата данни ако няма въведени данни в полетата";
            }
            else
            {
                FindInDatabaseData();
            }
        }


        //==================MENU
        private void setMenu()
        {
            MenuStrip menu = new MenuStrip();

            // tochka 1
            ToolStripMenuItem mPriceOver1000 = new ToolStripMenuItem("&Автомобили с цена над 1000 лв");
            mPriceOver1000.Click += new System.EventHandler(mPriceOver1000_Click);

            // tochka 2
            ToolStripMenuItem mSvedeniq = new ToolStripMenuItem("&Сведения");
            mSvedeniq.Click += new System.EventHandler(bSpravkaNomerModelEGN_Click);

            // tochka 3
            ToolStripMenuItem mSort = new ToolStripMenuItem("&Сортирай");

            ToolStripMenuItem mMarka = new ToolStripMenuItem("&по Марка");
            mMarka.Click += new System.EventHandler(mMarka_Click);
            mSort.DropDownItems.Add(mMarka);

            ToolStripMenuItem mDataProdajba = new ToolStripMenuItem("&по Дата продажба");
            mDataProdajba.Click += new System.EventHandler(mDataProdajba_Click);
            mSort.DropDownItems.Add(mDataProdajba);

            // tochka 4, 5
            ToolStripMenuItem mSpravki = new ToolStripMenuItem("&Справки");

            // tochka 4
            ToolStripMenuItem mSpravkaEGN = new ToolStripMenuItem("&ЕГН 4812296560");
            mSpravkaEGN.Click += new System.EventHandler(bSpravkaEGN_Click);
            mSpravki.DropDownItems.Add(mSpravkaEGN);

            // tochka 5
            ToolStripMenuItem mSpravkaMarkaGodina = new ToolStripMenuItem("&Марка с 'М' и година 1990");
            mSpravkaMarkaGodina.Click += new System.EventHandler(bSpravkaMarkaGodina_Click);
            mSpravki.DropDownItems.Add(mSpravkaMarkaGodina);

            // tochka 6
            ToolStripMenuItem mNameri = new ToolStripMenuItem("&Намери непродадени и преди 1990");
            mNameri.Click += new System.EventHandler(bNameri_Click);

            // tochka 7
            ToolStripMenuItem mKomisionna = new ToolStripMenuItem("&Комисионна");
            mKomisionna.Click += new System.EventHandler(bKomisionna_Click);

            // tochka 8
            ToolStripMenuItem mSpisak = new ToolStripMenuItem("&Списък");
            mSpisak.Click += new System.EventHandler(bSpisak_Click);

            menu.Items.Add(mPriceOver1000);
            menu.Items.Add(mSvedeniq);
            menu.Items.Add(mSort);
            menu.Items.Add(mSpravki);
            menu.Items.Add(mNameri);
            menu.Items.Add(mKomisionna);
            menu.Items.Add(mSpisak);

            menu.BackColor = Color.White;

            this.Controls.Add(menu);
        }

        //==========ADD DATA TO LIST
        private void populateList(int numb)
        {
            switch (numb)
            {
                case 1:
                    DatabaseData();
                    break;

                case 2:
                    NomerMarkaEGN();
                    break;

                case 3:
                    FindEGN();
                    break;

                case 4:
                    SpravkaZaMarkaGodina();
                    break;

                case 5:
                    BroiNeprodadeni();
                    break;

                case 6:
                    Komisionna();
                    break;

                case 7:
                    Pechalba();
                    break;

                default:
                    Dobavi();
                    break;
            }
        }



        //===========================MENU FUNCTIONS
        // tochka 1
        private void mPriceOver1000_Click(object sender, EventArgs e)
        {
            CenaOver1000();
        }



        // tochka 2
        private void bSpravkaNomerModelEGN_Click(object sender, EventArgs e)
        {
            this.populateList(2);
        }



        // tochka 3
        private void mMarka_Click(object sender, EventArgs e)
        {
            SortMarka();
        }
        private void mDataProdajba_Click(object sender, EventArgs e)
        {
            SortDataProdajba();
        }



        // tochka 4
        private void bSpravkaEGN_Click(object sender, EventArgs e)
        {
            this.populateList(3);
        }



        // tochka 5
        private void bSpravkaMarkaGodina_Click(object sender, EventArgs e)
        {
            this.populateList(4);
        }



        // tochka 6
        private void bNameri_Click(object sender, EventArgs e)
        {
            this.populateList(5);
        }



        // tochka 7
        private void bKomisionna_Click(object sender, EventArgs e)
        {
            this.populateList(6);
        }



        // tochka 8
        private void bSpisak_Click(object sender, EventArgs e)
        {
            this.populateList(7);
        }



        // ======================== proverki
        private void ProveriPrazni()
        {
            if (reg_nomer.Text.Length == 0 || marka.Text.Length == 0 || godina.Text.Length == 0 || cena.Text.Length == 0 || egn.Text.Length == 0 || imeFamiliq.Text.Length == 0)
                error.Text = "Едно или няколко полета са празни";
            if ((prod_nomer.Text.Length == 0 && data_prod.Text.Length != 0) || (prod_nomer.Text.Length != 0 && data_prod.Text.Length == 0))
                error.Text = "Задължително е да има номер и дата на продажбата";
            if (data_prod.Text.Length != 0)
                ProveriData();
        }

        private void ProveriData()
        {
            try
            {
                DateTime dt = Convert.ToDateTime(data_prod.Text);
                data_prod.Text = dt.ToShortDateString();

                DateTime current = DateTime.Now;
                if (dt.Year < 1700 || dt.Year > current.Year)
                    error.Text = "Годината от датата на продажбата не е реална";
                else if (dt.Month <= 0 || dt.Month >= 13)
                    error.Text = "Месеца от датата на продажбата не е реален";
                else if (dt.Day <= 0 || dt.Day >= 32)
                    error.Text = "Деня от датата на продажбата не е реален";
            }
            catch(Exception ex)
            {
                error.Text = "Датата на продажбата не е валидна";
            }
        }

        private void ProveriPovtarqshtiseNomera()
        {
            foreach(DataGridViewRow row in dgv.Rows)
            {
                int temp = 0;

                temp = (int)row.Cells["Reg_nomer"].Value;
                if (temp == int.Parse(reg_nomer.Text))
                    error.Text = "Този регистрационнен номер вече съществува";

                if (row.Cells["Prod_nomer"].Value != DBNull.Value)
                {
                    temp = (int)row.Cells["Prod_nomer"].Value;
                    if (temp == int.Parse(prod_nomer.Text))
                        error.Text = "Този номер на продажба вече съществува";
                }
            }
        }

        private void ProveriDanni()
        {
            DateTime current = DateTime.Now;
            if (int.Parse(godina.Text) < 1700 || int.Parse(godina.Text) > current.Year)
                error.Text = "Годината на производство не е реална";

            if(egn.Text.Length != 10)
                error.Text = "ЕГН-то не трябва да съдържа по-малко числа от 10";
        }



        private void Reg_nomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (Char.IsNumber(c) == false)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = "Регистрационния номер не може да има букви !";
                    e.Handled = true;
                }
            }

            TextBox tb = (TextBox)sender;
            int max = 3;
            if (tb.Text.Length >= max)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = $"Регистрационния номер трябва да съдържа до {max} числа";
                    e.Handled = true;
                }
            }
        }



        private void Marka_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            char c = e.KeyChar;
            int max = 20;
            if (tb.Text.Length >= max && char.IsControl(c) == false)
            {
                error.Text = $"Марката може да има до {max} символа!";
                e.Handled = true;
            }
            if (!(c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я' || c >= '0' && c <= '9' || c == ' ') && !char.IsControl(c))
            {
                error.Text = "Символите и чуждите езици са забранени!";
                e.Handled = true;
            }
            if (tb.Text.EndsWith(" ") && c == ' ')
            {
                error.Text = "Не може да слагате повече от 2 празни места едни след други!";
                e.Handled = true;
            }
            if (tb.Text.Contains(" ") && c == ' ')
            {
                error.Text = "Не може да слагате повече от 2 празни места!";
                e.Handled = true;
            }
        }


        private void Godina_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (Char.IsNumber(c) == false)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = "Годината не може да има букви !";
                    e.Handled = true;
                }
            }

            TextBox tb = (TextBox)sender;
            int max = 4;
            if (tb.Text.Length >= max)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = $"Годината е до {max} числа !";
                    e.Handled = true;
                }
            }
        }



        private void Cena_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (Char.IsNumber(c) == false)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = "Цената не може да има букви !";
                    e.Handled = true;
                }
            }

            TextBox tb = (TextBox)sender;
            int max = 6;
            if (tb.Text.Length >= max)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = $"Цената е до {max} числа !";
                    e.Handled = true;
                }
            }
        }



        private void EGN_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (Char.IsNumber(c) == false)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = "ЕГН не може да има букви !";
                    e.Handled = true;
                }
            }

            TextBox tb = (TextBox)sender;
            if (tb.Text.Length >= 10)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = "ЕГН е до 10 числа !";
                    e.Handled = true;
                }
            }
        }



        private void ime_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            char c = e.KeyChar;
            int max = 30;
            if (tb.Text.Length >= max && char.IsControl(c) == false)
            {
                error.Text = $"Името може да има до {max} символа!";
                e.Handled = true;
            }
            if (!(c >= 'а' && c <= 'я' || c >= 'А' && c <= 'Я' || c == ' ') && !char.IsControl(c))
            {
                error.Text = "Символите и чуждите езици са забранени!";
                e.Handled = true;
            }
            if(tb.Text.EndsWith(" ") && c == ' ')
            {
                error.Text = "Не може да слагате повече от 2 празни места едни след други!";
                e.Handled = true;
            }
            if (tb.Text.Contains(" ") && c == ' ')
            {
                error.Text = "Не може да слагате повече от 2 празни места!";
                e.Handled = true;
            }
        }



        private void Prod_nomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (Char.IsNumber(c) == false)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = "Номера на продажба не може да има букви !";
                    e.Handled = true;
                }
            }

            TextBox tb = (TextBox)sender;
            int max = 3;
            if (tb.Text.Length >= max)
            {
                if (c != (Char)Keys.Back)
                {
                    error.Text = $"Номера на продажба трябва да съдържа до {max} числа";
                    e.Handled = true;
                }
            }
        }



        private void Data_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            char c = e.KeyChar;
            int max = 10;
            if (tb.Text.Length >= max && char.IsControl(c) == false)
            {
                error.Text = $"Датата на продажбата може да има до {max} символа!";
                e.Handled = true;
            }
            if (!(c >= '0' && c <= '9' || c == '/') && !char.IsControl(c))
            {
                error.Text = "Символите и буквите са забранени!";
                e.Handled = true;
            }
        }



        private void ime_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string s = tb.Text;

            if (s.Length == 1)
                s.ToUpper();
            else if (s.Length >= 2)
            {
                string temp = s.Substring(0, 1).ToUpper();

                for (int i = 1; i < s.Length; i++)
                {
                    if (s.Substring(i - 1, 1) == " ")
                        temp = temp + s.Substring(i, 1).ToUpper();
                    else
                        temp = temp + s.Substring(i, 1).ToLower();
                }

                s = temp;
            }

            tb.Text = s;
            tb.SelectionStart = tb.Text.Length;
        }



        private void Marka_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string s = tb.Text;

            if (s.Length == 1)
            {
                try
                {
                    int numb = int.Parse(s);
                }
                catch(Exception ex)
                {
                    s.ToUpper();
                }
            }
            else if (s.Length >= 2)
            {
                string temp = s.Substring(0, 1).ToUpper();

                for (int i = 1; i < s.Length; i++)
                {
                    try
                    {
                        int numb = int.Parse(s);

                        temp = temp + s.Substring(i, 1);
                    }
                    catch (Exception ex)
                    {
                        if (s.Substring(i - 1, 1) == " ")
                            temp = temp + s.Substring(i, 1).ToUpper();
                        else
                            temp = temp + s.Substring(i, 1).ToLower();
                    }
                }

                s = temp;
            }

            tb.Text = s;
            tb.SelectionStart = tb.Text.Length;
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            Keys PasteKeys = Keys.Control | Keys.V;

            if (key == PasteKeys)
            {
                error.Text = "Комбинацията за поставяне е забранена!";
                return true;
            }
            else
                return false;
        }



        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (reg_nomer.Text.Length != 0 || marka.Text.Length != 0 || godina.Text.Length != 0 || cena.Text.Length != 0 || egn.Text.Length != 0 || imeFamiliq.Text.Length != 0)
            {
                if (MessageBox.Show("Ако затворите програмата, ще изгубите данните в полетата. Искате ли да продължите ?", "Внимание !", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
