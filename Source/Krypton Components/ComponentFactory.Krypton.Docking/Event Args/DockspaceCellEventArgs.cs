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
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Event arguments for a DockspaceCellAdding/DockspaceCellRemoving events.
    /// </summary>
    public class DockspaceCellEventArgs : EventArgs
    {
        #region Instance Fields

        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockspaceCellEventArgs class.
        /// </summary>
        /// <param name="dockspace">Reference to existing dockspace control instance.</param>
        /// <param name="element">Reference to docking dockspace element that is managing the dockspace control.</param>
        /// <param name="cell">Reference to dockspace control cell instance.</param>
        public DockspaceCellEventArgs(KryptonDockspace dockspace,
                                      KryptonDockingDockspace element,
                                      KryptonWorkspaceCell cell)
        {
            DockspaceControl = dockspace;
            DockspaceElement = element;
            CellControl = cell;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the KryptonDockspace that contains the cell.
        /// </summary>
        public KryptonDockspace DockspaceControl { get; }

        /// <summary>
        /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace.
        /// </summary>
        public KryptonDockingDockspace DockspaceElement { get; }

        /// <summary>
        /// Gets a reference to the KryptonWorkspaceCell control.
        /// </summary>
        public KryptonWorkspaceCell CellControl { get; }

        #endregion
    }
}
