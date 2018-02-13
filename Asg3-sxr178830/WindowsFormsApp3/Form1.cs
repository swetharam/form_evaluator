using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        //many variables declared globally for the purpose of calculations
        public int[] backspace= { 0,0,0,0,0,0,0,0,0,0};
        public string[] entrytimes= {"0", "0" , "0" , "0" , "0" , "0" , "0" , "0", "0", "0", "0", "0" };
        public string[] savetimes = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"  };
        public double[] times = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 };
        public double[] inter = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        double totaltime;
        double averagetime;
        double averagespan;
        int val = 0;

        public int i = 0;
        public List<String> information = new List<String>();

        public object SafeConvert { get; private set; }

        public Form1()
        {
            InitializeComponent();
            bool isDataLoaded = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //InitializeComponent();
            //listView1.Columns[1].Text = "Column 1";

        }

    // the following is when the browse button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            try {//try catch basically helps to avoid the system to crash when an invalid file type input has been selected
                DialogResult result = openFileDialog1.ShowDialog();// used for having the browse buttons
                string abcd;
                bool isDataLoaded = true;
                status.Text = "Choosing a file";// outputting results on the status bar
                if (result == DialogResult.OK) // Test result.
                {
                    status.Text = "File Selected";// comments in the status bar allwoing the user to know what is going on in the background


                    filename.Text = openFileDialog1.FileName;
                    String file = openFileDialog1.FileName;

                    try
                    {
                        string[] readText = File.ReadAllLines(file);// opening the file that has been selected

                        status.Text = "Updated list view";//comments in the status bar
                        foreach (string s in readText)

                        {
                            if (s != "\r\n")
                            {
                                char[] delimiterChars = { '\t' };// reading the file when selected and giving the output on the list view
                                string name = "", phone = "";
                                string address = "", csz = "", proof = "";
                                string mailid = "";
                                string[] words = s.Split(delimiterChars);

                                name = words[0] + " " + words[1] + " " + words[2];
                                phone = words[3];
                                address = words[4] + " " + words[5];
                                csz = words[6] + " " + words[7] + " " + words[8];
                                mailid = words[9];
                                proof = words[10];

                                string[] row = { name, phone, address, csz, mailid, proof };
                                var listViewItem = new ListViewItem(row);// entering the values in the listview frm the file selected
                                listView1.Items.Add(listViewItem);
                                entrytimes[i] = words[12];
                                savetimes[i] = words[13];

                                if (int.TryParse(words[14], out backspace[i]))
                                {


                                }




                                i = i + 1;


                            }
                            else
                            {
                                continue;
                            }

                        }


                        for (int k = 0; k < 10; k++)
                        {
                            int random = 0;
                            random = backspace[k];
                            val += random;
                        }
                        status.Text = "Opened a file for evaluation";// giving comments on the status bar for the user to know
                    }
                    catch (IOException)
                    {
                    }
                }

            }
            catch(Exception ex)
            {
                status.Text = "Invalid File Type";// comment when an invalid data type has been selected
            }

}
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        //this is the class which represents all the actions that take place when the run button is clicked
        private void run_Click(object sender, EventArgs e)
        {
            status.Text = "Running the Evaluator";
            timeconversion();
            double min = 1000.0;
            double intermax =0.0;
            double max = 0.0;
            double intermin =1000.0;
            
            for(int l=0;l<backspace.Length;l++)
            {
                if(times[l]>max)
                {
                    max = times[l];
                }
                if(times[l]<min)
                {
                    min = times[l];
                }

            }
            for (int w = 0; w < backspace.Length; w++)
            {
                if (inter[w] > intermax)
                {
                    intermax = inter[w];
                    
                }
                if (inter[w] < intermin)
                {
                    intermin = inter[w];
                }

            }

            TimeSpan intmax = TimeSpan.FromSeconds(intermax);
            TimeSpan intmin = TimeSpan.FromSeconds(intermin);

            TimeSpan t = TimeSpan.FromSeconds(max);
            TimeSpan span = TimeSpan.FromSeconds(min);
            TimeSpan mn = TimeSpan.FromSeconds(totaltime);
            TimeSpan avgr = TimeSpan.FromSeconds(averagetime);
            TimeSpan intavg = TimeSpan.FromSeconds(averagespan);
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"CS6326Asg3.txt", true);

            outputbox.AppendText("NUMBER OF RECORDS:= " + backspace.Length + "\r\n");// printing the total number of records in the file
            file.Write("NUMBER OF RECORDS:= " + backspace.Length + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("MAXIMUM ENTRY TIME:="+ string.Format("{0}:{1}", t.Minutes, t.Seconds) + "\r\n");// print the maximum time taken for entry
            file.Write("MAXIMUM ENTRY TIME:=" + string.Format("{0}:{1}", t.Minutes, t.Seconds) + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("MINIMUM ENTRY TIME:=" + string.Format("{0}:{1}", span.Minutes, span.Seconds) + "\r\n");// will print the minium time taken for entry
            file.Write("MINIMUM ENTRY TIME:=" + string.Format("{0}:{1}", span.Minutes, span.Seconds) + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("AVERAGE ENTRY TIME:=" + string.Format("{0}:{1}", avgr.Minutes, avgr.Seconds) + "\r\n");// will print the value of the average time taken for entry
            file.Write("AVERAGE ENTRY TIME:=" + string.Format("{0}:{1}", avgr.Minutes, avgr.Seconds) + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("TOTAL TIME:=" +string.Format("{0}:{1}",mn.Minutes,mn.Seconds) + "\r\n");// this will print the total time taken for entry
            file.Write("TOTAL TIME:=" + string.Format("{0}:{1}", mn.Minutes, mn.Seconds) + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("MAXIMUM INTER-RECORD TIME:=" + string.Format("{0}:{1}", intmax.Minutes, intmax.Seconds) + "\r\n");// this will print the maximum inter record time
            file.Write("MAXIMUM INTER-RECORD TIME:=" + string.Format("{0}:{1}", intmax.Minutes, intmax.Seconds) + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("MINIMUM INTER-RECORDS TIME:=" + string.Format("{0}:{1}", intmin.Minutes, intmin.Seconds) + "\r\n");// this will print the minimum inter record time
            file.Write("MINIMUM INTER-RECORDS TIME:=" + string.Format("{0}:{1}", intmin.Minutes, intmin.Seconds) + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("AVERAGE INTER-RECORD TIME:=" + string.Format("{0}:{1}", intavg.Minutes, intavg.Seconds) + "\r\n");// this will print the average inter record time
            file.Write("AVERAGE INTER-RECORD TIME:=" + string.Format("{0}:{1}", intavg.Minutes, intavg.Seconds) + "\r\n");
            outputbox.AppendText("\r\n");
            outputbox.AppendText("NUMBER OF BACKSPACES:= " + val + "\r\n");// this will print the number of backspaces
            file.Write("NUMBER OF BACKSPACES:= " + val + "\r\n");
            file.Close();
        }
        public void timeconversion()
        {//this method has all the time related functions 
            double avg=0.0,avv=0.0;
            for (int m = 0; m < backspace.Length; m++)
            {
                //string start = entrytimes[m];
                //DateTime startTime = entrytimes[m];

                //DateTime endTime = saveTime[m];

                //TimeSpan span = endTime.Subtract(startTime);
                TimeSpan duration = DateTime.Parse(savetimes[m].ToString()).Subtract(DateTime.Parse(entrytimes[m].ToString()));// calculation of the times for entries
                times[m] = duration.TotalSeconds;
                if (m <9)
                {
                    TimeSpan interspan = DateTime.Parse(entrytimes[m+1].ToString()).Subtract(DateTime.Parse(savetimes[m].ToString()));// calculating the times between two records
                    inter[m] = interspan.TotalSeconds;
                   
                    
                    
                
                }
                else 
                {
                    continue;

                }
                
            
            }
            for(int m = 0; m < backspace.Length; m++)
            {
                avg += times[m];

            }
            averagetime = avg / backspace.Length;
            for (int m = 0; m < backspace.Length; m++)
            {
                avv += inter[m];

            }
            averagespan = avv / backspace.Length;
            TimeSpan dur = DateTime.Parse(savetimes[9].ToString()).Subtract(DateTime.Parse(entrytimes[0].ToString()));// calculating the time taken for all the records to enter
            totaltime = dur.TotalSeconds;


        }
    }
}
