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

using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implementation for internal calendar buttons.
    /// </summary>
    public class ButtonSpecCalendar : ButtonSpec
    {
        #region Instance Fields
        private ViewDrawMonth _month;
        private readonly RelativeEdgeAlign _edge;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecCalendar class.
        /// </summary>
        /// <param name="month">Reference to owning view.</param>
        /// <param name="fixedStyle">Fixed style to use.</param>
        /// <param name="edge">Alignment edge.</param>
        public ButtonSpecCalendar(ViewDrawMonth month,
                                  PaletteButtonSpecStyle fixedStyle,
                                  RelativeEdgeAlign edge)
        {
            Debug.Assert(month != null);

            // Remember back reference to owning navigator.
            _month = month;
            _edge = edge;
            Enabled = true;
            Visible = true;

            // Fix the type
            ProtectedType = fixedStyle;
        }      
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the visible state.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets and sets the enabled state.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Can a component be associated with the view.
        /// </summary>
        public override bool AllowComponent => false;

        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibiliy.</returns>
        public override bool GetVisible(IPalette palette)
        {
            return Visible;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            return (Enabled ? ButtonEnabled.Container : ButtonEnabled.False);
        }

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette)
        {
            return ButtonCheckState.Unchecked;
        }

        /// <summary>
        /// Gets the button edge to position against.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Edge position.</returns>
        public override RelativeEdgeAlign GetEdge(IPalette palette)
        {
            return _edge;
        }
        #endregion
    }
}
