using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class LiteratureForm : Form
    {
        public Literature LiteratureData { get; private set; }

        public LiteratureForm()
        {
            InitializeComponent();
            LiteratureData = new Literature();
        }

        public LiteratureForm(Literature literature)
        {
            InitializeComponent();
            LiteratureData = literature;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                if (LiteratureData != null)
                {
                    txtTitle.Text = LiteratureData.Title;
                    txtAuthor.Text = LiteratureData.Author;
                    numYear.Value = LiteratureData.Year > 0 ? LiteratureData.Year : DateTime.Now.Year;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message);
            }
        }
        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {

        }

        private void numYear_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text == "")
                {
                    throw new Exception("Введите название литературы!");
                }

                if (LiteratureData == null)
                {
                    LiteratureData = new Literature();
                }

                LiteratureData.Title = txtTitle.Text;
                LiteratureData.Author = txtAuthor.Text;
                LiteratureData.Year = (int)numYear.Value;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message);
                this.DialogResult = DialogResult.None;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
