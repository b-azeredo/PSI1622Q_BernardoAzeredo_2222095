﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminSysWF
{
    public partial class AddTarefa : Form
    {
        int userID;
        public AddTarefa(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void ComfirmAddDespesa_Click(object sender, EventArgs e)
        {
            Database.AddTarefa(userID, txb_DescTarefa.Text);
            this.Close();
        }
    }
}