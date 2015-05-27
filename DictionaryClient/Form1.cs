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
        private Dictionary<string, string> dictionary;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var m = Client.Connect("", new MyMessage { Action = ActionType.Add, Key = textBox1.Text, Value = textBox2.Text }).Result;
            toolStripStatusLabel1.Text = m;
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var m = Client.Connect("", new MyMessage { Action = ActionType.Edit, Key = textBox4.Text, Value = textBox3.Text }).Result;
            toolStripStatusLabel1.Text = m;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var m = Client.Connect("", new MyMessage { Action = ActionType.Remove, Key = textBox6.Text}).Result;
            toolStripStatusLabel1.Text = m;
            textBox6.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var m = Client.Connect("", new MyMessage { Action = ActionType.SearchByWord, Key = textBox7.Text });
            toolStripStatusLabel1.Text = m.Result;
            textBox5.Text = m.Value;
        }

        private async void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name == tabPage6.Name)
            {
                comboBox1.Items.Clear();
                textBox8.Clear();
                toolStripStatusLabel1.Text = "Loading...";
                //toolStripStatusLabel1.Refre
                var m = await Client.GetDictionary("", new MyMessage { Action = ActionType.GetAllDictionary});
                foreach (var entry in m.Dictionary.OrderBy(d => d.Key))
                {
                    comboBox1.Items.Add(entry.Key);
                }
                dictionary = m.Dictionary;
                toolStripStatusLabel1.Text = m.Result;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox8.Text = dictionary[comboBox1.SelectedItem.ToString()];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            var m = Client.Connect("", new MyMessage { Action = ActionType.SearchByLetter, Key = textBox9.Text });
            foreach (var entry in m.Dictionary.OrderBy(d => d.Key))
            {
                comboBox2.Items.Add(entry.Key);
            }
            dictionary = m.Dictionary;
            toolStripStatusLabel1.Text = m.Result;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox10.Text = dictionary[comboBox2.SelectedItem.ToString()];
        }
    }
}
