using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Order_Application
{
    public partial class Form1 : Form
    {

        DataTable[] tableOrder;
        public Form1()
        {
            InitializeComponent();
            initComboBox();
        }

        private void getMon(object o, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tableOrder[index].Rows.Count > 0)
            {
                DialogResult d = MessageBox.Show("Bàn này đã có order rồi. Reset order?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (d)
                {
                    case DialogResult.Yes:
                        tableOrder[index] = new DataTable();
                        dataGridView1.Rows.Clear();
                        break;
                    case DialogResult.No:
                        return;
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1[0, i].Value == null) break;
                else if (dataGridView1[0, i].Value.ToString() == ((Button)o).Text)
                {
                    dataGridView1[1, i].Value = Convert.ToInt32(dataGridView1[1, i].Value) + 1;
                    return;
                }
            }
            string[] row = new string[] { ((Button)o).Text, "1" };
            dataGridView1.Rows.Add(row);
        }
        private void initComboBox()
        {
            for (int i = 1; i < 6; i++)
            {
                comboBox1.Items.Add("Bàn " + i);
            }                    
            tableOrder = new DataTable[comboBox1.Items.Count + 1];
            for (int i = 0; i < comboBox1.Items.Count + 1; i++)
            {
                tableOrder[i] = new DataTable();
            }                
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                tableOrder[index].Columns.Add(c.HeaderText);
            }
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                DataRow row = tableOrder[index].NewRow();
                foreach (DataGridViewCell cell in r.Cells)
                {
                    row[cell.ColumnIndex] = cell.Value; 
                }
                tableOrder[index].Rows.Add(row);
            }
            if (tableOrder[index] != null) MessageBox.Show("Order thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
           int index = comboBox1.SelectedIndex;
           if (dataGridView1.Rows.Count > 1 && tableOrder[index].Rows.Count <= 0)
            {
                DialogResult d = MessageBox.Show("Order hiện tại chưa được lưu, nếu chuyển bàn sẽ mất order. Tiếp tục?", "Unsaved Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch(d)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        return;
                }    
            }
           dataGridView1.Rows.Clear();
           foreach (DataRow row in tableOrder[comboBox1.SelectedIndex].Rows)
           {
                string[] r = row.ItemArray.OfType<string>().ToArray();
                dataGridView1.Rows.Add(r);
           }
           dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (tableOrder[index].Rows.Count > 0)
            {
                DialogResult d = MessageBox.Show("Bàn này đã có order rồi. Reset order?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (d)
                {
                    case DialogResult.Yes:
                        tableOrder[index] = new DataTable();
                        dataGridView1.Rows.Clear();
                        break;
                    case DialogResult.No:
                        return;
                }
            }
            dataGridView1.Rows.Clear();
        }

    }
}
       