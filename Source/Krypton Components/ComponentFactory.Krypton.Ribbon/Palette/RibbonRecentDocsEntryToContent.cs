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

using System.Drawing;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class RibbonRecentDocsEntryToContent : RibbonToContent
    {
        #region Instance Fields
        private readonly IPaletteRibbonText _ribbonRecentDocEntryText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonRecentDocsEntryToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        /// <param name="ribbonRecentDocEntryText">Source for ribbon recent document entry settings.</param>
        public RibbonRecentDocsEntryToContent(PaletteRibbonGeneral ribbonGeneral,
                                              IPaletteRibbonText ribbonRecentDocEntryText)
            : base(ribbonGeneral)
        {
            Debug.Assert(ribbonRecentDocEntryText != null);
            _ribbonRecentDocEntryText = ribbonRecentDocEntryText;
        }
        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
        {
            return PaletteRelativeAlign.Near;
        }

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentShortTextTrim(PaletteState state)
        {
            return PaletteTextTrim.EllipsisPath;
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            return _ribbonRecentDocEntryText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            return _ribbonRecentDocEntryText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextH(PaletteState state)
        {
            return PaletteRelativeAlign.Far;
        }

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentLongTextTrim(PaletteState state)
        {
            return PaletteTextTrim.EllipsisPath;
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            return _ribbonRecentDocEntryText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            return _ribbonRecentDocEntryText.GetRibbonTextColor(state);
        }
        #endregion
    }
}
