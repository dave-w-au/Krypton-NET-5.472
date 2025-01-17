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
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Create and manage the view for a application button specific ButtonSpec definition.
    /// </summary>
    public class ButtonSpecViewAppButton : ButtonSpecView
    {
        #region Instance Fields
        private ButtonSpecAppButtonController _controller;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecViewAppButton class.
        /// </summary>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="paletteMetric">Source for metric values.</param>
        /// <param name="metricPadding">Padding metric for border padding.</param>
        /// <param name="manager">Reference to owning manager.</param>
        /// <param name="buttonSpec">Access</param>
        public ButtonSpecViewAppButton(PaletteRedirect redirector,
                                       IPaletteMetric paletteMetric,
                                       PaletteMetricPadding metricPadding,
                                       ButtonSpecManagerBase manager,
                                       ButtonSpec buttonSpec)
            : base(redirector, paletteMetric, metricPadding, manager, buttonSpec)
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create a button controller for the view.
        /// </summary>
        /// <param name="viewButton">View to be controlled.</param>
        /// <param name="needPaint">Paint delegate.</param>
        /// <param name="clickHandler">Reference to click handler.</param>
        /// <returns>Controller instance.</returns>
        public override ButtonSpecViewControllers CreateController(ViewDrawButton viewButton,
                                                                   NeedPaintHandler needPaint,
                                                                   MouseEventHandler clickHandler)
        {
            // Create a ribbon specific button controller
            ButtonSpecManagerLayoutAppButton managerAppButton = (ButtonSpecManagerLayoutAppButton)Manager;
            _controller = new ButtonSpecAppButtonController(managerAppButton.ViewManager, viewButton, needPaint)
            {
                BecomesFixed = true
            };
            _controller.Click += clickHandler;

            // If associated with a tooltip manager then pass mouse messages onto tooltip manager
            IMouseController mouseController = _controller;
            if (Manager.ToolTipManager != null)
            {
                mouseController = new ToolTipController(Manager.ToolTipManager, viewButton, _controller);
            }

            // Return a collection of controllers
            return new ButtonSpecViewControllers(mouseController, _controller, _controller);
        }

        /// <summary>
        /// Processes the finish of the button being pressed.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnFinishDelegate(object sender, EventArgs e)
        {
            // Ask the button to remove the fixed pressed appearance
            _controller.RemoveFixed();
        }
        #endregion
    }
}
