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

using System.ComponentModel;
using System.Drawing;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Details for an cancellable event that provides a Point value.
    /// </summary>
    public class PointEventCancelArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PointEventCancelArgs class.
        /// </summary>
        /// <param name="point">Point associated with event.</param>
        public PointEventCancelArgs(Point point)
        {
            Point = point;
        }
        #endregion

        #region Point
        /// <summary>
        /// Gets and sets the point.
        /// </summary>
        public Point Point { get; set; }

        #endregion
    }
}
