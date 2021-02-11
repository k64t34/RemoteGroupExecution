using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PCHTMLBlock
{
    public String InnerHtml = "";
    String TagId;    
    public PCHTMLBlock(String TagId) 
    {
        this.TagId = TagId;
    }
    public override string ToString()
    {
        StringBuilder Output = new StringBuilder();        
        Output.Append("<input type=\"checkbox\" id=\"" + TagId + "\" class=\"PC\"/>");
        HTMLBuilder.TagBuilder Label = new HTMLBuilder.TagBuilder("label");
        Output.Append(Label.ToString());
        Output.Append("<BR>\n");
        return Output.ToString();
    }
}

