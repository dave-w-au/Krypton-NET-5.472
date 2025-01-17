﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006-2019, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by MegaKraken, Thavarajan, Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2019. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.472)
//  Version 5.472.0.0  www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
// ReSharper disable UnusedMember.Global

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Hosts a collection of KryptonDataGridViewComboBoxCell cells.
    /// </summary>
    [Designer(typeof(KryptonComboBoxColumnDesigner))]
    [ToolboxBitmap(typeof(KryptonDataGridViewComboBoxColumn), "ToolboxBitmaps.KryptonComboBox.bmp")]
    public class KryptonDataGridViewComboBoxColumn : KryptonDataGridViewIconColumn
    {
        #region Instance Fields

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewComboBoxColumn class.
        /// </summary>
        public KryptonDataGridViewComboBoxColumn()
            : base(new KryptonDataGridViewComboBoxCell())
        {
            ButtonSpecs = new DataGridViewColumnSpecCollection(this);
            Items = new List<object>();
            AutoCompleteCustomSource = new AutoCompleteStringCollection();
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewComboBoxColumn { Name=");
            // ReSharper disable RedundantBaseQualifier
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            // ReSharper restore RedundantBaseQualifier
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewComboBoxColumn cloned = base.Clone() as KryptonDataGridViewComboBoxColumn;

            cloned.Items.AddRange(Items);

            // Convert collection of strings to an array
            string[] strings = new string[AutoCompleteCustomSource.Count];
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = AutoCompleteCustomSource[i];
            }

            cloned.AutoCompleteCustomSource.AddRange(strings);


            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
            {
                cloned.ButtonSpecs.Add(bs.Clone());
            }

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;

            set
            {
                if ((value != null)
                    && !(value is KryptonDataGridViewComboBoxCell)
                    )
                {
                    throw new InvalidCastException(@"Value provided for CellTemplate must be of type KryptonDataGridViewComboBoxCell or derive from it.");
                }

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs { get; }

        /// <summary>
        /// Gets the collection of allowable items of the domain up down.
        /// </summary>
        [Category("Data")]
        [Description("The allowable items of the domain up down.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        public List<object> Items { get; }

        private bool ShouldSerializeItems => true;

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category("Appearance")]
        [Description("Controls the appearance and functionality of the KryptonComboBox.")]
        [DefaultValue(typeof(ComboBoxStyle), "DropDown")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ComboBoxStyle DropDownStyle
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.DropDownStyle;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DropDownStyle = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetDropDownStyle(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum number of entries to display in the drop-down list.
        /// </summary>
        [Category("Behavior")]
        [Description("The maximum number of entries to display in the drop-down list.")]
        [Localizable(true)]
        [DefaultValue(8)]
        public int MaxDropDownItems
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.MaxDropDownItems;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.MaxDropDownItems = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetMaxDropDownItems(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The height, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(200)]
        [Browsable(true)]
        public int DropDownHeight
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.DropDownHeight;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DropDownHeight = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetMaxDropDownItems(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the width, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The width, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public int DropDownWidth
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.DropDownWidth;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DropDownWidth = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetDropDownWidth(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
        /// </summary>
        [Description("The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Browsable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource { get; }

        private bool ShouldSerializeAutoCompleteCustomSource => true;

        /// <summary>
        /// Gets or sets the text completion behavior of the combobox.
        /// </summary>
        [Description("Indicates the text completion behavior of the combobox.")]
        [DefaultValue(typeof(AutoCompleteMode), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.AutoCompleteMode;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.AutoCompleteMode = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetAutoCompleteMode(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
        /// </summary>
        [Description("The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
        [DefaultValue(typeof(AutoCompleteSource), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.AutoCompleteSource;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.AutoCompleteSource = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetAutoCompleteSource(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to display for the items in this control.")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string DisplayMember
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.DisplayMember;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DisplayMember = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetDisplayMember(rowIndex, value);
                        }
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to display for the items in this control.")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ValueMember
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.ValueMember;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.ValueMember = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        if (dataGridViewRow.Cells[Index] is KryptonDataGridViewComboBoxCell dataGridViewCell)
                        {
                            dataGridViewCell.SetValueMember(rowIndex, value);
                        }
                    }
                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// "Indicates the Datasource for the items in this control.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the Datasource for the items in this control.")]
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Editor("System.Windows.Forms.Design.DataSourceListEditor, System.Design", typeof(UITypeEditor))]
        public object DataSource
        {

            get
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                return ComboBoxCellTemplate.DataSource;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(@"Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DataSource = value;
            }
        }
        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewComboBoxCell
        /// </summary>
        private KryptonDataGridViewComboBoxCell ComboBoxCellTemplate => (KryptonDataGridViewComboBoxCell)CellTemplate;

        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args) => ButtonSpecClick?.Invoke(this, args);
        #endregion
    }
}