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
    /// Storage for palette ribbon quick access bar mini version.
    /// </summary>
    public class KryptonPaletteRibbonQATMinibar : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonBackInheritRedirect _stateInherit;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonQATMinibar class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonQATMinibar(PaletteRedirect redirect,
                                              NeedPaintHandler needPaint) 
        {
            // Create the storage objects
            _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonQATMinibar);
            StateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
            StateActive = new PaletteRibbonBack(StateCommon, needPaint);
            StateInactive = new PaletteRibbonBack(StateCommon, needPaint);
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
                                          StateActive.IsDefault &&
                                          StateInactive.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            StateActive.PopulateFromBase(PaletteState.Normal);
            StateInactive.PopulateFromBase(PaletteState.Disabled);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon quick access toolbar minibar values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon quick access minibar values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateCommon { get; }

        private bool ShouldSerializeStateCommon()
        {
            return !StateCommon.IsDefault;
        }
        #endregion

        #region StateActive
        /// <summary>
        /// Gets access to the active ribbon quick access toolbar minibar values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining active ribbon quick access minibar values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateActive { get; }

        private bool ShouldSerializeStateActive()
        {
            return !StateActive.IsDefault;
        }
        #endregion

        #region StateInactive
        /// <summary>
        /// Gets access to the inactive ribbon quick access toolbar minibar values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining inactive ribbon quick access minibar values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateInactive { get; }

        private bool ShouldSerializeStateInactive()
        {
            return !StateInactive.IsDefault;
        }
        #endregion
    }
}
