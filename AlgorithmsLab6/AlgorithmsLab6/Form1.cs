using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace AlgorithmsLab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TextBox[,] t;
        Label[,] l;
        int n;
        int m;
        int shag;

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Ошибка!\nЗадайте значение входных параметров");
                return;
            }

            n = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            m = Convert.ToInt32(comboBox2.SelectedItem.ToString());
            m += 1;
            int i, j, c, d;
            int x = -90, y = 25;

            if (t != null)
            {
                tabPage2.Controls.Clear();
                //tabPage2.Controls.Add(button2);
            }

            t = new TextBox[n, m];

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < m; j++)
                {
                    t[i, j] = new TextBox();
                    tabPage2.Controls.Add(t[i, j]);
                    t[i, j].Location = new Point(x += 90, y);
                    t[i, j].Size = new Size(30, 20);
                    t[i, j].TabStop = false;
                }
                y += 40;
                x = -90;
            }

            y = 28;
            x = -60;
            l = new Label[n, m];

            for (c = 0; c < n; c++)
            {
                for (d = 0; d < m - 1; d++)
                {
                    l[c, d] = new Label();
                    tabPage2.Controls.Add(l[c, d]);
                    l[c, d].Location = new Point(x += 90, y);
                    l[c, d].Size = new Size(80, 40);
                    int z = d + 1;
                    if (d == m - 2)
                        l[c, d].Text = "x" + z + " =";
                    else
                        l[c, d].Text = "x" + z + " +";
                    l[c, d].TabStop = false;
                }
                y += 41;
                x = -60;
            }

            tabControl1.SelectedTab = tabPage2;
        }

        public string Gauss(int Rows, int Column, double[,] matr)
        {
            int i, k, q;
            double v;
            string answer = "";
            for (q = 0; q < Rows; q++)
            {
                //делаем главную диагональ единицами
                v = matr[q, q];
                richTextBox1.AppendText("\nШаг" + (shag++) + ". Разделим строку " + (q + 1) + " на элемент[" + (q + 1) + "," + (q + 1) + "]= " + matr[q, q] + "\n\n");

                for (k = 0; k < Column; k++)
                    matr[q, k] /= v;

                for (int z = 0; z < Rows; z++) //Записываем результат со 2-го шага
                {
                    for (int x = 0; x < Column; x++)
                        richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");

                    richTextBox1.AppendText("\n\n");
                }

                for (i = q + 1; i < Rows; i++) //обнуляем числа под единицами главной диогoнали
                {
                    v = matr[i, q];
                    for (k = q; k < Column; k++)
                        matr[i, k] = matr[i, k] - matr[q, k] * v; //Отнимаем от строки строку, помноженную на элемент

                    richTextBox1.AppendText("\nШаг" + (shag++) + ".Отнимем от строки " + (i + 1) + " строку " + (q + 1) + " умноженную начисло " + v + "\n\n");

                    for (int z = 0; z < Rows; z++) //Записываем результат
                    {
                        for (int x = 0; x < Column; x++)
                            richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");

                        richTextBox1.AppendText("\n\n");
                    }
                }
            }

            for (q = 0; q < Rows; q++)
            {
                for (i = 0; i < (Rows - 1) - q; i++)
                {
                    v = matr[i, (Column - 1) - q - 1];

                    for (k = Column - 1 - q - 1; k < Column; k++)
                        matr[i, k] = matr[i, k] - matr[(Rows - 1) - q, k] * v;

                    richTextBox1.AppendText("\nШаг" + (shag++) + ".Отнимем от строки " + (i + 1) + " строку " + (Rows - q) + ", умноженную на число " + v + "\n\n");

                    for (int z = 0; z < Rows; z++) //Записываем результат
                    {
                        for (int x = 0; x < Column; x++)
                            richTextBox1.AppendText((Convert.ToString(matr[z, x])).PadRight(2) + " ");

                        richTextBox1.AppendText("\n\n");
                    }
                }
            }

            richTextBox1.AppendText("Результатом, будут оставшийся столбец чисел\n\n");

            for (i = 0; i < Rows; i++)
                answer += "x" + (i + 1) + " = " + matr[i, Column - 1] + "\r\n";

            return answer;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            shag = 2;
            int i, j;
            richTextBox1.Clear();
            string[,] v = new string[n, m];
            double[,] v1 = new double[n, m];

            for (i = 0; i < n; i++)
                for (j = 0; j < m; j++)
                {
                    v1[i, j] = Convert.ToDouble(t[i, j].Text);
                    v[i, j] = Convert.ToString(t[i, j].Text);
                }

            richTextBox1.AppendText("Задача :\nНайти решение системы уравнений:\n");

            for (i = 0; i < n; i++)
            {
                string s = "";
                for (j = 0; j < m; j++)
                {
                    if (t[i, j].Text == "")
                    {
                        MessageBox.Show("Были введенны не все коэффициенты");
                        return;
                    }

                    if (j == m - 1)
                        s += v[i, j];

                    else
                    {
                        int z = j + 1;

                        if (v1[i, j] < 0)
                            s += v[i, j] + " * x" + z + " - ";

                        if (v1[i, j] == 0)
                            s += " ";

                        if (j == m - 2)
                        {
                            s += v[i, j] + " * x" + z + " = ";
                            continue;
                        }

                        if (v1[i, j] > 0)
                            s += v[i, j] + " * x" + z + " + ";
                    }
                }

                richTextBox1.AppendText(s + "\n");
            }

            richTextBox1.AppendText("Шаг 1:\nСформируем расширенную матрицу:\n\r");

            for (i = 0; i < n; i++)
            {
                string s = "";
                for (j = 0; j < m; j++)
                {
                    if (j == m - 1)
                        s += " " + (v[i, j]).PadRight(2);

                    else
                        s += (v[i, j].PadRight(3)) + " ";
                }

                richTextBox1.AppendText(s + "\n");
            }

            richTextBox1.AppendText("Применяя к расширенной матрице последовательность элементарных операций, стремимся, чтобы каждая строка, кроме, быть может, первой, начиналась с нулей, и число нулей до первого ненулевого элемента в каждой следующей строке было больше, чем в предыдущей.\n");

            richTextBox1.AppendText(Gauss(n, m, v1));
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if ((n == 0) || (m == 0))
            {
                e.Cancel = true;
                MessageBox.Show("Вы не сформировали СЛАУ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var matrix1 = new double[1, 2];
            for (int i = 0; i < 1; i++)
                for (int j = 0; j < 2; j++)
                    matrix1[i, j] = new Random().Next(1, 99);
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            Gauss1.GaussMethod(matrix1);
            stopwatch1.Stop();

            var result = "";

            long previosRes = 0;

            for (int n = 1; n < 513; n *= 2)
            {
                var matrix = new double[n, n + 1];

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n + 1; j++)
                        matrix[i, j] = new Random().Next(1, 99);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                Gauss1.GaussMethod(matrix);

                stopwatch.Stop();

                result += "Потрачено тактов на решение матрицы " + n + "X" + n + ": " + stopwatch.ElapsedTicks + "\r\n";
                if(n != 1)
                    result += "Число тиков увеличилось в " + stopwatch.ElapsedTicks / previosRes + " раз" + "\r\n";
                result += "\r\n";

                previosRes = stopwatch.ElapsedTicks;

            }

            richTextBox1.Clear();
            richTextBox1.AppendText(result);
        }
    }
}
