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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Details about the context menu about to be shown when clicking the drop down button on a KryptonDateTimePicker.
    /// </summary>
    public class DateTimePickerDropArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DateTimePickerDropArgs class.
        /// </summary>
        /// <param name="kcm">KryptonContextMenu that can be customized.</param>
        /// <param name="positionH">Relative horizontal position of the KryptonContextMenu.</param>
        /// <param name="positionV">Relative vertical position of the KryptonContextMenu.</param>
        public DateTimePickerDropArgs(KryptonContextMenu kcm,
                                     KryptonContextMenuPositionH positionH,
                                     KryptonContextMenuPositionV positionV)
        {
            KryptonContextMenu = kcm;
            PositionH = positionH;
            PositionV = positionV;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the KryptonContextMenu instance.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        /// <summary>
        /// Gets and sets the relative horizontal position of the KryptonContextMenu.
        /// </summary>
        public KryptonContextMenuPositionH PositionH { get; set; }

        /// <summary>
        /// Gets and sets the relative vertical position of the KryptonContextMenu.
        /// </summary>
        public KryptonContextMenuPositionV PositionV { get; set; }

        #endregion
    }
}
