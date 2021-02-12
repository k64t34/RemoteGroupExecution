using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PCHTMLBlock
{
    //public String InnerHtml;
    String TagId;
    public HTMLBuilder.TagBuilder Label;
    public HTMLBuilder.TagBuilder Div;
    
    public PCHTMLBlock(String TagId) 
    {
        this.TagId = TagId;
        Label = new HTMLBuilder.TagBuilder("label");
        Label.InnerHtmlBlock.Add(TagId);
        Label.Attribute.Add("for",TagId);        
        Div = new HTMLBuilder.TagBuilder("div");

    }
    public override string ToString()
    {
        StringBuilder Output = new StringBuilder();        
        Output.Append("<input type=\"checkbox\" id=\"" + TagId + "\" class=\"PC\"/>");        
        Output.Append(Label.ToString());
        Output.Append("\n");
        Output.Append(Div.ToString());
        Output.Append("<BR>\n");
        return Output.ToString();
    }
}

