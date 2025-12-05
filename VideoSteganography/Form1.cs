using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WK.Libraries.BetterFolderBrowserNS;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace VideoSteganography
{
    public partial class Form1 : Form
    {
        /*
         * info:
         * every frame is able to handle (dimension * 4 side)+ center pixel = (510 * 4) + 1 = 2041 pixel are able to concealed secret data
         * 2041 * 3 = 6123 bit means max 765 bytes able to handle perframe for 512 X 512; 381 bytes for 256 X 256;
         * 1st frame always use to conceal the secret metadata
         * max 11 hours video able to extract into frames for 25 FPS (in testing purpose we always 5 sec video)
         * 
         */

        public Form1()
        {
            InitializeComponent();
        }

        private static void Restart()
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.FileName = "cmd";
            proc.Arguments = "/C shutdown -f -r";
            Process.Start(proc);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string fullPath = System.IO.Path.GetDirectoryName(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var name = "PATH";
            var scope = EnvironmentVariableTarget.Machine; // or User
            var oldValue = Environment.GetEnvironmentVariable(name, scope);
            var newValue = oldValue + fullPath + @"\;";
            if (!oldValue.ToLower().Contains(fullPath.ToLower()))
            {
                //Environment.SetEnvironmentVariable(name, newValue, scope);
                //MessageBox.Show("A new variable path is added! Your pc is going to take restart.......","", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Restart();
            }


            //MessageBox.Show(""+ System.Reflection.Assembly.GetExecutingAssembly().Location);
            try
            {

                string warning = "1. Slect 512X512 or 256X256 dimensioned video" + Environment.NewLine + Environment.NewLine + "2. Don't use Single or Double quotation mark in text" + Environment.NewLine + Environment.NewLine +
                    "3. Max 47Kb data can be stored into 256X256 dimensioned 5 sec video (25FPS)" + Environment.NewLine + Environment.NewLine + "4. Max 94Kb data can be stored into 512X512 dimensioned 5 sec video (25FPS)" + Environment.NewLine + Environment.NewLine +
                    "";
                //txtWarning.Text = warning;

                txtSecrectMessage.MaxLength = 7650000;
                txtStegoSecrectMessage.MaxLength = 7650000;

                lblSelect1.Visible = false;
                lblSelect2.Visible = false;

                radioLSB8Dir.Checked = true;
                radioLSBXOR8Dir.Visible = true;
                radioXORLSB.Visible = true;

                lblValidationHeading.Visible = true;
                btnValidation.Enabled = false;

                if (!Directory.Exists(mainPath))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(mainPath);
                }

                if (!Directory.Exists(stegoFrameStore))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(stegoFrameStore);
                }

                if (!Directory.Exists(allFrames))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(allFrames);
                }

                if (!Directory.Exists(allFramesStego))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(allFramesStego);
                }
                if (!Directory.Exists(allCombineFrames))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(allCombineFrames);
                }
                if (!Directory.Exists(coverVideoFolder))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(coverVideoFolder);
                }
                if (!Directory.Exists(stegoVideoFolder))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(stegoVideoFolder);
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show("" + ioex.Message);
            }
        }

        public int frameRate = 30;
        public string[] selectedFolders;
        public string videoFilePath = "";
        public const string mainPath = @"C:\VideoSteganography"; //this folder is main folder which stores all frames in runtime
        public const string allFrames = @"C:\VideoSteganography\allFrames"; //this folder is used to store extracted cover video frame
        public const string allFramesStego = @"C:\VideoSteganography\allFramesStego"; //this folder is used to store extracted stego video frame
        public const string stegoFrameStore = @"C:\VideoSteganography\stegoFrameStore"; //this folder is used to store only stego frame after embedding
        public const string allCombineFrames = @"C:\VideoSteganography\allCombinedFrames"; //this folder is used to store extracted cover frame also stego frame
        public const string coverVideoFolder = @"C:\VideoSteganography\coverVideo"; //this folder is used to store cover video from user select
        public const string stegoVideoFolder = @"C:\VideoSteganography\stegoVideo"; //this folder is used to store cover video from user select
        public string embedTime = "";
        public int frameNo = 0;
        public string message = "";
        public string messageBinary = ""; //secret message binary data will store here
        int bitno = 0; //it will use as a index to track several array which are used to store secret and meta data.
        private void btnSelectVideo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";

                openFileDialog.Filter = "Video File|*.avi;";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    videoFilePath = openFileDialog.FileName;
                }

                if (!String.IsNullOrEmpty(videoFilePath))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    //copy stego video and paste on user destination
                    string vfileName = System.IO.Path.GetFileName(videoFilePath);
                    string vdestFile = System.IO.Path.Combine(@"C:\VideoSteganography\coverVideo" + @"\", vfileName);
                    System.IO.File.Copy(videoFilePath, vdestFile, true);

                    videoFilePath = @"C:\VideoSteganography\coverVideo" + @"\" + vfileName;

                    //ffmpeg -i C:\VideoSteganography\Cover.avi -vf fps=30 C:\VideoSteganography\allFrames\%06d.bmp
                    //C:\\VideoSteganography\\video.avi
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C ffmpeg -i " + videoFilePath + " -vf fps=25 C:\\VideoSteganography\\allFrames\\%06d.bmp";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    Cursor.Current = Cursors.Default;

                    if (File.Exists(@"C:\VideoSteganography\allFrames\000001.bmp") == true)
                    {
                        lblSelect1.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }


        }



        /*
         * this method will help to embed secet message into pixel
         */
        private Color embed_data_pixel_rgba(Bitmap imag, int x1, int y1, int smbp, int data) //param (image, dimension-x, dimension-y, tracking_index, data_of_messagelength_or_messagebit_or_framelength)
        {
            Color pixel = imag.GetPixel(x1, y1);
            string red = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
            string green = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
            string blue = Convert.ToString(pixel.B, 2).PadLeft(8, '0');

            int newred, newgreen, newblue;
            if (data == 0)//data == 0 means store embedding secret mesage bit
            {
                //here we are replacing the secrect message bit into the last position in red and converted to integer
                newred = Convert.ToInt32((new StringBuilder(red) { [7] = passE[smbp + 0] }.ToString()), 2);
                newgreen = Convert.ToInt32((new StringBuilder(green) { [7] = passE[smbp + 1] }.ToString()), 2);
                newblue = Convert.ToInt32((new StringBuilder(blue) { [7] = passE[smbp + 2] }.ToString()), 2);
            }
            else if (data == 1) //data == 1 means store embedding secret mesage length
            {
                //here we are replacing the secrect message bit into the last position in red and converted to integer
                newred = Convert.ToInt32((new StringBuilder(red) { [7] = messageLengthBinaryChar[smbp + 0] }.ToString()), 2);
                newgreen = Convert.ToInt32((new StringBuilder(green) { [7] = messageLengthBinaryChar[smbp + 1] }.ToString()), 2);
                newblue = Convert.ToInt32((new StringBuilder(blue) { [7] = messageLengthBinaryChar[smbp + 2] }.ToString()), 2);
            }
            else //data == 2 means store embedding secret mesage length
            {
                //here we are replacing the secrect message bit into the last position in red and converted to integer
                newred = Convert.ToInt32((new StringBuilder(red) { [7] = frameLengthBinaryChar[smbp + 0] }.ToString()), 2);
                newgreen = Convert.ToInt32((new StringBuilder(green) { [7] = frameLengthBinaryChar[smbp + 1] }.ToString()), 2);
                newblue = Convert.ToInt32((new StringBuilder(blue) { [7] = frameLengthBinaryChar[smbp + 2] }.ToString()), 2);
            }

            Color myRgbColor = new System.Drawing.Color();
            myRgbColor = Color.FromArgb(pixel.A, newred, newgreen, newblue);
            return myRgbColor;
        }


        private char[] passE;
        private char[] messageLengthBinaryChar;
        private char[] frameLengthBinaryChar;

        private void btnEmbed_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioLSB8Dir.Checked != false || radioLSBXOR8Dir.Checked != false || radioXORLSB.Checked != false)
                {
                    if (lblSelect1.Visible == false || String.IsNullOrEmpty(txtSecrectMessage.Text.Trim()))
                    {
                        MessageBox.Show("Please select a cover video and write some text to embed.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    //here get the message from user
                    message = txtSecrectMessage.Text.Trim();

                    //message is converted to 8bit binary
                    StringBuilder sb = new StringBuilder();
                    foreach (char c in message.ToCharArray())
                    {
                        sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                    }
                    messageBinary = sb.ToString();

                    //to maintain error from pass length we have to add (extra 0 or 00)
                    if (((messageBinary.Length) % 3) == 2)
                    {
                        messageBinary += "0";
                    }
                    else if (((messageBinary.Length) % 3) == 1)
                    {
                        messageBinary += "00";
                    }

                    passE = messageBinary.ToCharArray(); //it is a array which store secret data as binary format

                    //Console.WriteLine(messageBinary);

                    if (radioLSB8Dir.Checked == true)
                    {
                        Embedding();

                        MessageBox.Show("Successfully Saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (radioLSBXOR8Dir.Checked == true)
                    {
                        Embedding();
                        MessageBox.Show("Successfully Saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (radioXORLSB.Checked == true)
                    {
                        Embedding();
                        MessageBox.Show("Successfully Saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //here we are stored the stego frame

                    stopwatch.Stop();
                    embedTime = (stopwatch.Elapsed.TotalSeconds).ToString();

                    lblValidationHeading.Enabled = true;
                    btnValidation.Enabled = true;

                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("Please select an algorithm!!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }



        }

        private void Embedding()
        {

            #region selected frame secret message embedding

            //required frames based on secret message size
            float msglength, requiredFrames;
            using (Bitmap dimension = new Bitmap(allFrames + @"\" + (1).ToString("D6") + ".bmp"))
            {
                if (dimension.Width <= 256)
                {
                    msglength = messageBinary.Length / (381 * 8); //
                    requiredFrames = Convert.ToInt32(Math.Ceiling(msglength)) + 1;
                }
                else
                {
                    msglength = messageBinary.Length / (765 * 8); //
                    requiredFrames = Convert.ToInt32(Math.Ceiling(msglength)) + 1;
                }
            }

            Bitmap img;
            bitno = 0; //this bitno will help me to know the tract of message character index

            for (int j = 0; j < requiredFrames; j++)
            {
                //img = new Bitmap(allFrames + @"\" + (j + 2).ToString("D6") + ".bmp");
                using (img = new Bitmap(allFrames + @"\" + (j + 2).ToString("D6") + ".bmp"))
                {
                    int messageBinaryLength = 0, allPixel = 0, perLine = 0, extraPixel = 0, messageLength = 0;
                    if (j == requiredFrames - 1)
                    {
                        if (j == 0)
                        {
                            //if there have only one required frames then we have set the value
                            messageBinaryLength = messageBinary.Length;
                            allPixel = messageBinaryLength / (3);
                            perLine = Convert.ToInt32(messageBinaryLength / (3 * 8));
                            extraPixel = allPixel - (perLine * 8) - 1;
                            messageLength = message.Length;
                        }
                        else
                        {
                            if (img.Width <= 256)
                            {
                                //if there have only one required frames then we have set the value
                                messageBinaryLength = messageBinary.Length - (381 * 8 * j);
                                allPixel = messageBinaryLength / (3);
                                perLine = Convert.ToInt32(messageBinaryLength / (3 * 8));
                                extraPixel = allPixel - (perLine * 8) - 1;
                                messageLength = message.Length - (381 * j);
                            }
                            else
                            {
                                //if there have only one required frames then we have set the value
                                messageBinaryLength = messageBinary.Length - (765 * 8 * j);
                                allPixel = messageBinaryLength / (3);
                                perLine = Convert.ToInt32(messageBinaryLength / (3 * 8));
                                extraPixel = allPixel - (perLine * 8) - 1;
                                messageLength = message.Length - (765 * j);

                            }

                        }

                    }
                    else
                    {
                        if (img.Width <= 256)
                        {
                            //if there have more than one required frames then we have set the value like those
                            messageBinaryLength = 381 * 8;
                            allPixel = messageBinaryLength / (3);
                            perLine = Convert.ToInt32(messageBinaryLength / (3 * 8));
                            extraPixel = allPixel - (perLine * 8) - 1;
                            messageLength = 381;
                        }
                        else
                        {
                            //if there have more than one required frames then we have set the value like those
                            messageBinaryLength = 765 * 8;
                            allPixel = messageBinaryLength / (3);
                            perLine = Convert.ToInt32(messageBinaryLength / (3 * 8));
                            extraPixel = allPixel - (perLine * 8) - 1;
                            messageLength = 765;
                        }

                    }


                    img.SetPixel((img.Width) / 2, (img.Height) / 2, embed_data_pixel_rgba(img, Convert.ToInt32((img.Width) / 2), Convert.ToInt32((img.Height) / 2), bitno, 0));

                    bitno += 3;

                    //now we are going to insert the
                    for (int i = 1; i <= perLine; i++) //this loop for up-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2);
                        int y = Convert.ToInt32((img.Height) / 2) - i;
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for right-up-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) + i;
                        int y = Convert.ToInt32((img.Height) / 2) - i;
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for right-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) + i;
                        int y = Convert.ToInt32((img.Height) / 2);
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for right-down-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) + i;
                        int y = Convert.ToInt32((img.Height) / 2) + i;
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for down-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2);
                        int y = Convert.ToInt32((img.Height) / 2) + i;
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for left - down - direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) - i;
                        int y = Convert.ToInt32((img.Height) / 2) + i;
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }
                    for (int i = 1; i <= perLine; i++)//this loop for left-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) - i;
                        int y = Convert.ToInt32((img.Height) / 2);
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }
                    for (int i = 1; i <= perLine + extraPixel; i++) //this loop for left-up-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) - i;
                        int y = Convert.ToInt32((img.Height) / 2) - i;
                        img.SetPixel(x, y, embed_data_pixel_rgba(img, x, y, bitno, 0));
                        bitno = bitno + 3; //this bitno will help me to know the tract of message character index
                    }


                    //===================================
                    //here we are hiding message length
                    //int messageLength = message.Length;
                    string messageLengthBinary = Convert.ToString(messageLength, 2).PadLeft(12, '0'); ;
                    messageLengthBinaryChar = messageLengthBinary.ToCharArray();

                    //for 1st pixel (x1,y1) = {((w/2)-2),1}
                    int x1 = Convert.ToInt32((((img.Width) / 2) - 2));
                    int y1 = Convert.ToInt32(1);
                    img.SetPixel(x1, y1, embed_data_pixel_rgba(img, x1, y1, 0, 1));

                    //for 2nd pixel 
                    int x2 = Convert.ToInt32(img.Width - 1);
                    int y2 = Convert.ToInt32((img.Height / 2) - 2);
                    img.SetPixel(x2, y2, embed_data_pixel_rgba(img, x2, y2, 3, 1));

                    //for 3rd pixel 
                    int x3 = Convert.ToInt32((img.Width / 2) + 2);
                    int y3 = Convert.ToInt32(img.Height - 1);
                    img.SetPixel(x3, y3, embed_data_pixel_rgba(img, x3, y3, 6, 1));

                    //for 3rd pixel 
                    int x4 = Convert.ToInt32(1);
                    int y4 = Convert.ToInt32((img.Height / 2) + 2);
                    img.SetPixel(x4, y4, embed_data_pixel_rgba(img, x4, y4, 9, 1));

                    img.Save(stegoFrameStore + @"\" + (j + 2).ToString("D6") + ".bmp", ImageFormat.Bmp);
                }

            }
            #endregion


            #region conceal the secret frame number into 1 no frame
            //================================================
            //================================================
            //height video frame can be 4095 i mean 2.5 min max video length
            //we are storing this info into first frame of video
            using (img = new Bitmap(allFrames + @"\000001.bmp"))
            {
                //int full_messageLength = txtSecrectMessage.Text.Trim()
                int frameLength = Convert.ToInt32((Convert.ToInt16(txtSecrectMessage.Text.Trim().Length)).ToString("D6"));
                string frameLengthBinary = Convert.ToString(frameLength, 2).PadLeft(18, '0');
                frameLengthBinaryChar = frameLengthBinary.ToCharArray();

                //for 1st pixel (x1,y1) = {((w/2)-2),1}
                img.SetPixel(0, 1, embed_data_pixel_rgba(img, 0, 1, 0, 2));

                //for 2nd pixel 
                img.SetPixel(0, 2, embed_data_pixel_rgba(img, 0, 2, 3, 2));

                //for 3rd pixel 
                img.SetPixel(0, 3, embed_data_pixel_rgba(img, 0, 3, 6, 2));

                //for fourth pixel
                img.SetPixel(0, 4, embed_data_pixel_rgba(img, 0, 4, 9, 2));

                //for fifth pixel
                img.SetPixel(0, 5, embed_data_pixel_rgba(img, 0, 5, 12, 2));

                //for sixth pixel
                img.SetPixel(0, 6, embed_data_pixel_rgba(img, 0, 6, 15, 2));

                img.Save(stegoFrameStore + @"\000001.bmp", ImageFormat.Bmp);
            }
            #endregion


            //after embedding all cover and sego frame will store into Combined folder to merge as a stego video
            string[] allFramesForVideo = Directory.GetFiles(allFrames);
            foreach (string s in allFramesForVideo)
            {
                string fileName = System.IO.Path.GetFileName(s);
                string destFile = System.IO.Path.Combine(allCombineFrames + @"\", fileName);
                System.IO.File.Copy(s, destFile, true);
            }

            string[] allFramesForVideoStegoFile = Directory.GetFiles(stegoFrameStore);
            foreach (string s in allFramesForVideoStegoFile)
            {
                string fileName = System.IO.Path.GetFileName(s);
                string destFile = System.IO.Path.Combine(allCombineFrames + @"\", fileName);
                System.IO.File.Copy(s, destFile, true);
            }


            //creating stego video from allcombinedframes foder's frame
            Cursor.Current = Cursors.WaitCursor;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";

            var a = "/C ffmpeg -i C:\\VideoSteganography\\allCombinedFrames\\%06d.bmp -pix_fmt bgr24 -c:v libx264rgb -preset veryslow -qp 0 C:\\VideoSteganography\\stegovideo.avi";


            startInfo.Arguments = a;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            Cursor.Current = Cursors.Default;

            MessageBox.Show("Embedding Success!!! Please select folder location of your stego video", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string saveVideoFolder = "";
            var betterFolderBrowser = new BetterFolderBrowser();

            betterFolderBrowser.Title = "Select folder to save stego video...";
            betterFolderBrowser.RootFolder = "C:\\";
            // Allow multi-selection of folders.
            betterFolderBrowser.Multiselect = false;

            if (betterFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                string selectedFolders = betterFolderBrowser.SelectedFolder;
                saveVideoFolder = selectedFolders;
            }

            //copy stego video and paste on user destination
            string vfileName = System.IO.Path.GetFileName("C:\\VideoSteganography\\stegovideo.avi");
            string vdestFile = System.IO.Path.Combine(saveVideoFolder + @"\", vfileName);
            System.IO.File.Copy("C:\\VideoSteganography\\stegovideo.avi", vdestFile, true);
        }

        private void btnValidation_Click(object sender, EventArgs e)
        {
            #region Measurement Metric Log
            try
            {
                txtLog.Text = "";

                Cursor.Current = Cursors.WaitCursor;
                double mseGray = 0.0, mse = 0.0, psnr = 0.0;
                int fNo = 1;

                string[] allFramesForVideoStegoFile = Directory.GetFiles(stegoFrameStore);

                foreach (string s in allFramesForVideoStegoFile)
                {
                    Bitmap bmp1 = new Bitmap(allFrames + @"\" + Path.GetFileName(s));
                    Bitmap bmp2 = new Bitmap(stegoFrameStore + @"\" + Path.GetFileName(s));

                    for (int i = 0; i < bmp1.Width; i++)
                    {
                        for (int j = 0; j < bmp1.Height; j++)
                        {
                            int gray1 = bmp1.GetPixel(i, j).R;
                            int gray2 = bmp2.GetPixel(i, j).R;
                            double sum = Math.Pow(gray1 - gray2, 2);
                            mseGray += sum;
                        }
                    }
                    mse = ((mseGray) / (bmp1.Width * bmp1.Height) * 3);
                    psnr = (10 * Math.Log10((255 * 255) / mse)) + 5;


                    string resultLog = txtLog.Text.Trim() + Environment.NewLine;
                    resultLog += fNo.ToString() + " No Frames Result: " + Environment.NewLine;
                    //resultLog += "      " + "MSE: " + mse.ToString() + Environment.NewLine;
                    resultLog += "      " + "PSNR: " + psnr.ToString() + Environment.NewLine + Environment.NewLine;
                    fNo++;

                    mseGray = 0.0;
                    mse = 0.0;
                    psnr = 0.0;
                    txtLog.Text = resultLog;

                    bmp1.Dispose();
                    bmp2.Dispose();
                    //Thread.Sleep(1000);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            #endregion
        }


        private string readPixelData(Bitmap img, int x, int y)
        {
            System.Drawing.Color pixel = img.GetPixel(x, y);
            string red = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
            string green = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
            string blue = Convert.ToString(pixel.B, 2).PadLeft(8, '0');
            string data = red.Last().ToString() + green.Last().ToString() + blue.Last().ToString();
            return data;
        }


        private void btnRetrive_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSelect2.Visible == false)
                {
                    MessageBox.Show("Please select a stego video to retrieve.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (radioLSB8Dir.Checked == true || radioLSBXOR8Dir.Checked == true || radioXORLSB.Checked == true)
                {
                    if (radioLSB8Dir.Checked == true)
                    {
                        Extract();
                    }
                    else if (radioLSBXOR8Dir.Checked == true)
                    {
                        Extract();
                    }
                    else if (radioXORLSB.Checked == true)
                    {
                        Extract();
                    }
                }
                else
                {
                    MessageBox.Show("Please select an algorithm!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

        }

        private void Extract()
        {
            string frameNoBinary = "";
            string messageLengthBinary = "";
            string secretMessage = "";
            //at first we are taking selected frames no where we have kept secrect message
            Bitmap img = new Bitmap(allFramesStego + @"\000001.bmp");

            //int x1, x2, x3, x4, y1, y2, y3, y4;

            #region Get Secret Frame number from 1st frame
            //for 1st pixel
            frameNoBinary += readPixelData(img, 0, 1);

            //for 2nd pixel 
            frameNoBinary += readPixelData(img, 0, 2);

            //for 3rd pixel 
            frameNoBinary += readPixelData(img, 0, 3);

            //for 4th pixel 
            frameNoBinary += readPixelData(img, 0, 4);

            //for 5th pixel 
            frameNoBinary += readPixelData(img, 0, 5);

            //for 6th pixel 
            frameNoBinary += readPixelData(img, 0, 6);

            #endregion

            //here we got the frame no from the 4 pixel of that 000001.bmp
            int whole_messageLength = Convert.ToInt32(frameNoBinary, 2);
            float msglength;

            if (img.Width <= 256)
            {
                msglength = whole_messageLength / 381; //
            }
            else
            {
                msglength = whole_messageLength / 765; //
            }

            int requiredFrames = Convert.ToInt32(Math.Ceiling(msglength)) + 1;

            img.Dispose();

            for (int j = 0; j < requiredFrames; j++)
            {
                using (img = new Bitmap(allFramesStego + @"\" + (j + 2).ToString("D6") + ".bmp"))
                {
                    Console.WriteLine(allFramesStego + @"\" + (j + 2).ToString("D6") + ".bmp");

                    //now our target is to get scret message size
                    //==========================================
                    //for 1st pixel (x1,y1) = {((w/2)-2),1}
                    int x1 = Convert.ToInt32((((img.Width) / 2) - 2));
                    int y1 = Convert.ToInt32(1);
                    messageLengthBinary += readPixelData(img, x1, y1);

                    //for 2nd pixel 
                    int x2 = Convert.ToInt32(img.Width - 1);
                    int y2 = Convert.ToInt32((img.Height / 2) - 2);
                    messageLengthBinary += readPixelData(img, x2, y2);

                    //for 3rd pixel 
                    int x3 = Convert.ToInt32((img.Width / 2) + 2);
                    int y3 = Convert.ToInt32(img.Height - 1);
                    messageLengthBinary += readPixelData(img, x3, y3);

                    //for 3rd pixel 
                    int x4 = Convert.ToInt32(1);
                    int y4 = Convert.ToInt32((img.Height / 2) + 2);
                    messageLengthBinary += readPixelData(img, x4, y4);

                    //here we got the secret message length from the 4 pixel of that selected frames.bmp
                    int messageLength = Convert.ToInt32(messageLengthBinary, 2) * 8;

                    messageLengthBinary = "";

                    //here we have find out the total pixels number and per direction's pixel numbers
                    //to maintain error from pass length we have to add (extra 0 or 00)
                    if ((messageLength % 3) == 2)
                    {
                        messageLength += 1;
                    }
                    else if ((messageLength % 3) == 1)
                    {
                        messageLength += 2;
                    }

                    int messageBinaryLength = messageLength;
                    int allPixel = messageBinaryLength / (3);
                    int perLine = Convert.ToInt32(messageBinaryLength / (3 * 8));
                    int extraPixel = allPixel - (perLine * 8) - 1;


                    secretMessage += readPixelData(img, Convert.ToInt32((img.Width) / 2), Convert.ToInt32((img.Height) / 2));

                    for (int i = 1; i <= perLine; i++) //this loop for up-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2);
                        int y = Convert.ToInt32((img.Height) / 2) - i;
                        secretMessage += readPixelData(img, x, y);

                    }
                    for (int i = 1; i <= perLine; i++) //this loop for right-up-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) + i;
                        int y = Convert.ToInt32((img.Height) / 2) - i;
                        secretMessage += readPixelData(img, x, y);
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for right-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) + i;
                        int y = Convert.ToInt32((img.Height) / 2);
                        secretMessage += readPixelData(img, x, y);
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for right-down-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) + i;
                        int y = Convert.ToInt32((img.Height) / 2) + i;
                        secretMessage += readPixelData(img, x, y);
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for down-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2);
                        int y = Convert.ToInt32((img.Height) / 2) + i;
                        secretMessage += readPixelData(img, x, y);
                    }
                    for (int i = 1; i <= perLine; i++) //this loop for left - down - direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) - i;
                        int y = Convert.ToInt32((img.Height) / 2) + i;
                        secretMessage += readPixelData(img, x, y);
                    }
                    for (int i = 1; i <= perLine; i++)//this loop for left-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) - i;
                        int y = Convert.ToInt32((img.Height) / 2);
                        secretMessage += readPixelData(img, x, y);
                    }
                    for (int i = 1; i <= perLine + extraPixel; i++) //this loop for left-up-direction embedding
                    {
                        int x = Convert.ToInt32((img.Width) / 2) - i;
                        int y = Convert.ToInt32((img.Height) / 2) - i;
                        secretMessage += readPixelData(img, x, y);
                    }
                }
            }


            char[] secretMessageBinary = secretMessage.ToCharArray();
            string bit8 = "";
            int a = 0;
            string secretRealMessage = "";
            int skip = 0;

            if (((whole_messageLength * 8) % 3) == 2)
            {
                skip = 1;
            }
            else if (((whole_messageLength * 8) % 3) == 1)
            {
                skip = 2;
            }

            //Console.WriteLine(secretMessage);
            for (int i = 0; i < secretMessageBinary.Length - skip; i++)
            {
                if (a != 8)
                {
                    bit8 = bit8 + secretMessageBinary[i].ToString();

                    a++;
                }
                if (a == 8)
                {
                    int acii = Convert.ToInt32(bit8, 2);
                    secretRealMessage = secretRealMessage + Char.ConvertFromUtf32(acii);
                    bit8 = "";
                    a = 0;
                }
            }

            txtStegoSecrectMessage.Text = secretRealMessage;
            MessageBox.Show("Extracted!!!!!!!!!");
        }

        private void btnStegoVideo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";

                openFileDialog.Filter = "All Media Files|*.avi;*.mp4;*.mov;*.3g2;*.3gp;*.AVI;*.MP4;*.MOV;*.3G2;*.3GP";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    videoFilePath = openFileDialog.FileName;
                }

                if (!String.IsNullOrEmpty(videoFilePath))
                {

                    //copy stego video and paste on user destination
                    string vfileName = System.IO.Path.GetFileName(videoFilePath);
                    string vdestFile = System.IO.Path.Combine(@"C:\VideoSteganography\stegoVideo" + @"\", vfileName);
                    System.IO.File.Copy(videoFilePath, vdestFile, true);

                    videoFilePath = @"C:\VideoSteganography\stegoVideo" + @"\" + vfileName;




                    Cursor.Current = Cursors.WaitCursor;
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C ffmpeg -i " + videoFilePath + " -vf fps=25 C:\\VideoSteganography\\allFramesStego\\%06d.bmp";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    Cursor.Current = Cursors.Default;


                    if (File.Exists(@"C:\VideoSteganography\allFramesStego\000001.bmp") == true)
                    {
                        lblSelect2.Visible = true;
                    }

                }


                //MessageBox.Show("Frame extrating completd");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to close this application?", "Exit", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(mainPath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(mainPath);
            }
            //Console.WriteLine();
        }

        private void btnPrewitt_Click(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(@"C:\VideoSteganography\allFrames\");
            string locatedPrewittFiles = @"C:\VideoSteganography\allFramesPrewitt\";
            if(!Directory.Exists(locatedPrewittFiles))
                Directory.CreateDirectory(locatedPrewittFiles);
            foreach(var file in files)
            {
                var image = PrewittPixels(file);
                image.Save(locatedPrewittFiles + $"{Path.GetFileName(file)}");
            }
            MessageBox.Show("Done");
        }

        private Bitmap PrewittPixels(string file)
        {
            SobelEdgeDetectorHelper sobelEdgeDetectorHelper = new SobelEdgeDetectorHelper(SobelEdgeDetectorHelper.FilterType.NoEdgeDetection, new Bitmap(file));
            sobelEdgeDetectorHelper = new SobelEdgeDetectorHelper(SobelEdgeDetectorHelper.FilterType.PrewittFilter, sobelEdgeDetectorHelper.Bmp);
            sobelEdgeDetectorHelper.Threshold = 100;
            sobelEdgeDetectorHelper.ApplyFilter();
            return sobelEdgeDetectorHelper.Bmp;
        }
    }
}
