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
    /// Storage for palette ribbon group text states.
    /// </summary>
    public class KryptonPaletteRibbonGroupBaseText : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonTextInheritRedirect _stateInherit;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupBaseText class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="textStyle">Inherit text style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupBaseText(PaletteRedirect redirect,
                                                 PaletteRibbonTextStyle textStyle,
                                                 NeedPaintHandler needPaint) 
        {
            // Create the storage objects
            _stateInherit = new PaletteRibbonTextInheritRedirect(redirect, textStyle);
            StateCommon = new PaletteRibbonText(_stateInherit, needPaint);
            StateNormal = new PaletteRibbonText(StateCommon, needPaint);
            StateDisabled = new PaletteRibbonText(StateCommon, needPaint);
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
                                          StateDisabled.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            StateNormal.PopulateFromBase(PaletteState.Normal);
            StateDisabled.PopulateFromBase(PaletteState.Disabled);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon group text appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon group text appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateCommon { get; }

        private bool ShouldSerializeStateCommon()
        {
            return !StateCommon.IsDefault;
        }
        #endregion

        #region StateNormal
        /// <summary>
        /// Gets access to the normal ribbon group text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal ribbon group text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateNormal { get; }

        private bool ShouldSerializeStateNormal()
        {
            return !StateNormal.IsDefault;
        }
        #endregion

        #region StateDisabled
        /// <summary>
        /// Gets access to the tracking ribbon group text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking ribbon group text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateDisabled { get; }

        private bool ShouldSerializeStateDisabled()
        {
            return !StateDisabled.IsDefault;
        }
        #endregion
    }
}
