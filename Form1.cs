using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int counter;
        public static string firstnumber;
        public static string secondnumber;
        public static int first = 0;
        public static int second = 0;
        public static int value;
        private static bool firstvalue;
        private static bool secondvalue;
        public static bool opcomplete;
        public static bool add;
        public static bool subtract;
        public static bool multiply;
        public static bool divide;
        public static bool equals;
        public static bool changeop;
        public static char[] operands = new char[] {'+', '-', '*', '/'};
        public static char lastop = new char();

        public void parseValues()
        {
            string t = textBox1.Text;
            int index;
            if (firstvalue)
            {
                index = t.IndexOfAny(operands);
                t = t.Substring(index + 1);
                t = t.TrimEnd(operands);
                t = t.TrimStart(operands);
                secondnumber = t;
                if (int.TryParse(secondnumber, out second))
                { 
                    secondvalue = true;
                }
                return;
            }
            index = t.IndexOfAny(operands);
            t = t.TrimEnd(operands); 
            firstnumber = t;
            if (int.TryParse(firstnumber, out first))
            {
                firstvalue = true;
            }
        }
        private void Solve()
        {
            if (opcomplete)
            {
                if (subtract)
                {
                    value = value - second;
                    textBox1.Text = value.ToString() + "-";
                    textBox2.AppendText(value.ToString() + "-" + second.ToString() + "=" + value.ToString() + Environment.NewLine);
                }
                if (add)
                {
                    value = value + second;
                    textBox1.Text = value.ToString() + "+";
                    textBox2.AppendText(value.ToString() + "+" + second.ToString() + "=" + value.ToString() + Environment.NewLine);
                }
                return;
            }
           switch (lastop)
            {
                case '+': add = true; break;
                case '-': subtract = true; break;
            }
            if (!opcomplete)
            {
                opcomplete = true;
                if (subtract)
                {
                    add = true;
                    subtract = false;
                    value = first + second;
                    textBox1.Text = value.ToString() + "-";
                    textBox2.AppendText(first.ToString() + "+" + second.ToString() + "=" + value.ToString() + Environment.NewLine);
                    return;
                }
                if (add)
                {
                    add = false;
                    subtract = true;
                    value = first - second;
                    textBox1.Text = value.ToString() + "+";
                    textBox2.AppendText(first.ToString() + "-" + second.ToString() + "=" + value.ToString() + Environment.NewLine);
                }
            }
        }
        
        private void Operator_click(object sender, EventArgs e)
        { 
            var btn = sender as System.Windows.Forms.Button;
            string s = btn.Text;
            textBox1.Text = textBox1.Text.TrimEnd('+', '-', '*', '/');
            textBox1.AppendText(s);

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }
            if (btn.Text == "+")
            {
                lastop = '+';
                add = true;
                subtract = false;
            }
            if (btn.Text == "-")
            {
                lastop = '-';
                subtract = true;
                add = false;
            }
            parseValues();
            if (firstvalue && secondvalue)
            {
                Solve();
            }
            if (DisableOp())
            {
                return;
            }
        }
        private void Number_click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
            {
                textBox1.Clear();
            }
            var btn = sender as System.Windows.Forms.Button;
            string s = btn.Text;
            textBox1.AppendText(s);
        }
        private bool DisableOp()
        {
            if (textBox1.Text.EndsWith("+") || textBox1.Text.EndsWith("-") || textBox1.Text.EndsWith("/") || textBox1.Text.EndsWith("*") || textBox1.Text.EndsWith(".") || textBox1.Text.EndsWith("%"))
            {
                return true;
            }
            return false;
        }
        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        public static string trimEnd(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            return s = s.Remove(s.Length - 1);
        }
        private void button19_Click(object sender, EventArgs e)
        {
            string newstring = trimEnd(textBox1.Text);
            textBox1.Text = newstring;
        }

        private void button21_Click(object sender, EventArgs e)
        {

            if (equals)
            {
                parseValues();
            }
        }
    }
}

