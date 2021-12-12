using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

class DESSample
{

    static void Main()
    {
        Console.WriteLine("Lütfen 8 karakterli bir seri giriniz");
        string key = Console.ReadLine();


        byte[] bytes = Encoding.ASCII.GetBytes(key);
        Console.WriteLine(bytes.Length);
        
        byte[]key1 = Convert.FromBase64String(key);
        
        byte[] arr = {61,62,49,50,51,52,53,54};
      
        DES DESalg = DES.Create();
        Console.WriteLine(Convert.ToBase64String(DESalg.Key));
        DESalg.Key = bytes;
         // byte [] arr = Convert.FromBase64String(key);
        Console.WriteLine(DESalg.Key.Length);
        byte[] imageArray = System.IO.File.ReadAllBytes("serdar.png");
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
        // Create a string to encrypt. //4 
            string encryptFileName = "serdar.txt";
        //Console.WriteLine(base64ImageRepresentation);
        Console.WriteLine(Encoding.ASCII.GetString(DESalg.Key));
        

        // Encrypt text to a file using the file name, key, and IV.
        EncryptTextToFile(base64ImageRepresentation, encryptFileName, DESalg.Key, DESalg.IV);

            // Decrypt the text from a file using the file name, key, and IV.
            string Final = DecryptTextFromFile(encryptFileName, DESalg.Key, DESalg.IV);

        // Display the decrypted string to the console.
            imageArray = Convert.FromBase64String(Final);
            File.WriteAllBytesAsync("serdar2r.png", imageArray);


    }

    public static void EncryptTextToFile(String Data, String FileName, byte[] Key, byte[] IV)
    {
        
            // Create or open the specified file.
            FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

            // Create a new DES object.
            DES DESalg = DES.Create();

            // Create a CryptoStream using the FileStream
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(fStream,
                DESalg.CreateEncryptor(Key, IV),
                CryptoStreamMode.Write);

            // Create a StreamWriter using the CryptoStream.
            StreamWriter sWriter = new StreamWriter(cStream);

            // Write the data to the stream
            // to encrypt it.
            sWriter.WriteLine(Data);

            // Close the streams and
            // close the file.
            sWriter.Close();
            cStream.Close();
            fStream.Close();
        
    }

    public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
    {
          // Create or open the specified file.
            FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

            // Create a new DES object.
            DES DESalg = DES.Create();

            // Create a CryptoStream using the FileStream
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(fStream,
                DESalg.CreateDecryptor(Key, IV),
                CryptoStreamMode.Read);

            // Create a StreamReader using the CryptoStream.
            StreamReader sReader = new StreamReader(cStream);

            // Read the data from the stream
            // to decrypt it.
            string val = sReader.ReadLine();

            // Close the streams and
            // close the file.
            sReader.Close();
            cStream.Close();
            fStream.Close();

            // Return the string.
            return val;
       
    }
}