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
	//MyPaper��ҳ�ؼ�Դ���룺
    //��Ȩ���У������֡�kingeric����QQ��330611174����http://kingeric.cnblogs.com/����
    //��Դ�������ѧϰ�ο������������κ���ҵ��;��
    //�����޸Ĳ����±���ÿؼ����뱣��������Դ����İ�Ȩ��Ϣ��
    //�йؿؼ��������¿ؼ�������Ϣ�������� http://kingeric.cnblogs.com/ ��
    /// Download from www.51aspx.com(���������������)

	[
	ToolboxData("<{0}:MyPaper runat=server></{0}:MyPaper>"),
	ValidationPropertyAttribute("Text"),
	Designer(typeof(MyPaperControls.Designer.MyPaperDesigner)),
	DescriptionAttribute("һ������ MSHTML �� ASP.NET ���ӻ���ҳ�ؼ�")
	]

	[ParseChildren(true)]
	[PersistChildren(false)]
	public class MyPaper : WebControl, INamingContainer,IPostBackDataHandler
	{
		string Pager_LinkButton_Left = "";
		string Pager_LinkButton_Right = "";
		string RPageNo ="";
		

		#region============������������==========================
		private string linknumberwidth = "10px";
		private string linknumbercssclass = "";
		private Color currentnumberbgcolor = Color.Red;
		private int leftpagesize = 5;
		private int rightpagesize = 5;

		[Category("�ؼ���������"),DefaultValue("10px"),DescriptionAttribute("��ȡ��������������������"),NotifyParentProperty(true)]		
		public string LinkNumberWidth
		{
			get{return linknumberwidth;}
			set{linknumberwidth = value;}
		}
		[Category("�ؼ���������"),DefaultValue("20px"),DescriptionAttribute("��ȡ��������������css��ʽ"),NotifyParentProperty(true)]		
		public string LinkNumberCssClass
		{
			get{return linknumbercssclass;}
			set{linknumbercssclass = value;}
		}
		[Category("�ؼ���������"),DefaultValue("#FF6600"),DescriptionAttribute("��ȡ�����õ�ǰ�������ֵı�����ɫ"),NotifyParentProperty(true)]
		public Color CurrentNumberBgColor
		{
			get{return currentnumberbgcolor;}
			set{currentnumberbgcolor = value;}
		}
		[Category("�ؼ���������"),DefaultValue(5),DescriptionAttribute("���û��ȡ��ҳ���Ҫ��ʾ��ҳ����"),NotifyParentProperty(true)]
		public int LeftPageSize
		{
			get{return leftpagesize;}
			set{leftpagesize = value;}
		}
		[Category("�ؼ���������"),DefaultValue(5),DescriptionAttribute("���û��ȡ��ҳ�ұ�Ҫ��ʾ��ҳ����"),NotifyParentProperty(true)]
		public int RightPageSize
		{
			get{return rightpagesize;}
			set{rightpagesize = value;}
		}
		#endregion

		#region============�����������ֻ�ͼƬ==========================

		private string firstlink = "��һҳ";
		private string previouslink = "��һҳ";
		private string nextlink = "��һҳ";
		private string lastlink = "���һҳ";
		private string linktextcssclass = "";
		private bool linkistext = true;

		[Category("�ؼ��������ֻ�ͼƬ"),DefaultValue(""),DescriptionAttribute("��ȡ�������������ֻ�ͼƬ��ַ��CSS����"),NotifyParentProperty(true)]
		public string LinkTextCssClass
		{
			get{return linktextcssclass;}
			set{linktextcssclass = value;}
		}
		[Category("�ؼ��������ֻ�ͼƬ"),DefaultValue(""),DescriptionAttribute("��ȡ�����������Ƿ������ֻ�ͼƬ"),NotifyParentProperty(true)]
		public bool LinkIsText
		{
			get{return linkistext;}
			set{linkistext = value;}
		}

		[Category("�ؼ��������ֻ�ͼƬ"),DefaultValue("��һҳ"),DescriptionAttribute("��ȡ�����õ�һҳ�����ֻ�ͼƬ��ַ"),NotifyParentProperty(true)]
		public string FirstLink 
		{
			get{return firstlink;}
			set{firstlink = value;}
		}
		[Category("�ؼ��������ֻ�ͼƬ"),DefaultValue("��һҳ"),DescriptionAttribute("��ȡ��������һҳ�����ֻ�ͼƬ��ַ"),NotifyParentProperty(true)]
		public string PreviousLink 
		{
			get{return previouslink;}
			set{previouslink = value;}
		}
		[Category("�ؼ��������ֻ�ͼƬ"),DefaultValue("��һҳ"),DescriptionAttribute("��ȡ��������һҳ�����ֻ�ͼƬ��ַ"),NotifyParentProperty(true)]
		public string NextLink 
		{
			get{return nextlink;}
			set{nextlink = value;}
		}
		[Category("�ؼ��������ֻ�ͼƬ"),DefaultValue("���һҳ"),DescriptionAttribute("��ȡ���������һҳ�����ֻ�ͼƬ��ַ"),NotifyParentProperty(true)]
		public string LastLink 
		{
			get{return lastlink;}
			set{lastlink = value;}
		}

		#endregion

		#region===============��ʼ��غ���=============
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
			
			throw new Exception(string.Format("���͡�{1}���Ŀؼ���{0}��������ھ��� runat=server �Ĵ������ڡ�",this.ID,this.GetType().ToString()));
			
		}
		private bool IsHtmlForm(Control c) 
		{
			return (c is HtmlForm);
		}
		protected override void OnLoad(EventArgs e) 
		{
			if ((base.ID + "")== "") throw new Exception("MyPaper �ؼ�����ָ�� ID ���ԡ�");
			
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

		#region================��ȡ�����ñ༭���е����ݣ����ǰ������һЩ����==========================
		[
		CategoryAttribute("���"),
		DescriptionAttribute("��ȡ�ؼ������ݡ�")
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

		#region==================�ؼ���ʼ��������Ϣ=======================

		private string GetPageUrl(string paraPageUrl)
		{
			//===========����&==================
			if(paraPageUrl.LastIndexOf("&")!=-1)
			{
				//========&�������һ���ַ�
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
				//===========����?==================
				if(paraPageUrl.LastIndexOf("?")!=-1)
				{
					//========?�������һ���ַ�
					if(paraPageUrl.LastIndexOf("?")!=paraPageUrl.Length-1)
					{
						paraPageUrl = paraPageUrl+"&PageNo=";
					}
					else
					{
						paraPageUrl = paraPageUrl+"PageNo=";
					}
				}
				else//========������&��?=================
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
			//==============�ұ߰�ť
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

			//===========�Ƿ�ɼ�===================
			if(this.Visible==true)
			{
				TT_Text ="<table align="+Align.ToString()+" style=\""+FontCollection+"\" title=\""+this.ToolTip+"\" cellspacing=\""+this.CellSpacing+"\" cellpadding=\""+this.CellPadding+"\" ID=\"Table_"+base.UniqueID+"\" style=\""+this.Table_Style+"\" height=\""+this.Height+"\" width=\""+this.Width+"\" class=\""+this.CssClass+"\" border=\""+this.BorderWidth+"\"  bgcolor=\""+ColorTranslator.ToHtml(BackColor).ToString()+"\" bordercolor=\""+ColorTranslator.ToHtml(BorderColor)+"\"><tr>\t\n";
				TT_Text += "<td nowrap=\"true\"  align=\"center\" bordercolor=\"\" style = ''  bgcolor=\"\">"+this.DescriptionText+"</td>\t\n";
				TT_Text += "<td nowrap=\"true\"  align=\"center\" bordercolor=\"\"   bgcolor=\"\">&nbsp;"+this.Pager_LinkButton_Left+"&nbsp;</td>\t\n";
			
				string TT_Text_Left="";
				int TT_Text_Num ;
				int TT_Text_Start;
				
				if(this.PageTotal<=(this.LeftPageSize+this.RightPageSize))//===============�����ҳ��С������Ҫ��ʾ��ҳ��=====================
				{
					TT_Text_Start = 1;
					TT_Text_Num=PageTotal;
				}
				else
				{
					if(this.CurrentPageIndex<=this.LeftPageSize)//===============��ǰҳ��С�������ʾ��¼=====================
					{
						TT_Text_Start = 1;
						TT_Text_Num=this.LeftPageSize+this.RightPageSize;
					}
					else//===============��ǰҳ����ڵ����ұ���ʾ��¼=====================
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
					TT_Text += "<td  align=\"center\" nowrap=\"true\" bordercolor=\"\"  bgcolor=\"\"><input onkeydown=\""+this.UniqueID+"_SubmitKeyClick('"+this.UniqueID+"_goto');\" type=\"text\" onkeyup=\"value=value.replace(/[^\\d]/g,'') \"  onbeforepaste=\"clipboardData.setData('text',clipboardData.getData('text').replace(/[^\\d]/g,''))\"  name=\""+this.UniqueID+"_PageNo\" value=\""+this.CurrentPageIndex+"\" size=\"4\"/><input id=\""+this.UniqueID+"_goto\" onclick=\""+this.UniqueID+"_GotoPage()\" type=\"button\" value=\"ת��\" /></td>\t\n";
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
			HTML += "      alert(\"��Чҳ����\");\n";
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
		private string tooltip = "һ������ MSHTML �� ASP.NET ���ӻ���ҳ�ؼ�&#10;[����STDUIO]&#10;[QQ��330611174]";
		private bool visible = true;
		private Align _align;

		[Category("�ؼ�����"),DefaultValue(1),DescriptionAttribute("���û��ȡ���ļ��ֵ")]
		public Unit CellSpacing
		{
			get{return cellspacing;}
			set{cellspacing = value;}
		}

		[CategoryAttribute("�ؼ�����"),DefaultValue(Align.Center),
		DescriptionAttribute("��ȡ�����ÿؼ���ˮƽ���뷽ʽ������Ϊ��Center��Left��Right��")
		]
		public Align Align
		{
			get { return _align;}
			set {_align = value;}
		}
		
		[Category("�ؼ�����"),DefaultValue("0"),DescriptionAttribute("���û��ȡ�������ֵ")]
		public Unit CellPadding
		{
			get{return cellpadding;}
			set{cellpadding = value;}
		}
		[Category("�ؼ�����"),DefaultValue("һ������ MSHTML �� ASP.NET ���ӻ���ҳ�ؼ�&#10;[����STDUIO]&#10;[QQ��330611174]"),DescriptionAttribute("���ŵ��ؼ���ʱ��ʾ�Ĺ�����ʾ")]
		public override string ToolTip
		{
			get{return tooltip;}
			//set{tooltip = value;}
		}
		[Category("�ؼ�����"),DescriptionAttribute("���û��ȡ�ؼ��ı߿���ɫ")]
		public override Color BorderColor
		{
			get{return bordercolor;}
			set{bordercolor = value;}
		}
		[Category("�ؼ�����"),DescriptionAttribute("���û��ȡ�ؼ��ı�����ɫ")]
		public override Color BackColor
		{
			get{return backcolor;}
			set{backcolor = value;}
		}
		[Category("�ؼ�����"),DefaultValue("0"),DescriptionAttribute("���û��ȡ�ؼ��ı߿���")]
		public override Unit BorderWidth
		{
			get{return borderwidth;}
			set{borderwidth = value;}
		}
		[Category("�ؼ�����"),DefaultValue("300px"),DescriptionAttribute("���û��ȡ�ؼ��Ŀ��")]
		public override Unit Width
		{
			get{return width;}
			set{width = value;}
		}
		[Category("�ؼ�����"),DefaultValue("28px"),DescriptionAttribute("���û��ȡ�ؼ��ĸ߶�")]
		public override Unit Height
		{
			get{return height;}
			set{height = value;}
		}
		[Category("�ؼ�����"),DefaultValue(""),DescriptionAttribute("Ӧ���ڸÿؼ���CSS����")]
		public override string CssClass
		{
			get{return cssclass;}
			set{cssclass = value;}
		}
		[Category("�ؼ�����"),DefaultValue(true),DescriptionAttribute("ָʾ�ÿؼ��Ƿ���ֲ������ֳ���")]
		public override bool Visible
		{
			get{return visible;}
			set{visible = value;}
		}
		[Category("�ؼ�����"),DefaultValue(true),DescriptionAttribute("���ÿؼ���������")]
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

		[Category("�ؼ�����"),DescriptionAttribute("���û��ȡ���ӵ�ַ"),NotifyParentProperty(true)]
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

		[Category("�ؼ�����"),DescriptionAttribute("���û��ȡ��ҳ��������"),NotifyParentProperty(true)]
		public string PageNoName
		{
			get{return pagenoname;}
			set{pagenoname = value;}
		}

		[Category("�ؼ�����"),DescriptionAttribute("��ȡ��¼����"),NotifyParentProperty(true)]
		public int RecordCount
		{
			get{return recordcount;}
			set{recordcount=value;}
		}
		[Category("�ؼ�����"),DefaultValue("15"),DescriptionAttribute("���û��ȡÿҳ��¼��"),NotifyParentProperty(true)]
		public int PageSize
		{
			get{return pagesize;}
			set{pagesize = value;}
		}
		[Category("�ؼ�����"),DescriptionAttribute("���û��ȡ��ǰҳ��"),NotifyParentProperty(true)]
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
		[Category("�ؼ�����"),DescriptionAttribute("��ȡҳ������"),NotifyParentProperty(true)]
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
		[Category("�ؼ�����"),DescriptionAttribute("��ȡ���ݼ���ʼλ��"),NotifyParentProperty(true)]

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

		[Category("�ؼ����"),DescriptionAttribute("��ȡ�����÷�ҳ����������"),NotifyParentProperty(true)]
		public string DescriptionText
		{
			get{return descriptiontext;}
			set{descriptiontext = value;}
		}

		[Category("�ؼ����"),DefaultValue(true),DescriptionAttribute("��ȡ������ת���ı���Ͱ�ť�Ƿ���ʾ"),NotifyParentProperty(true)]
		public bool IsGotoTextBoxVisible
		{
			get{return isgototextboxvisible;}
			set{isgototextboxvisible = value;}
		}

		private bool isNumber(string s)//k��1��ʾs���Դ��ڣ�Ҳ���Բ����ڣ�k��2��ʾsһ��Ҫ����
		{
			int Flag = 0;
			if(s==null)//������ǿյ�
				s="";
			if(s!="")//�����ֵ
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
			else//���û��ֵ
			{
				return false;
				
			}
		}
		

	}
}
