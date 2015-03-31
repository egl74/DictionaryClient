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

        private void Form1_Load(object sender, EventArgs e)
        {
            Client.Connect("", new MyMessage { Action = ActionType.Add, Key = "ioio", Value = "32w0"}, textBox1);
        }
    }
}
