using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AI_1_Csh
{
    public partial class Form1 : Form
    {
        GameCondition activeCondition;
        GameCondition targetCondition;
        List<GameCondition> viewSolve;
        int posView = 0;
        int countOpen = 0;
        int countClose = 0;
        int countConditoin_5sec = 0;
        public Form1()
        {
            InitializeComponent();
            GameCondition parrentnul = null;
            activeCondition = new GameCondition(getStartCondition(), ref parrentnul);
            targetCondition = new GameCondition(getFinishCondition(), ref parrentnul);
            viewSolve = new List<GameCondition>();
            FormUdate();

        }
        bool preparing = false;
        ColorElement chooseColor = ColorElement.Blue;

        bool mouseMove = false;

        private ColorElement[] getStartCondition() {
            return new ColorElement[] {
                            ColorElement.Blue, ColorElement.Blue, ColorElement.Blue,
                    ColorElement.Blue, ColorElement.Blue, ColorElement.Blue, ColorElement.Blue,
            ColorElement.Blue, ColorElement.Blue, ColorElement.Orange, ColorElement.Blue, ColorElement.Blue,
                    ColorElement.Blue, ColorElement.Blue , ColorElement.Blue, ColorElement.Blue,
                            ColorElement.Blue, ColorElement.Blue , ColorElement.Blue
            };  
        }
        private ColorElement[] getFinishCondition()
        {
            return new ColorElement[] {
                            ColorElement.Blue, ColorElement.Blue , ColorElement.Blue,
                    ColorElement.Blue, ColorElement.Blue , ColorElement.Blue, ColorElement.Blue,
            ColorElement.Blue, ColorElement.Blue , ColorElement.Blue, ColorElement.Blue, ColorElement.Blue,
                    ColorElement.Blue, ColorElement.Blue , ColorElement.Blue, ColorElement.Blue,
                            ColorElement.Blue, ColorElement.Blue , ColorElement.Blue
            };
        }

        void FormUdate()
        {
            foreach (Control item in this.Controls)
            {
                if (item.Name.Contains("pb_a_") || item.Name.Contains("pb_t_"))
                {
                    int position = Convert.ToInt32(item.Tag.ToString().Split(',').Where(x => x.Contains("pos")).First().ToString().Split('=')[1]);
                    ColorElement reqColor = (item.Name.Contains("pb_a_"))? activeCondition.arr[position - 1]: targetCondition.arr[position - 1];
                    if (reqColor == ColorElement.Blue)
                        (item as PictureBox).Image =  global::AI_1_Csh.Properties.Resources.blue;
                    else if (reqColor == ColorElement.Orange)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.orange;
                    else if (reqColor == ColorElement.Purle)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.purpure;
                    else if (reqColor == ColorElement.Scarlet)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.red;
                    else if (reqColor == ColorElement.Green)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.green;

                    item.Tag = "pos=" + position.ToString() + ",color=" + reqColor.ToString();
                    //
                }
            }
        }

        private void pb_blue_MouseMove(object sender, MouseEventArgs e)
        {
            /*if (mouseMove)
                pb_blue.Location = this.PointToClient(Control.MousePosition);
            */
        }

        private void pb_blue_MouseLeave(object sender, EventArgs e)
        {
            /*
            if(mouseMove)
                pb_blue.Location = this.PointToClient(Control.MousePosition);
            */
        }

        private void pb_blue_MouseUp(object sender, MouseEventArgs e)
        {
            mouseMove = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pb_taskbar_MouseDown(object sender, MouseEventArgs e)
        {
            string color = (sender as PictureBox).Tag.ToString().Split(',')[1];
            if (color == "blue")
                chooseColor = ColorElement.Blue;
            else if (color == "green")
                chooseColor = ColorElement.Green;
            else if (color == "purple")
                chooseColor = ColorElement.Purle;
            else if (color == "scarlet")
                chooseColor = ColorElement.Scarlet;
            else if (color == "orange")
                chooseColor = ColorElement.Orange;

            foreach (Control item in this.Controls)
            {
                if (item.Tag != null && item.Tag.ToString().Contains("taskbar") && item != (sender as Control))
                {
                    (item as PictureBox).BorderStyle = BorderStyle.None;
                }
            }
            if ((sender as PictureBox).BorderStyle == BorderStyle.Fixed3D)
            {
                (sender as PictureBox).BorderStyle = BorderStyle.None;
                preparing = false;
            }
            else
            {
                preparing = true;
                (sender as PictureBox).BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void pb_map_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {

        }

        private void pb_a_Click(object sender, EventArgs e)
        {
            if (sender!= null && (sender as PictureBox) != null & (sender as PictureBox).Name != null && (sender as PictureBox).Name.Contains("pb_a_") && (sender as PictureBox).Tag != null)
            {
                PictureBox pb = (sender as PictureBox);
                int position = Convert.ToInt32(pb.Tag.ToString().Split(',').Where(x => x.Contains("pos")).First().ToString().Split('=')[1]);
                labelClick.Text = "Позиция: "+position.ToString();
                if (new int[] { 5, 6, 9, 10, 11, 14, 15 }.Contains(position))
                {
                    activeCondition = activeCondition.nextCondition(position);
                    FormUdate();
                }
            }
        }

       

        private void pb_a_check_Click(object sender, EventArgs e)
        {
            if (preparing)
            {
                int position = Convert.ToInt32((sender as PictureBox).Tag.ToString().Split(',').Where(x => x.Contains("pos")).First().ToString().Split('=')[1]);
                if (chooseColor == ColorElement.Blue)
                    (sender as PictureBox).Image = global::AI_1_Csh.Properties.Resources.blue;
                else if (chooseColor == ColorElement.Orange)
                    (sender as PictureBox).Image = global::AI_1_Csh.Properties.Resources.orange;
                else if (chooseColor == ColorElement.Purle)
                    (sender as PictureBox).Image = global::AI_1_Csh.Properties.Resources.purpure;
                else if (chooseColor == ColorElement.Scarlet)
                    (sender as PictureBox).Image = global::AI_1_Csh.Properties.Resources.red;
                else if (chooseColor == ColorElement.Green)
                    (sender as PictureBox).Image = global::AI_1_Csh.Properties.Resources.green;

                activeCondition.arr[position - 1] = chooseColor;
            }
            else
                pb_a_Click(sender, e);
            checkWin();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            preparing = false;
            btn_solve.Visible = true;
            btn_viewLeft.Visible = btn_viewRight.Visible = false;
            btn_solve.Enabled = true;
            labelBigCircle.Text = "Активное";
            labelSmallCircle.Text = "Цель";
            targetCondition = activeCondition.Clone();
            //activeCondition.Shuffle();
            activeCondition.Shuffle();
            FormUdate();
            checkWin();
        }
        bool checkWin()
        {
            bool ans = activeCondition.Equals(targetCondition);
            
            if(ans)
                MessageBox.Show("Вы победили!!", "Ура");
            return ans;

        }

        private void btn_solve_Click(object sender, EventArgs e)
        {
            btn_solve.Enabled = false;
            Task.Run(() =>
            {
            //Invoke((MethodInvoker)delegate{
            Queue<GameCondition> Open = new Queue<GameCondition>();
            Queue<GameCondition> Close = new Queue<GameCondition>();
            Open.Enqueue(activeCondition);

            while (Open.Count != 0)
            {
                countConditoin_5sec++;
                GameCondition x = Open.Dequeue();
                if (x.Equals(targetCondition))
                {
                    Close.Enqueue(x);
                    break;
                }

                Close.Enqueue(x);

                //List<GameCondition> list = ;
                List<GameCondition> set = x.generateListActions();
                foreach (var item in set)
                {//x =>x.hash = item.hash).
                    if (Open.Where(o => o.hash == item.hash).Count() == 0 && Close.Where(c => c.hash == item.hash).Count() == 0)
                    {
                        Open.Enqueue(item);
                    }
                }
                countClose = Close.Count;
                countOpen = Open.Count;
            }
            GameCondition start = Close.Last();
            viewSolve = new List<GameCondition>();

            while (start != null)
            {
                viewSolve.Add(start);
                start = start.parentCondition;
            }
            viewSolve.Reverse();
            Invoke((MethodInvoker)delegate
            {
                btn_viewRight.Visible = true;
                btn_viewLeft.Visible = true;
            });
                posView = 0;
            });
        }

        void PrintView()
        {
            foreach (Control item in this.Controls)
            {
                if (item.Name.Contains("pb_a_"))
                {
                    int position = Convert.ToInt32(item.Tag.ToString().Split(',').Where(x => x.Contains("pos")).First().ToString().Split('=')[1]);
                    ColorElement reqColor = viewSolve[posView].arr[position - 1];
                    if (reqColor == ColorElement.Blue)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.blue;
                    else if (reqColor == ColorElement.Orange)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.orange;
                    else if (reqColor == ColorElement.Purle)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.purpure;
                    else if (reqColor == ColorElement.Scarlet)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.red;
                    else if (reqColor == ColorElement.Green)
                        (item as PictureBox).Image = global::AI_1_Csh.Properties.Resources.green;

                    item.Tag = "pos=" + position.ToString() + ",color=" + reqColor.ToString();
                    //
                }
            }
        }

        private void btn_viewLeft_Click(object sender, EventArgs e)
        {
            posView = (posView > 0) ? posView - 1 : 0;
            activeCondition = viewSolve[posView];
            labelClick.Text = "Позиция: " + viewSolve[posView].posClick.ToString();
            PrintView();
        }

        private void btn_viewRight_Click(object sender, EventArgs e)
        {
            posView = (posView < viewSolve.Count-1) ? posView + 1 : viewSolve.Count - 1;
            activeCondition = viewSolve[posView];
            labelClick.Text = "Позиция: " + viewSolve[posView].posClick.ToString();
            PrintView();
        }

        private void timer5sec_Tick(object sender, EventArgs e)
        {
            LabelCountCondition.Text = "Состояний в сек: "+Math.Round(countConditoin_5sec/5.0);
            countConditoin_5sec = 0;
        }

        private void timer100ms_Tick(object sender, EventArgs e)
        {

        }

        private void timer1000ms_Tick(object sender, EventArgs e)
        {
            labelOpen.Text = "Открытый список: " + Math.Round(countOpen / 5.0);
            labelClose.Text = "Закрытый список: " + Math.Round(countClose / 5.0);
            countClose = 0;
            countOpen = 0;
        }
    }
}
