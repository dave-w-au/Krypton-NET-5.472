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

using System;
using System.Drawing;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Acts as a proxy for a KryptonPage inside a auto hidden group.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonAutoHiddenProxyPage : KryptonPage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonAutoHiddenProxyPage class.
        /// </summary>
        public KryptonAutoHiddenProxyPage(KryptonPage page)
        {

            // We are a proxy for this cached page reference
            Page = page ?? throw new ArgumentNullException(nameof(page));

            // Text property was updated by the base class constructor, so now we update the actual referenced class
            Page.Text = Text;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Page?.Dispose();

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the page for which this is a proxy.
        /// </summary>
        public KryptonPage Page { get; }

        /// <summary>
        /// Gets and sets the page text.
        /// </summary>
        public override string Text
        {
            get => Page != null ? Page.Text : base.Text;

            set 
            {
                base.Text = value;
                if (Page != null)
                {
                    Page.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the title text for the page.
        /// </summary>
        public override string TextTitle
        {
            get => Page.TextTitle;
            set => Page.TextTitle = value;
        }

        /// <summary>
        /// Gets and sets the description text for the page.
        /// </summary>
        public override string TextDescription
        {
            get => Page.TextDescription;
            set => Page.TextDescription = value;
        }

        /// <summary>
        /// Gets and sets the small image for the page.
        /// </summary>
        public override Image ImageSmall
        {
            get => Page.ImageSmall;
            set => Page.ImageSmall = value;
        }

        /// <summary>
        /// Gets and sets the medium image for the page.
        /// </summary>
        public override Image ImageMedium
        {
            get => Page.ImageMedium;
            set => Page.ImageMedium = value;
        }

        /// <summary>
        /// Gets and sets the large image for the page.
        /// </summary>
        public override Image ImageLarge
        {
            get => Page.ImageLarge;
            set => Page.ImageLarge = value;
        }

        /// <summary>
        /// Gets and sets the page tooltip image.
        /// </summary>
        public override Image ToolTipImage
        {
            get => Page.ToolTipImage;
            set => Page.ToolTipImage = value;
        }

        /// <summary>
        /// Gets and sets the tooltip image transparent color.
        /// </summary>
        public override Color ToolTipImageTransparentColor
        {
            get => Page.ToolTipImageTransparentColor;
            set => Page.ToolTipImageTransparentColor = value;
        }

        /// <summary>
        /// Gets and sets the page tooltip title text.
        /// </summary>
        public override string ToolTipTitle
        {
            get => Page.ToolTipTitle;
            set => Page.ToolTipTitle = value;
        }

        /// <summary>
        /// Gets and sets the page tooltip body text.
        /// </summary>
        public override string ToolTipBody
        {
            get => Page.ToolTipBody;
            set => Page.ToolTipBody = value;
        }

        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        public override LabelStyle ToolTipStyle
        {
            get => Page.ToolTipStyle;
            set => Page.ToolTipStyle = value;
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        public override KryptonContextMenu KryptonContextMenu
        {
            get => Page.KryptonContextMenu;
            set => Page.KryptonContextMenu = value;
        }

        /// <summary>
        /// Gets and sets the unique name of the page.
        /// </summary>
        public override string UniqueName
        {
            get => Page.UniqueName;
            set => Page.UniqueName = value;
        }

        /// <summary>
        /// Gets the string that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Text mapping.</param>
        /// <returns>Matching string.</returns>
        public override string GetTextMapping(MapKryptonPageText mapping)
        {
            return Page.GetTextMapping(mapping);
        }

        /// <summary>
        /// Gets the image that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Image mapping.</param>
        /// <returns>Image reference.</returns>
        public override Image GetImageMapping(MapKryptonPageImage mapping)
        {
            return Page.GetImageMapping(mapping);
        }

        /// <summary>
        /// Gets and sets the set of page flags.
        /// </summary>
        public override int Flags
        {
            get => Page.Flags;
            set => Page.Flags = value;
        }

        /// <summary>
        /// Set all the provided flags to true.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public override void SetFlags(KryptonPageFlags flags)
        {
            Page.SetFlags(flags);
        }

        /// <summary>
        /// Sets all the provided flags to false.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public override void ClearFlags(KryptonPageFlags flags)
        {
            Page.ClearFlags(flags);
        }

        /// <summary>
        /// Are all the provided flags set to true.
        /// </summary>
        /// <param name="flags">Flags to test.</param>
        /// <returns>True if all provided flags are defined as true; otherwise false.</returns>
        public override bool AreFlagsSet(KryptonPageFlags flags)
        {
            return Page.AreFlagsSet(flags);
        }

        /// <summary>
        /// Gets the last value set to the Visible property.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override bool LastVisibleSet
        {
            get => Page.LastVisibleSet;
            set => Page.LastVisibleSet = value;
        }
        #endregion
    }
}
