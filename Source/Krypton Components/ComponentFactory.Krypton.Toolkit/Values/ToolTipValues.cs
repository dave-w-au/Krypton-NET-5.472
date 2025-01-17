﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  Created by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2019 - 2019. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.472)
//  Version 5.472.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit.Values
{
    /// <summary>
    /// What will be displayed in the designer
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class ToolTipValues : HeaderValues
    {
        private LabelStyle _toolTipStyle;

        /// <summary>
        /// </summary>
        /// <param name="needPaint"></param>
        public ToolTipValues(NeedPaintHandler needPaint)
            : base(needPaint)
        {
            ResetToolTipStyle();
            ToolTipPosition = new PopupPositionValues();
        }

        public void Reset()
        {
            ResetEnableToolTips();
            ResetToolTipStyle();
            ResetToolTipPosition();
        }

        /// <summary>
        /// Make sure default values are         
        /// Gets and sets the EnableToolTips
        /// </summary>
        [DefaultValue(false)]
        public bool EnableToolTips { get; set; }

        private bool ShouldSerializeEnableToolTips()
        {
            return EnableToolTips;
        }

        public void ResetEnableToolTips()
        {
            EnableToolTips = false;
        }

        /// <summary>
        /// Gets and sets the EnableToolTips
        /// </summary>
        [Description("The orientation of the ToolTip control when it opens, and specifies how the ToolTip control behaves when it overlaps screen boundaries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PopupPositionValues ToolTipPosition { get; set; }

        private bool ShouldSerializeToolTipPosition()
        {
            return !ToolTipPosition.IsDefault;
        }

        /// <summary>
        /// Resets the ToolTipStyle property to its default value.
        /// </summary>
        public void ResetToolTipPosition()
        {
            ToolTipPosition.Reset();
        }

        #region ToolTipStyle

        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        [Description("Button tooltip label style.")]
        [DefaultValue(typeof(LabelStyle), "SuperTip")]
        public LabelStyle ToolTipStyle
        {
            get => _toolTipStyle;
            set => _toolTipStyle = value;
        }

        private bool ShouldSerializeToolTipStyle()
        {
            return ToolTipStyle != LabelStyle.SuperTip;
        }

        /// <summary>
        /// Resets the ToolTipStyle property to its default value.
        /// </summary>
        public void ResetToolTipStyle()
        {
            ToolTipStyle = LabelStyle.SuperTip;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (!ShouldSerializeEnableToolTips()
                                           && !ShouldSerializeToolTipStyle()
                                           && !ShouldSerializeToolTipPosition()
                                           && base.IsDefault
            );


        #endregion

    }
}
