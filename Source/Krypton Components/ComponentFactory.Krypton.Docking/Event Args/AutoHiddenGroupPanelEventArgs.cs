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

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Event arguments for a AutoHiddenGroupPanelAdding/AutoHiddenGroupPanelRemoved events.
    /// </summary>
    public class AutoHiddenGroupPanelEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AutoHiddenGroupPanelEventArgs class.
        /// </summary>
        /// <param name="autoHiddenPanel">Reference to auto hidden panel control instance.</param>
        /// <param name="element">Reference to docking auto hidden edge element that is managing the panel.</param>
        public AutoHiddenGroupPanelEventArgs(KryptonAutoHiddenPanel autoHiddenPanel,
                                             KryptonDockingEdgeAutoHidden element)
        {
            AutoHiddenPanelControl = autoHiddenPanel;
            EdgeAutoHiddenElement = element;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonAutoHiddenPanel control.
        /// </summary>
        public KryptonAutoHiddenPanel AutoHiddenPanelControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingEdgeAutoHidden that is managing the edge.
        /// </summary>
        public KryptonDockingEdgeAutoHidden EdgeAutoHiddenElement { get; }

        #endregion
    }
}
