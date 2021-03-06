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
using System.Globalization;
using System.Reflection;

namespace VelerSoftware.SZC.Debugger.Debugger.MetaData
{
    public class DebugConstructorInfo : System.Reflection.ConstructorInfo, IDebugMemberInfo
    {
        DebugMethodInfo methodInfo;

        internal DebugConstructorInfo(DebugMethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        Debugger.Module IDebugMemberInfo.DebugModule
        {
            get { return methodInfo.DebugModule; }
        }

        /// <inheritdoc/>
        public override Type DeclaringType
        {
            get { return methodInfo.DeclaringType; }
        }

        /// <inheritdoc/>
        public override int MetadataToken
        {
            get { return methodInfo.MetadataToken; }
        }

        /// <inheritdoc/>
        public override System.Reflection.Module Module
        {
            get { return methodInfo.Module; }
        }

        /// <inheritdoc/>
        public override string Name
        {
            get { return methodInfo.Name; }
        }

        /// <inheritdoc/>
        public override Type ReflectedType
        {
            get { return methodInfo.ReflectedType; }
        }

        /// <inheritdoc/>
        public override MethodAttributes Attributes
        {
            get { return methodInfo.Attributes; }
        }

        /// <inheritdoc/>
        public override bool ContainsGenericParameters
        {
            get { return methodInfo.ContainsGenericParameters; }
        }

        /// <inheritdoc/>
        public override bool IsGenericMethod
        {
            get { return methodInfo.IsGenericMethod; }
        }

        /// <inheritdoc/>
        public override bool IsGenericMethodDefinition
        {
            get { return methodInfo.IsGenericMethodDefinition; }
        }

        /// <inheritdoc/>
        public override RuntimeMethodHandle MethodHandle
        {
            get { return methodInfo.MethodHandle; }
        }

        DebugType IDebugMemberInfo.MemberType
        {
            get { return ((IDebugMemberInfo)methodInfo).MemberType; }
        }

        /// <inheritdoc/>
        public override object[] GetCustomAttributes(bool inherit)
        {
            return methodInfo.GetCustomAttributes(inherit);
        }

        /// <inheritdoc/>
        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return methodInfo.GetCustomAttributes(attributeType, inherit);
        }

        /// <inheritdoc/>
        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return methodInfo.IsDefined(attributeType, inherit);
        }

        /// <inheritdoc/>
        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            return methodInfo.GetMethodImplementationFlags();
        }

        /// <inheritdoc/>
        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
        {
            return methodInfo.Invoke(null, invokeAttr, binder, parameters, culture);
        }

        /// <inheritdoc/>
        public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
        {
            return methodInfo.Invoke(null, invokeAttr, binder, parameters, culture);
        }

        /// <inheritdoc/>
        public override ParameterInfo[] GetParameters()
        {
            return methodInfo.GetParameters();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return methodInfo.ToString();
        }
    }
}