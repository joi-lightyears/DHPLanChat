using System;

class HillCipher {
    static void Main() {
        // Define the plaintext message to be encrypted
        string message = "Nguyen Thanh Dat sinh ngay 05/10/2002.";

        // Define the encryption key matrix (must be a square matrix)
        int[,] key = { { 2, 0, 1, 0 }, { 0, 1, 0, 0 }, { 1, 0, 3, 2 }, { 0, 0, 2, 1 } };
        
        string encrypted = encrypt(message, key);
        string decrypted = decrypt(encrypted, key);
        
        Console.WriteLine("Message:" + message);
        Console.WriteLine("Encrypted Message: " + encrypted);
        Console.WriteLine("Decrypted Message: " + decrypted);
    }
    
    static string encrypt(string message, int[,] key)
    {
        int messageLength = message.Length;
        string[] words = message.Split(' ');
        int[] wordLengths = new int[words.Length];
        for (int i = 0; i < words.Length; i++) {
            wordLengths[i] = words[i].Length;
        }
        
        // remove spaces
        // message = message.Replace(" ", "");
        String messageTemp = message;
        
        // Ensure that the message length is a multiple of the key size
        int padding = message.Length % key.GetLength(0);
        int paddRight = 0;
        if (padding != 0) {
            paddRight = message.Length + (key.GetLength(0) - padding);
            message = message.PadRight(paddRight, 'X');
            paddRight = paddRight - messageTemp.Length;
        
        }
        // Define variables for the plaintext and ciphertext matrices
        int[,] plaintext = new int[key.GetLength(0), message.Length / key.GetLength(0)];
        int[,] ciphertext = new int[key.GetLength(0), message.Length / key.GetLength(0)];

        // Convert the message to a matrix of numerical values based on the alphabet
        int messageIndex = 0;
        for (int i = 0; i < message.Length; i++) {
            plaintext[messageIndex % key.GetLength(0), messageIndex / key.GetLength(0)] = message[i] - ' ';
            messageIndex++;
            // if (message[i] != ' ') {
            // }
        }

        // Multiply the plaintext matrix by the key matrix to get the ciphertext matrix
        for (int i = 0; i < key.GetLength(0); i++) {
            for (int j = 0; j < messageIndex / key.GetLength(0); j++) {
                for (int k = 0; k < key.GetLength(0); k++) {
                    ciphertext[i, j] += key[i, k] * plaintext[k, j];
                }
                ciphertext[i, j] %= 94;
            }
        }

        // Convert the ciphertext matrix back to a string of characters based on the alphabet
        string output = "";
        for (int i = 0; i < messageIndex / key.GetLength(0); i++) {
            for (int j = 0; j < key.GetLength(0); j++) {
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
    static string decrypt(string cipherText, int[,] keyMatrix)
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
            adj[0,0] = 1;
            return;
        }
        int sign = 1;
        int[,] temp = new int[n,n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                getCofactor(mat,temp,i,j,n);
                sign = ((i + j) % 2 == 0) ? 1 : -1;
                adj[j,i] = sign * determinantOfMatrix(temp,n - 1);
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
                    temp[i,j++] = mat[row,col];
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
            return mat[0,0];
        int D = 0;
        int[,] temp = new int[n,n];
        int sign = 1;
        for (int f = 0; f < n; f++)
        {
            getCofactor(mat,temp,0,f,n);
            D += sign * mat[0,f] * determinantOfMatrix(temp,n - 1);
            sign = -sign;
        }
        return D;
    }

    static void adjjacent(int[,] mat, int[,] adj)
    {
        int n = adj.GetLength(0);
        if (n == 1)
        {
            adj[0,0] = 1;
            return;
        }
        int sign = 1;
        int[,] temp = new int[n,n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                getCofactor(mat,temp,i,j,n);
                sign = ((i+j) % 2 == 0) ? 1 : -1;
                adj[j,i] = sign * determinantOfMatrix(temp,n - 1);
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
}