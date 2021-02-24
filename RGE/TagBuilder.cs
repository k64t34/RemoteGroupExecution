using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HTMLBuilder
{
    class HtmlElementBuilder //: TagBuilder
    {
        public HtmlElement htmlElement;
        //public Dictionary<String, HtmlElement> Elements = new Dictionary<String, HtmlElement>();
        public HtmlElementBuilder(HtmlElement htmlElement)
        {
            this.htmlElement = htmlElement;
        }
        public void Add(HtmlElement htmlElement) { htmlElement.AppendChild(htmlElement); }
        public void Uppdate(HtmlElement BODY) { BODY.AppendChild(htmlElement); }
    }

        class TagBuilder //TagBuilder Class https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.tagbuilder?view=aspnet-webpages-3.2
    {
        public const string _BR = "<BR>";
        public const string _BRLF = "<BR>\n";
        public const string _LF = "\n";
        public const string _CR = "\r";
        public const string _CRLF = "\r\n";
        public const string _TAB = "\t";
        public const string _SP = "&nbsp;";
        public const string _SP3 = "&nbsp;&nbsp;&nbsp;";


        String TagName;
        bool SelfClosing = false; // true - <tag> or <tag />; false - <tag></tag> 
        //public System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<string, string>> Attributes { get; }
        public ArrayList InnerHtmlBlock = new ArrayList();
        public Dictionary<String, String> Attribute = new Dictionary<String, String>();
        //IDictionary Интерфейс https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.idictionary?view=net-5.0
        public TagBuilder(String TagName)
        {
            this.TagName = TagName;
        }
        public override string ToString()
        {
            StringBuilder Output = new StringBuilder();
            Output.Append("<" + TagName);
            foreach (KeyValuePair<String, String> kvp in Attribute) Output.Append(" " + kvp.Key + "=" + kvp.Value);
            Output.Append(">");
            foreach (object o in InnerHtmlBlock) Output.Append(o.ToString());
            if (!SelfClosing) Output.Append("</" + TagName + ">");
            return Output.ToString();
        }
        public void Add(String InnerHTML) { InnerHtmlBlock.Add(InnerHTML); }
        public void Add(TagBuilder objTagBuilder) { InnerHtmlBlock.Add(objTagBuilder); }
        internal void InnerHtml(String InnerHTML)
        {
            this.InnerHtmlBlock.Clear();
            this.InnerHtmlBlock.Add(InnerHTML);
        }
    }
}

        /*class InnerHtmlBlock : System.Collections.IList //IList Интерфейс https://docs.microsoft.com/ru-ru/dotnet/api/system.collections.ilist?view=net-5.0
        {

            private object[] _contents = new object[8];
            private int _count;

            public InnerHtmlBlock()
            {

            }
            public int Add(object value) { return 1; }
            public int Add(String value) { return 1; }
            public int Add(TagBuilder value) { return 1; }

            public void Clear()        {        }
            public bool Contains(object value) { return true; }
            public int IndexOf(object value){ return 1; }
            public void Insert(int index, object value) { }
            public void Remove(object value) { }
            public void RemoveAt(int index) { }

            public bool IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public void Remove(object value)
            {
                RemoveAt(IndexOf(value));
            }

            public void RemoveAt(int index)
            {
                if ((index >= 0) && (index < Count))
                {
                    for (int i = index; i < Count - 1; i++)
                    {
                        _contents[i] = _contents[i + 1];
                    }
                    _count--;
                }
            }

            public object this[int index]
            {
                get
                {
                    return _contents[index];
                }
                set
                {
                    _contents[index] = value;
                }
            }

        }*/





