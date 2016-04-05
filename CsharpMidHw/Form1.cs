using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpMidHw
{
    public partial class Form1 : Form
    {
        bool timeOut = true;
        List<ListModel> listDescription = new List<ListModel>();
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            if (listDescription.Count() != 0)
            {
                var selectList = listDescription.FindAll(x => x.StartTime >= DateTime.Now);
                if (selectList != null)
                {
                    var finTime = selectList[0].EndTime;

                    if (timeOut &&
                        (DateTime.Now >= finTime
                            && DateTime.Now <= finTime.AddMinutes(3)))
                    {
                        System.Media.SystemSounds.Beep.Play();

                        //System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer(@"C:\CsharpMidHw");
                        //myPlayer.Play();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timeOut = !timeOut;
            listDescription.Remove(listDescription[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker2.Value.AddMinutes(10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool isLaterTime = this.IsLaterTime(dateTimePicker2.Value, DateTime.Now);

            if (!string.IsNullOrEmpty(textBox1.Text)
                    && comboBox1.SelectedItem != null
                    && isLaterTime)
            {
                var newListItem = new ListModel();
                var str = string.Empty;
                var finishTime = Convert.ToInt32(comboBox1.SelectedItem);
                newListItem.StartTime = dateTimePicker2.Value;
                newListItem.EndTime = dateTimePicker2.Value.AddHours(finishTime);
                newListItem.Title = textBox1.Text;
                listDescription.Add(newListItem);

                if (listDescription.Count() > 1)
                {
                    ////listDescription.Sort((a, b) => DateTime.Compare(a.StartTime, b.StartTime));
                    var tempList = listDescription.OrderBy(x => x.StartTime).Distinct().ToList();
                    listDescription = tempList;
                }

                if (listDescription != null && listDescription.Count() > 0)
                {
                    label8.Text = listDescription[0].Title + "將在" + finishTime + "小時後結束";
                }

                listBox1.Items.Clear();
                foreach (var unit in listDescription)
                {
                    str = "[" + unit.StartTime.ToShortTimeString() + "  ~  " + unit.EndTime.ToShortTimeString() + "]  " + unit.Title;
                    listBox1.Items.Add(str);
                }

            }
            else if (!string.IsNullOrEmpty(textBox1.Text)
                        && comboBox1.SelectedItem != null
                        && !isLaterTime)
            {
                MessageBox.Show("開始時間必須晚於現在時間");
            }
            else
            {
                MessageBox.Show("請輸入待做事項");
            }
        }

        private bool IsLaterTime(DateTime dateTime1, DateTime dateTime2)
        {
            int result = DateTime.Compare(dateTime1, dateTime2);

            if (result < 0)
            {
                return false;
            }
            else if (result == 0)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        private bool IsOverLap()
        {
            ////to-do
            return true;
        }
    }
}
