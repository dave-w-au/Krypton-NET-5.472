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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    internal partial class KryptonCheckButtonCollectionForm : Form
    {
        #region Type Definitions
        private class ListEntry
        {
            #region Instance Fields

            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the ListEntry class.
            /// </summary>
            /// <param name="checkButton">CheckButton to encapsulate.</param>
            public ListEntry(KryptonCheckButton checkButton)
            {
                Debug.Assert(checkButton != null);
                CheckButton = checkButton;
            }

            /// <summary>
            /// Gets a string representation of the encapsulated check button.
            /// </summary>
            /// <returns>String instance.</returns>
            public override string ToString()
            {
                return CheckButton.Site.Name + "  (Text: " + CheckButton.Text + ")";
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets access to the encapsulated check button instance.
            /// </summary>
            public KryptonCheckButton CheckButton { get; }

            #endregion
        }
        #endregion

        #region Instance Fields
        private readonly KryptonCheckSet _checkSet;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckButtonCollectionForm class.
        /// </summary>
        public KryptonCheckButtonCollectionForm()
            : this(null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonCheckButtonCollectionForm class.
        /// </summary>
        public KryptonCheckButtonCollectionForm(KryptonCheckSet checkSet)
        {
            // Remember the owning control
            _checkSet = checkSet;

            InitializeComponent();
        }
        #endregion

        #region Implementation
        private void KryptonCheckButtonCollectionForm_Load(object sender, EventArgs e)
        {
            // Get access to the container of the check set
            IContainer container = _checkSet.Container;

            // Assuming we manage to find a container
            if (container != null)
            {
                // Find all the check buttons inside the container
                foreach (object obj in container.Components)
                {
                    // Cast to the correct type
                    // We are only interested in check buttons
                    if (obj is KryptonCheckButton checkButton)
                    {

                        // Add a new entry to the list box but only check it if 
                        // it is already present in the check buttons collection
                        checkedListBox.Items.Add(new ListEntry(checkButton),
                                                 _checkSet.CheckButtons.Contains(checkButton));
                    }
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Create a copy of the current check set buttons
            List<KryptonCheckButton> copy = new List<KryptonCheckButton>();
            foreach (KryptonCheckButton checkButton in _checkSet.CheckButtons)
            {
                copy.Add(checkButton);
            }

            // Process each of the list entries in turn
            for(int i=0; i<checkedListBox.Items.Count; i++)
            {
                // Get access to the encapsulated list box entry
                ListEntry entry = (ListEntry)checkedListBox.Items[i];

                // Is this entry checked in the list box?
                if (checkedListBox.GetItemChecked(i))
                {
                    // Make sure the check button is in the check set
                    if (!_checkSet.CheckButtons.Contains(entry.CheckButton))
                    {
                        _checkSet.CheckButtons.Add(entry.CheckButton);
                    }
                    else
                    {
                        copy.Remove(entry.CheckButton);
                    }
                }
                else
                {
                    // Make sure the check button is not in the check set
                    if (_checkSet.CheckButtons.Contains(entry.CheckButton))
                    {
                        _checkSet.CheckButtons.Remove(entry.CheckButton);
                        copy.Remove(entry.CheckButton);
                    }
                }
            }

            // If there are any dangling references in the checkset that are
            // not in the component list from the list box then remove them
            foreach(KryptonCheckButton checkButton in copy)
            {
                _checkSet.CheckButtons.Remove(checkButton);
            }
        }
        #endregion
    }
}
