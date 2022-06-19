using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDotNet;

namespace RNET_Calculator
{
    public partial class RNET_Calculator : Form
    {
        // set up basics and create RDotNet instance
        // if anticipated install of R is not found, ask the user to find it.
        public RNET_Calculator()
        {
            InitializeComponent();
            string dlldir = @"C:\Program Files\R\R-2.13.0\bin\i386";
            bool r_located = false;

            while (r_located == false)
            {
                try
                {
                    REngine.GetInstance();
                    r_located = true;
                }

                catch
                {
                    MessageBox.Show(@"Unable to find R installation's \bin\i386 folder.
                    Press OK to attempt to locate it.");
                }
            }
        }

        // This adds the input into the text box, and resets if necessary
        private void add_input(string input)
        {
            if (textBox_output.Text != "")
            {
                textBox_input.Text = "";
                textBox_output.Text = "";
            }

            textBox_input.Text += input;
        }

        // the equals button, which evaluates the text
        private void button_equals_Click(object sender, EventArgs e)
        {
            textBox_output.Text = "";

            REngine engine = REngine.GetInstance();

            String input = textBox_input.Text;

            try
            {
                NumericVector x = engine.Evaluate(input).AsNumeric();
                textBox_output.Text += x[0];
            }

            catch
            {
                textBox_output.Text = "Equation Error";
            }

        }

        // Begin the button function calls - long list and not exciting
        private void button_1_Click(object sender, EventArgs e)
        {
            add_input(button_1.Text);
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            add_input(button_2.Text);
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            add_input(button_3.Text);
        }

        private void button_4_Click(object sender, EventArgs e)
        {
            add_input(button_4.Text);
        }

        private void button_5_Click(object sender, EventArgs e)
        {
            add_input(button_5.Text);
        }

        private void button_6_Click(object sender, EventArgs e)
        {
            add_input(button_6.Text);
        }

        private void button_7_Click(object sender, EventArgs e)
        {
            add_input(button_7.Text);
        }

        private void button_8_Click(object sender, EventArgs e)
        {
            add_input(button_8.Text);
        }

        private void button_9_Click(object sender, EventArgs e)
        {
            add_input(button_9.Text);
        }

        private void button_point_Click(object sender, EventArgs e)
        {
            add_input(button_point.Text);
        }

        private void button_0_Click(object sender, EventArgs e)
        {
            add_input(button_0.Text);
        }

        private void button_plus_Click(object sender, EventArgs e)
        {
            add_input(button_plus.Text);
        }

        private void button_minus_Click(object sender, EventArgs e)
        {
            add_input(button_minus.Text);
        }

        private void button_multiply_Click(object sender, EventArgs e)
        {
            add_input(button_multiply.Text);
        }

        private void button_divide_Click(object sender, EventArgs e)
        {
            add_input(button_divide.Text);
        }

        private void button_left_bracket_Click(object sender, EventArgs e)
        {
            add_input(button_left_bracket.Text);
        }

        private void button_right_bracket_Click(object sender, EventArgs e)
        {
            add_input(button_right_bracket.Text);
        }

        private void button_ce_Click(object sender, EventArgs e)
        {
            textBox_input.Text = "";
            textBox_output.Text = "";
        }

    }
}
