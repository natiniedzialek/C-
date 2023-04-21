// 1. Program szyfrujący przy pomocy kryptografii klucza asymetrycznego. Napisz program który jako parametr przyjmuje typ polecenia. W zależności od wybranego typu polecenia:
//     - Jeżeli typ polecenia = 0 program ma wygenerować i zapisać do dwóch plików (o dowolnych nazwach, można je wpisać "na sztywno") klucz publiczny oraz klucz prywatny algorytmu RSA.
//     - Jeżeli typ polecenia = 1, program dodatkowo pobiera nazwę dwóch plików (a), (b). Podany plik (a) ma zostać zaszyfrowany przy pomocy klucza publicznego odczytanego z pliku, który został stworzony przy pomocy tego programu kiedy typ polecenia = 0. Zaszyfrowane dane mają być zapisane w pliku (b).
//     - Jeżeli typ polecenia = 2, program dodatkowo pobiera nazwę dwóch plików (a), (b). Podany plik (a) ma zostać odszyfrowany przy pomocy klucza prywatnego odczytanego z pliku, który został stworzony przy pomocy tego programu kiedy typ polecenia = 0. Odszyfrowane dane mają być zapisane w pliku (b).
namespace lab7;
using System;
using System.Security.Cryptography;  
using System.Text;

public class RSAProgram 
{
    string privateKeyFilePath = "./Files/privateKey.dat";
    string publicKeyFilePath = "./Files/publicKey.dat";
    public RSAProgram() {}

    // generate and write to file a pair of public and private key
    public void GenerateKeys()
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string ?publicKey;
        string ?privateKey;

        publicKey = rsa.ToXmlString(false);
        privateKey = rsa.ToXmlString(true);

        File.WriteAllText(publicKeyFilePath, publicKey);
        File.WriteAllText(privateKeyFilePath, privateKey);
    }

    // encrypt data from file using RSA public key and write it to file
    public void EncryptData()
    {
        Console.WriteLine("Enter file to encrypt:");
        string ?fileToEncrypt = Console.ReadLine();
        Console.WriteLine("Enter filename to save the encrypted data");
        string ?encryptedFile = Console.ReadLine();

        if(!(File.Exists(fileToEncrypt) && File.Exists(publicKeyFilePath)))
        {
            throw new FileNotFoundException("File not found!");
        }

        UnicodeEncoding byteConverter = new UnicodeEncoding();  
        byte[] dataToEncrypt = byteConverter.GetBytes(
            File.ReadAllText(fileToEncrypt)
        );
        
        byte[] encryptedData;   
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
        { 
            rsa.FromXmlString(File.ReadAllText(publicKeyFilePath));  
            encryptedData = rsa.Encrypt(dataToEncrypt, false);   
        }  
  
        File.WriteAllBytes(encryptedFile, encryptedData);  

        Console.WriteLine("Data has been successfully encrypted");   
    }

    // decrypt encrypted data
    public void DecryptData()
    {
        Console.WriteLine("Enter file to decrypt:");
        string ?fileToDecrypt = Console.ReadLine();
        Console.WriteLine("Enter file to save the decrypted data");
        string ?decryptedFile = Console.ReadLine();

        byte[] dataToDecrypt = File.ReadAllBytes(fileToDecrypt);  

        byte[] decryptedData;  
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())  
        {   
            rsa.FromXmlString(File.ReadAllText(privateKeyFilePath));  
            decryptedData = rsa.Decrypt(dataToDecrypt, false);   
        }  
 
        UnicodeEncoding byteConverter = new UnicodeEncoding();
        string decryptedString = byteConverter.GetString(decryptedData);

        File.WriteAllText(decryptedFile, decryptedString);

        Console.WriteLine(String.Format("Decrypted data: {0}", decryptedString));
    }

}