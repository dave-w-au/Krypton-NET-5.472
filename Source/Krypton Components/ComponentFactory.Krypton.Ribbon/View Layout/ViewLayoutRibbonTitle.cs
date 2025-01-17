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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// View element that draws nothing and will center all children within itself.
    /// </summary>
    internal class ViewLayoutRibbonTitle: ViewLayoutDocker
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonTitle class.
        /// </summary>
        public ViewLayoutRibbonTitle()
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutRibbonTitle:" + Id;
        }
        #endregion

        #region VertOffset
        /// <summary>
        /// Gets and sets the vertial offset for bottom docked elements.
        /// </summary>
        public int VertOffset { get; set; }

        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Let base class perform simple layout
            base.Layout(context);

            // We adjust the vertical layout position of the bottom docked items
            Rectangle tempRect = context.DisplayRectangle;
            foreach(ViewBase view in this)
            {
                if (GetDock(view) == ViewDockStyle.Bottom)
                {
                    // Ask the element to layout again but offset
                    Rectangle layoutRect = view.ClientRectangle;
                    layoutRect.Y += VertOffset;
                    context.DisplayRectangle = layoutRect;

                    view.Layout(context);
                }
            }
            
            // Must restore the original value
            context.DisplayRectangle = tempRect;
        }
        #endregion
    }
}
