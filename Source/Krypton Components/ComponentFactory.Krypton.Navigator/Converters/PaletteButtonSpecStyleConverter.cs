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

using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that PaletteNavButtonSpecStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteNavButtonSpecStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavButtonSpecStyleConverter clas.
        /// </summary>
        public PaletteNavButtonSpecStyleConverter()
            : base(typeof(PaletteNavButtonSpecStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(PaletteNavButtonSpecStyle.Generic,            "Generic"),
            new Pair(PaletteNavButtonSpecStyle.ArrowLeft,          "Arrow Left"),
            new Pair(PaletteNavButtonSpecStyle.ArrowRight,         "Arrow Right"),
            new Pair(PaletteNavButtonSpecStyle.ArrowUp,            "Arrow Up"),
            new Pair(PaletteNavButtonSpecStyle.ArrowDown,          "Arrow Down"),
            new Pair(PaletteNavButtonSpecStyle.DropDown,           "Drop Down"),
            new Pair(PaletteNavButtonSpecStyle.PinVertical,        "Pin Vertical"),
            new Pair(PaletteNavButtonSpecStyle.PinHorizontal,      "Pin Horizontal"),
            new Pair(PaletteNavButtonSpecStyle.FormClose,          "Form Close"),
            new Pair(PaletteNavButtonSpecStyle.FormMax,            "Form Max"),
            new Pair(PaletteNavButtonSpecStyle.FormMin,            "Form Min"),
            new Pair(PaletteNavButtonSpecStyle.FormRestore,        "Form Restore"),
            new Pair(PaletteNavButtonSpecStyle.PendantClose,       "Pendant Close"),
            new Pair(PaletteNavButtonSpecStyle.PendantMin,         "Pendant Min"),
            new Pair(PaletteNavButtonSpecStyle.PendantRestore,     "Pendant Restore"),
            new Pair(PaletteNavButtonSpecStyle.WorkspaceMaximize,  "Workspace Maximize"),
            new Pair(PaletteNavButtonSpecStyle.WorkspaceRestore,   "Workspace Restore"),
            new Pair(PaletteNavButtonSpecStyle.RibbonMinimize,     "Ribbon Minimize"),
            new Pair(PaletteNavButtonSpecStyle.RibbonExpand,       "Ribbon Expand")};

        #endregion
    }
}
