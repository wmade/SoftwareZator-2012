﻿// *****************************************************************************
// 
//  © Veler Software 2012. All rights reserved.
//  The current code and the associated software are the proprietary 
//  information of Etienne Baudoux from Veler Software and are
//  supplied subject to licence terms.
// 
//  www.velersoftware.com
// *****************************************************************************




namespace VelerSoftware.SZC.Debugger.Debugger
{
    /// <summary>
    /// Type of exception thrown by the Debugger.
    /// </summary>
    public class DebuggerException : System.Exception
    {
        public DebuggerException() { }

        public DebuggerException(string message) : base(message) { }

        public DebuggerException(string message, params object[] args) : base(string.Format(message, args)) { }

        public DebuggerException(string message, System.Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// An exception that is thrown when the debugged process unexpectedly exits.
    /// </summary>
    public class ProcessExitedException : DebuggerException
    {
        string processName = null;

        /// <summary>
        /// The name of the process that has exited.
        /// </summary>
        public string ProcessName
        {
            get { return processName; }
        }

        /// <summary>
        /// Creates a ProcessExitedException for an unnamed process.
        /// </summary>
        public ProcessExitedException() : base("Process exited") { }

        /// <summary>
        /// Creates a ProcessExitedException for a process.
        /// </summary>
        /// <param name="processName">The name of the process</param>
        public ProcessExitedException(string processName)
            : base(string.Format("Process '{0}' exited.", processName))
        {
            this.processName = processName;
        }
    }
}