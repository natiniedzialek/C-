// 2. Program liczący sumę kontrolną. Napisz program który jako parametry przyjmuje nazwę pliku (a), nazwę pliku zawierającego hash (b) oraz algorytm hashowania (SHA256, SHA512 lub MD5) (c). Jeżeli plik hash (b) nie istnieje, program ma policzyć hash z pliku (a) i zapisać go pod nazwą (b). Jeżeli plik (b) istnieje, program ma zweryfikować hash i wypisać do konsoli, czy hash jest zgodny.
namespace lab7;
using System.Security.Cryptography;
using System.Text;

public class HashProgram
{
    string data;
    string ?hashFilePath;
    HashAlgorithm hashAlgorithm;

    public HashProgram(string dataFilePath, string hashFilePath, string algorithmFilePath) 
    {
        this.data = File.ReadAllText(dataFilePath);

        string algorithm = File.ReadAllText(algorithmFilePath);
        switch(algorithm)
        {
            case "SHA256":
                hashAlgorithm = SHA256.Create();
                break;
            case "SHA512":
                hashAlgorithm = SHA512.Create();
                break;
            default:
                hashAlgorithm = MD5.Create();
                break;
        }

        this.hashFilePath = hashFilePath;
    }

    public string GenerateHash() 
    {
        Encoding enc = Encoding.UTF8;
        StringBuilder hashBuilder = new();

        byte[] result = hashAlgorithm.ComputeHash(enc.GetBytes(data));

        foreach (byte b in result)
            hashBuilder.Append(b.ToString("x2"));
        
        return hashBuilder.ToString();
    }

    public void ToFile() 
    {
        string hashString = GenerateHash();
        File.WriteAllText(hashFilePath, hashString);
    }

    public bool CheckHash() 
    {
        string hashString = File.ReadAllText(hashFilePath);
        if(hashString == GenerateHash()) return true;
        return false;
    }
}