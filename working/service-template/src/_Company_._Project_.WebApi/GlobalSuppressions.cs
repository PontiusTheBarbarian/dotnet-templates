// <copyright file="GlobalSuppressions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

/*
 * This file is used by Code Analysis to maintain SuppressMessage
 * attributes that are applied to this project.
 * Project-level suppressions either have no target or are given
 * a specific target and scoped to a namespace, type, member, etc.
*/

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "No need to prefix with this.")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Ensure private field is prefixed with an underscore.")]
