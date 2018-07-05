﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV) 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.72)
//	The software and associated documentation supplied hereunder are the 
    //  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.72.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Extends the ViewLayoutDocker by drawing the ribbon application button outer background.
	/// </summary>
    internal class ViewDrawRibbonAppMenuOuter : ViewLayoutDocker
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        private IDisposable _memento;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonAppMenuOuter class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public ViewDrawRibbonAppMenuOuter(KryptonRibbon ribbon)
        {
            _ribbon = ribbon;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonAppMenuOuter:" + Id;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            base.RenderBefore(context);

            // Draw the application menu outer background
            _memento = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, ClientRectangle, State, 
                                                                    _ribbon.StateCommon.RibbonAppMenuOuter,
                                                                    VisualOrientation.Top, false, _memento);

        }
        #endregion
    }
}
