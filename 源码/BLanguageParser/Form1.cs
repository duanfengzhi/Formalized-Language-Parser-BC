using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BLanguageBParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 浏览文件按钮事件.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            //打开文件
            using (OpenFileDialog open = new OpenFileDialog())
            {
                string fileName = string.Empty;  //文件名
                open.Multiselect = false;
                open.Title = "打开文件";
                open.Filter = "代码|*.txt";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    fileName = open.FileName;
                    textBox1.Text = fileName;
                    //读取文件内容
                    StreamReader sr = new StreamReader(fileName, Encoding.Default);
                    String ls_input = sr.ReadToEnd().TrimStart();
                    if (!string.IsNullOrEmpty(ls_input))
                        textBox2.Text = ls_input;
                    sr.Close();
                }
                if (fileName == null)
                    return;
            }
        }

        /// <summary>
        /// 解析按钮事件.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();  //开始监视代码运行时间
                Compile compile = new Compile();
                compile.scanner(textBox1.Text);
                Parser parser = new Parser();         
                parser.BParser();
                watch.Stop();  //停止监视
                TimeSpan timespan = watch.Elapsed;  //获取当前实例测量得出的总时间
                textBox3.Text = parser.parseresult + "编译时间:\r\n" + timespan.TotalMilliseconds + "(毫秒)";
            }
            else
            {
                MessageBox.Show("要解析的内容为空!", "错误", MessageBoxButtons.OKCancel);
            }
        }

        /// <summary>
        /// 文件拖拽事件.
        /// </summary>
        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;  //指定鼠标形状（更好看）
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 文件拖拽事件.
        /// </summary>
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            textBox1.Text = path;
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String ls_input = sr.ReadToEnd().TrimStart();
            if (!string.IsNullOrEmpty(ls_input))
                textBox2.Text = ls_input;
            sr.Close();
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
        }
    }
}
