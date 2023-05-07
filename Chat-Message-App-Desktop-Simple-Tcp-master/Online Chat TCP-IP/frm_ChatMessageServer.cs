using Online_Chat_TCP_IP.Services;
using SimpleTcp;
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
using System.Threading;
using System.Net.Sockets;



namespace Online_Chat_TCP_IP
{
    public partial class frm_ChatMessageServer : Form
    {
        #region Variable
        List<string> peopleConnected;
        #endregion
        private const int fileDataPort = 12345;
        int[,] key = { { 2, 0, 1, 0 }, { 0, 1, 0, 0 }, { 1, 0, 3, 2 }, { 0, 0, 2, 1 } };

        public frm_ChatMessageServer(string PORTServer)
        {
            InitializeComponent();
            peopleConnected = new List<string>();
            messageConnexion(PORTServer);
        }

        /// <param name="port"></param>
        private void messageConnexion(string port)
        {
            ActionUser(port);
        }

        /// <summary>
        /// Create Server and start
        /// </summary>
        /// <param name="port">string port</param>
        private void ActionUser(string port)
        {
            try
            {
                Program.server = new ClassServer(port);
                Program.server.CreateServer();
                Program.server.server.Events.DataReceived += Events_DataReceived;
                Program.server.server.Events.ClientDisconnected += Events_ClientDisconnected;
                Program.server.server.Events.ClientConnected += Events_ClientConnected;
                Program.server.server.Start(); 

                lblTitle.Text = $"Welcome to the server {Program.classIP.RecoverLocalIp()}:{port}";
                txtMessage.Text += $"You have joined the server {DateTime.Now} {Environment.NewLine}";
            }
            catch(Exception e)
            {
                lblTitle.Text = $"Server not create";
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Client Connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtMessage.Text += $"[{e.IpPort}] connected {Environment.NewLine}";
                peopleConnected.Add(e.IpPort);
            });
        }

        /// <summary>
        /// Client Deconnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtMessage.Text += $"[{e.IpPort}] disconnected {Environment.NewLine}";
                peopleConnected.Remove(e.IpPort);
            });
        }

        /// <summary>
        /// Data received client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
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
                    txtMessage.AppendText($"[{e.IpPort}] sent a file: ");
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
                    // Display the message normally
                    string messageForShow = Decrypt(message, key);
                    
                    txtMessage.Text += $"[{e.IpPort}] : {messageForShow} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} {Environment.NewLine}";
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
        /// Button send message to all client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSendMessage.Text))
            {
                string messageForShow = txtSendMessage.Text;
                txtSendMessage.Text = Encrypt(txtSendMessage.Text, key);
                
                foreach(var ip in peopleConnected)
                {
                    try
                    {
                        Program.server.server.Send(ip, txtSendMessage.Text);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                    
                txtMessage.Text += $"[Server] : {messageForShow} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} {Environment.NewLine}";
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
        /// Close server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopServer_Click(object sender, EventArgs e)
        {
            try
            {
                Program.server.server.Stop();
                this.Close();
            }
            catch
            {
                this.Close();
            }
        }

        /// <summary>
        /// Display people Connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeopleConnected_Click(object sender, EventArgs e)
        {
            string peopleCo = "";
            foreach (string people in peopleConnected)
                peopleCo += $"¤ {people}\n";
            MessageBox.Show(peopleCo, "List of people Connected !!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All Files (*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileName(dialog.FileName);
                string filePath = dialog.FileName;

                foreach (var ip in peopleConnected)
                {
                    try
                    {
                        byte[] fileData = File.ReadAllBytes(filePath);

                        // Send the file name to the client first
                        Program.server.server.Send(ip, $"FILE_{fileName}_{fileDataPort}");

                        // Start a new thread to send the file data over a different port
                        new Thread(() =>
                        {
                            try
                            {
                                using (TcpClient client = new TcpClient(ip, fileDataPort))
                                using (NetworkStream stream = client.GetStream())
                                {
                                    stream.Write(fileData, 0, fileData.Length);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }).Start();

                        txtMessage.Text += $"[Server] Sent file {fileName} to {ip} at {DateTime.Now}{Environment.NewLine}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

    }
}
