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
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Event arguments for cancellable events that need to provide a unique name and context menu.
    /// </summary>
    public class CancelDropDownEventArgs : CancelEventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CancelDropDownEventArgs class.
        /// </summary>
        /// <param name="contextMenu">Reference to associated context menu.</param>
        /// <param name="page">Reference to the associated page.</param>
        public CancelDropDownEventArgs(KryptonContextMenu contextMenu, KryptonPage page)
            : base(false)
        {
            KryptonContextMenu = contextMenu;
            Page = page;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the context menu.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu { get; }

        /// <summary>
        /// Gets a reference to the page.
        /// </summary>
        public KryptonPage Page { get; }

        #endregion
    }
}
