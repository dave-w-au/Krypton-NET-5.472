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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Map button spec tooltip value to content values.
    /// </summary>
    public class ButtonSpecToContent : IContentValues
    {
        #region Instance Fields
        private readonly ButtonSpec _buttonSpec;
        private readonly IPalette _palette;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageToTooltipMapping class.
        /// </summary>
        /// <param name="palette">Palette for sourcing information.</param>
        /// <param name="buttonSpec">Source button spec instance.</param>
        public ButtonSpecToContent(IPalette palette,
                                   ButtonSpec buttonSpec)
        {
            Debug.Assert(palette != null);
            Debug.Assert(buttonSpec != null);
            _palette = palette;
            _buttonSpec = buttonSpec;
        }
        #endregion

        #region HasContent
        /// <summary>
        /// Gets a value indicating if the mapping produces any content.
        /// </summary>
        public bool HasContent => (GetImage(PaletteState.Normal) != null) ||
                                  !string.IsNullOrEmpty(GetShortText()) ||
                                  !string.IsNullOrEmpty(GetLongText());

        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            return _buttonSpec.ToolTipImage;
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return _buttonSpec.ToolTipImageTransparentColor;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            return _buttonSpec.GetToolTipTitle(_palette);
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            return _buttonSpec.ToolTipBody;
        }
        #endregion
    }
}
