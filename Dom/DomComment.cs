﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jtc.CsQuery
{
    public interface IDomComment : IDomSpecialElement
    {
        bool IsQuoted { get; set; }
    }
    public class DomComment : DomObject<DomComment>, IDomComment
    {
        public DomComment()
            : base()
        {
        }
        //public DomComment(CsQuery owner)
        //    : base(owner)
        //{

        //}
        public DomComment(string text)
        {
            Text = text;
        }
        public override NodeType NodeType
        {
            get { return NodeType.COMMENT_NODE; }
        }
        public bool IsQuoted { get; set; }
        protected string TagOpener
        {
            get { return IsQuoted ? "<!--" : "<!"; }
        }
        protected string TagCloser
        {
            get { return IsQuoted ? "-->" : ">"; }
        }
        public override string Render()
        {
            if (Dom != null
                && Dom.DomRenderingOptions.HasFlag(DomRenderingOptions.RemoveComments))
            {
                return String.Empty;
            }
            else
            {
                return GetComment(NonAttributeData);
            }
        }
        protected string GetComment(string innerText)
        {
            return TagOpener + innerText + TagCloser;
        }

        public override bool InnerHtmlAllowed
        {
            get { return false; }
        }
        public override bool HasChildren
        {
            get { return false; }
        }
        public override bool Complete
        {
            get { return true; }
        }
        public override string ToString()
        {
            string innerText = NonAttributeData.Length > 80 ? NonAttributeData.Substring(0, 80) + " ... " : NonAttributeData;
            return GetComment(innerText);
        }
        #region IDomSpecialElement Members

        public string NonAttributeData
        {
            get;
            set;
        }

        public string Text
        {
            get
            {
                return NonAttributeData;
            }
            set
            {
                NonAttributeData = value;
            }
        }
        public override DomComment Clone()
        {
            DomComment clone = base.Clone();
            clone.NonAttributeData = NonAttributeData;
            return clone;

        }

        #endregion
    }
}