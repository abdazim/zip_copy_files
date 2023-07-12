using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;



namespace copy2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();      
        }




        //////////////////////////GroupBox- zip file///////////////////////////////////
        /// <summary>
        /// Base Browse  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(fbd.SelectedPath);
                string[] dirs = Directory.GetDirectories(fbd.SelectedPath);

                foreach (string file in files)
                {
                    listBox1.Items.Add(Path.GetFileName(file));
                }
                foreach (string dir in dirs)
                {
                    listBox1.Items.Add(Path.GetFileName(dir));
                }
                textBox3.Text = fbd.SelectedPath;  
            }
        }


        /// <summary>
        /// textboox3 (base directory zip files)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = false;
            //// if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9a-zA-Z]"))
            //if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^a-zA-Z]"))
            //{
            //    MessageBox.Show("Enter a valid directory");
            //    textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            //}
        }




        /// <summary>
        /// zip button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        string newtime;
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("Browse a File to zip");
                }
                else
                {
                    label2.Visible = true;
                    label2.Refresh();

                    DateTime startTime = DateTime.Now;
                    filezip(textBox3.Text);
                    DateTime endTime = DateTime.Now;
                    TimeSpan span = endTime.Subtract(startTime);
                    newtime = Convert.ToString(span.Hours + "Hour ;" + span.Minutes + " Min ;" + span.Seconds + "Sec .");                
                    csvzipfile();
                    MessageBox.Show("the zip finished successfully");
                    //MessageBox.Show(span.Hours+ "Hour ," + span.Minutes+" Min ,"+ span.Seconds+"Sec .");            
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        



        /// <summary>
        /// clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            listBox1.Items.Clear();
        }


        /// <summary>
        /// listbox1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //////////////////////////GroupBox - copy file////////////////////////////////
        /// <summary>
        /// Base Browse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        string newpath; //= sFileName = openFileDialog1.FileName; use in copy funiction
        private void button11_Click(object sender, EventArgs e)
        {
            bool isExist = false;  // flag use to check if zip file is exist  
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "zip files (*.zip)|*.zip";
            //openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "zip files Browser";
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        string sFileName = openFileDialog1.FileName;
                        string[] arrAllFiles = openFileDialog1.FileNames; //used when Multiselect = true 
                        using (myStream)
                        {                      
                            foreach (string x in listBox3.Items)   //foreach to avoid add zip file twice
                            {
                                //if (x == sFileName)   //sfile path of zip file that add to listbox c:/ ...../.ZIP
                                if (x == sFileName)
                                    isExist = true;   // if listBox3.Items is exist put true in isexist bool
                            }
                            if (!isExist) //if the file not exist add to listbox3
                            {
                                listBox3.Items.Add(sFileName);
                                textBox5.Text = sFileName;
                                newpath = sFileName;
                                //MessageBox.Show(Path.GetFileName(newpath));
                            }
                            else
                                MessageBox.Show("File already exist");                                                         
                        }                      
                    }                  
                }
                if (listBox3.Items.Count > 0) //auto select the first item 
                    listBox3.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }

        


        /// <summary>
        /// target Browse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd3 = new FolderBrowserDialog();
                if (fbd3.ShowDialog() == DialogResult.OK)
                {
                    //listBox1.Items.Clear();
                    string[] files = Directory.GetFiles(fbd3.SelectedPath);
                    string[] dirs = Directory.GetDirectories(fbd3.SelectedPath);

                    foreach (string file in files)
                    {
                        // listBox3.Items.Add(Path.GetFileName(file));
                    }
                    foreach (string dir in dirs)
                    {
                        //  listBox3.Items.Add(Path.GetFileName(dir));
                    }
                    textBox6.Text = fbd3.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror. "+ex.Message);
            }
        }




       /// <summary>
       /// listBox3_Selected
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       //  string listbox3;
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox3.SelectionMode = SelectionMode.MultiExtended;
                //listbox3 = listBox3.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        /// <summary>
        /// start copy button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        String strItem; //
        string newtime1; //time elapsed: 
        string temp = "";  // strItem get data from selected list and temp get from  strItem and ++ . type all files that enter to the funiction
        int newcopycounter;
        private void button13_Click(object sender, EventArgs e)
        {         
            try
            {
                if (string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox1.Text) || listBox3.SelectedItem.ToString() == null)
                {
                    MessageBox.Show("Choose Base/Target directory ,LOG file name and select files to copy ");
                }
                else
                {
                    label3.Visible = true;
                    label3.Refresh();
                    counter = 0;
                    DateTime startTime1 = DateTime.Now;  //DateTime now  when you press the start copy button 
                    foreach (Object selecteditem in listBox3.SelectedItems) //forech to put listBox3.SelectedItems to selectitem varible 
                    {                                            
                        strItem = selecteditem as String;
                        string source = strItem; // listbox3 select item 
                        if (counter > 0)  //if the file not the first one add counter(1,2,3,4)and stritem(selected list).
                            temp += (", "+ (counter+1) + ": " + strItem);
                        else   //if the file the first one add counter(1)+ stritem=(slelected listbox)
                            temp += (counter+1) +": "+ strItem;
                        string targetfile = textBox6.Text; // target textbox-copy area   
                        copyfile(source, targetfile,true);
                        //funiction of time ( after the funiction is finished - when you press start button = get the elapsed time 
                        DateTime endTime1 = DateTime.Now;  //date time after the test is finished 
                        TimeSpan span = endTime1.Subtract(startTime1); // difference betwen start and end 
                        newtime1 = Convert.ToString(span.Hours + "Hour ;" + span.Minutes + " Min ;" + span.Seconds + "Sec .");  //put the time in newtime1                      
                    }
                    csvcopyzipfile(); //call a funiction  that make csv file                   
                    MessageBox.Show("copy is finished");
                    label3.Visible = false; // LABEL3 IS VISIBLE=true at first , after the code is finished(messagebox(finished)and label false(hidden) 
                    label3.Refresh();     //Always need to refresh label after change 
                }
            }
                
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }       
        }



        /// <summary>
        /// clear copy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            textBox6.Text = "";
            textBox1.Text = "";
            listBox3.Items.Clear();
        }



        /// <summary>
        /// destination directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text))
                {
                MessageBox.Show("Choose a Target directory");
                }
            else
              {
                Process.Start(textBox6.Text);
              }                      
        }



        /// <summary>
        /// TEXTBOX1 CSV FILE NAME
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
              if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9a-zA-Z]"))
            {
                MessageBox.Show("Enter a valid Name/Number A-Z - 0-9");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }


        /// <summary>
        /// textbox5 (base directory copy zip files)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = false;

        }




        /// <summary>
        /// textbox6 (target directory copy zip files)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = false;
            //// if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9a-zA-Z]"))
            //if (System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, @"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$"))
            //{
            //    MessageBox.Show("Enter a valid directory");
            //    textBox6.Text = textBox6.Text.Remove(textBox6.Text.Length - 1);
            //}
        }


        //////////////////////////function////////////////////////////////////////////
        /// <summary>
        /// filezip function
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        /// 
        string newfilename;//csv zip file function new string[]{ "Target directory: " , newfilename.ToString(), " "," "}, // directory path into the ecxel file 
        string newfilename1; //csv zip file function  string filePath = (newfilename1 + ".csv");   //the name of the file we make 
        private  void filezip(string source)
        {
            string parent = Path.GetDirectoryName(source);
            string name = Path.GetFileName(source);
            string filename = Path.Combine(parent, name + ".zip");
            newfilename1 = filename;
            try
            {
                if (!File.Exists((filename)))
                {
                    ZipFile.CreateFromDirectory(source, filename, CompressionLevel.Fastest, true); // add referance system.io.compreshion.filesystem
                }
                else
                {                
                    MessageBox.Show("The file is exist ");
                    File.Delete(filename);
                    ZipFile.CreateFromDirectory(source, filename, CompressionLevel.Fastest, true); // add referance system.io.compreshion.filesystem
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show("the zip finished successfully");
            newfilename = filename; // put filename(path of zip in desktop) to newfilename
            label2.Visible = false;
            label2.Refresh();           
            Process.Start(filename); //using System.Diagnostics;  open the destination directory after zip          
        }




        /// <summary>
        /// copy file function
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="destName"></param>
        /// 
        string newdestnamecsv="";
        string csvnamenewdestname;
        public int counter = 0;   //(counter to give the target file base name+counter number) destName + @"\" + Path.GetFileName(strItem) + counter + ".zip";
        public void copyfile(string sourceName, string destName,bool overwrite)
        {
            counter++;   //(counter to give the target file base name+ counter number ++)
            FileInfo f = new FileInfo(sourceName);
            long filesize = f.Length; /// file size with (bytes)
            //double  inputValue = Math.Round(filesizenew, 2);  // two places after . (16.00) 
            //1 Byte = 8 Bit
            ///1 Kilobyte = 1024 Bytes
            ///1 Megabyte = 1048576 Bytes
            ///1 Gigabyte = 1073741824 Bytes

            try
                {
                    if (filesize >= 17179869184)     // check if filesize >  2147483648=2GB*4=8GB*2= 16GB
                    {
                        MessageBox.Show("ZIP file is bigger than 16.0 GB");
                    }
                    else
                    {
                        string destnamenew = destName + @"\" + Path.GetFileName(strItem) + counter + ".zip";
                        File.Copy(sourceName, destnamenew, overwrite);
                    }
                    newdestnamecsv = destName + @"\" + Path.GetFileName(strItem);
                    csvnamenewdestname = destName + @"\";
                    //MessageBox.Show(csvnamenewdestname); 
                }
            catch (Exception e)
                {
                MessageBox.Show(e.Message);
                }
        }




        /// <summary>
        /// csv zip file function
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="destName"></param>
        /// <param name="overwrite"></param>
        /// 
        string newtime2;  //for the csv file name 
        public void csvzipfile()
        {
            //string filePath = @"C:\test.csv";
            DateTime startTime1 = DateTime.Now;  //get time now 
            //newtime2 get dayofweek(sundy)+day(5)+month(10)+year(2017)+hour(15)+minute(20)
            newtime2 = Convert.ToString("-" + startTime1.DayOfWeek + "-" + startTime1.Day + "-" + startTime1.Month + "-" + startTime1.Year + "-" + startTime1.Hour + "-" + startTime1.Minute);

            string filePath = (newfilename1 + newtime2 + ".csv");   //the name of the file we make 
            string delimiter = ",";
            string[][] output = new string[][]{
             new string[]{ "time of Log File created: ", DateTime.Now.ToString(), " "," "},
             new string[]{ "Base directory: ", textBox3.Text , " "," "},
             new string[]{ "target path: ", newfilename.ToString(), " "," "},  // directory path into the ecxel file 
             new string[]{ "ZIP file time elapsed: " , newtime, " "," "}  //newtime1 from zip button time 
            };

            int length = output.GetLength(0);
                    StringBuilder sb = new StringBuilder();
                    for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                    File.WriteAllText(filePath, sb.ToString());                    
        }




        /// <summary>
        /// csv copy zip file function
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="destName"></param>
        /// <param name="overwrite"></param>
        public void csvcopyzipfile()
        {
            //string filePath = @"C:\test.csv";
            //string filePath = (newdestnamecsv + ".csv"); 
            DateTime startTime =   DateTime.Now;  //get time now 
            //newtime get dayofweek(sundy)+day(5)+month(10)+year(2017)+hour(15)+minute(20)
            newtime = Convert.ToString("-" + startTime.DayOfWeek + "-"+ startTime.Day + "-" + startTime.Month + "-" + startTime.Year+ "_" + startTime.Hour + "-" + startTime.Minute );
            string filePath = (csvnamenewdestname + textBox1.Text + newtime + ".csv");  //csvnamenewdestname=distinathon of target file +textbox1=name of file+newtime(time)+.csv
            string delimiter = ",";
            newcopycounter = counter; // from start copy button (counter of files that copy)
            string[][] output = new string[][]{
             new string[]{ "time of Log File created: ", DateTime.Now.ToString(), " "," "},
             new string[]{ "Selected files: ", temp ,  "", " "},
             new string[]{ "target  path: ", textBox6.Text, " "," "},
             new string[]{ "Copy ZIP files time elapsed: ", newtime1, " "," "}, //newcopycounter
             new string[]{ "files number: ",  newcopycounter.ToString(), " "," "}
            };

            int length = output.GetLength(0);
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));
            File.WriteAllText(filePath, sb.ToString());
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All rights reserved to : SanDisk | a Western Digital brand | Abed.azem@wdc.com ");

        }
    }
}
