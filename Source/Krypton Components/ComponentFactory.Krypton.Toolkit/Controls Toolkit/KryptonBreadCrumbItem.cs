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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Krypton object used to represent nodes in a hierarchical bread crumb data structure.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonBreadCrumb), "ToolboxBitmaps.KryptonBreadCrumbItem.bmp")]
    [DesignTimeVisible(false)]
    [Designer(typeof(KryptonBreadCrumbItemDesigner))]
    public class KryptonBreadCrumbItem : KryptonListItem
    {
        #region Type Definitons
        /// <summary>
        /// Manages a collection of KryptonBreadCrumbItems
        /// </summary>
        [Editor(typeof(KryptonBreadCrumbItemsEditor), typeof(UITypeEditor))]
        public class BreadCrumbItems : TypedCollection<KryptonBreadCrumbItem>
        {
            #region Instance Fields
            private readonly KryptonBreadCrumbItem _owner;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the BreadCrumbItems class.
            /// </summary>
            /// <param name="owner">Reference to owning item.</param>
            internal BreadCrumbItems(KryptonBreadCrumbItem owner)
            {
                _owner = owner;
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets the item with the provided unique name.
            /// </summary>
            /// <param name="name">Name of the ribbon tab instance.</param>
            /// <returns>Item at specified index.</returns>
            public override KryptonBreadCrumbItem this[string name]
            {
                get
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        foreach (KryptonBreadCrumbItem item in this)
                        {
                            string text = item.ShortText;
                            if (!string.IsNullOrEmpty(text) && (text == name))
                            {
                                return item;
                            }

                            text = item.LongText;
                            if (!string.IsNullOrEmpty(text) && (text == name))
                            {
                                return item;
                            }
                        }
                    }

                    return null;
                }
            }
            #endregion

            #region Protected
            /// <summary>
            /// Raises the Inserting event.
            /// </summary>
            /// <param name="e">A KryptonRibbonTabEventArgs instance containing event data.</param>
            protected override void OnInserting(TypedCollectionEventArgs<KryptonBreadCrumbItem> e)
            {
                // Setup parent relationship
                e.Item.Parent = _owner;

                base.OnInserting(e);
            }

            /// <summary>
            /// Raises the Inserted event.
            /// </summary>
            /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
            protected override void OnInserted(TypedCollectionEventArgs<KryptonBreadCrumbItem> e)
            {
                base.OnInserted(e);

                // Notify a change in the owners items property
                _owner.OnPropertyChanged(new PropertyChangedEventArgs("Items"));
            }

            /// <summary>
            /// Raises the Removed event.
            /// </summary>
            /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
            protected override void OnRemoved(TypedCollectionEventArgs<KryptonBreadCrumbItem> e)
            {
                base.OnRemoved(e);

                // Clear down parent relationship
                e.Item.Parent = null;

                // Notify a change in the owners items property
                _owner.OnPropertyChanged(new PropertyChangedEventArgs("Items"));
            }

            /// <summary>
            /// Raises the Clearing event.
            /// </summary>
            /// <param name="e">An EventArgs instance containing event data.</param>
            protected override void OnClearing(EventArgs e)
            {
                // Clear down parent relationship
                foreach (KryptonBreadCrumbItem child in this)
                {
                    child.Parent = null;
                }

                base.OnClearing(e);
            }

            /// <summary>
            /// Raises the Cleared event.
            /// </summary>
            /// <param name="e">An EventArgs instance containing event data.</param>
            protected override void OnCleared(EventArgs e)
            {
                base.OnCleared(e);

                // Notify a change in the owners items property
                _owner.OnPropertyChanged(new PropertyChangedEventArgs("Items"));
            }
            #endregion
        };
        #endregion

        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumbItem class.
        /// </summary>
        public KryptonBreadCrumbItem()
            : this("ListItem", null, null, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumbItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        public KryptonBreadCrumbItem(string shortText)
            : this(shortText, null, null, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumbItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        /// <param name="longText">Initial long text value.</param>
        public KryptonBreadCrumbItem(string shortText, string longText)
            : this(shortText, longText, null, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumbItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        /// <param name="longText">Initial long text value.</param>
        /// <param name="image">Initial image value.</param>
        public KryptonBreadCrumbItem(string shortText,
                                     string longText,
                                     Image image)
            : this(shortText, longText, image, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumbItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        /// <param name="longText">Initial long text value.</param>
        /// <param name="image">Initial image value.</param>
        /// <param name="imageTransparentColor">Initial transparent image color.</param>
        public KryptonBreadCrumbItem(string shortText,
                                     string longText,
                                     Image image,
                                     Color imageTransparentColor)
            : base(shortText, longText, image, imageTransparentColor)
        {
            // Create child collection
            Items = new BreadCrumbItems(this);
        }

        /// <summary>
        /// Gets the string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + Items.Count.ToString() + ") " + ShortText;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the colletion of child items.
        /// </summary>
        [Category("Data")]
        [Description("Collection of child items.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public BreadCrumbItems Items { get; }

        /// <summary>
        /// Gets access to the Parent item in the hierarchy.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonBreadCrumbItem Parent { get; internal set; }

        #endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // Raise event via the base class
            base.OnPropertyChanged(e);

            // If we have a parent instance
            KryptonBreadCrumbItem parent = Parent;
            if (parent != null)
            {
                // Find the root instance
                while (parent.Parent != null)
                {
                    parent = parent.Parent;
                }

                // Raise event in the root
                parent.OnPropertyChanged(e);
            }
        }
        #endregion     
    }
}
