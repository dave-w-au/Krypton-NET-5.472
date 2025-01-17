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
using System.Drawing;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Event arguments for events that need a screen point and element offset.
    /// </summary>
    public class ScreenAndOffsetEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ScreenAndOffsetEventArgs class.
        /// </summary>
        /// <param name="screenPoint">Screen point.</param>
        /// <param name="elementOffset">Element offset.</param>
        public ScreenAndOffsetEventArgs(Point screenPoint, Point elementOffset)
        {
            ScreenPoint = screenPoint;
            ElementOffset = elementOffset;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the screen point.
        /// </summary>
        public Point ScreenPoint { get; }

        /// <summary>
        /// Gets the element offset.
        /// </summary>
        public Point ElementOffset { get; }

        #endregion
    }
}
