﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  Created by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2019 - 2019. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.472)
//  Version 5.472.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class OverrideComboBoxStyleDropDownStyle : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>UITypeEditorEditStyle value.</returns>
        /// <remarks>
        /// We show a drop down for editing the PaletteDrawBorders value.
        /// </remarks>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => context?.Instance != null
            ? UITypeEditorEditStyle.DropDown
            : base.GetEditStyle(context);


        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider?.GetService(typeof(IWindowsFormsEditorService));
            if (svc != null)
            {
                UserControl ctrl = new UserControl();
                ListBox clb = new ListBox { Dock = DockStyle.Fill };
                clb.Items.Add(ComboBoxStyle.DropDown);
                clb.Items.Add(ComboBoxStyle.DropDownList);
                clb.SelectedIndexChanged += delegate 
                    {
                        value = Enum.Parse(typeof(ComboBoxStyle), clb.SelectedItem.ToString());
                        svc.CloseDropDown();
                    };
                ctrl.Controls.Add(clb);
                svc.DropDownControl(ctrl);
            }

            return value;
        }
    }
}
