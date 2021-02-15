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
/*
<input type="checkbox" id="m16" class="PC"/>
    <label for=m16>m16<span class="OK"> OK </span></label>
        <div>
            Ping <span class="OK"> OK </span><BR>
            Copy script D:\Users\Andrew\Project\RemoteGroupExecution\RGE\bin\Debug\xcopy01.cmd to remote host \\m16\c$\xcopy01.cmd<span class="OK"> OK </span><BR>
            Run remote  script 
        </div>

cancellationToken.ThrowIfCancellationRequested();
local_result = false;
var subBlock = new PCHTMLBlock(Host + ".WMI");
subBlock.Label.InnerHtml("Execute remote script ");
try
{
    ...Do something ...
    local_result = true;
    subBlock.Label.InnerHtmlBlock.Add(HTMLSpanOK());
}
catch (Exception e) { subBlock.Label.InnerHtmlBlock.Add(HTMLSpanFAULT()); subBlock.Div.Add(e.Message); }
finally {block.Div.Add(subBlock.ToString());                        }
if (local_result)
{
 ...Continue...
}


 
 
*/
