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

using System.ComponentModel.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonNumericUpDownActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonNumericUpDown _numericUpDown;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonNumericUpDownActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonNumericUpDownActionList(KryptonNumericUpDownDesigner owner)
            : base(owner.Component)
        {
            // Remember the text box instance
            _numericUpDown = owner.Component as KryptonNumericUpDown;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _numericUpDown.PaletteMode;

            set 
            {
                if (_numericUpDown.PaletteMode != value)
                {
                    _service.OnComponentChanged(_numericUpDown, null, _numericUpDown.PaletteMode, value);
                    _numericUpDown.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get => _numericUpDown.InputControlStyle;

            set
            {
                if (_numericUpDown.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_numericUpDown, null, _numericUpDown.InputControlStyle, value);
                    _numericUpDown.InputControlStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the increment value of the control.
        /// </summary>
        public decimal Increment
        {
            get => _numericUpDown.Increment;

            set
            {
                if (_numericUpDown.Increment != value)
                {
                    _service.OnComponentChanged(_numericUpDown, null, _numericUpDown.Increment, value);
                    _numericUpDown.Increment = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the increment value of the control.
        /// </summary>
        public decimal Maximum
        {
            get => _numericUpDown.Maximum;

            set
            {
                if (_numericUpDown.Maximum != value)
                {
                    _service.OnComponentChanged(_numericUpDown, null, _numericUpDown.Maximum, value);
                    _numericUpDown.Maximum = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the increment value of the control.
        /// </summary>
        public decimal Minimum
        {
            get => _numericUpDown.Minimum;

            set
            {
                if (_numericUpDown.Minimum != value)
                {
                    _service.OnComponentChanged(_numericUpDown, null, _numericUpDown.Minimum, value);
                    _numericUpDown.Minimum = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_numericUpDown != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("InputControlStyle", "Style", "Appearance", "NumericUpDown display style."));
                actions.Add(new DesignerActionHeaderItem("Data"));
                actions.Add(new DesignerActionPropertyItem("Increment", "Increment", "Data", "NumericUpDown increment value."));
                actions.Add(new DesignerActionPropertyItem("Maximum", "Maximum", "Data", "NumericUpDown maximum value."));
                actions.Add(new DesignerActionPropertyItem("Minimum", "Minimum", "Data", "NumericUpDown minimum value."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
