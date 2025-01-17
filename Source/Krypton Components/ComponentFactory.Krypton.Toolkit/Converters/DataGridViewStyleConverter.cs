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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that DataGridViewStyle values appear as neat text at design time.
    /// </summary>
    internal class DataGridViewStyleConverter : StringLookupConverter
    {
        #region Static Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewStyleConverter clas.
        /// </summary>
        public DataGridViewStyleConverter()
            : base(typeof(DataGridViewStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs { get; } =
        { new Pair(DataGridViewStyle.List,       "List"),
            new Pair(DataGridViewStyle.Sheet,      "Sheet"),
            new Pair(DataGridViewStyle.Custom1,    "Custom1"),
            new Pair(DataGridViewStyle.Custom2,    "Custom2"),
            new Pair(DataGridViewStyle.Custom3,    "Custom3"),
            new Pair(DataGridViewStyle.Mixed,      "Mixed")

        };

        #endregion
    }
}
