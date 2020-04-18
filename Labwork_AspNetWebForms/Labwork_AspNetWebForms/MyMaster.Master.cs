using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Labwork_AspNetWebForms
{
    public partial class MyMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Выполнять инициализацию, только если страница запрашивается
            // впервые.
            // После этого данная информация отслеживается в состоянии вида,
            if (!this.Page.IsPostBack)
            {
                // Установить атрибуты стиля для настройки внешнего вида.
                TextBox1.Style["font-size"] = "20px";
                TextBox1.Style["color"] = "red";
                // Использовать слегка отличающийся, но эквивалентный синтаксис
                // для установки атрибута стиля.
                TextBox1.Style.Add("background-color", "lightyellow");
                // Установить текст по умолчанию.
                TextBox1.Text = "<Enter e-mail address here>";
                // Установить/ другие нестандартные атрибуты.
                TextBox1.Attributes["onfocus"] = "alert(TextBoxl.value)";
            }
        }
    }
}