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

using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Specialise the generic collection with type specific rules for gallery range item accessor.
    /// </summary>
    public class KryptonGalleryRangeCollection : TypedCollection<KryptonGalleryRange>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="heading">Heading of the gallery range instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonGalleryRange this[string heading]
        {
            get
            {
                // Search for a gallery range with the same heading as that requested.
                foreach (KryptonGalleryRange range in this)
                {
                    if (range.Heading == heading)
                    {
                        return range;
                    }
                }

                // Let base class perform standard processing
                return base[heading];
            }
        }
        #endregion
    }
}
