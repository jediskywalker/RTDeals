using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.IO;
using MyPaperControls;

/// Download from www.51aspx.com(£µ£±£á£ó£ð£ø£®£ã£ï£í)

namespace MyPaperControls.Designer
{
	public class MyPaperDesigner: ControlDesigner 
	{
		private MyPaper mp = null;
		public MyPaperDesigner()
		{
		}
		public override void Initialize(IComponent component) 
		{
			mp = (MyPaper)component;
			base.Initialize(component);
		}
		public override string GetDesignTimeHtml() 
		{
			StringWriter sw = new StringWriter();

			HtmlTextWriter htw = new HtmlTextWriter(sw);
			HtmlTable t = new HtmlTable();
			t.Align = mp.Align.ToString();
			t.CellPadding = 2;
			t.CellSpacing = 0;
			t.BorderColor = ColorTranslator.ToHtml(mp.BorderColor);
			t.BgColor = ColorTranslator.ToHtml(mp.BackColor);			
			t.Width = mp.Width.ToString();
			t.Height = mp.Height.ToString();

			HtmlTableRow tr = new HtmlTableRow();
			HtmlTableCell td = new HtmlTableCell();
			td.VAlign = "top";
			td.Align = "center";
			
			// inner table for iframe
			HtmlTable iframe = new HtmlTable();
			//iframe.BgColor = "#FFFFFF";
			iframe.Width="100%";
			iframe.Height="100%";
			iframe.CellPadding = 0;
			iframe.CellSpacing = 0;
			//iframe.Style.Add("border","1 solid ");
			//iframe.Style.Add("border","1 solid " + ColorTranslator.ToHtml(mytb.EditorBorderColorDark));
			HtmlTableRow tr2 = new HtmlTableRow();
			HtmlTableCell td2 = new HtmlTableCell();
			td2.VAlign = "middle";
			td2.Align = "center";
			td2.Controls.Add(new LiteralControl("<b><font face=arial size=2><font color=green>My</font>Pager:</b> " + mp.ID + "</font>&nbsp;&nbsp;<font size=2>1 2 3 4 <font color="+ColorTranslator.ToHtml(mp.CurrentNumberBgColor)+">5</font> 6 7 8 9</font>"));
			tr2.Cells.Add(td2);
			iframe.Rows.Add(tr2);

			td.Controls.Add(iframe);
			//td.Controls.Add(new LiteralControl("<br><br><br>"));
			tr.Cells.Add(td);
			t.Rows.Add(tr);
			t.RenderControl(htw);
			return sw.ToString();
		}
	}
}
