﻿// <copyright file="DiagnosticMessageFactory.cs" company="JP Dillingham">
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
    internal class DiagnosticMessageFactory : IDiagnosticMessageFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DiagnosticMessageFactory"/> class.
        /// </summary>
        /// <param name="source">The source object which originates diagnostic messages.</param>
        /// <param name="eventHandler">The event handler used to raise diagnostics events.</param>
        public DiagnosticMessageFactory(object source, EventHandler<DiagnosticMessageGeneratedEventArgs> eventHandler)
        {
            Source = source;
            EventHandler = eventHandler;
        }

        private EventHandler<DiagnosticMessageGeneratedEventArgs> EventHandler { get; }
        private object Source { get; }

        /// <summary>
        ///     Creates a <see cref="DiagnosticMessageLevel.Debug"/> diagnostic message.
        /// </summary>
        /// <param name="message">The desired message.</param>
        public void Debug(string message)
        {
            RaiseEvent(DiagnosticMessageLevel.Debug, message);
        }

        /// <summary>
        ///     Creates an <see cref="DiagnosticMessageLevel.Info"/> diagnostic message.
        /// </summary>
        /// <param name="message">The desired message.</param>
        public void Info(string message)
        {
            RaiseEvent(DiagnosticMessageLevel.Info, message);
        }

        /// <summary>
        ///     Creates a <see cref="DiagnosticMessageLevel.Warning"/> diagnostic message.
        /// </summary>
        /// <param name="message">The desired message.</param>
        /// <param name="exception">An optional Exception.</param>
        public void Warning(string message, Exception exception = null)
        {
            RaiseEvent(DiagnosticMessageLevel.Warning, message, exception);
        }

        private void RaiseEvent(DiagnosticMessageLevel level, string message, Exception exception = null)
        {
            var e = new DiagnosticMessageGeneratedEventArgs(level, message, exception);
            EventHandler?.Invoke(Source, e);
        }
    }
}