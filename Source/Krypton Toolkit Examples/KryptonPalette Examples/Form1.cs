﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012 - 2019. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 5.472.0.0 	www.ComponentFactory.com
// *****************************************************************************

using ComponentFactory.Krypton.Toolkit;
using System;

namespace KryptonPaletteExamples
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Blue;

            EnableDropShadow(true);
        }

        private void buttonOffice2010Blue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Blue;

            EnableDropShadow(true);
        }

        private void buttonOffice2010Silver_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Silver;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Silver;

            EnableDropShadow(true);
        }

        private void buttonOffice2010Black_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Black;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Black;

            EnableDropShadow(true);
        }

        private void buttonOffice2007Blue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2007Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2007Blue;

            EnableDropShadow(true);
        }

        private void buttonOffice2007Silver_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2007Silver;
            propertyGrid.SelectedObject = kryptonPaletteOffice2007Silver;

            EnableDropShadow(true);
        }

        private void buttonOffice2007Black_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2007Black;
            propertyGrid.SelectedObject = kryptonPaletteOffice2007Black;

            EnableDropShadow(true);
        }

        private void buttonOffice2003_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2003;
            propertyGrid.SelectedObject = kryptonPaletteOffice2003;

            EnableDropShadow(true);
        }

        private void buttonSystem_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSystem;
            propertyGrid.SelectedObject = kryptonPaletteSystem;

            EnableDropShadow(false);
        }

        private void buttonSparkleBlue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSparkleBlue;
            propertyGrid.SelectedObject = kryptonPaletteSparkleBlue;

            EnableDropShadow(true);
        }

        private void buttonSparkleOrange_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSparkleOrange;
            propertyGrid.SelectedObject = kryptonPaletteSparkleOrange;

            EnableDropShadow(true);
        }

        private void buttonSparklePurple_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSparklePurple;
            propertyGrid.SelectedObject = kryptonPaletteSparklePurple;

            EnableDropShadow(true);
        }

        private void buttonCustom_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteCustom;
            propertyGrid.SelectedObject = kryptonPaletteCustom;

            EnableDropShadow(false);

            btnExport.Enabled = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            kryptonPaletteCustom.Export();

            btnExport.Enabled = false;
        }

        private void EnableDropShadow(bool enabled)
        {
            UseDropShadow = enabled;
        }

        private void btnImportCustomPalette_Click(object sender, EventArgs e)
        {
            try
            {
                kryptonPaletteCustom.Import();

                kryptonManager.GlobalPalette = kryptonPaletteCustom;

                kryptonManager.GlobalPaletteMode = PaletteModeManager.Custom;
            }
            catch (Exception exc)
            {

                throw;
            }
        }
    }
}
