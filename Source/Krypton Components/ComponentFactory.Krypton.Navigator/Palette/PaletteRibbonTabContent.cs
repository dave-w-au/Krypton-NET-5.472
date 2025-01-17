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
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Implement storage for ribbon tab and content.
    /// </summary>
    public class PaletteRibbonTabContent : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonDouble _paletteTabDraw;
        private readonly PaletteNavContent _paletteContent;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonTabContent class.
        /// </summary>
        /// <param name="paletteBack">Source for inheriting palette ribbon background.</param>
        /// <param name="paletteText">Source for inheriting palette ribbon text.</param>
        /// <param name="paletteContent">Source for inheriting palette content.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonTabContent(IPaletteRibbonBack paletteBack,
                                       IPaletteRibbonText paletteText,
                                       IPaletteContent paletteContent,
                                       NeedPaintHandler needPaint)
        {
            Debug.Assert(paletteBack != null);
            Debug.Assert(paletteText != null);
            Debug.Assert(paletteContent != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _paletteTabDraw = new PaletteRibbonDouble(paletteBack, paletteText, needPaint);
            _paletteContent = new PaletteNavContent(paletteContent, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (TabDraw.IsDefault && Content.IsDefault);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public void PopulateFromBase(PaletteState state)
        {
            _paletteTabDraw.PopulateFromBase(state);
            _paletteContent.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteRibbonBack paletteBack,
                               IPaletteRibbonText paletteText,
                               IPaletteContent paletteContent)
        {
            _paletteTabDraw.SetInherit(paletteBack, paletteText);
            _paletteContent.SetInherit(paletteContent);
        }
        #endregion

        #region TabDraw
        /// <summary>
        /// Gets access to the tab drawing appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tab drawing appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonDouble TabDraw => _paletteTabDraw;

        private bool ShouldSerializeTabDraw()
        {
            return !_paletteTabDraw.IsDefault;
        }
        #endregion

        #region Content
        /// <summary>
        /// Gets access to the tab content appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tab content appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavContent Content => _paletteContent;

        private bool ShouldSerializeContent()
        {
            return !_paletteContent.IsDefault;
        }
        #endregion
    }
}
