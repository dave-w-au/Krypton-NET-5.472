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
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyVersion("5.472.713.0")]
[assembly: AssemblyFileVersion("5.472.713.0")]
[assembly: AssemblyInformationalVersion("5.472.713.0")]
[assembly: AssemblyCopyright("© Component Factory Pty Ltd, 2006-2019. Then modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV) 2017-2019. All rights reserved.")]
[assembly: AssemblyProduct("Krypton Workspace")]
[assembly: AssemblyDefaultAlias("ComponentFactory.Krypton.Workspace.dll")]
[assembly: AssemblyTitle("ComponentFactory.Krypton.Workspace")]
[assembly: AssemblyCompany("Component Factory Pty Ltd, Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV)")]
[assembly: AssemblyDescription("ComponentFactory.Krypton.Workspace")]
[assembly: AssemblyConfiguration("Production")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: StringFreezing]
[assembly: ComVisible(true)]
[assembly: CLSCompliant(true)]
[assembly: AllowPartiallyTrustedCallers()]
[assembly: Dependency("System", LoadHint.Always)]
[assembly: Dependency("System.Drawing", LoadHint.Always)]
[assembly: Dependency("System.Windows.Forms", LoadHint.Always)]
[assembly: Dependency("System.XML", LoadHint.Always)]
[assembly: Dependency("ComponentFactory.Krypton.Toolkit", LoadHint.Always)]
[assembly: Dependency("ComponentFactory.Krypton.Navigator", LoadHint.Always)]
[assembly: SecurityRules(SecurityRuleSet.Level1)]