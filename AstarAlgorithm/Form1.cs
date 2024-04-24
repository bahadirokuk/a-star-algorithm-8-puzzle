using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Astar.AstarAlgorithm;

namespace Astar
{
    public partial class Form1 : Form
    {
        private readonly List<Control> controlsTofour;
        private readonly List<Control> controlsTothree;
        private readonly List<Control> controlsTotwo;
        int[][] goal;
        int[][] start;
        public Form1()
        {
            InitializeComponent();
            controlsTotwo = new List<Control>(){
                textBox11,textBox12,textBox21,textBox22,textBox_11,textBox_12,textBox_21,textBox_22,sonuc11,sonuc12,sonuc21,sonuc22
            };
            controlsTothree = new List<Control>() {
               textBox11,textBox12,textBox13,textBox21,textBox22,textBox23,textBox31,textBox32,textBox33,textBox_11,textBox_12,textBox_13,textBox_21,textBox_22,textBox_23,textBox_31,textBox_32,textBox_33,sonuc11,sonuc12,sonuc13,sonuc21,sonuc22,sonuc23,sonuc31,sonuc32,sonuc33
            };
            controlsTofour = new List<Control>()
            {
                textBox11,textBox12,textBox13,textBox14,textBox21,textBox22,textBox23,textBox24,textBox31,textBox32,textBox33,textBox34,textBox41,textBox42,textBox43,textBox44,textBox_11,textBox_12,textBox_13,textBox_14,textBox_21,textBox_22,textBox_23,textBox_24,textBox_31,textBox_32,textBox_33,textBox_34,textBox_41,textBox_42,textBox_43,textBox_44,sonuc11,sonuc12,sonuc13,sonuc14,sonuc21,sonuc22,sonuc23,sonuc24,sonuc31,sonuc32,sonuc33,sonuc34,sonuc41,sonuc42,sonuc43,sonuc44
            };
            comboBox1.SelectedIndex = 1;
        }
        private void ToggleControls(List<Control> list)
        {
            foreach (var control in list)
            {
                control.Visible = true;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var control in controlsTofour)
            {
                control.Visible = false;
            }
            switch (comboBox1.SelectedItem)
            {
                case "2":
                    ToggleControls(controlsTotwo);
                    break;
                case "3":
                    ToggleControls(controlsTothree);
                   break;
                case "4":
                    ToggleControls(controlsTofour);
                   break;
                default:
                    ToggleControls(controlsTothree);
                    break;
            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            add();
            var solver = new PuzzleSolver(start, goal);
            var node = solver.Solve();
            Stack<Node> path = new Stack<Node>();
            while (node != null)
            {
                path.Push(node);
                node = node.Parent;
            }

            while (path.Count > 0)
            {
                node = path.Pop();
                for (int i = 0; i < node.State.Length; i++)
                {
                    for (int j = 0; j < node.State[i].Length; j++)
                    {
                        foreach (var control in controlsTofour)
                        {
                            if (control.Name == $"sonuc{i + 1}{j + 1}")
                            {
                                control.Text = node.State[i][j].ToString();
                            }
                        }
                    }
                }
                await Task.Delay(700);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var control in controlsTofour)
            {
                if (control is Label)
                {
                    control.Text = "X";
                }
                else
                {
                    control.Text = "";
                }
            }
        }
        void add(){
            switch (comboBox1.SelectedItem)
            {
                case "2":
                    goal = new int[2][] { new int[] { int.Parse(textBox_11.Text), int.Parse(textBox_12.Text) }, new int[] { int.Parse(textBox_21.Text), int.Parse(textBox_22.Text) } };
                    start = new int[2][] { new int[] { int.Parse(textBox11.Text), int.Parse(textBox12.Text) }, new int[] { int.Parse(textBox21.Text), int.Parse(textBox22.Text) } };
                    break;
                case "3":
                    goal = new int[3][] {
                        new int[] { int.Parse(textBox_11.Text), int.Parse(textBox_12.Text), int.Parse(textBox_13.Text) },
                        new int[] { int.Parse(textBox_21.Text), int.Parse(textBox_22.Text), int.Parse(textBox_23.Text) },
                        new int[] { int.Parse(textBox_31.Text), int.Parse(textBox_32.Text), int.Parse(textBox_33.Text) } };
                    start = new int[3][] {
                        new int[] { int.Parse(textBox11.Text), int.Parse(textBox12.Text), int.Parse(textBox13.Text) },
                        new int[] { int.Parse(textBox21.Text), int.Parse(textBox22.Text), int.Parse(textBox23.Text) },
                        new int[] { int.Parse(textBox31.Text), int.Parse(textBox32.Text), int.Parse(textBox33.Text) } };
                    break;
                case "4":
                    goal = new int[4][] {
                        new int[] { int.Parse(textBox_11.Text), int.Parse(textBox_12.Text), int.Parse(textBox_13.Text),int.Parse(textBox_14.Text)},
                        new int[] { int.Parse(textBox_21.Text), int.Parse(textBox_22.Text), int.Parse(textBox_23.Text),int.Parse(textBox_24.Text)},
                        new int[] { int.Parse(textBox_31.Text), int.Parse(textBox_32.Text), int.Parse(textBox_33.Text),int.Parse(textBox_34.Text)},
                        new int[] { int.Parse(textBox_41.Text), int.Parse(textBox_42.Text), int.Parse(textBox_43.Text),int.Parse(textBox_44.Text)}};
                    start = new int[4][] {
                        new int[] { int.Parse(textBox11.Text), int.Parse(textBox12.Text), int.Parse(textBox13.Text),int.Parse(textBox14.Text)},
                        new int[] { int.Parse(textBox21.Text), int.Parse(textBox22.Text), int.Parse(textBox23.Text),int.Parse(textBox24.Text)},
                        new int[] { int.Parse(textBox31.Text), int.Parse(textBox32.Text), int.Parse(textBox33.Text),int.Parse(textBox34.Text)},
                        new int[] { int.Parse(textBox41.Text), int.Parse(textBox42.Text), int.Parse(textBox43.Text),int.Parse(textBox44.Text)}};
                    break;
                default:
                    break;
            }
        }
    }
}