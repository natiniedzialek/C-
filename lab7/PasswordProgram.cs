// 4. Zaszyfrowanie pliku algorytmem klucza symetrycznego przy użyciu hasła. Program ma przyjmować cztery parametry: pliki (a), (b), hasło, typ operacji.
// Program ma zaszyfrować plik (a) algorytmem AES, którego klucz ma zostać wygenerowany przy pomocy podanego hasła. Zaszyfrowane dane mają być zapisane do pliku (b).
// Program ma odszyfrować plik (a) algorytmem AES, którego klucz ma zostać wygenerowany przy pomocy podanego hasła. Odszyfrowane dane mają być zapisane do pliku (b). Wszystkie dane wymagane do utworzenia klucza algorytmu AES z wyjątkiem hasła mogą byc wpisane "na sztywno" do programu.
namespace lab7;
using System.Text;
using System.Security.Cryptography;

public class PasswordProgram 
{
    string password;
    byte[] salt = RandomNumberGenerator.GetBytes(8);
    byte[] initVector = RandomNumberGenerator.GetBytes(16);
    int iterations = 2000;
    byte[] ?data;

    string dataToEncryptFilePath = "files/data.txt";
    string encryptedDataFilePath = "files/dataEncryptedWithPassword.dat";
    string decryptedDataFilePath = "files/dataDecryptedWithPassword.txt";

    public PasswordProgram(string password) => this.password = password;

    public void EncryptWithPassword()
    {
        data = new UTF8Encoding(false).GetBytes(
                File.ReadAllText(dataToEncryptFilePath)
            );

        Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        Aes encAlg = Aes.Create();
        encAlg.IV = initVector;
        encAlg.Key = k1.GetBytes(16);

        MemoryStream encryptionStream = new MemoryStream();
        CryptoStream encrypt = new CryptoStream(
            encryptionStream,
            encAlg.CreateEncryptor(),
            CryptoStreamMode.Write
        );

        encrypt.Write(data, 0, data.Length);
        encrypt.FlushFinalBlock();
        encrypt.Close();

        byte[] encryptedData = encryptionStream.ToArray();
        k1.Reset();
        
        File.WriteAllBytes(encryptedDataFilePath, encryptedData);
    }

    public void DecryptWithPassword()
    {
        data = File.ReadAllBytes(encryptedDataFilePath);

        Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        Aes decAlg = Aes.Create();
        decAlg.Key = k1.GetBytes(16);
        decAlg.IV = initVector;

        MemoryStream decryptionStreamBacking = new MemoryStream();
        CryptoStream decrypt = new CryptoStream(
            decryptionStreamBacking,
            decAlg.CreateDecryptor(),
            CryptoStreamMode.Write,
            true
        );
        
        decrypt.Write(data, 0, data.Length);
        //decrypt.FlushFinalBlock();
        decrypt.Close();

        byte[] result = decryptionStreamBacking.ToArray();
        k1.Reset();

        string resultString = new UTF8Encoding(false).GetString(result);
        
        File.WriteAllText(decryptedDataFilePath, resultString);
    }
}