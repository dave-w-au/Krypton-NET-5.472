﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006-2019, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2019. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.472)
//  Version 5.472.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Navigator
{
    internal partial class KryptonPageFormEditFlags : Form
    {
        #region Instance Fields
        private readonly KryptonPage _page;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPageFormEditFlags class.
        /// </summary>
        public KryptonPageFormEditFlags()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new instance of the KryptonPageFormEditFlags class.
        /// </summary>
        /// <param name="page">Reference to page to display flags for.</param>
        public KryptonPageFormEditFlags(KryptonPage page)
        {
            _page = page;
            InitializeComponent();
        }
        #endregion

        #region Implementation
        private void OnLoad(object sender, EventArgs e)
        {
            checkBoxPageInOverflowBarForOutlookMode.Checked = _page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode);
            checkBoxAllowPageDrag.Checked = _page.AreFlagsSet(KryptonPageFlags.AllowPageDrag);
            checkBoxAllowPageReorder.Checked = _page.AreFlagsSet(KryptonPageFlags.AllowPageReorder);
            checkBoxAllowConfigSave.Checked = _page.AreFlagsSet(KryptonPageFlags.AllowConfigSave);
            checkBoxDockingAllowClose.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowClose);
            checkBoxDockingAllowDropDown.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowDropDown);
            checkBoxDockingAllowAutoHidden.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowAutoHidden);
            checkBoxDockingAllowDocked.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowDocked);
            checkBoxDockingAllowFloating.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowFloating);
            checkBoxDockingAllowWorkspace.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowWorkspace);
            checkBoxDockingAllowNavigator.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowNavigator);
        }
        
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (checkBoxPageInOverflowBarForOutlookMode.Checked)
            {
                _page.SetFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);
            }

            if (checkBoxAllowPageDrag.Checked)
            {
                _page.SetFlags(KryptonPageFlags.AllowPageDrag);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.AllowPageDrag);
            }

            if (checkBoxAllowPageReorder.Checked)
            {
                _page.SetFlags(KryptonPageFlags.AllowPageReorder);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.AllowPageReorder);
            }

            if (checkBoxAllowConfigSave.Checked)
            {
                _page.SetFlags(KryptonPageFlags.AllowConfigSave);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.AllowConfigSave);
            }

            if (checkBoxDockingAllowClose.Checked)
            {
                _page.SetFlags(KryptonPageFlags.DockingAllowClose);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.DockingAllowClose);
            }

            if (checkBoxDockingAllowDropDown.Checked)
            {
                _page.SetFlags(KryptonPageFlags.DockingAllowDropDown);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.DockingAllowDropDown);
            }

            if (checkBoxDockingAllowAutoHidden.Checked)
            {
                _page.SetFlags(KryptonPageFlags.DockingAllowAutoHidden);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.DockingAllowAutoHidden);
            }

            if (checkBoxDockingAllowDocked.Checked)
            {
                _page.SetFlags(KryptonPageFlags.DockingAllowDocked);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.DockingAllowDocked);
            }

            if (checkBoxDockingAllowFloating.Checked)
            {
                _page.SetFlags(KryptonPageFlags.DockingAllowFloating);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.DockingAllowFloating);
            }

            if (checkBoxDockingAllowWorkspace.Checked)
            {
                _page.SetFlags(KryptonPageFlags.DockingAllowWorkspace);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.DockingAllowWorkspace);
            }

            if (checkBoxDockingAllowNavigator.Checked)
            {
                _page.SetFlags(KryptonPageFlags.DockingAllowNavigator);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.DockingAllowNavigator);
            }
        }
        #endregion
    }
}
