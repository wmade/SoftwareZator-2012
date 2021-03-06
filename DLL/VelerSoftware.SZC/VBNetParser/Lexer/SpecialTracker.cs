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
using System.Text;

namespace VelerSoftware.SZC.VBNetParser.Parser
{
	public class SpecialTracker
	{
		List<ISpecial> currentSpecials = new List<ISpecial>();
		
		CommentType   currentCommentType;
		StringBuilder sb = new StringBuilder();
		Location         startPosition;
		bool commentStartsLine;
		
		public List<ISpecial> CurrentSpecials {
			get {
				return currentSpecials;
			}
		}
		
		/// <summary>
		/// Gets the specials from the SpecialTracker and resets the lists.
		/// </summary>
		public List<ISpecial> RetrieveSpecials()
		{
			List<ISpecial> tmp = currentSpecials;
			currentSpecials = new List<ISpecial>();
			return tmp;
		}
		
		public void AddEndOfLine(Location point)
		{
			currentSpecials.Add(new BlankLine(point));
		}
		
		public void AddPreprocessingDirective(PreprocessingDirective directive)
		{
			if (directive == null)
				throw new ArgumentNullException("directive");
			currentSpecials.Add(directive);
		}
		
		// used for comment tracking
		public void StartComment(CommentType commentType, bool commentStartsLine, Location startPosition)
		{
			this.currentCommentType = commentType;
			this.startPosition      = startPosition;
			this.sb.Length          = 0;
			this.commentStartsLine  = commentStartsLine;
		}
		
		public void AddChar(char c)
		{
			sb.Append(c);
		}
		
		public void AddString(string s)
		{
			sb.Append(s);
		}
		
		public void FinishComment(Location endPosition)
		{
			currentSpecials.Add(new Comment(currentCommentType, sb.ToString(), commentStartsLine, startPosition, endPosition));
		}
	}
}
