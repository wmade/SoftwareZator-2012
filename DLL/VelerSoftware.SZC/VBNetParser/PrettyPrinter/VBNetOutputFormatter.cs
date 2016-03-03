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
using VelerSoftware.SZC.VBNetParser.Parser.VB;

namespace VelerSoftware.SZC.VBNetParser.PrettyPrinter
{
	/// <summary>
	/// Description of VBNetOutputFormatter.
	/// </summary>
	public sealed class VBNetOutputFormatter : AbstractOutputFormatter
	{
		public VBNetOutputFormatter(VBNetPrettyPrintOptions prettyPrintOptions) : base(prettyPrintOptions)
		{
		}
		
		public override void PrintToken(int token)
		{
			PrintText(Tokens.GetTokenString(token));
		}
		
		public override void PrintIdentifier(string identifier)
		{
			if (Keywords.IsNonIdentifierKeyword(identifier)) {
				PrintText("[");
				PrintText(identifier);
				PrintText("]");
			} else {
				PrintText(identifier);
			}
		}
		
		public override void PrintComment(Comment comment, bool forceWriteInPreviousBlock)
		{
			switch (comment.CommentType) {
				case CommentType.Block:
					WriteLineInPreviousLine("'" + comment.CommentText.Replace("\n", "\n'"), forceWriteInPreviousBlock);
					break;
				case CommentType.Documentation:
					WriteLineInPreviousLine("'''" + comment.CommentText, forceWriteInPreviousBlock);
					break;
				default:
					WriteLineInPreviousLine("'" + comment.CommentText, forceWriteInPreviousBlock);
					break;
			}
		}
		
		public override void PrintPreprocessingDirective(PreprocessingDirective directive, bool forceWriteInPreviousBlock)
		{
			if (IsInMemberBody
			    && (string.Equals(directive.Cmd, "#Region", StringComparison.InvariantCultureIgnoreCase)
			        || string.Equals(directive.Cmd, "#End", StringComparison.InvariantCultureIgnoreCase)
			        && directive.Arg.StartsWith("Region", StringComparison.InvariantCultureIgnoreCase)))
			{
				WriteLineInPreviousLine("'" + directive.Cmd + " " + directive.Arg, forceWriteInPreviousBlock);
			} else if (!directive.Expression.IsNull) {
				VBNetOutputVisitor visitor = new VBNetOutputVisitor();
				directive.Expression.AcceptVisitor(visitor, null);
				WriteLineInPreviousLine(directive.Cmd + " " + visitor.Text + " Then", forceWriteInPreviousBlock);
			} else {
				base.PrintPreprocessingDirective(directive, forceWriteInPreviousBlock);
			}
		}
		
		public void PrintLineContinuation()
		{
			if (!LastCharacterIsWhiteSpace)
				Space();
			PrintText("_" + Environment.NewLine);
		}
	}
}