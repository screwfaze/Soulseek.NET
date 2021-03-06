﻿// <copyright file="DiagnosticFactory.cs" company="JP Dillingham">
//     Copyright (c) JP Dillingham. All rights reserved.
//
//     This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as
//     published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//     of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License along with this program. If not, see https://www.gnu.org/licenses/.
// </copyright>

namespace Soulseek.NET
{
    using System;

    /// <summary>
    ///     Creates diagnostic messages.
    /// </summary>
    internal class DiagnosticFactory : IDiagnosticFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DiagnosticFactory"/> class.
        /// </summary>
        /// <param name="source">The source object which originates diagnostic messages.</param>
        /// <param name="minimumLevel">The minimum level of messages to generate.</param>
        /// <param name="eventHandler">The event handler used to raise diagnostics events.</param>
        public DiagnosticFactory(object source, DiagnosticLevel minimumLevel, Action<DiagnosticGeneratedEventArgs> eventHandler)
        {
            Source = source;
            MinimumLevel = minimumLevel;
            EventHandler = eventHandler;
        }

        private Action<DiagnosticGeneratedEventArgs> EventHandler { get; }
        private DiagnosticLevel MinimumLevel { get; }
        private object Source { get; }

        /// <summary>
        ///     Creates a <see cref="DiagnosticLevel.Debug"/> diagnostic message.
        /// </summary>
        /// <param name="message">The desired message.</param>
        public void Debug(string message)
        {
            RaiseEvent(DiagnosticLevel.Debug, message);
        }

        /// <summary>
        ///     Creates an <see cref="DiagnosticLevel.Info"/> diagnostic message.
        /// </summary>
        /// <param name="message">The desired message.</param>
        public void Info(string message)
        {
            RaiseEvent(DiagnosticLevel.Info, message);
        }

        /// <summary>
        ///     Creates a <see cref="DiagnosticLevel.Warning"/> diagnostic message.
        /// </summary>
        /// <param name="message">The desired message.</param>
        /// <param name="exception">An optional Exception.</param>
        public void Warning(string message, Exception exception = null)
        {
            RaiseEvent(DiagnosticLevel.Warning, message, exception);
        }

        private void RaiseEvent(DiagnosticLevel level, string message, Exception exception = null)
        {
            if (level <= MinimumLevel)
            {
                var e = new DiagnosticGeneratedEventArgs(level, message, exception);
                EventHandler(e);
            }
        }
    }
}