using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Translator
{
    public partial class Form1 : Form
    {
        private TransHelper transHelper = new TransHelper();
        Hashtable hashtable = new Hashtable();
        string f, t;
        string[] lang = new string[] {"自动识别", "汉语", "英语", "日语", "韩语", "文言文", "粤语" };
        public Form1()
        {
            InitializeComponent();
            hashtable.Add("自动识别", "auto");
            hashtable.Add("汉语", "zh");
            hashtable.Add("英语", "en");
            hashtable.Add("日语", "jp");
            hashtable.Add("韩语", "kor");
            hashtable.Add("文言文", "wyw");
            hashtable.Add("粤语", "yue");
            
            
            foreach (string v in lang)
            {
                FromComboBox.Items.Add(v);
                ToComboBox.Items.Add(v);
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //{"from":"zh","to":"en","trans_result":[{"src":"\u4f60\u597d","dst":"Hello"}]}
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //button1.Dispose();
            Console.WriteLine(f);
            Console.WriteLine(t);
            string res="";
            try
            {
                f = (string)hashtable[FromComboBox.SelectedItem.ToString()];
                t = (string)hashtable[ToComboBox.SelectedItem.ToString()];
                string str = transHelper.Trans(textBox1.Text,f,t);
                JObject jObject = (JObject)JsonConvert.DeserializeObject(str);
                JArray jArray = (JArray)JsonConvert.DeserializeObject(jObject["trans_result"].ToString());
                res = jArray[0]["dst"].ToString();
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("请选择语言！", "发生异常！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(WebException)
            {
                MessageBox.Show("网络请求超时！", "发生异常！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                textBox2.Text = res;
            }
            
        }

        private void FromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //f = hashtable[lang[FromComboBox.SelectedIndex]]
        }

        private void ToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            t = ToComboBox.SelectedItem.ToString();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = "";
        }
    }
}
