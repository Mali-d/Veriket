using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Sıra No", typeof(int));
            table.Columns.Add("Tarih", typeof(string));
            table.Columns.Add("Bilgisayar Adı", typeof(string));
            table.Columns.Add("Windowsta Oturum Açan Kullanıcı Adı", typeof(string));

            dataGridView1.DataSource = table;

            string[] lines = File.ReadAllLines(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Windows)) + "ProgramData\\VeriketApp\\VeriketApp.txt");
            string[] values;
            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(',');
                var aaa = table.Columns.Count;
                string[] row = new string[values.Length + 1];
                row[0] = (i + 1).ToString();
                for (int j = 1; j < values.Length + 1; j++)
                {
                    row[j] = values[j - 1].Trim();
                }
                table.Rows.Add(row);
            }
        }
    }
}
