using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageNamespace;
using MyMessage = MessageNamespace.Message;

namespace DictionaryClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var m = Client.Connect("", new MyMessage { Action = ActionType.Add, Key = textBox1.Text, Value = textBox2.Text });
            toolStripStatusLabel1.Text = m.Result;
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
