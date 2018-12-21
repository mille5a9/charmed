using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASS
{
    public partial class FormPASS : Form
    {
        public FormPASS()
        {
            InitializeComponent();
            btnAccess.Click += new EventHandler(btnAccess_Click);
            btnFile.Click += new EventHandler(btnFile_Click);
        }
    }
}
