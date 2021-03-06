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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VelerSoftware.SZC.Obfuscator.Confuser.Core.Poly.Expressions
{
    public class VariableExpression : Expression
    {
        public override Expression GetVariableExpression()
        {
            return this;
        }

        public override void Visit(ExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void VisitReverse(ExpressionVisitor visitor, Expression child)
        {
            visitor.VisitReverse(this);
            if (Parent != null)
                Parent.VisitReverse(visitor, this);
        }

        public override bool HasVariable
        {
            get { return true; }
        }

        public override string ToString()
        {
            return "Var";
        }
    }
}
