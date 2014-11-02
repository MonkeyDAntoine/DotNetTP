using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagManagerLib
{
    /*
     * 
     * 
     * */
    public class SyntaxTree
    {
        public static List<ITag> _tags;

        private static int _indexProcess;
        private static string _source;

        private NodeRoot _root;

        public NodeRoot Root { get { return _root; } }
        public static String Source { get { return _source; } }
        public static int IndexProcess { get { return _indexProcess;} set {_indexProcess = value;}  }

        public SyntaxTree(string source)
        {
            _tags = new List<ITag>();
            _indexProcess = 0;
            _source = source;

            _tags.Add(new TagBold());
            _tags.Add(new TagNoProcess());

            _root = new NodeRoot();
        }

        public void process()
        {
            _root.process();
        }

        public static ITag getNextChildTag(ITag parentTag)
        {
            ITag result = null;
            int indexMin =_source.Length;

            foreach (ITag tag in _tags) {

                int index;
                if (parentTag == null)
                {
                    index = _source.IndexOf(tag.OpenTag, _indexProcess);
                }
                else
                {
                    index = _source.Substring(_indexProcess, _source.IndexOf(parentTag.CloseTag,_indexProcess) - _indexProcess).IndexOf(tag.OpenTag);
                }
                
                if (index >=0 && index < indexMin)
                {
                    indexMin = index;
                    result = tag;
                }
            }

            return result;
        }

        public override string ToString()
        {
            result = "";
            tab = 0;
            displayNode(_root);
            return result;
        }

        private string result = "";
        private int tab = 0;

        private void displayNode(Node node)
        {
            tab++;
            foreach (Node child in node.Children) {
                for (int i = 0; i < tab-1; i++)
                {
                    result += "   ";
                }
                result += "'--";
                result +=  child.ToString() + "\n";
                displayNode(child);
            }
            tab--;
        }
    }


    public abstract class Node
    {

        private List<Node> _children;
        public ITag _tag;

        public Node(ITag tag)
        {
            _tag = tag;
            _children = new List<Node>();
        }

        
        public virtual void process() {
            int startIndex = SyntaxTree.IndexProcess;
            ITag tagChild = null;

            //Add children
            while (SyntaxTree.IndexProcess < SyntaxTree.Source.Length && ((tagChild = SyntaxTree.getNextChildTag(Tag)) != null))
            {
                int offset = SyntaxTree.Source.IndexOf(tagChild.OpenTag, startIndex) - startIndex;

                //InnertText before Child
                if (offset>0)
                {
                    SyntaxTree.IndexProcess += offset;
                    _children.Add(new InnerTextNode(startIndex, SyntaxTree.IndexProcess - 1));
                }
                SyntaxTree.IndexProcess += tagChild.OpenTag.Length;
                //Plus Child
                NodeTag child = new NodeTag(tagChild);
                _children.Add(child);
                child.process();
                startIndex = SyntaxTree.IndexProcess;
            }

            //No more child, innerText until the end of the tag
            if (Tag != null) {
                int endIndex = SyntaxTree.Source.IndexOf(_tag.CloseTag, SyntaxTree.IndexProcess);
                if (endIndex > 0 && startIndex != endIndex) {
                    _children.Add(new InnerTextNode(startIndex, endIndex-1));
                }
                if (endIndex >= 0)
                {
                    SyntaxTree.IndexProcess = endIndex + _tag.CloseTag.Length;
                }
            }
            else {
                int endIndex = SyntaxTree.Source.Length;
                if (startIndex != endIndex)
                {
                    _children.Add(new InnerTextNode(startIndex, endIndex - 1));
                }
            }
        }
        public ITag Tag { get{return _tag;} }
        public List<Node> Children {get { return _children;}}
    }

    public class NodeRoot : Node
    {
        public NodeRoot() : base(null)
        {
        }
    }

    public class NodeTag : Node
    {       
        public NodeTag(ITag tag) : base(tag)
        {
        }

        public override string ToString()
        {
            if (Tag != null)
            {
                return Tag.ToString();
            }
            return base.ToString();
        }
    }

    public class InnerTextNode : Node
    {
        public int _fromIndex;
        public int _toIndex;

        public InnerTextNode(int from, int to) : base(null)
        {
            _fromIndex= from;
            _toIndex = to;
        }

        override
        public void process() { }

        public string Text { get { return SyntaxTree.Source.Substring(this._fromIndex, this._toIndex - this._fromIndex+1); } }

        override
        public string ToString()
        {
            return "Text{"+this.Text+"}";
        }
    }
}