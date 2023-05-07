using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Online_Chat_TCP_IP.Services;
using SimpleTcp;
using System.IO;
using System.Net.Sockets;


namespace Online_Chat_TCP_IP
{
    public partial class frm_ChatMessageClient : Form
    {
        #region Variable

        string ip;
        string port;
        #endregion
        int[,] key = { { 2, 0, 1, 0 }, { 0, 1, 0, 0 }, { 1, 0, 3, 2 }, { 0, 0, 2, 1 } };
        public frm_ChatMessageClient(string IPServer, string PORTServer)
        {
            InitializeComponent();
            ip = IPServer;
            port = PORTServer;
        }

        /// <summary>
        /// Create Client and connect to server
        /// </summary>
        /// <param name="ip">string ip</param>
        /// <param name="port">string port</param>
        private void ActionUser(string ip, string port)
        {
            try
            {
                Program.client = new ClassClient(ip, port);
                Program.client.client.Events.Connected += Events_Connected;
                Program.client.client.Events.Disconnected += Events_Disconnected;
                Program.client.client.Events.DataReceived += Events_DataReceived1;
                Program.client.client.Connect();
                lblTitle.Text = $"Welcome to the server\n{ip} : {port}";
                btnConnect.Visible = false;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                this.Close();
            }

        }

        /// <summary>
        /// Data received server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        /*
        private void Events_DataReceived1(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string message = Encoding.UTF8.GetString(e.Data);
                if (message.StartsWith("FILE_"))
                {
                    // Extract the file name from the message
                    string fileName = message.Substring(5);

                    // Display the file as a download link
                    txtMessage.SelectionStart = txtMessage.TextLength;
                    txtMessage.SelectionLength = 0;
                    //txtMessage.SelectionColor = Color.Blue;
                    txtMessage.AppendText($"The server has sent you a file: ");
                    //txtMessage.SelectionColor = Color.Black;
                    int index = txtMessage.TextLength;
                    txtMessage.AppendText(fileName);
                    txtMessage.Select(index, fileName.Length);
                    //txtMessage.SelectionColor = Color.Blue;
                    txtMessage.AppendText($" ({DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")})");
                    txtMessage.AppendText(Environment.NewLine);
                    txtMessage.Select(txtMessage.TextLength, 0);
                    txtMessage.ScrollToCaret();
                }
                else
                {
                    // Display the message normally
                    txtMessage.Text += $"\n[{e.IpPort}] : {Encoding.UTF8.GetString(e.Data)} {Environment.NewLine}";
                }
            });
        }*/


        private void Events_DataReceived1(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string message = Encoding.UTF8.GetString(e.Data);
                if (message.StartsWith("FILE_"))
                {
                    // Extract the file name from the message
                    string[] parts = message.Substring(5).Split('_');
                    string fileName = parts[0];
                    int fileDataPort = int.Parse(parts[1]);

                    // Display the file as a download link
                    txtMessage.SelectionStart = txtMessage.TextLength;
                    txtMessage.SelectionLength = 0;
                    //txtMessage.SelectionColor = Color.Blue;
                    txtMessage.AppendText($"The server has sent you a file: ");
                    //txtMessage.SelectionColor = Color.Black;
                    int index = txtMessage.TextLength;
                    txtMessage.AppendText(fileName);
                    txtMessage.Select(index, fileName.Length);
                    //txtMessage.SelectionColor = Color.Blue;
                    txtMessage.AppendText($" ({DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")})");
                    txtMessage.AppendText(Environment.NewLine);
                    txtMessage.Select(txtMessage.TextLength, 0);
                    txtMessage.ScrollToCaret();

                    // Ask the user if they want to download the file
                    DialogResult result = MessageBox.Show($"Do you want to download the file {fileName}?", "Download File", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Start a new thread to receive the file data over a different port
                        new Thread(() =>
                        {
                            try
                            {
                                using (TcpClient client = new TcpClient(e.IpPort, fileDataPort))
                                using (NetworkStream stream = client.GetStream())
                                using (SaveFileDialog dialog = new SaveFileDialog())
                                {
                                    dialog.FileName = fileName;
                                    dialog.Filter = "All Files (*.*)|*.*";
                                    dialog.CheckFileExists = false;
                                    dialog.CheckPathExists = true;
                                    if (dialog.ShowDialog() == DialogResult.OK)
                                    {
                                        // Save the file to the selected path
                                        using (FileStream fileStream = File.Create(dialog.FileName))
                                        {
                                            byte[] buffer = new byte[1024];
                                            int bytesRead;
                                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                fileStream.Write(buffer, 0, bytesRead);
                                            }
                                        }

                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            txtMessage.SelectionStart = txtMessage.TextLength;
                                            txtMessage.SelectionLength = 0;
                                            //txtMessage.SelectionColor = Color.Blue;
                                            txtMessage.AppendText($"File {fileName} has been saved to {dialog.FileName} ({DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")})");
                                            txtMessage.AppendText(Environment.NewLine);
                                            txtMessage.Select(txtMessage.TextLength, 0);
                                            txtMessage.ScrollToCaret();
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }).Start();
                    }
                }
                else
                {
                    // Display the message normally
                    string messageForShow = Decrypt(Encoding.UTF8.GetString(e.Data), key);
                    txtMessage.Text += $"\n[{e.IpPort}] : {messageForShow} {Environment.NewLine}";
                }
            });


        }

        static string Decrypt(string cipherText, int[,] keyMatrix)
        {
            string[] parts = cipherText.Split('~');
            int messageLength = int.Parse(parts[1]);
            cipherText = parts[0];

            // string[] words = cipherText.Split(' ');
            // int[] wordLengths = new int[words.Length];
            // for (int i = 0; i < words.Length; i++)
            // {
            //     wordLengths[i] = words[i].Length;
            // }

            // cipherText = cipherText.Replace(" ", "");
            int n = keyMatrix.GetLength(0);
            int m = (int)Math.Ceiling((double)cipherText.Length / n);
            int[,] cipherMatrix = new int[n, m];
            for (int i = 0; i < cipherText.Length; i++)
            {
                int row = i % n;
                int col = i / n;
                cipherMatrix[row, col] = (cipherText[i] - ' ') % 94;
            }
            int[,] adj = new int[n, n];
            int det = determinantOfMatrix(keyMatrix, n);
            det = det % 94;
            if (det < 0)
                det += 94;
            int invDet = modInverse(det, 94);
            adjoint(keyMatrix, adj);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    adj[i, j] = (((adj[i, j] % 94) + 94) * invDet) % 94;
            int[,] plainMatrix = new int[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    for (int k = 0; k < n; k++)
                        plainMatrix[i, j] += adj[i, k] * cipherMatrix[k, j];
            string plainText = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                int row = i % n;
                int col = i / n;
                plainText += (char)((plainMatrix[row, col] % 94) + ' ');
            }
            // Split the output string into words based on the word lengths
            // string[] outputWords = new string[wordLengths.Length + 2];
            // int startIndex = 0;
            // for (int i = 0; i < wordLengths.Length; i++)
            // {

            //     if (i == wordLengths.Length - 1)
            //     {
            //         int wordLength = wordLengths[i];
            //         outputWords[i] = plainText.Substring(startIndex, wordLength);

            //     }
            //     else
            //     {
            //         int wordLength = wordLengths[i];
            //         outputWords[i] = plainText.Substring(startIndex, wordLength);
            //         startIndex += wordLength;
            //     }
            // }
            // string tempOutput = string.Join(" ", outputWords);
            string finalOutput = plainText.Substring(0, messageLength);
            return finalOutput;
        }
        static void adjoint(int[,] mat, int[,] adj)
        {
            int n = adj.GetLength(0);
            if (n == 1)
            {
                adj[0, 0] = 1;
                return;
            }
            int sign = 1;
            int[,] temp = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    getCofactor(mat, temp, i, j, n);
                    sign = ((i + j) % 2 == 0) ? 1 : -1;
                    adj[j, i] = sign * determinantOfMatrix(temp, n - 1);
                }
        }
        static void getCofactor(int[,] mat, int[,] temp, int p, int q, int n)
        {
            int i = 0, j = 0;
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (row != p && col != q)
                    {
                        temp[i, j++] = mat[row, col];
                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }

        static int determinantOfMatrix(int[,] mat, int n)
        {
            if (n == 1)
                return mat[0, 0];
            int D = 0;
            int[,] temp = new int[n, n];
            int sign = 1;
            for (int f = 0; f < n; f++)
            {
                getCofactor(mat, temp, 0, f, n);
                D += sign * mat[0, f] * determinantOfMatrix(temp, n - 1);
                sign = -sign;
            }
            return D;
        }

        static void adjjacent(int[,] mat, int[,] adj)
        {
            int n = adj.GetLength(0);
            if (n == 1)
            {
                adj[0, 0] = 1;
                return;
            }
            int sign = 1;
            int[,] temp = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    getCofactor(mat, temp, i, j, n);
                    sign = ((i + j) % 2 == 0) ? 1 : -1;
                    adj[j, i] = sign * determinantOfMatrix(temp, n - 1);
                }
            }
        }
        static int modInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return 1;
        }

        /// <summary>
        /// Client disconnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtMessage.Text += $"\nYou are offline {Environment.NewLine}";
            });
        }

        /// <summary>
        /// Client Connected to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtMessage.Text += $"\nYou are connected {Environment.NewLine}";
            });
        }

        /// <summary>
        /// Button send message to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSendMessage.Text))
            {
                string messageForShow = txtSendMessage.Text;
                txtSendMessage.Text = Encrypt(txtSendMessage.Text, key);
                try
                {
                    Program.client.client.Send(txtSendMessage.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                txtMessage.Text += $"\n[Me] {messageForShow} {Environment.NewLine}";
                txtSendMessage.Text = "";
            }
        }

        static string Encrypt(string message, int[,] key)
        {
            int messageLength = message.Length;
            string[] words = message.Split(' ');
            int[] wordLengths = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                wordLengths[i] = words[i].Length;
            }

            // remove spaces
            // message = message.Replace(" ", "");
            String messageTemp = message;

            // Ensure that the message length is a multiple of the key size
            int padding = message.Length % key.GetLength(0);
            int paddRight = 0;
            if (padding != 0)
            {
                paddRight = message.Length + (key.GetLength(0) - padding);
                message = message.PadRight(paddRight, 'X');
                paddRight = paddRight - messageTemp.Length;

            }
            // Define variables for the plaintext and ciphertext matrices
            int[,] plaintext = new int[key.GetLength(0), message.Length / key.GetLength(0)];
            int[,] ciphertext = new int[key.GetLength(0), message.Length / key.GetLength(0)];

            // Convert the message to a matrix of numerical values based on the alphabet
            int messageIndex = 0;
            for (int i = 0; i < message.Length; i++)
            {
                plaintext[messageIndex % key.GetLength(0), messageIndex / key.GetLength(0)] = message[i] - ' ';
                messageIndex++;
                // if (message[i] != ' ') {
                // }
            }

            // Multiply the plaintext matrix by the key matrix to get the ciphertext matrix
            for (int i = 0; i < key.GetLength(0); i++)
            {
                for (int j = 0; j < messageIndex / key.GetLength(0); j++)
                {
                    for (int k = 0; k < key.GetLength(0); k++)
                    {
                        ciphertext[i, j] += key[i, k] * plaintext[k, j];
                    }
                    ciphertext[i, j] %= 94;
                }
            }

            // Convert the ciphertext matrix back to a string of characters based on the alphabet
            string output = "";
            for (int i = 0; i < messageIndex / key.GetLength(0); i++)
            {
                for (int j = 0; j < key.GetLength(0); j++)
                {
                    output += (char)(ciphertext[j, i] + ' ');
                }
            }
            // Split the output string into words based on the word lengths
            // string[] outputWords = new string[wordLengths.Length];
            // int startIndex = 0;
            // for (int i = 0; i < wordLengths.Length; i++) {

            //     if(i==wordLengths.Length-1)
            //     {
            //         int wordLength = wordLengths[i];
            //         outputWords[i] = output.Substring(startIndex, wordLength+paddRight);

            //     }else{
            //         int wordLength = wordLengths[i];
            //         outputWords[i] = output.Substring(startIndex, wordLength);
            //         startIndex += wordLength;
            //     }
            // }
            // Join the words back together with spaces and output the result
            // string finalOutput = string.Join(" ", outputWords);
            output = output + "~" + messageLength.ToString();
            // Console.WriteLine(finalOutput);

            // Display the encrypted message
            // Console.WriteLine(output);
            return output;
        }
        /// <summary>
        /// Disconnect client to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClientDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Program.client.client.Disconnect();
                this.Close();
            }
            catch
            {
                this.Close();
            }
        }

        /// <summary>
        /// Connect client to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            ActionUser(ip, port);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.client != null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "All Files (*.*)|*.*";
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = Path.GetFileName(dialog.FileName);
                    string filePath = dialog.FileName;

                    try
                    {
                        byte[] fileData = File.ReadAllBytes(filePath);

                        // Send the file name to the server first
                        Program.client.client.Send($"FILE_{fileName}");

                        // Send the file data to the server
                        Program.client.client.Send(fileData);

                        txtMessage.Text += $"[Me] Sent file {fileName} to server at {DateTime.Now}{Environment.NewLine}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Client is not connected to server.");
            }
        }


    }
}
