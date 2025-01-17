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
    /// Storage for KryptonContextMenuItem highlight state values.
    /// </summary>
    public class PaletteContextMenuItemStateHighlight : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuItemStateHighlight class.
        /// </summary>
        /// <param name="redirect">Redirector for inheriting values.</param>
        public PaletteContextMenuItemStateHighlight(PaletteContextMenuRedirect redirect)
            : this(redirect.ItemHighlight, redirect.ItemSplit)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuItemStateHighlight class.
        /// </summary>
        /// <param name="redirect">Redirector for inheriting values.</param>
        public PaletteContextMenuItemStateHighlight(PaletteContextMenuItemStateRedirect redirect)
            : this(redirect.ItemHighlight, redirect.ItemSplit)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuItemStateHighlight class.
        /// </summary>
        /// <param name="redirectItemHighlight">Redirector for the ItemHighlight.</param>
        /// <param name="redirectItemSplit">Redirector for the ItemSplit.</param>
        public PaletteContextMenuItemStateHighlight(PaletteDoubleMetricRedirect redirectItemHighlight,
                                                    PaletteDoubleRedirect redirectItemSplit)
        {
            ItemHighlight = new PaletteDoubleMetric(redirectItemHighlight);
            ItemSplit = new PaletteDouble(redirectItemSplit);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => ItemHighlight.IsDefault &&
                                          ItemSplit.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="common">Reference to common settings.</param>
        /// <param name="state">State to inherit.</param>
        public void PopulateFromBase(KryptonPaletteCommon common,
                                     PaletteState state)
        {
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuItemHighlight;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuItemHighlight;
            ItemHighlight.PopulateFromBase(state);
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuSeparator;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuSeparator;
            ItemSplit.PopulateFromBase(state);
        }
        #endregion

        #region ItemHighlight
        /// <summary>
        /// Gets access to the item highlight appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item highlight appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleMetric ItemHighlight { get; }

        private bool ShouldSerializeItemHighlight()
        {
            return !ItemHighlight.IsDefault;
        }
        #endregion

        #region ItemSplit
        /// <summary>
        /// Gets access to the item split appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item split appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble ItemSplit { get; }

        private bool ShouldSerializeItemSplit()
        {
            return !ItemSplit.IsDefault;
        }
        #endregion
    }
}
