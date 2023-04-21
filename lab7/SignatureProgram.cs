// 3. Podpisywanie danych z pliku. Zakładamy, że mamy dwa pliki w których znajduje się klucz prywatny i publiczny algorytmu RSA. Te pliki zostały utworzone np. programem z punktu 1. Program pobiera nazwę dwóch plików (a) i (b). Program wczytuje plik (a). Jeśli plik (b) istnieje, program ma potraktować go jako podpis wygenerowany z pliku (a) przy pomocy klucza prywatnego. Program ma zweryfikować, czy podpis jest poprawny i wypisać wynik weryfikacji na ekran. Jeśli plik (b) nie istnieje, program ma wygenerować podpis danych z pliku (a) używając klucza prywatnego i zapisać ten podpis do pliku (b).
namespace lab7;
using System.Security.Cryptography;
using System.Text;

public class SignatureProgram
{
    string data;
    string signatureFilePath;
    string privateKeyFilePath = "./Files/privateKey.dat";
    string publicKeyFilePath = "./Files/publicKey.dat";

    public SignatureProgram(string dataFilePath, string signatureFilePath)
    {
        this.data = File.ReadAllText(dataFilePath);
        this.signatureFilePath = signatureFilePath;
    }

    public void GenerateSignature()
    {
        SHA256 algorithm = SHA256.Create();

        byte[] dataBytes = Encoding.ASCII.GetBytes(data);
        byte[] hash = algorithm.ComputeHash(dataBytes);

        RSAParameters rsaParameters;

        using (RSA rsa = RSA.Create())
        {
            rsa.FromXmlString(File.ReadAllText(privateKeyFilePath));   
            rsaParameters = rsa.ExportParameters(false);

            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm(nameof(SHA256));

            File.WriteAllBytes(
                signatureFilePath,
                rsaFormatter.CreateSignature(hash)
            );
        }
    }

    public bool CheckSignature()
    {
        RSA rsa = RSA.Create();
        rsa.FromXmlString(File.ReadAllText(publicKeyFilePath));   

        RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
        rsaDeformatter.SetHashAlgorithm(nameof(SHA256));

        SHA256 algorithm = SHA256.Create();
        byte[] dataBytes = Encoding.ASCII.GetBytes(data);
        byte[] hash = algorithm.ComputeHash(dataBytes);
        byte[] signedHash = File.ReadAllBytes(signatureFilePath);

        return rsaDeformatter.VerifySignature(hash, signedHash);
    }
}