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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using VelerSoftware.SZC.Debugger.Base.Gui;

using WinForms = System.Windows.Forms;

namespace VelerSoftware.SZC.Debugger.Base
{
    /// <summary>
    /// Extension methods used in SharpDevelop.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Raises the event.
        /// Does nothing if eventHandler is null.
        /// Because the event handler is passed as parameter, it is only fetched from the event field one time.
        /// This makes
        /// <code>MyEvent.RaiseEvent(x,y);</code>
        /// thread-safe
        /// whereas
        /// <code>if (MyEvent != null) MyEvent(x,y);</code>
        /// would not be safe.
        /// </summary>
        /// <remarks>Using this method is only thread-safe under the Microsoft .NET memory model,
        /// not under the less strict memory model in the CLI specification.</remarks>
        public static void RaiseEvent(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        /// <summary>
        /// Raises the event.
        /// Does nothing if eventHandler is null.
        /// Because the event handler is passed as parameter, it is only fetched from the event field one time.
        /// This makes
        /// <code>MyEvent.RaiseEvent(x,y);</code>
        /// thread-safe
        /// whereas
        /// <code>if (MyEvent != null) MyEvent(x,y);</code>
        /// would not be safe.
        /// </summary>
        public static void RaiseEvent<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        /// <summary>
        /// Runs an action for all elements in the input.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> input, Action<T> action)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            foreach (T element in input)
            {
                action(element);
            }
        }

        /// <summary>
        /// Adds all <paramref name="elements"/> to <paramref name="list"/>.
        /// </summary>
        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> elements)
        {
            foreach (T o in elements)
                list.Add(o);
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> arr)
        {
            return new ReadOnlyCollection<T>(arr);
        }

        public static ReadOnlyCollectionWrapper<T> AsReadOnly<T>(this ICollection<T> arr)
        {
            return new ReadOnlyCollectionWrapper<T>(arr);
        }

        public static IEnumerable<WinForms.Control> GetRecursive(this WinForms.Control.ControlCollection collection)
        {
            return collection.Cast<WinForms.Control>().Flatten(c => c.Controls.Cast<WinForms.Control>());
        }

        /// <summary>
        /// Converts a recursive data structure into a flat list.
        /// </summary>
        /// <param name="input">The root elements of the recursive data structure.</param>
        /// <param name="recursion">The function that gets the children of an element.</param>
        /// <returns>Iterator that enumerates the tree structure in preorder.</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> input, Func<T, IEnumerable<T>> recursion)
        {
            Stack<IEnumerator<T>> stack = new Stack<IEnumerator<T>>();
            try
            {
                stack.Push(input.GetEnumerator());
                while (stack.Count > 0)
                {
                    while (stack.Peek().MoveNext())
                    {
                        T element = stack.Peek().Current;
                        yield return element;
                        IEnumerable<T> children = recursion(element);
                        if (children != null)
                        {
                            stack.Push(children.GetEnumerator());
                        }
                    }
                    stack.Pop().Dispose();
                }
            }
            finally
            {
                while (stack.Count > 0)
                {
                    stack.Pop().Dispose();
                }
            }
        }

        /// <summary>
        /// Creates an array containing a part of the array (similar to string.Substring).
        /// </summary>
        public static T[] Splice<T>(this T[] array, int startIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            return Splice(array, startIndex, array.Length - startIndex);
        }

        /// <summary>
        /// Creates an array containing a part of the array (similar to string.Substring).
        /// </summary>
        public static T[] Splice<T>(this T[] array, int startIndex, int length)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (startIndex < 0 || startIndex > array.Length)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "Value must be between 0 and " + array.Length);
            if (length < 0 || length > array.Length - startIndex)
                throw new ArgumentOutOfRangeException("length", length, "Value must be between 0 and " + (array.Length - startIndex));
            T[] result = new T[length];
            Array.Copy(array, startIndex, result, 0, length);
            return result;
        }

        /// <summary>
        /// Sets the Content property of the specified ControlControl to the specified content.
        /// If the content is a Windows-Forms control, it is wrapped in a WindowsFormsHost.
        /// If the content control already contains a WindowsFormsHost with that content,
        /// the old WindowsFormsHost is kept.
        /// When a WindowsFormsHost is replaced with another content, the host is disposed (but the control
        /// inside the host isn't)
        /// </summary>
        public static void SetContent(this ContentControl contentControl, object content)
        {
            SetContent(contentControl, content, null);
        }

        public static void SetContent(this ContentPresenter contentControl, object content)
        {
            SetContent(contentControl, content, null);
        }

        public static void SetContent(this ContentControl contentControl, object content, object serviceObject)
        {
            if (contentControl == null)
                throw new ArgumentNullException("contentControl");
            // serviceObject = object implementing the old clipboard/undo interfaces
            // to allow WinForms AddIns to handle WPF commands

            var host = contentControl.Content as SDWindowsFormsHost;
            if (host != null)
            {
                if (host.Child == content)
                {
                    host.ServiceObject = serviceObject;
                    return;
                }
                host.Dispose();
            }
            if (content is WinForms.Control)
            {
                contentControl.Content = new SDWindowsFormsHost
                {
                    Child = (WinForms.Control)content,
                    ServiceObject = serviceObject,
                    DisposeChild = false
                };
            }
            else if (content is string)
            {
                contentControl.Content = new TextBlock
                {
                    Text = content.ToString(),
                    TextWrapping = TextWrapping.Wrap
                };
            }
            else
            {
                contentControl.Content = content;
            }
        }

        public static void SetContent(this ContentPresenter contentControl, object content, object serviceObject)
        {
            if (contentControl == null)
                throw new ArgumentNullException("contentControl");
            // serviceObject = object implementing the old clipboard/undo interfaces
            // to allow WinForms AddIns to handle WPF commands

            var host = contentControl.Content as SDWindowsFormsHost;
            if (host != null)
            {
                if (host.Child == content)
                {
                    host.ServiceObject = serviceObject;
                    return;
                }
                host.Dispose();
            }
            if (content is WinForms.Control)
            {
                contentControl.Content = new SDWindowsFormsHost
                {
                    Child = (WinForms.Control)content,
                    ServiceObject = serviceObject,
                    DisposeChild = false
                };
            }
            else if (content is string)
            {
                contentControl.Content = new TextBlock
                {
                    Text = content.ToString(),
                    TextWrapping = TextWrapping.Wrap
                };
            }
            else
            {
                contentControl.Content = content;
            }
        }

        #region System.Drawing <-> WPF conversions

        public static System.Drawing.Point ToSystemDrawing(this Point p)
        {
            return new System.Drawing.Point((int)p.X, (int)p.Y);
        }

        public static System.Drawing.Size ToSystemDrawing(this Size s)
        {
            return new System.Drawing.Size((int)s.Width, (int)s.Height);
        }

        public static System.Drawing.Rectangle ToSystemDrawing(this Rect r)
        {
            return new System.Drawing.Rectangle(r.TopLeft.ToSystemDrawing(), r.Size.ToSystemDrawing());
        }

        public static System.Drawing.Color ToSystemDrawing(this System.Windows.Media.Color c)
        {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static Point ToWpf(this System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }

        public static Size ToWpf(this System.Drawing.Size s)
        {
            return new Size(s.Width, s.Height);
        }

        public static Rect ToWpf(this System.Drawing.Rectangle rect)
        {
            return new Rect(rect.Location.ToWpf(), rect.Size.ToWpf());
        }

        public static System.Windows.Media.Color ToWpf(this System.Drawing.Color c)
        {
            return System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        #endregion System.Drawing <-> WPF conversions

        #region DPI independence

        public static Rect TransformToDevice(this Rect rect, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformToDevice;
            return Rect.Transform(rect, matrix);
        }

        public static Rect TransformFromDevice(this Rect rect, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformFromDevice;
            return Rect.Transform(rect, matrix);
        }

        public static Size TransformToDevice(this Size size, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformToDevice;
            return new Size(size.Width * matrix.M11, size.Height * matrix.M22);
        }

        public static Size TransformFromDevice(this Size size, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformFromDevice;
            return new Size(size.Width * matrix.M11, size.Height * matrix.M22);
        }

        public static Point TransformToDevice(this Point point, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformToDevice;
            return new Point(point.X * matrix.M11, point.Y * matrix.M22);
        }

        public static Point TransformFromDevice(this Point point, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformFromDevice;
            return new Point(point.X * matrix.M11, point.Y * matrix.M22);
        }

        #endregion DPI independence

        /// <summary>
        /// Removes <param name="stringToRemove" /> from the start of this string.
        /// Throws ArgumentException if this string does not start with <param name="stringToRemove" />.
        /// </summary>
        public static string RemoveStart(this string s, string stringToRemove)
        {
            if (s == null)
                return null;
            if (string.IsNullOrEmpty(stringToRemove))
                return s;
            if (!s.StartsWith(stringToRemove, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format("{0} does not start with {1}", s, stringToRemove));
            return s.Substring(stringToRemove.Length);
        }

        /// <summary>
        /// Removes <param name="stringToRemove" /> from the end of this string.
        /// Throws ArgumentException if this string does not end with <param name="stringToRemove" />.
        /// </summary>
        public static string RemoveEnd(this string s, string stringToRemove)
        {
            if (s == null)
                return null;
            if (string.IsNullOrEmpty(stringToRemove))
                return s;
            if (!s.EndsWith(stringToRemove))
                throw new ArgumentException(string.Format("{0} does not end with {1}", s, stringToRemove));
            return s.Substring(0, s.Length - stringToRemove.Length);
        }

        /// <summary>
        /// Takes at most <param name="length" /> first characters from string.
        /// String can be null.
        /// </summary>
        public static string TakeStart(this string s, int length)
        {
            if (string.IsNullOrEmpty(s) || length >= s.Length)
                return s;
            return s.Substring(0, length);
        }

        /// <summary>
        /// Takes at most <param name="length" /> first characters from string, and appends '...' if string is longer.
        /// String can be null.
        /// </summary>
        public static string TakeStartEllipsis(this string s, int length)
        {
            if (string.IsNullOrEmpty(s) || length >= s.Length)
                return s;
            return s.Substring(0, length) + "...";
        }

        public static string Replace(this string original, string pattern, string replacement, StringComparison comparisonType)
        {
            if (original == null)
                throw new ArgumentNullException("original");
            if (pattern == null)
                throw new ArgumentNullException("pattern");
            if (pattern.Length == 0)
                throw new ArgumentException("String cannot be of zero length.", "pattern");
            if (comparisonType != StringComparison.Ordinal && comparisonType != StringComparison.OrdinalIgnoreCase)
                throw new NotSupportedException("Currently only ordinal comparisons are implemented.");

            StringBuilder result = new StringBuilder(original.Length);
            int currentPos = 0;
            int nextMatch = original.IndexOf(pattern, comparisonType);
            while (nextMatch >= 0)
            {
                result.Append(original, currentPos, nextMatch - currentPos);
                // The following line restricts this method to ordinal comparisons:
                // for non-ordinal comparisons, the match length might be different than the pattern length.
                currentPos = nextMatch + pattern.Length;
                result.Append(replacement);

                nextMatch = original.IndexOf(pattern, currentPos, comparisonType);
            }

            result.Append(original, currentPos, original.Length - currentPos);
            return result.ToString();
        }

        public static byte[] GetBytesWithPreamble(this Encoding encoding, string text)
        {
            byte[] encodedText = encoding.GetBytes(text);
            byte[] bom = encoding.GetPreamble();
            if (bom != null && bom.Length > 0)
            {
                byte[] result = new byte[bom.Length + encodedText.Length];
                bom.CopyTo(result, 0);
                encodedText.CopyTo(result, bom.Length);
                return result;
            }
            else
            {
                return encodedText;
            }
        }

        /// <summary>
        /// Creates a new image for the image source.
        /// </summary>
        public static System.Drawing.Image CreateImage(this System.Drawing.Image image)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            return image;
        }

        /// <summary>
        /// Creates a new image for the image source.
        /// </summary>
        [ObsoleteAttribute("Use layout rounding instead")]
        public static UIElement CreatePixelSnappedImage(this System.Drawing.Image image)
        {
            //return CreateImage(image);
            return null;
        }

        /// <summary>
        /// Translates a WinForms menu to WPF.
        /// </summary>
        public static ICollection TranslateToWpf(this ToolStripItem[] items)
        {
            return items.OfType<ToolStripMenuItem>().Select(item => TranslateMenuItemToWpf(item)).ToList();
        }

        private static System.Windows.Controls.MenuItem TranslateMenuItemToWpf(ToolStripMenuItem item)
        {
            var r = new System.Windows.Controls.MenuItem();
            //r.InputGestureText = new KeyGesture(Key.F6).GetDisplayStringForCulture(Thread.CurrentThread.CurrentUICulture);
            if (item.ImageIndex >= 0)
                //r.Icon = ClassBrowserIconService.GetImageByIndex(item.ImageIndex).CreateImage();
                if (item.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem subItem in item.DropDownItems)
                    {
                        r.Items.Add(TranslateMenuItemToWpf(subItem));
                    }
                }
                else
                {
                    r.Click += delegate { item.PerformClick(); };
                }
            r.IsChecked = item.Checked;
            return r;
        }

        /// <summary>
        /// Returns the index of the first element for which <paramref name="predicate"/> returns true.
        /// If none of the items in the list fits the <paramref name="predicate"/>, -1 is returned.
        /// </summary>
        public static int FindIndex<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Adds item to the list if the item is not null.
        /// </summary>
        public static void AddIfNotNull<T>(this IList<T> list, T itemToAdd)
        {
            if (itemToAdd != null)
                list.Add(itemToAdd);
        }
    }
}