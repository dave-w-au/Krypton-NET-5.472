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
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Draws a semi-transparent window to indicate a drop rectangle.
    /// </summary>
    public class DropSolidWindow : Form
    {
        #region Instance Fields
        private readonly IPaletteDragDrop _paletteDragDrop;
        private readonly IRenderer _renderer;
        private Rectangle _solidRect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DropSolidWindow class.
        /// </summary>
        /// <param name="paletteDragDrop">Drawing palette.</param>
        /// <param name="renderer">Drawing renderer.</param>
        public DropSolidWindow(IPaletteDragDrop paletteDragDrop, IRenderer renderer)
        {
            _paletteDragDrop = paletteDragDrop;
            _renderer = renderer;
            
            FormBorderStyle = FormBorderStyle.None;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = Color.Magenta;
            TransparencyKey = Color.Magenta;
            Opacity = _paletteDragDrop.GetDragDropSolidOpacity();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Show the window without taking activation.
        /// </summary>
        public void ShowWithoutActivate()
        {
            // Show the window without activating it (i.e. do not take focus)
            PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
        }

        /// <summary>
        /// Gets and sets the new solid rectangle area.
        /// </summary>
        public Rectangle SolidRect
        {
            get => _solidRect;

            set
            {
                if (_solidRect != value)
                {
                    _solidRect = value;
                    DesktopBounds = _solidRect;
                    Refresh();
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs with event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Let base class do its stuff
            base.OnPaint(e);

            // If we have a solid rectangle to draw
            if (!SolidRect.IsEmpty)
            {
                using(RenderContext context = new RenderContext(this, e.Graphics, e.ClipRectangle, _renderer))
                {
                    _renderer.RenderGlyph.DrawDragDropSolidGlyph(context, ClientRectangle, _paletteDragDrop);
                }
            }
        }

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process. </param>
        protected override void WndProc(ref Message m)
        {
            // We are a transparent window, so mouse is never over us
            if (m.Msg == PI.WM_.NCHITTEST)
            {
                // Allow actions to occur to window beneath us
                m.Result = (IntPtr)PI.HT.TRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        #endregion
    }
}
