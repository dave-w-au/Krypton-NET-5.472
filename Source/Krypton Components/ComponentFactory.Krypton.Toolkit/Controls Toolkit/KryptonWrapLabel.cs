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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Display a windows forms label but with Krypton palette text and font settings.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonWrapLabel), "ToolboxBitmaps.KryptonWrapLabel.bmp")]
    [DefaultProperty("Text")]
    [DefaultBindingProperty("Text")]
    [Designer(typeof(KryptonWrapLabelDesigner))]
    [DesignerCategory("code")]
    [Description("Displays descriptive information.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public sealed class KryptonWrapLabel : Label
    {
        #region Static Field
        private static MethodInfo _miPTB;
        #endregion

        #region Instance Fields
        private IPalette _localPalette;
        private IPalette _palette;
        private PaletteMode _paletteMode;
        private readonly PaletteRedirect _redirector;
        private LabelStyle _labelStyle;
        private PaletteContentStyle _labelContentStyle;
        private KryptonContextMenu _kryptonContextMenu;
        private bool _globalEvents;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the palette changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the Palette property is changed.")]
        public event EventHandler PaletteChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonWrapLabel class.
        /// </summary>
        public KryptonWrapLabel()
        {
            // We use double buffering to reduce drawing flicker
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            // We need to repaint entire control whenever resized
            SetStyle(ControlStyles.ResizeRedraw, true);

            // Yes, we want to be drawn double buffered by default
            DoubleBuffered = true;

            // Create the state storage
            StateCommon = new PaletteWrapLabel(this);
            StateNormal = new PaletteWrapLabel(this);
            StateDisabled = new PaletteWrapLabel(this);

            // Set the palette to the defaults as specified by the manager
            _localPalette = null;
            SetPalette(KryptonManager.CurrentGlobalPalette);
            _paletteMode = PaletteMode.Global;
            AttachGlobalEvents();

            // Create constant target for resolving palette delegates
            _redirector = CreateRedirector();

            // Default properties
            SetLabelStyle(LabelStyle.NormalControl);
            AutoSize = true;
            TabStop = false;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Must unhook from the palette paint event
                if (_palette != null)
                {
                    _palette.PalettePaint -= OnPaletteNeedPaint;
                    _palette.BasePaletteChanged -= OnBaseChanged;
                    _palette.BaseRendererChanged -= OnBaseChanged;
                }

                UnattachGlobalEvents();

                _palette = null;
                _localPalette = null;
                _redirector.Target = null;
                Renderer = null;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the tab order of the KryptonSplitterPanel within its KryptonSplitContainer.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int TabIndex
        {
            get => base.TabIndex;
            set => base.TabIndex = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can give the focus to this KryptonSplitterPanel using the TAB key.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool TabStop
        {
            get => base.TabStop;
            set => base.TabStop = value;
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        /// <summary>
        /// Determines if the label has a border.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override BorderStyle BorderStyle
        {
            get => base.BorderStyle;
            set => base.BorderStyle = value;
        }

        /// <summary>
        /// Determines appearance of the control when the mouse pressed on the label.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public new FlatStyle FlatStyle
        {
            get => base.FlatStyle;
            set => base.FlatStyle = value;
        }

        /// <summary>
        /// Gets and sets the automatic resize of the control to fit contents.
        /// </summary>
        [DefaultValue(true)]
        public override bool AutoSize
        {
            get => base.AutoSize;
            set => base.AutoSize = value;
        }

        /// <summary>
        /// Gets access to the common wrap label appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common wrap label appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteWrapLabel StateCommon { get; }

        private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

        /// <summary>
        /// Gets access to the disabled wrap label appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled wrap label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteWrapLabel StateDisabled { get; }

        private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

        /// <summary>
        /// Gets access to the normal wrap label appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal wrap label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteWrapLabel StateNormal { get; }

        private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;


        /// <summary>
        /// Gets and sets the label style.
        /// </summary>
        [Category("Visuals")]
        [Description("Label style.")]
        public LabelStyle LabelStyle
        {
            get => _labelStyle;

            set
            {
                if (_labelStyle != value)
                {
                    _labelStyle = value;
                    SetLabelStyle(_labelStyle);
                    Refresh();
                }
            }
        }

        private bool ShouldSerializeLabelStyle() => (LabelStyle != LabelStyle.NormalControl);

        private void ResetLabelStyle() => LabelStyle = LabelStyle.NormalControl;

        /// <summary>
        /// Gets or sets the palette to be applied.
        /// </summary>
        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        public PaletteMode PaletteMode
        {
            [System.Diagnostics.DebuggerStepThrough]
            get => _paletteMode;

            set
            {
                if (_paletteMode != value)
                {
                    // Action depends on new value
                    switch (value)
                    {
                        case PaletteMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'Palette' property in order to get the custom mode
                            break;
                        default:
                            // Use the new value
                            _paletteMode = value;

                            // Get a reference to the standard palette from its name
                            _localPalette = null;
                            SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));

                            // Must raise event to change palette in redirector
                            OnPaletteChanged(EventArgs.Empty);
                            NeedPaint(true);
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializePaletteMode() => (PaletteMode != PaletteMode.Global);

        /// <summary>
        /// Resets the PaletteMode property to its default value.
        /// </summary>
        public void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

        /// <summary>
        /// Gets and sets the custom palette implementation.
        /// </summary>
        [Category("Visuals")]
        [Description("Custom palette applied to drawing.")]
        [DefaultValue(null)]
        public IPalette Palette
        {
            [System.Diagnostics.DebuggerStepThrough]
            get => _localPalette;

            set
            {
                // Only interested in changes of value
                if (_localPalette != value)
                {
                    // Remember the starting palette
                    IPalette old = _localPalette;

                    // Use the provided palette value
                    SetPalette(value);

                    // If no custom palette is required
                    if (value == null)
                    {
                        // No custom palette, so revert back to the global setting
                        _paletteMode = PaletteMode.Global;

                        // Get the appropriate palette for the global mode
                        _localPalette = null;
                        SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));
                    }
                    else
                    {
                        // No longer using a standard palette
                        _localPalette = value;
                        _paletteMode = PaletteMode.Custom;
                    }

                    // If real change has occurred
                    if (old != _localPalette)
                    {
                        // Raise the change event
                        OnPaletteChanged(EventArgs.Empty);
                        NeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Resets the Palette property to its default value.
        /// </summary>
        public void ResetPalette() => PaletteMode = PaletteMode.Global;

        /// <summary>
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut menu to show when the user right-clicks the page.")]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get => _kryptonContextMenu;

            set
            {
                if (_kryptonContextMenu != value)
                {
                    if (_kryptonContextMenu != null)
                    {
                        _kryptonContextMenu.Closed -= OnContextMenuClosed;
                        _kryptonContextMenu.Disposed -= OnKryptonContextMenuDisposed;
                    }

                    _kryptonContextMenu = value;

                    if (_kryptonContextMenu != null)
                    {
                        _kryptonContextMenu.Closed += OnContextMenuClosed;
                        _kryptonContextMenu.Disposed += OnKryptonContextMenuDisposed;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the resolved palette to actually use when drawing.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IPalette GetResolvedPalette() => _palette;

        /// <summary>
        /// Gets access to the current renderer.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IRenderer Renderer
        {
            [System.Diagnostics.DebuggerStepThrough]
            get;
            private set;
        }

        /// <summary>
        /// Create a tool strip renderer appropriate for the current renderer/palette pair.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ToolStripRenderer CreateToolStripRenderer() => Renderer.RenderToolStrip(GetResolvedPalette());

        /// <summary>
        /// Update the font property.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UpdateFont()
        {
            Font font;
            Color textColor;
            PaletteTextHint hint;
            PaletteState ps = PaletteState.Normal;

            // Get values from correct enabled/disabled state
            if (Enabled)
            {
                font = StateNormal.Font;
                textColor = StateNormal.TextColor;
                hint = StateNormal.Hint;
            }
            else
            {
                font = StateDisabled.Font;
                textColor = StateDisabled.TextColor;
                hint = StateDisabled.Hint;
                ps = PaletteState.Disabled;
            }

            // Recover font from state common or as last resort the inherited palette
            if (font == null)
            {
                font = StateCommon.Font ?? _redirector.GetContentShortTextFont(_labelContentStyle, ps);
            }

            // Recover text color from state common or as last resort the inherited palette
            if (textColor == Color.Empty)
            {
                textColor = StateCommon.TextColor;
                if (textColor == Color.Empty)
                {
                    textColor = _redirector.GetContentShortTextColor1(_labelContentStyle, ps);
                }
            }

            // Recover text hint from state common or as last resort the inherited palette
            if (hint == PaletteTextHint.Inherit)
            {
                hint = StateCommon.Hint;
                if (hint == PaletteTextHint.Inherit)
                {
                    hint = _redirector.GetContentShortTextHint(_labelContentStyle, ps);
                }
            }

            // Only update the font when the control is created
            if (Handle != IntPtr.Zero)
            {
                Font = font;
            }
        }

        /// <summary>
        /// Attach the control to global events.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AttachGlobalEvents()
        {
            if (!_globalEvents)
            {
                UpdateGlobalEvents(true);
                _globalEvents = true;
            }
        }

        /// <summary>
        /// Attach the control to global events.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UnattachGlobalEvents()
        {
            if (_globalEvents)
            {
                UpdateGlobalEvents(false);
                _globalEvents = false;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        private void OnPaletteChanged(EventArgs e)
        {
            // Update the redirector with latest palette
            _redirector.Target = _palette;

            // Layout and repaint with new settings
            NeedPaint(true);

            PaletteChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Font font;
            Color textColor;
            PaletteTextHint hint;
            PaletteState ps = PaletteState.Normal;

            // Get values from correct enabled/disabled state
            if (Enabled)
            {
                font = StateNormal.Font;
                textColor = StateNormal.TextColor;
                hint = StateNormal.Hint;
            }
            else
            {
                font = StateDisabled.Font;
                textColor = StateDisabled.TextColor;
                hint = StateDisabled.Hint;
                ps = PaletteState.Disabled;
            }

            // Recover font from state common or as last resort the inherited palette
            if (font == null)
            {
                font = StateCommon.Font ?? _redirector.GetContentShortTextFont(_labelContentStyle, ps);
            }

            // Recover text color from state common or as last resort the inherited palette
            if (textColor == Color.Empty)
            {
                textColor = StateCommon.TextColor;
                if (textColor == Color.Empty)
                {
                    textColor = _redirector.GetContentShortTextColor1(_labelContentStyle, ps);
                }
            }

            // Recover text hint from state common or as last resort the inherited palette
            if (hint == PaletteTextHint.Inherit)
            {
                hint = StateCommon.Hint;
                if (hint == PaletteTextHint.Inherit)
                {
                    hint = _redirector.GetContentShortTextHint(_labelContentStyle, ps);
                }
            }

            // Only update the font when the control is created
            if (Handle != IntPtr.Zero)
            {
                Font = font;
            }

            ForeColor = textColor;
            e.Graphics.TextRenderingHint = CommonHelper.PaletteTextHintToRenderingHint(hint);

            base.OnPaint(e);
        }

        /// <summary>
        /// Raises the PaintBackground event.
        /// </summary>
        /// <param name="pEvent">An PaintEventArgs containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs pEvent)
        {
            // Do we have a parent control and we need to paint background?
            if (Parent != null)
            {
                // Only grab the required reference once
                if (_miPTB == null)
                {
                    // Use reflection so we can call the Windows Forms internal method for painting parent background
                    _miPTB = typeof(Control).GetMethod("PaintTransparentBackground",
                                                       BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                                                       null, CallingConventions.HasThis,
                                                       new Type[] { typeof(PaintEventArgs), typeof(Rectangle), typeof(Region) },
                                                       null);
                }

                _miPTB.Invoke(this, new object[] { pEvent, ClientRectangle, null });
            }
            else
            {
                base.OnPaintBackground(pEvent);
            }
        }

        /// <summary>
        /// Create the redirector instance.
        /// </summary>
        /// <returns>PaletteRedirect derived class.</returns>
        private PaletteRedirect CreateRedirector()
        {
            return new PaletteRedirect(_palette);
        }

        /// <summary>
        /// Update the view elements based on the requested label style.
        /// </summary>
        /// <param name="style">New label style.</param>
        private void SetLabelStyle(LabelStyle style)
        {
            _labelContentStyle = CommonHelper.ContentStyleFromLabelStyle(style);
        }

        /// <summary>
        /// Update global event attachments.
        /// </summary>
        /// <param name="attach">True if attaching; otherwise false.</param>
        private void UpdateGlobalEvents(bool attach)
        {
            if (attach)
            {
                KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
                SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
            }
            else
            {
                KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
                SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
            }
        }

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A Message, passed by reference, that represents the window message to process.</param>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>True is handled; otherwise false.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // If we have a defined context menu then need to check for matching shortcut
            if (KryptonContextMenu != null)
            {
                if (KryptonContextMenu.ProcessShortcut(keyData))
                {
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            // We need to snoop the need to show a context menu
            if (m.Msg == PI.WM_.CONTEXTMENU)
            {
                // Only interested in overriding the behavior when we have a krypton context menu...
                if (KryptonContextMenu != null)
                {
                    // Extract the screen mouse position (if might not actually be provided)
                    Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                    // If keyboard activated, the menu position is centered
                    if (((int)((long)m.LParam)) == -1)
                    {
                        mousePt = new Point(Width / 2, Height / 2);
                    }
                    else
                    {
                        mousePt = PointToClient(mousePt);

                        // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                        // of the showing context menu just like it happens for a ContextMenuStrip.
                        mousePt.X -= 1;
                        mousePt.Y -= 1;
                    }

                    // If the mouse position is within our client area
                    if (ClientRectangle.Contains(mousePt))
                    {
                        // Show the context menu
                        KryptonContextMenu.Show(this, PointToScreen(mousePt));

                        // We eat the message!
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Called when a context menu has just been closed.
        /// </summary>
        private void ContextMenuClosed()
        {
        }
        #endregion

        #region Implementation
        private void SetPalette(IPalette palette)
        {
            if (palette != _palette)
            {
                // Unhook from current palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= OnPaletteNeedPaint;
                    _palette.BasePaletteChanged -= OnBaseChanged;
                    _palette.BaseRendererChanged -= OnBaseChanged;
                }

                // Remember the new palette
                _palette = palette;

                // Get the renderer associated with the palette
                Renderer = _palette.GetRenderer();

                // Hook to new palette events
                if (_palette != null)
                {
                    _palette.PalettePaint += OnPaletteNeedPaint;
                    _palette.BasePaletteChanged += OnBaseChanged;
                    _palette.BaseRendererChanged += OnBaseChanged;
                }
            }
        }

        private void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e) => NeedPaint(e);

        // Change in base renderer or base palette require we fetch the latest renderer
        private void OnBaseChanged(object sender, EventArgs e) => Renderer = _palette.GetRenderer();

        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            // We only care if we are using the global palette
            if (PaletteMode == PaletteMode.Global)
            {
                // Update ourself with the new global palette
                _localPalette = null;
                SetPalette(KryptonManager.CurrentGlobalPalette);
                _redirector.Target = _palette;

                // A new palette source means we need to layout and redraw
                NeedPaint(true);

                // Must raise event to change palette in redirector
                OnPaletteChanged(EventArgs.Empty);
            }
        }

        private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) => NeedPaint(true);

        private void OnContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            // Get the actual strip instance
            // ReSharper disable RedundantBaseQualifier
            ContextMenuStrip cms = base.ContextMenuStrip;
            // ReSharper restore RedundantBaseQualifier

            // Make sure it has the correct renderer
            cms.Renderer = CreateToolStripRenderer();
        }

        private void OnKryptonContextMenuDisposed(object sender, EventArgs e)
        {
            // When the current krypton context menu is disposed, we should remove 
            // it to prevent it being used again, as that would just throw an exception 
            // because it has been disposed.
            KryptonContextMenu = null;
        }

        private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e) => ContextMenuClosed();

        private void NeedPaint(bool layout) => NeedPaint(new NeedLayoutEventArgs(layout));

        private void NeedPaint(NeedLayoutEventArgs e)
        {
            if (e.NeedLayout)
            {
                PerformLayout();
            }

            Invalidate();
        }
        #endregion
    }
}
