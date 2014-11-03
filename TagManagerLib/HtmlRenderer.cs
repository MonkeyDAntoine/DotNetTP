using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TagManagerLib
{
    public class HtmlRenderer : Renderer
    {
        private Dictionary<AbstractTag, AbstractHtmlTag> _htmlTags;

        public HtmlRenderer()
        {
            _htmlTags = new Dictionary<AbstractTag, AbstractHtmlTag>();
            _htmlTags.Add(new TagBold(), new BoldHtmlTag());
            _htmlTags.Add(new TagItalic(), new ItalicHtmlTag());
            _htmlTags.Add(new TagNoProcess(), NoProcessHtmlTag.Instance);
        }
        
        public string Render(SyntaxTree tree)
        {
            string result = "";
            foreach (Node child in tree.Root.Children)
            {
                result += renderNode(child);
            }
            return result;
        }

        private string renderNode(Node node)
        {
            if (node is InnerTextNode)
            {
                return HttpUtility.HtmlEncode(((InnerTextNode)node).Text);
            }

            string result = "";
            AbstractHtmlTag tagHtml = null;
            if (node.Tag != null && _htmlTags.ContainsKey(node.Tag))
            {
                tagHtml = _htmlTags[node.Tag];
            }

            if (tagHtml != null)
            {
                result = tagHtml.OpenHtmlTag;
            }

            if (NoProcessHtmlTag.Instance.Equals(tagHtml))
            {
                result += renderNoProcessNode(node);
            }
            else
            {
                foreach (Node child in node.Children)
                {
                    result += renderNode(child);
                }
            }

            if (tagHtml != null)
            {
                result += tagHtml.CloseHtmlTag;
            }

            return result;

        }

        private string renderNoProcessNode(Node node)
        {
            string result = "";
            foreach (Node child in node.Children)
            {
                if (child.Tag != null)
                {
                    result += child.Tag.OpenTag + renderNoProcessNode(child) + child.Tag.CloseTag;
                }
                else
                {
                    result += child.ToString() + renderNoProcessNode(child);
                }
            }
            return HttpUtility.HtmlEncode(result);
        }
    }

    public abstract class AbstractHtmlTag 
    {
        private AbstractTag _tag;
        public AbstractHtmlTag(AbstractTag tag)
        {
            _tag = tag;
        }

        public AbstractTag Tag { get { return _tag; } }
        public abstract string OpenHtmlTag { get; }
        public abstract string CloseHtmlTag { get; }

        public sealed override bool Equals(object obj)
        {
            if (obj is AbstractHtmlTag)
            {
                AbstractHtmlTag tag = (AbstractHtmlTag)obj;
                return tag.OpenHtmlTag.Equals(OpenHtmlTag) && tag.CloseHtmlTag.Equals(CloseHtmlTag);
            }
            return base.Equals(obj);
        }

        public sealed override int GetHashCode()
        {
            return OpenHtmlTag.GetHashCode() * CloseHtmlTag.GetHashCode() ;
        }
    }

    public class BoldHtmlTag : AbstractHtmlTag
    {

        public BoldHtmlTag() : base (new TagBold()) {
        }

        public override string CloseHtmlTag
        {
            get { return "</b>"; }
        }

        public override string OpenHtmlTag
        {
            get { return "<b>"; }
        }
    }

    public class ItalicHtmlTag : AbstractHtmlTag
    {

        public ItalicHtmlTag()
            : base(new TagItalic())
        {
        }

        public override string CloseHtmlTag
        {
            get { return "</i>"; }
        }

        public override string OpenHtmlTag
        {
            get { return "<i>"; }
        }
    }

    public class NoProcessHtmlTag : AbstractHtmlTag
    {
        public static readonly NoProcessHtmlTag Instance = new NoProcessHtmlTag();
        public NoProcessHtmlTag()
            : base(new TagNoProcess())
        {
        }

        public override string CloseHtmlTag
        {
            get { return "</a>"; }
        }

        public override string OpenHtmlTag
        {
            get { return "<a>"; }
        }
    }

}
