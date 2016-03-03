﻿// *****************************************************************************
// 
//  © Veler Software 2012. All rights reserved.
//  The current code and the associated software are the proprietary 
//  information of Etienne Baudoux from Veler Software and are
//  supplied subject to licence terms.
// 
//  www.velersoftware.com
// *****************************************************************************




using System;
using System.Diagnostics;

namespace VelerSoftware.SZC.Debugger.Base.Debugging
{
    public class DefaultDebugger : IDebugger
    {
        Process attachedProcess = null;

        public bool IsDebugging
        {
            get
            {
                return attachedProcess != null;
            }
        }

        public bool IsProcessRunning
        {
            get
            {
                return IsDebugging;
            }
        }

        public bool CanDebug(object project)
        {
            return true;
        }

        public void Start(ProcessStartInfo processStartInfo)
        {
            if (attachedProcess != null)
            {
                return;
            }

            OnDebugStarting(EventArgs.Empty);
            try
            {
                attachedProcess = new Process();
                attachedProcess.StartInfo = processStartInfo;
                attachedProcess.Exited += new EventHandler(AttachedProcessExited);
                attachedProcess.EnableRaisingEvents = true;
                attachedProcess.Start();
                OnDebugStarted(EventArgs.Empty);
            }
            catch (Exception)
            {
                OnDebugStopped(EventArgs.Empty);
                throw new ApplicationException("Can't execute \"" + processStartInfo.FileName + "\"\n");
            }
        }

        public void ShowAttachDialog()
        {
        }

        public void Attach(Process process)
        {
        }

        public void Detach()
        {
        }

        private void AttachedProcessExited(object sender, EventArgs e)
        {
            attachedProcess.Exited -= new EventHandler(AttachedProcessExited);
            attachedProcess.Dispose();
            attachedProcess = null;
        }

        public void StartWithoutDebugging(ProcessStartInfo processStartInfo)
        {
            Process.Start(processStartInfo);
        }

        public void Stop()
        {
            if (attachedProcess != null)
            {
                attachedProcess.Exited -= new EventHandler(AttachedProcessExited);
                attachedProcess.Kill();
                attachedProcess.Close();
                attachedProcess.Dispose();
                attachedProcess = null;
            }
        }

        // ExecutionControl:

        public void Break()
        {
            throw new NotSupportedException();
        }

        public void Continue()
        {
            throw new NotSupportedException();
        }

        // Stepping:

        public void StepInto()
        {
            throw new NotSupportedException();
        }

        public void StepOver()
        {
            throw new NotSupportedException();
        }

        public void StepOut()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the current value of the variable as string that can be displayed in tooltips.
        /// </summary>
        public string GetValueAsString(string variable)
        {
            return null;
        }

        /// <summary>
        /// Gets the tooltip control that shows the value of given variable.
        /// Return null if no tooltip is available.
        /// </summary>
        public object GetTooltipControl(string variable)
        {
            return null;
        }

        public bool CanSetInstructionPointer(string filename, int line, int column)
        {
            return false;
        }

        public bool SetInstructionPointer(string filename, int line, int column)
        {
            return false;
        }

        public event EventHandler DebugStarted;

        protected virtual void OnDebugStarted(EventArgs e)
        {
            if (DebugStarted != null)
            {
                DebugStarted(this, e);
            }
        }

        public event EventHandler IsProcessRunningChanged;

        protected virtual void OnIsProcessRunningChanged(EventArgs e)
        {
            if (IsProcessRunningChanged != null)
            {
                IsProcessRunningChanged(this, e);
            }
        }

        public event EventHandler DebugStopped;

        protected virtual void OnDebugStopped(EventArgs e)
        {
            if (DebugStopped != null)
            {
                DebugStopped(this, e);
            }
        }

        public event EventHandler DebugStarting;

        protected virtual void OnDebugStarting(EventArgs e)
        {
            if (DebugStarting != null)
            {
                DebugStarting(this, e);
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}