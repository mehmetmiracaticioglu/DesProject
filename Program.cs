using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

class DESSample
{

    static void Main()
    {
        string key;
        do
        {
            Console.WriteLine("Lütfen 8 karakterli bir seri giriniz");
             key = Console.ReadLine();

        } while (key.Length != 8);
              
        byte[] bytes = Encoding.ASCII.GetBytes(key);
        DES DesKriptoAlg = DES.Create();
        DesKriptoAlg.Key = bytes;
      

        
        Console.WriteLine("Şifrelenmesini istediğiniz dosya adını giriniz");
        string input = Console.ReadLine();
        string[] words = input.Split('.');
       
        byte[] encryptedArray = File.ReadAllBytes(input);
        string base64ImageRepresentation = Convert.ToBase64String(encryptedArray);
        // Create a string to encrypt. //
        string encryptDosyaAdi = "encrypted-"+words[0]+"."+ words[words.Length - 1];
    
        EncryptTextToFile(base64ImageRepresentation, encryptDosyaAdi, DesKriptoAlg.Key, DesKriptoAlg.IV);

        string Final = DecryptTextFromFile(encryptDosyaAdi, DesKriptoAlg.Key, DesKriptoAlg.IV);

        encryptedArray = Convert.FromBase64String(Final);
        File.WriteAllBytesAsync("Decrypted."+words[words.Length-1], encryptedArray);
    }

    public static void EncryptTextToFile(String Veri, String DosyaAdi, byte[] Key, byte[] IV)
    {
                  
            FileStream fStream = File.Open(DosyaAdi, FileMode.OpenOrCreate);
     
            DES DesKriptoAlg = DES.Create();
        
            CryptoStream kriptoStream = new CryptoStream(fStream,DesKriptoAlg.CreateEncryptor(Key, IV),CryptoStreamMode.Write);
       
            StreamWriter streamYazici = new StreamWriter(kriptoStream);
        
            streamYazici.WriteLine(Veri);
            streamYazici.Close();
            kriptoStream.Close();
            fStream.Close();
        
    }
    public static string DecryptTextFromFile(String DosyaAdi, byte[] Key, byte[] IV)
    {         
            FileStream fStream = File.Open(DosyaAdi, FileMode.OpenOrCreate);
         
            DES DesKriptoAlg = DES.Create();
         
            CryptoStream kriptoStream = new CryptoStream(fStream,DesKriptoAlg.CreateDecryptor(Key, IV),CryptoStreamMode.Read);
       
            StreamReader StreamOkuyucu = new StreamReader(kriptoStream);
      
            string Sonuc = StreamOkuyucu.ReadLine();
       
            StreamOkuyucu.Close();
            kriptoStream.Close();
            fStream.Close();
       
            return Sonuc;
       
    }
}