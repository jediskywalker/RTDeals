using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Resources;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Runtime.CompilerServices;
using MyPaperControls.Designer;

[assembly:TagPrefix("MyPaperControls", "MP")]
namespace MyPaperControls
{
	//=======================================
	//MyPaper分页控件源代码：
    //版权所有：唐运乐【kingeric】【QQ：330611174】【http://kingeric.cnblogs.com/】；
    //此源代码仅供学习参考，不得用作任何商业用途；
    //若需修改并重新编译该控件，请保留完整的源代码的版权信息！
    //有关控件升级及新控件发布信息，请留意 http://kingeric.cnblogs.com/ 。
    /// Download from www.51aspx.com(５１ａｓｐｘ．ｃｏｍ)

	[
	ToolboxData("<{0}:MyPaper runat=server></{0}:MyPaper>"),
	ValidationPropertyAttribute("Text"),
	Designer(typeof(MyPaperControls.Designer.MyPaperDesigner)),
	DescriptionAttribute("一个基于 MSHTML 的 ASP.NET 可视化分页控件")
	]

	[ParseChildren(true)]
	[PersistChildren(false)]
	public class MyPaper : WebControl, INamingContainer,IPostBackDataHandler
	{
		string Pager_LinkButton_Left = "";
		string Pager_LinkButton_Right = "";
		string RPageNo ="";
		

		#region============设置连接数字==========================
		private string linknumberwidth = "10px";
		private string linknumbercssclass = "";
		private Color currentnumberbgcolor = Color.Red;
		private int leftpagesize = 5;
		private int rightpagesize = 5;

		[Category("控件连接数字"),DefaultValue("10px"),DescriptionAttribute("获取或设置连接数字区域宽度"),NotifyParentProperty(true)]		
		public string LinkNumberWidth
		{
			get{return linknumberwidth;}
			set{linknumberwidth = value;}
		}
		[Category("控件连接数字"),DefaultValue("20px"),DescriptionAttribute("获取或设置连接数字css样式"),NotifyParentProperty(true)]		
		public string LinkNumberCssClass
		{
			get{return linknumbercssclass;}
			set{linknumbercssclass = value;}
		}
		[Category("控件连接数字"),DefaultValue("#FF6600"),DescriptionAttribute("获取或设置当前连接数字的背景颜色"),NotifyParentProperty(true)]
		public Color CurrentNumberBgColor
		{
			get{return currentnumberbgcolor;}
			set{currentnumberbgcolor = value;}
		}
		[Category("控件连接数字"),DefaultValue(5),DescriptionAttribute("设置或获取分页左边要显示的页码数"),NotifyParentProperty(true)]
		public int LeftPageSize
		{
			get{return leftpagesize;}
			set{leftpagesize = value;}
		}
		[Category("控件连接数字"),DefaultValue(5),DescriptionAttribute("设置或获取分页右边要显示的页码数"),NotifyParentProperty(true)]
		public int RightPageSize
		{
			get{return rightpagesize;}
			set{rightpagesize = value;}
		}
		#endregion

		#region============设置连接文字或图片==========================

		private string firstlink = "第一页";
		private string previouslink = "上一页";
		private string nextlink = "下一页";
		private string lastlink = "最后一页";
		private string linktextcssclass = "";
		private bool linkistext = true;

		[Category("控件连接文字或图片"),DefaultValue(""),DescriptionAttribute("获取或设置链接文字或图片地址的CSS类名"),NotifyParentProperty(true)]
		public string LinkTextCssClass
		{
			get{return linktextcssclass;}
			set{linktextcssclass = value;}
		}
		[Category("控件连接文字或图片"),DefaultValue(""),DescriptionAttribute("获取或设置链接是否是文字或图片"),NotifyParentProperty(true)]
		public bool LinkIsText
		{
			get{return linkistext;}
			set{linkistext = value;}
		}

		[Category("控件连接文字或图片"),DefaultValue("第一页"),DescriptionAttribute("获取或设置第一页的文字或图片地址"),NotifyParentProperty(true)]
		public string FirstLink 
		{
			get{return firstlink;}
			set{firstlink = value;}
		}
		[Category("控件连接文字或图片"),DefaultValue("上一页"),DescriptionAttribute("获取或设置上一页的文字或图片地址"),NotifyParentProperty(true)]
		public string PreviousLink 
		{
			get{return previouslink;}
			set{previouslink = value;}
		}
		[Category("控件连接文字或图片"),DefaultValue("下一页"),DescriptionAttribute("获取或设置下一页的文字或图片地址"),NotifyParentProperty(true)]
		public string NextLink 
		{
			get{return nextlink;}
			set{nextlink = value;}
		}
		[Category("控件连接文字或图片"),DefaultValue("最后一页"),DescriptionAttribute("获取或设置最后一页的文字或图片地址"),NotifyParentProperty(true)]
		public string LastLink 
		{
			get{return lastlink;}
			set{lastlink = value;}
		}

		#endregion

		#region===============初始相关函数=============
		protected override void OnInit( EventArgs e ) 
		{
			
			//controltopaginate = Parent.FindControl(ControlsCollection.ToString());
			GetFormClientID();
			
			base.OnInit(e);
		}
		private string GetFormClientID() 
		{
			if(IsHtmlForm(this.Parent)) 
			{
				return this.Parent.ClientID;
			}
			Control c = this.Parent;
			while(c != null) 
			{
				c = c.Parent;
				if(IsHtmlForm(c)) 
				{
					return c.Parent.ClientID;
				}
			}
			
			throw new Exception(string.Format("类型“{1}”的控件“{0}”必须放在具有 runat=server 的窗体标记内。",this.ID,this.GetType().ToString()));
			
		}
		private bool IsHtmlForm(Control c) 
		{
			return (c is HtmlForm);
		}
		protected override void OnLoad(EventArgs e) 
		{
			if ((base.ID + "")== "") throw new Exception("MyPaper 控件必须指定 ID 属性。");
			
		}
//		protected override void Page_Load(object sender, System.EventArgs e)
//		{
//		}
		protected override void OnPreRender( EventArgs e ) 
		{
			Page.RegisterRequiresPostBack(this);
			base.OnPreRender(e);
		}
		ResourceManager _resourceManager;
		private string GetResourceStringFromResourceManager(string key) 
		{
			if (this._resourceManager == null) 
				lock (this)

					if (this._resourceManager == null)
						this._resourceManager = new ResourceManager("MyPaperControls.MyPaper", typeof(MyPaper).Module.Assembly);

			return this._resourceManager.GetString(key, null);
		}

		
		#endregion

		#region================LoadPostData==========================
		public bool LoadPostData(String postDataKey, NameValueCollection values) 
		{			
//			string PresentValue = this.ViewStateText;
//			string PostedValue = values[base.ID];
//			if (!PresentValue.Equals(PostedValue)) 
//			{
//				//this.Text = PostedValue;
//				return true;
//			} 
//			else 
//			{
//				return false;
//			}
			return true;
		}
		#endregion

		#region================获取或设置编辑器中的内容，输出前将经过一些整理==========================
		[
		CategoryAttribute("输出"),
		DescriptionAttribute("获取控件的内容。")
		]
		public string Text 
		{
			get 
			{
				string output = this.ViewStateText;
				return output;
			}
//			set 
//			{
//				ViewState["Text"] = value;
//			}
		}
		#endregion

		#region================ViewStateText==========================
		private string ViewStateText 
		{
			get 
			{
				object savedState = this.ViewState["Text"];
				return (savedState == null) ? "" : (string) savedState;
			}
		}
		#endregion

		#region================RaisePostDataChangedEvent==========================
		public void RaisePostDataChangedEvent() 
		{
			// nothing happens for text changed
		}
		#endregion

		#region==================控件初始化基本信息=======================

		private string GetPageUrl(string paraPageUrl)
		{
			//===========存在&==================
			if(paraPageUrl.LastIndexOf("&")!=-1)
			{
				//========&不是最后一个字符
				if(paraPageUrl.LastIndexOf("&")!=paraPageUrl.Length-1)
				{
					paraPageUrl = paraPageUrl+"&PageNo=";
				}
				else
				{
					paraPageUrl = paraPageUrl+"PageNo=";
				}
			}
			else
			{
				//===========存在?==================
				if(paraPageUrl.LastIndexOf("?")!=-1)
				{
					//========?不是最后一个字符
					if(paraPageUrl.LastIndexOf("?")!=paraPageUrl.Length-1)
					{
						paraPageUrl = paraPageUrl+"&PageNo=";
					}
					else
					{
						paraPageUrl = paraPageUrl+"PageNo=";
					}
				}
				else//========不存在&和?=================
				{
					paraPageUrl = paraPageUrl+"?PageNo=";
				}
			}
			return paraPageUrl;
		}
		#endregion

		protected override void Render(HtmlTextWriter output)
		{
			
			if(this.LinkIsText==false)
			{
				FirstLink = "<img src="+FirstLink+" border='0'>";
				PreviousLink = "<img src="+PreviousLink+" border='0'>";
				NextLink = "<img src="+NextLink+" border='0'>";
				LastLink = "<img src="+LastLink+" border='0'>";
			}
			PageUrl = this.GetPageUrl(PageUrl);

			Pager_LinkButton_Left = "<a href=\""+this.PageUrl+"1\">"+this.FirstLink+"</a>";
			if(this.CurrentPageIndex>1)
				Pager_LinkButton_Left += "&nbsp;<a class=\""+LinkTextCssClass+"\" href=\""+this.PageUrl+""+(this.CurrentPageIndex-1)+"\">"+this.PreviousLink+"</a>";
			else
				Pager_LinkButton_Left += "&nbsp;"+this.PreviousLink+"";
			//==============右边按钮
			if(this.CurrentPageIndex<this.PageTotal)
				Pager_LinkButton_Right = "<a class=\""+LinkTextCssClass+"\" href=\""+this.PageUrl+""+(this.CurrentPageIndex+1)+"\">"+this.NextLink+"</a>";
			else
				Pager_LinkButton_Right = ""+this.NextLink+"";
			Pager_LinkButton_Right += "&nbsp;<a class=\""+LinkTextCssClass+"\" href=\""+this.PageUrl+""+this.PageTotal+"\">"+this.LastLink+"</a>";
			
			string TT_Text;
			string FontCollection = "";
			FontCollection += "font:'"+this.Font.Names+"';";
			FontCollection += "font-size:"+this.Font.Size+";";
			if(this.Font.Italic==true)
			{
				FontCollection += "font-style: italic;";
			}
			if(this.Font.Bold==true)
			{
				FontCollection += "font-weight: bold;";
			}
			FontCollection += "text-decoration:";
			if(this.Font.Overline==true)
			{
				FontCollection += " overline ";
			}
			if(this.Font.Underline==true)
			{
				FontCollection += " underline ";
			}
			if(this.Font.Strikeout==true)
			{
				FontCollection += " line-through ";
			}
			FontCollection += ";";
			FontCollection += "";
			FontCollection += "";
			FontCollection += "";

			//===========是否可见===================
			if(this.Visible==true)
			{
				TT_Text ="<table align="+Align.ToString()+" style=\""+FontCollection+"\" title=\""+this.ToolTip+"\" cellspacing=\""+this.CellSpacing+"\" cellpadding=\""+this.CellPadding+"\" ID=\"Table_"+base.UniqueID+"\" style=\""+this.Table_Style+"\" height=\""+this.Height+"\" width=\""+this.Width+"\" class=\""+this.CssClass+"\" border=\""+this.BorderWidth+"\"  bgcolor=\""+ColorTranslator.ToHtml(BackColor).ToString()+"\" bordercolor=\""+ColorTranslator.ToHtml(BorderColor)+"\"><tr>\t\n";
				TT_Text += "<td nowrap=\"true\"  align=\"center\" bordercolor=\"\" style = ''  bgcolor=\"\">"+this.DescriptionText+"</td>\t\n";
				TT_Text += "<td nowrap=\"true\"  align=\"center\" bordercolor=\"\"   bgcolor=\"\">&nbsp;"+this.Pager_LinkButton_Left+"&nbsp;</td>\t\n";
			
				string TT_Text_Left="";
				int TT_Text_Num ;
				int TT_Text_Start;
				
				if(this.PageTotal<=(this.LeftPageSize+this.RightPageSize))//===============如果总页数小于两边要显示的页数=====================
				{
					TT_Text_Start = 1;
					TT_Text_Num=PageTotal;
				}
				else
				{
					if(this.CurrentPageIndex<=this.LeftPageSize)//===============当前页码小于左边显示记录=====================
					{
						TT_Text_Start = 1;
						TT_Text_Num=this.LeftPageSize+this.RightPageSize;
					}
					else//===============当前页码大于等于右边显示记录=====================
					{
						if(this.CurrentPageIndex>=this.PageTotal-this.RightPageSize)
						{
							TT_Text_Start = this.CurrentPageIndex-this.LeftPageSize;
							TT_Text_Num = this.PageTotal;
						}
						else
						{
							TT_Text_Start = this.CurrentPageIndex-this.LeftPageSize;
							TT_Text_Num=this.CurrentPageIndex+this.RightPageSize;
						}
					}
				}
				
				for(int i=TT_Text_Start;i<=TT_Text_Num;i++)
				{
					if(this.CurrentPageIndex==i)
					{
						TT_Text_Left += "<td  nowrap=\"true\"  style=\"width:"+this.LinkNumberWidth+";\" align=\"center\" bgcolor=\""+CurrentNumberBgColor.Name+"\" ><a href='"+PageUrl+""+i+"' class='"+this.LinkNumberCssClass+"'><b><font color=\""+this.CurrentPageIndex+"\">"+i+"</font></b></a></td>\t\n";
					}
					else
					{
						TT_Text_Left += "<td  nowrap=\"true\"  style=\"width:"+this.LinkNumberWidth+";\" align=\"center\" bgcolor=\"\"><a href='"+PageUrl+""+i+"' class='"+this.LinkNumberCssClass+"'>"+i+"</a></td>\t\n";
					}
				}
				TT_Text += TT_Text_Left;


				TT_Text += "<td  align=\"center\" nowrap=\"true\" bordercolor=\"\"  bgcolor=\"\">&nbsp;"+this.Pager_LinkButton_Right+"&nbsp;</td>\t\n";
				if(this.IsGotoTextBoxVisible==true)
				{
					TT_Text += "<td  align=\"center\" nowrap=\"true\" bordercolor=\"\"  bgcolor=\"\"><input onkeydown=\""+this.UniqueID+"_SubmitKeyClick('"+this.UniqueID+"_goto');\" type=\"text\" onkeyup=\"value=value.replace(/[^\\d]/g,'') \"  onbeforepaste=\"clipboardData.setData('text',clipboardData.getData('text').replace(/[^\\d]/g,''))\"  name=\""+this.UniqueID+"_PageNo\" value=\""+this.CurrentPageIndex+"\" size=\"4\"/><input id=\""+this.UniqueID+"_goto\" onclick=\""+this.UniqueID+"_GotoPage()\" type=\"button\" value=\"转到\" /></td>\t\n";
					TT_Text +=  WriteJs();
				}
				TT_Text += "</tr></table>\t\n";
				output.Write(TT_Text);
			}
			else
			{
				output.Write(this.Text);
			}
		}

		private string WriteJs()
		{
			string HTML = "";
			HTML += "<script language =javascript>\n";
			HTML += "function "+this.UniqueID+"_GotoPage()\n";
			HTML += "{\n";
			HTML += "   var b = document.getElementById(\""+this.UniqueID+"_PageNo\").value;\n";
			HTML += "   if(b>"+this.PageTotal+"){\n";
			HTML += "      alert(\"无效页数！\");\n";
			HTML += "      document.getElementById(\""+this.UniqueID+"_PageNo\").value=\"\";\n";
			HTML += "      document.getElementById(\""+this.UniqueID+"_PageNo\").focus();\n";
			HTML += "   }else{\n";
			HTML += "      window.location.replace(\""+this.PageUrl+"\"+b+\"\");\n";
			HTML += "   }\n";
			HTML += "}\n";
			HTML += "function "+this.UniqueID+"_SubmitKeyClick(button)\n";
			HTML += "{ \n";
			HTML += "   if (event.keyCode == 13) \n";
			HTML += "   {\n";
			HTML += "       event.keyCode=9;\n";
			HTML += "       event.returnValue = false;\n";
			HTML += "       document.all[button].click();\n";
			HTML += "   }\n";
			HTML += "}\n";
			HTML += "</script>";
			return HTML;
		}

		private string Table_Style ="";
		private Color bordercolor = Color.White;
		private Color backcolor = Color.White;
		private Unit cellspacing = Unit.Pixel(1);
		private Unit cellpadding = Unit.Pixel(0);
		private Unit borderwidth = Unit.Pixel(1);
		private Unit width = Unit.Pixel(300);
		private Unit height = Unit.Pixel(28);
		private string cssclass = "";
		private string tooltip = "一个基于 MSHTML 的 ASP.NET 可视化分页控件&#10;[堂堂STDUIO]&#10;[QQ：330611174]";
		private bool visible = true;
		private Align _align;

		[Category("控件设置"),DefaultValue(1),DescriptionAttribute("设置或获取表格的间距值")]
		public Unit CellSpacing
		{
			get{return cellspacing;}
			set{cellspacing = value;}
		}

		[CategoryAttribute("控件设置"),DefaultValue(Align.Center),
		DescriptionAttribute("获取或设置控件的水平对齐方式。可以为：Center，Left，Right。")
		]
		public Align Align
		{
			get { return _align;}
			set {_align = value;}
		}
		
		[Category("控件设置"),DefaultValue("0"),DescriptionAttribute("设置或获取表格的填充值")]
		public Unit CellPadding
		{
			get{return cellpadding;}
			set{cellpadding = value;}
		}
		[Category("控件设置"),DefaultValue("一个基于 MSHTML 的 ASP.NET 可视化分页控件&#10;[堂堂STDUIO]&#10;[QQ：330611174]"),DescriptionAttribute("鼠标放到控件上时显示的工具提示")]
		public override string ToolTip
		{
			get{return tooltip;}
			//set{tooltip = value;}
		}
		[Category("控件设置"),DescriptionAttribute("设置或获取控件的边框颜色")]
		public override Color BorderColor
		{
			get{return bordercolor;}
			set{bordercolor = value;}
		}
		[Category("控件设置"),DescriptionAttribute("设置或获取控件的背景颜色")]
		public override Color BackColor
		{
			get{return backcolor;}
			set{backcolor = value;}
		}
		[Category("控件设置"),DefaultValue("0"),DescriptionAttribute("设置或获取控件的边框宽度")]
		public override Unit BorderWidth
		{
			get{return borderwidth;}
			set{borderwidth = value;}
		}
		[Category("控件设置"),DefaultValue("300px"),DescriptionAttribute("设置或获取控件的宽度")]
		public override Unit Width
		{
			get{return width;}
			set{width = value;}
		}
		[Category("控件设置"),DefaultValue("28px"),DescriptionAttribute("设置或获取控件的高度")]
		public override Unit Height
		{
			get{return height;}
			set{height = value;}
		}
		[Category("控件设置"),DefaultValue(""),DescriptionAttribute("应用于该控件的CSS类名")]
		public override string CssClass
		{
			get{return cssclass;}
			set{cssclass = value;}
		}
		[Category("控件设置"),DefaultValue(true),DescriptionAttribute("指示该控件是否呈现并被呈现出来")]
		public override bool Visible
		{
			get{return visible;}
			set{visible = value;}
		}
		[Category("控件设置"),DefaultValue(true),DescriptionAttribute("设置控件文字属性")]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}
		
		private int recordcount;
		private int pagesize = 15;
		private int currentpageindex = 0;
		private int pagetotal;
		private string pageurl = "";
		private string pagenoname = "PageNo";
		private int dataset_startindex;

		[Category("控件数据"),DescriptionAttribute("设置或获取连接地址"),NotifyParentProperty(true)]
		public string PageUrl
		{
			get{
				if(pageurl=="")
				{
					pageurl = "?";
				}
				return pageurl;
			}
			set{pageurl = value;}
		}

		[Category("控件数据"),DescriptionAttribute("设置或获取分页参数名称"),NotifyParentProperty(true)]
		public string PageNoName
		{
			get{return pagenoname;}
			set{pagenoname = value;}
		}

		[Category("控件数据"),DescriptionAttribute("获取记录总数"),NotifyParentProperty(true)]
		public int RecordCount
		{
			get{return recordcount;}
			set{recordcount=value;}
		}
		[Category("控件数据"),DefaultValue("15"),DescriptionAttribute("设置或获取每页记录数"),NotifyParentProperty(true)]
		public int PageSize
		{
			get{return pagesize;}
			set{pagesize = value;}
		}
		[Category("控件数据"),DescriptionAttribute("设置或获取当前页码"),NotifyParentProperty(true)]
		public int CurrentPageIndex
		{
			get{
				RPageNo =Page.Request[""+pagenoname+""];
				if(RPageNo==null)
				{
					currentpageindex=1;
				}
				else
				{
					if(RPageNo=="")
					{
						currentpageindex=1;
					}
					else
					{
						if(this.isNumber(RPageNo)==false)
						{
							currentpageindex=1;
						}
						else
						{
							if(Convert.ToInt32(RPageNo)<=0)
							{
								currentpageindex=1;
							}
							else
							{
								currentpageindex=Convert.ToInt32(RPageNo);
							}
						}
					}
				}
				return currentpageindex;
			}
		}
		[Category("控件数据"),DescriptionAttribute("获取页码总数"),NotifyParentProperty(true)]
		public int PageTotal
		{
			get{
				if(RecordCount%PageSize==0)
				{
					pagetotal=  RecordCount/PageSize;
				}
				else
				{
					pagetotal = RecordCount/PageSize+1;
				}
				return pagetotal;
			}
		}
		[Category("控件数据"),DescriptionAttribute("获取数据集开始位置"),NotifyParentProperty(true)]

		public int DataSet_StartIndex
		{
			get
			{
				dataset_startindex = (CurrentPageIndex-1)*this.PageSize;
				if(dataset_startindex<0)
					dataset_startindex = 0;
				return dataset_startindex;
			}
		}

		private string descriptiontext = "";
		private bool isgototextboxvisible = true;

		[Category("控件外观"),DescriptionAttribute("获取或设置分页的描述文字"),NotifyParentProperty(true)]
		public string DescriptionText
		{
			get{return descriptiontext;}
			set{descriptiontext = value;}
		}

		[Category("控件外观"),DefaultValue(true),DescriptionAttribute("获取或设置转到文本框和按钮是否显示"),NotifyParentProperty(true)]
		public bool IsGotoTextBoxVisible
		{
			get{return isgototextboxvisible;}
			set{isgototextboxvisible = value;}
		}

		private bool isNumber(string s)//k＝1表示s可以存在，也可以不存在，k＝2表示s一定要存在
		{
			int Flag = 0;
			if(s==null)//如果它是空的
				s="";
			if(s!="")//如果有值
			{
				char[]str = s.ToCharArray();
				for(int i = 0;i < str.Length ;i++)
				{
					if (Char.IsNumber(str[i]))
					{
						Flag++;
					}
					else
					{
						Flag = -1;
						break;
					}
				}
				if ( Flag > 0 )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else//如果没有值
			{
				return false;
				
			}
		}
		

	}
}
