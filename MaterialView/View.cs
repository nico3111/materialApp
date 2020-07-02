using MaterialData.models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MaterialView
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var materialEntities = new DcvEntities())
            {
                var notebook = new notebook();
                notebook.serial_number = TbSn.Text;
                notebook.make = TbMake.Text;
                notebook.model = TbModel.Text;
                Int32.TryParse(TbPersonId.Text, out int result1);
                notebook.person_id = result1;
                Int32.TryParse(TbPersonId.Text, out int result2);
                notebook.location_id = result2;

                materialEntities.notebook.Add(notebook);
                materialEntities.SaveChanges();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var materialEntities = new DcvEntities();

            var notebooks = materialEntities.notebook.ToList();


            foreach (var item in notebooks)
            {
                TbOutput.Text = item.ToString();
            }
        }
    }
}
