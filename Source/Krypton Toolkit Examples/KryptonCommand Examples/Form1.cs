﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012 - 2019. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 5.472.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonCommandExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAttach_Click(object sender, EventArgs e)
        {
            AssignCommand(kryptonCommand);
        }

        private void buttonUnattach_Click(object sender, EventArgs e)
        {
            AssignCommand(null);
        }

        private void AssignCommand(KryptonCommand command)
        {
            buttonSpecAny1.KryptonCommand = command;
            kryptonButton1.KryptonCommand = command;
            kryptonCheckButton1.KryptonCommand = command;
            kryptonDropButton1.KryptonCommand = command;
            kryptonColorButton1.KryptonCommand = command;
            kryptonLabel1.KryptonCommand = command;
            kryptonLinkLabel1.KryptonCommand = command;
            kryptonCheckBox1.KryptonCommand = command;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
