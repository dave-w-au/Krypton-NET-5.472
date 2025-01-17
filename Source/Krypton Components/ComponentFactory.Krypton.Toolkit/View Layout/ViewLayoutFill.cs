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

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// View element that is used to fill a docker area and positions a control to the same size.
    /// </summary>
    public class ViewLayoutFill : ViewLayoutNull
    {
        #region Instance Fields
        private readonly Control _control;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutNull class.
        /// </summary>
        public ViewLayoutFill()
            : this(null)
        {
            DisplayPadding = Padding.Empty;
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutNull class.
        /// </summary>
        /// <param name="control">Control to position in fill location.</param>
        public ViewLayoutFill(Control control)
        {
            _control = control;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutFill:" + Id;
        }
        #endregion

        #region DisplayPadding
        /// <summary>
        /// Gets and sets the padding used around the control.
        /// </summary>
        public Padding DisplayPadding { get; set; }

        #endregion

        #region FillRect
        /// <summary>
        /// Gets the latest calculated fill rectangle.
        /// </summary>
        public Rectangle FillRect { get; private set; }

        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Get requested size of the control
            Size size = _control?.GetPreferredSize(context.DisplayRectangle.Size) ?? Size.Empty;

            // Return size with padding added on
            return new Size(size.Width + DisplayPadding.Horizontal,
                            size.Height + DisplayPadding.Vertical);
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Cache the fill rectangle
            FillRect = ClientRectangle;

            // Reduce the fill rectangle to account for the display padding
            FillRect = CommonHelper.ApplyPadding(Orientation.Horizontal, FillRect, DisplayPadding);

            // We let the OnLayout override for the control perform the
            // actually positioning of the fill contents.
        }
        #endregion
    }
}
