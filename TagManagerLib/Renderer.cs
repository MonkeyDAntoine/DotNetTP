using System;
using System.Text.RegularExpressions;

namespace TagManagerLib
{
    /*
     * Renderer to tranform text with custom tag
     * 
     * */
    public interface Renderer
    {
        string Render(SyntaxTree tree);
    }

    /*
     * Custom tags
     * 
     * */
    public abstract class AbstractTag
    {
        public abstract string OpenTag { get; }
        public abstract string CloseTag { get; }

        public override sealed int GetHashCode()
        {
            return OpenTag.GetHashCode()*CloseTag.GetHashCode();
        }

        public override sealed bool Equals(object obj)
        {
            if (obj is AbstractTag)
            {
                AbstractTag tag = (AbstractTag)obj;
                return tag.OpenTag.Equals(OpenTag) && tag.CloseTag.Equals(CloseTag);
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }

    public sealed class TagNoProcess : AbstractTag
    {
        public static readonly TagNoProcess Instance = new TagNoProcess();
        override
        public string OpenTag { get { return "#{"; } }
        
        override
        public string CloseTag { get { return "}#"; } }
    }

    public sealed class TagBold : AbstractTag
    {
        override
        public string OpenTag { get { return "B{"; } }
        
        override
        public string CloseTag { get { return "}"; } }
    }

}
