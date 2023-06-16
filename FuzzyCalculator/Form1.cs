using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuzzyCalculator
{
    /*public class LRNumber
    {
        Dictionary<double, double> number=new Dictionary<double, double>();
        public LRNumber(DataGridView dataGrid)
        {
            for(int i = 0; i < dataGrid.Rows.Count - 1; i++)
            {
                double temp = Double.Parse(dataGrid[1, i].Value.ToString());
                number.Add(temp, Double.Parse(dataGrid[0, i].Value.ToString()));
            }
        }
        public static LRNumber operator +(LRNumber l1, LRNumber l2)
        {
            return null;
        }
    }*/
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int count = 0;
            for (int f = 0; f < 1000; f++)
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                Random random = new Random();
                double[] mu = { random.NextDouble(), random.NextDouble(), random.NextDouble() };
                double[] x = { 2, 3, 4 };
                for (int i = 0; i < mu.Length; i++)
                {
                    dataGridView1.Rows.Add(mu[i], x[i]);
                }
                mu = new double[] { random.NextDouble(), random.NextDouble(), random.NextDouble() };
                x = new double[] { 1, 2, 17 };
                for (int i = 0; i < mu.Length; i++)
                {
                    dataGridView2.Rows.Add(mu[i], x[i]);
                }
                result('*');
                DataGridView result1 = dataGridView3;
                DataGridView temp = dataGridView2;
                dataGridView2 = dataGridView1;
                dataGridView1 = temp;
                result('*');
                DataGridView result2 = dataGridView3;
                if (result1 == result2)
                {
                    count++;
                    button6.Text = (count).ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result('+');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result('-');
        }

        private void button3_Click(object sender, EventArgs e)
        {
            result('*');
        }

        private void button4_Click(object sender, EventArgs e)
        {
            result('/');
        }
        private double[,] result(char operation)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> x3 = new Dictionary<double, double>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView2.Rows.Count - 1; j++)
                {
                    double value1 = Double.Parse(dataGridView1[1, i].Value.ToString());
                    double value2 = Double.Parse(dataGridView2[1, j].Value.ToString());
                    double key = 0; 
                    switch(operation)
                    {
                        case '+':
                            key = value1+value2;
                            break;
                        case '-':
                            key = value1 - value2;
                            break;
                        case '*':
                            key = value1 * value2;
                            break;
                        case '/':
                            key = value1 / value2;
                            break;
                    }
                    double mu =
                        Math.Min(Double.Parse(dataGridView1[0, i].Value.ToString()),
                        Double.Parse(dataGridView2[0, j].Value.ToString()));
                    if (x3.ContainsKey(key))
                    {
                        if (mu > x3[key]) x3[key] = mu;
                    }
                    else x3.Add(key, mu);
                }
            }
            foreach (var item in x3)
            {
                dataGridView3.Rows.Add((item.Value).ToString(), (item.Key).ToString());
                if (item.Value == 1)
                {
                    textBox1.Text = $"Нечеткое число (А1{operation}А2) = примерно {item.Key}";
                }
            }
            return null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            for(int i = (dataGridView1.Rows.Count-2); i >= 0; i--)
            {
                double value=-1*Double.Parse(dataGridView1[1, i].Value.ToString());
                dataGridView3.Rows.Add(dataGridView1[0,i].Value, value.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> same = new Dictionary<double, double>();
            for (int i = 0; i <dataGridView1.Rows.Count-1; i++)
            {
                double number = Double.Parse(dataGridView1[1, i].Value.ToString());
                same.Add(number, Double.Parse(dataGridView1[0, i].Value.ToString()));
            }
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                double number = Double.Parse(dataGridView2[1, i].Value.ToString());
                if (same.ContainsKey(number))
                {
                    if (same[number] < Double.Parse(dataGridView2[0, i].Value.ToString()))
                    {
                        same[number] = Double.Parse(dataGridView2[0, i].Value.ToString());
                    }
                }
                else same.Add(number, Double.Parse(dataGridView2[0, i].Value.ToString()));
            }
            foreach(double key in same.Keys)
            {
                dataGridView3.Rows.Add(same[key].ToString(), key.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> same = new Dictionary<double, double>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                double number = Double.Parse(dataGridView1[1, i].Value.ToString());
                same.Add(number, Double.Parse(dataGridView1[0, i].Value.ToString()));
            }
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                double number = Double.Parse(dataGridView2[1, i].Value.ToString());
                if (same.ContainsKey(number))
                {
                    if (same[number] > Double.Parse(dataGridView2[0, i].Value.ToString()))
                    {
                        same[number] = Double.Parse(dataGridView2[0, i].Value.ToString());
                    }
                }
                else same.Add(number, Double.Parse(dataGridView2[0, i].Value.ToString()));
            }
            foreach (double key in same.Keys)
            {
                dataGridView3.Rows.Add(same[key].ToString(), key.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> same = Multiply(Put(dataGridView1), Put(dataGridView2));
            foreach (double key in same.Keys)
            {
                dataGridView3.Rows.Add((same[key]).ToString(), key.ToString());
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> same = Multiply(Put(dataGridView1), Put(dataGridView2));
            Dictionary<double, double> sum = Sum(Put(dataGridView1), Put(dataGridView2));
            foreach (double key in same.Keys)
            {
                dataGridView3.Rows.Add((sum[key]-same[key]).ToString(), key.ToString());
            }
        }
        private Dictionary<double,double> Put(DataGridView dataGrid)
        {
            Dictionary<double, double> result = new Dictionary<double, double>();
            for(int i = 0; i < dataGrid.Rows.Count - 1; i++)
            {
                double number = Double.Parse(dataGrid[1, i].Value.ToString());
                result.Add(number, Double.Parse(dataGrid[0, i].Value.ToString()));
            }
            return result;
        }
        private Dictionary<double,double> Multiply(Dictionary<double, double> A, Dictionary<double, double> B)
        {
            Dictionary<double, double> result = new Dictionary<double, double>();
            foreach(double key in A.Keys)
            {
                if (B.ContainsKey(key))
                {
                    result.Add(key,A[key] * B[key]);
                }
                else
                {
                    result.Add(key, 0);
                }
            }
            foreach(double key in B.Keys)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, 0);
                }
            }
            return result;
        }
        private Dictionary<double,double> Sum(Dictionary<double, double> A, Dictionary<double, double> B)
        {
            Dictionary<double, double> result = new Dictionary<double, double>();
            foreach (double key in A.Keys)
            {
                if (B.ContainsKey(key))
                {
                    result.Add(key, A[key] + B[key]);
                }
                else
                {
                    result.Add(key, A[key]);
                }
            }
            foreach (double key in B.Keys)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, B[key]);
                }
            }
            return result;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> sum = Sum(Put(dataGridView1), Put(dataGridView2));
            foreach (double key in sum.Keys)
            {
                dataGridView3.Rows.Add((Math.Min(1,sum[key])).ToString(), key.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> sum = Sum(Put(dataGridView1), Put(dataGridView2));
            foreach (double key in sum.Keys)
            {
                dataGridView3.Rows.Add((Math.Max(0, sum[key]-1)).ToString(), key.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> same = Multiply(Put(dataGridView1), Put(dataGridView2));
            Dictionary<double, double> sum = Sum(Put(dataGridView1), Put(dataGridView2));
            foreach (double key in same.Keys)
            {
                dataGridView3.Rows.Add((sum[key] /(1- same[key])).ToString(), key.ToString());
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Dictionary<double, double> same = Multiply(Put(dataGridView1), Put(dataGridView2));
            Dictionary<double, double> sum = Sum(Put(dataGridView1), Put(dataGridView2));
            foreach (double key in same.Keys)
            {
                dataGridView3.Rows.Add((same[key]/(2-(sum[key]-same[key]))).ToString(), key.ToString());
            }
        }
    }
}
