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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for palette content image details.
    /// </summary>
    public class PaletteContentImage : Storage
    {
        #region Internal Classes
        private class InternalStorage
        {
            public PaletteRelativeAlign ContentImageH;
            public PaletteRelativeAlign ContentImageV;
            public PaletteImageEffect ContentEffect;
            public Color ContentImageColorMap;
            public Color ContentImageColorTo;

            /// <summary>
            /// Initialize a new instance of the InternalStorage structure.
            /// </summary>
            public InternalStorage()
            {
                // Set to default values
                ContentImageH = PaletteRelativeAlign.Inherit;
                ContentImageV = PaletteRelativeAlign.Inherit;
                ContentEffect = PaletteImageEffect.Inherit;
                ContentImageColorMap = Color.Empty;
                ContentImageColorTo = Color.Empty;
            }

            /// <summary>
            /// Gets a value indicating if all values are default.
            /// </summary>
            public bool IsDefault => ((ContentImageH == PaletteRelativeAlign.Inherit) &&
                                      (ContentImageV == PaletteRelativeAlign.Inherit) &&
                                      (ContentEffect == PaletteImageEffect.Inherit) &&
                                      (ContentImageColorMap == Color.Empty) &&
                                      (ContentImageColorTo == Color.Empty));
        }
        #endregion

        #region Instance Fields
        private InternalStorage _storage;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Browsable(false)]  // SKC: Probably a special case for not exposing this event in the designer....
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContentImage class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteContentImage(NeedPaintHandler needPaint)
        {
            // Store the provided paint notification delegate
            NeedPaint = needPaint;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => ((_storage == null) || _storage.IsDefault);

        #endregion

        #region ImageH
        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Relative horizontal alignment of content image.")]
        [DefaultValue(typeof(PaletteRelativeAlign), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteRelativeAlign ImageH
        {
            get => _storage?.ContentImageH ?? PaletteRelativeAlign.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.ContentImageH != value)
                    {
                        _storage.ContentImageH = value;
                        OnPropertyChanged("ImageH");
                        PerformNeedPaint(true);
                    }
                }
                else
                {
                    if (value != PaletteRelativeAlign.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            ContentImageH = value
                        };
                        OnPropertyChanged("ImageH");
                        PerformNeedPaint(true);
                    }
                }
            }
        }
        #endregion

        #region ImageV
        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Relative vertical alignment of content image.")]
        [DefaultValue(typeof(PaletteRelativeAlign), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteRelativeAlign ImageV
        {
            get => _storage?.ContentImageV ?? PaletteRelativeAlign.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.ContentImageV != value)
                    {
                        _storage.ContentImageV = value;
                        OnPropertyChanged("ImageV");
                        PerformNeedPaint(true);
                    }
                }
                else
                {
                    if (value != PaletteRelativeAlign.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            ContentImageV = value
                        };
                        OnPropertyChanged("ImageV");
                        PerformNeedPaint(true);
                    }
                }
            }
        }
        #endregion

        #region Effect
        /// <summary>
        /// Gets the effect applied to drawing the image.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Effect applied to drawing the image.")]
        [DefaultValue(typeof(PaletteImageEffect), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteImageEffect Effect
        {
            get => _storage?.ContentEffect ?? PaletteImageEffect.Inherit;

            set
            {
                if (_storage != null)
                {
                    if (_storage.ContentEffect != value)
                    {
                        _storage.ContentEffect = value;
                        OnPropertyChanged("Effect");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteImageEffect.Inherit)
                    {
                        _storage = new InternalStorage
                        {
                            ContentEffect = value
                        };
                        OnPropertyChanged("Effect");
                        PerformNeedPaint();
                    }
                }
            }
        }
        #endregion

        #region ImageColorMap
        /// <summary>
        /// Gets and set the image color to remap into another color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color to remap in the image.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color ImageColorMap
        {
            get => _storage?.ContentImageColorMap ?? Color.Empty;

            set
            {
                if (_storage != null)
                {
                    if (_storage.ContentImageColorMap != value)
                    {
                        _storage.ContentImageColorMap = value;
                        OnPropertyChanged("ImageColorMap");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != Color.Empty)
                    {
                        _storage = new InternalStorage
                        {
                            ContentImageColorMap = value
                        };
                        OnPropertyChanged("ImageColorMap");
                        PerformNeedPaint();
                    }
                }
            }
        }
        #endregion

        #region ImageColorTo
        /// <summary>
        /// Gets and set the color to use in place of the image map color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color to use in place of the image map.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color ImageColorTo
        {
            get => _storage?.ContentImageColorTo ?? Color.Empty;

            set
            {
                if (_storage != null)
                {
                    if (_storage.ContentImageColorTo != value)
                    {
                        _storage.ContentImageColorTo = value;
                        OnPropertyChanged("ImageColorTo");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != Color.Empty)
                    {
                        _storage = new InternalStorage
                        {
                            ContentImageColorTo = value
                        };
                        OnPropertyChanged("ImageColorTo");
                        PerformNeedPaint();
                    }
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="property">Name of the property changed.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
