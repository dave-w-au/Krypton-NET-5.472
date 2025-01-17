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

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Implement storage for a ribbon state.
    /// </summary>
    public class PaletteRibbonJustGroup : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonBack _ribbonGroupNormalBorder;
        private readonly PaletteRibbonDouble _ribbonGroupNormalTitle;
        private readonly PaletteRibbonBack _ribbonGroupCollapsedBorder;
        private readonly PaletteRibbonBack _ribbonGroupCollapsedBack;
        private readonly PaletteRibbonBack _ribbonGroupCollapsedFrameBorder;
        private readonly PaletteRibbonBack _ribbonGroupCollapsedFrameBack;
        private readonly PaletteRibbonText _ribbonGroupCollapsedText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonJustGroup class.
        /// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonJustGroup(PaletteRibbonRedirect inherit,
                                      NeedPaintHandler needPaint)
        {
            Debug.Assert(inherit != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _ribbonGroupNormalBorder = new PaletteRibbonBack(inherit.RibbonGroupNormalBorder, needPaint);
            _ribbonGroupNormalTitle = new PaletteRibbonDouble(inherit.RibbonGroupNormalTitle, inherit.RibbonGroupNormalTitle, needPaint);
            _ribbonGroupCollapsedBorder = new PaletteRibbonBack(inherit.RibbonGroupCollapsedBorder, needPaint);
            _ribbonGroupCollapsedBack = new PaletteRibbonBack(inherit.RibbonGroupCollapsedBack, needPaint);
            _ribbonGroupCollapsedFrameBorder = new PaletteRibbonBack(inherit.RibbonGroupCollapsedFrameBorder, needPaint);
            _ribbonGroupCollapsedFrameBack = new PaletteRibbonBack(inherit.RibbonGroupCollapsedFrameBack, needPaint);
            _ribbonGroupCollapsedText = new PaletteRibbonText(inherit.RibbonGroupCollapsedText, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (RibbonGroupNormalBorder.IsDefault &&
                                           RibbonGroupNormalTitle.IsDefault &&
                                           RibbonGroupCollapsedBorder.IsDefault &&
                                           RibbonGroupCollapsedBack.IsDefault &&
                                           RibbonGroupCollapsedFrameBorder.IsDefault &&
                                           RibbonGroupCollapsedFrameBack.IsDefault &&
                                           RibbonGroupCollapsedText.IsDefault);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            _ribbonGroupNormalBorder.PopulateFromBase(state);
            _ribbonGroupNormalTitle.PopulateFromBase(state);
            _ribbonGroupCollapsedBorder.PopulateFromBase(state);
            _ribbonGroupCollapsedBack.PopulateFromBase(state);
            _ribbonGroupCollapsedFrameBorder.PopulateFromBase(state);
            _ribbonGroupCollapsedFrameBack.PopulateFromBase(state);
            _ribbonGroupCollapsedText.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public virtual void SetInherit(PaletteRibbonRedirect inherit)
        {
            _ribbonGroupNormalBorder.SetInherit(inherit.RibbonGroupNormalBorder);
            _ribbonGroupNormalTitle.SetInherit(inherit.RibbonGroupNormalTitle, inherit.RibbonGroupNormalTitle);
            _ribbonGroupCollapsedBorder.SetInherit(inherit.RibbonGroupCollapsedBorder);
            _ribbonGroupCollapsedBack.SetInherit(inherit.RibbonGroupCollapsedBack);
            _ribbonGroupCollapsedFrameBorder.SetInherit(inherit.RibbonGroupCollapsedFrameBorder);
            _ribbonGroupCollapsedFrameBack.SetInherit(inherit.RibbonGroupCollapsedFrameBack);
            _ribbonGroupCollapsedText.SetInherit(inherit.RibbonGroupCollapsedText);
        }
        #endregion

        #region RibbonGroupNormalBorder
        /// <summary>
        /// Gets access to the ribbon group normal border palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group normal border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupNormalBorder => _ribbonGroupNormalBorder;

        private bool ShouldSerializeRibbonGroupNormalBorder()
        {
            return !_ribbonGroupNormalBorder.IsDefault;
        }
        #endregion

        #region RibbonGroupNormalTitle
        /// <summary>
        /// Gets access to the ribbon group normal title palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group normal title appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonDouble RibbonGroupNormalTitle => _ribbonGroupNormalTitle;

        private bool ShouldSerializeRibbonGroupNormalTitle()
        {
            return !_ribbonGroupNormalTitle.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedBorder
        /// <summary>
        /// Gets access to the ribbon group collapsed border palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedBorder => _ribbonGroupCollapsedBorder;

        private bool ShouldSerializeRibbonGroupCollapsedBorder()
        {
            return !_ribbonGroupCollapsedBorder.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedBack
        /// <summary>
        /// Gets access to the ribbon group collapsed background palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedBack => _ribbonGroupCollapsedBack;

        private bool ShouldSerializeRibbonGroupCollapsedBack()
        {
            return !_ribbonGroupCollapsedBack.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedFrameBorder
        /// <summary>
        /// Gets access to the ribbon group collapsed frame border palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed frame border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedFrameBorder => _ribbonGroupCollapsedFrameBorder;

        private bool ShouldSerializeRibbonGroupCollapsedFrameBorder()
        {
            return !_ribbonGroupCollapsedFrameBorder.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedFrameBack
        /// <summary>
        /// Gets access to the ribbon group collapsed frame background palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed frame background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedFrameBack => _ribbonGroupCollapsedFrameBack;

        private bool ShouldSerializeRibbonGroupCollapsedFrameBack()
        {
            return !_ribbonGroupCollapsedFrameBack.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedText
        /// <summary>
        /// Gets access to the ribbon group collapsed text palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonGroupCollapsedText => _ribbonGroupCollapsedText;

        private bool ShouldSerializeRibbonGroupCollapsedText()
        {
            return !_ribbonGroupCollapsedText.IsDefault;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Handle a change event from palette source.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="needLayout">True if a layout is also needed.</param>
        protected void OnNeedPaint(object sender, bool needLayout)
        {
            // Pass request from child to our own handler
            PerformNeedPaint(needLayout);
        }
        #endregion
    }
}
