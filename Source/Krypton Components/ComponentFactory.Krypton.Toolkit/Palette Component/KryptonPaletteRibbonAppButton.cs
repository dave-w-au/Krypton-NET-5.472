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
    /// Storage for palette ribbon scroller states.
    /// </summary>
    public class KryptonPaletteRibbonAppButton : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonBackInheritRedirect _stateInherit;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonAppButton class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonAppButton(PaletteRedirect redirect,
                                             NeedPaintHandler needPaint) 
        {
            // Create the storage objects
            _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonAppButton);
            StateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
            StateNormal = new PaletteRibbonBack(StateCommon, needPaint);
            StateTracking = new PaletteRibbonBack(StateCommon, needPaint);
            StatePressed = new PaletteRibbonBack(StateCommon, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _stateInherit.SetRedirector(redirect);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => StateCommon.IsDefault &&
                                          StateNormal.IsDefault &&
                                          StateTracking.IsDefault &&
                                          StatePressed.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            StateNormal.PopulateFromBase(PaletteState.Normal);
            StateTracking.PopulateFromBase(PaletteState.Tracking);
            StatePressed.PopulateFromBase(PaletteState.Pressed);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon application button appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon application button appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateCommon { get; }

        private bool ShouldSerializeStateCommon()
        {
            return !StateCommon.IsDefault;
        }
        #endregion
    
        #region StateNormal
        /// <summary>
        /// Gets access to the normal ribbon application button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal ribbon application button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateNormal { get; }

        private bool ShouldSerializeStateNormal()
        {
            return !StateNormal.IsDefault;
        }
        #endregion

        #region StateTracking
        /// <summary>
        /// Gets access to the tracking ribbon application button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking ribbon application button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateTracking { get; }

        private bool ShouldSerializeStateTracking()
        {
            return !StateTracking.IsDefault;
        }
        #endregion

        #region StatePressed
        /// <summary>
        /// Gets access to the pressed ribbon application button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed ribbon application button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StatePressed { get; }

        private bool ShouldSerializeStatePressed()
        {
            return !StatePressed.IsDefault;
        }
        #endregion
    }
}
