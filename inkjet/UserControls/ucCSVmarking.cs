using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inkjet.UserControls
{
    public partial class ucCSVmarking : UserControl
    {
        public ucCSVmarking()
        {
            InitializeComponent();              
        }

        private void CsvButtonStart_Click(object sender, EventArgs e)
        {
            client.Program program = new client.Program();
            program.Execute_Client();

          
        }


    }
}
