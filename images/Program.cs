namespace lab9;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public static class Program 
{
    public static void Main()
    {
        // Task1("noiseimg.jpg");
        // Task2And3("noiseimg.jpg", 3, 3, "median");
        // Task2And3("noiseimg.jpg", 3, 3, "average");
        // Task2And3("noiseimg.jpg", 3, 3, "maximum");
        // Task2And3("noiseimg.jpg", 3, 3, "minimum");
        // List<double> kernelRow1 = new(){-1,-1,-1};
        // List<double> kernelRow2 = new(){-1,8,-1};
        // List<List<double>> kernel = new() {kernelRow1, kernelRow2, kernelRow1};
        // Task4("example.png", kernel);
    }

    // Napisz metodę, która przekształci obraz wejściowy na obraz w odcieniach szarości. Aby dokonać konwersji obrazu na obraz w odcieniu szarości wykorzystaj "Average Method" link.
    public static void Task1(string imgFileName)
    {
        using (Image<Rgb24> image = Image.Load<Rgb24>(imgFileName)) 
        {
            Image<Rgb24> imgClone = image.Clone();

            for (int a = 0; a < image.Width; a++)
            {
                for (int b = 0; b < image.Height; b++)
                {
                    byte R = image[a,b].R;
                    byte G = image[a,b].G;
                    byte B = image[a,b].B;

                    byte avg = (byte)(R / 3 + G / 3 + B / 3);
                    imgClone[a,b] = new Rgb24(avg,avg,avg);
                }
            }
            image.Save("task1original.jpg");
            imgClone.Save("task1clone.jpg");           
        }
    }

    // Zaimplementuj filtr medianowy, parametry metody to obraz oraz rozmiar okna filtra medianowego (szerokość, wysokość). Szerokość i wysokość muszą być wartościami nieparzystymi. Zasada działania filtra medianowego można znaleźć np. na stronie 6: [link](https://www.cs.auckland.ac.nz/courss/compsci373s1c/PatricesLectures/Image%20Filtering.pdf)
    // Zaimplementuj filtr maksymalny, minimalny, uśredniający. Parametry metody to obraz oraz rozmiar okna filtra (szerokość, wysokość). Szerokość i wysokość muszą być wartościami nieparzystymi. Filtry te działą na tych samych zasadach jak filtr medianowy (również używają okna), ale zamiast brać pod uwagę medianę używają odpowiednio wartości maksymlanej, minimalnej lub średniej w oknie.
    public static void Task2And3(string imgFileName, int filterHeight, int filterWidth, string method)
    {
        if(filterHeight % 2 == 0 || filterWidth % 2 == 0)
        {
            throw new Exception("Filter width and height must be odd");
        }

        using (Image<Rgb24> image = Image.Load<Rgb24>(imgFileName)) 
        {
            Image<Rgb24> imgClone = image.Clone();
            // loop through all the pixels
            for(int y = 0; y < image.Height; y++) 
            {
                for(int x = 0; x < image.Width; x++)
                {
                    List<Rgb24> pixels = GetFilterPixels(x, y, imgClone, filterHeight, filterWidth);
                    var RGB = CalculateRGB(pixels, method);

                    imgClone[x, y] = new Rgb24(RGB.Item1, RGB.Item2, RGB.Item3);
                }
            }

            image.Save("task3original.jpg");
            imgClone.Save(String.Format("task3clone{0}.jpg", method));  
        }
    }

    public static List<Rgb24> GetFilterPixels(int x, int y, Image<Rgb24> image, int filterHeight, int filterWidth)
    {
        List<Rgb24> pixels = new();
        int widthRadius = filterWidth / 2;
        int heightRadius = filterHeight / 2;

        for(int i = -heightRadius; i <= heightRadius; i++)
        {
            for(int j = -widthRadius; j <= widthRadius; j++)
            {
                int pixelY = y + i;
                int pixelX = x + j;

                // if the filter exceeds the boundaries of the image some pixels are skipped
                if (!(pixelY < 0 || pixelX < 0 || pixelX >= image.Width || pixelY >= image.Height))
                {
                    pixels.Add(image[pixelX, pixelY]);
                }
            }
        }

        return pixels;
    }

    public static (byte, byte, byte) CalculateRGB(List<Rgb24> pixels, string method)
    {
        List<int> R = pixels.Select(x => (int)x.R).Order().ToList();
        List<int> G = pixels.Select(x => (int)x.G).Order().ToList();
        List<int> B = pixels.Select(x => (int)x.B).Order().ToList();
        
        int RValue = 0;
        int GValue = 0;
        int BValue = 0;

        switch(method)
        {
            case "median":
                RValue = R[(R.Count / 2)];
                GValue = G[(G.Count / 2)];
                BValue = B[(B.Count / 2)];
                break;
            case "average":
                RValue = R.Sum() / R.Count;
                GValue = G.Sum() / G.Count;
                BValue = B.Sum() / B.Count;
                break;
            case "minimum":
                RValue = R[0];
                GValue = G[0];
                BValue = B[0];
                break;
            case "maximum":
                RValue = R[R.Count - 1];
                GValue = G[G.Count - 1];
                BValue = B[B.Count - 1];
                break;
        }

        return ((byte)RValue, (byte)GValue, (byte)BValue);
    }

    //Zaimplementuj filtr konwolucyjny, parametry metody to obraz, dwuwymiarowa tablica jądra filtru (kernel). Szerokość i wysokość kernela muszą być wartościami nieparzystymi. Działanie filtra wytłumaczone jest na stronie 36 [link](https://www.cs.auckland.ac.nz/courss/compsci373s1c/PatricesLectures/Image%20Filtering.pdf). Przetestuj działanie filtra używając kerneli z: [link](https://en.wikipedia.org/wiki/Kernel_(image_processing))
    public static void Task4(string imgFileName, List<List<double>> kernel)
    {
        if(kernel.Count % 2 == 0 || kernel[0].Count % 2 == 0)
        {
            throw new Exception("Filter width and height must be odd");
        }

        using (Image<Rgb24> image = Image.Load<Rgb24>(imgFileName)) 
        {
            Image<Rgb24> imgClone = image.Clone();
            // iterate through all the pixels
            for(int y = 0; y < image.Height; y++) 
            {
                for(int x = 0; x < image.Width; x++)
                {
                    var RGB = CalculateRGBWithKernel(x, y, image, kernel);

                    imgClone[x, y] = new Rgb24(RGB.Item1, RGB.Item2, RGB.Item3);
                }
            }

            image.Save("task4original.jpg");
            imgClone.Save(String.Format("task4clone.jpg"));  
        }
    }

    public static (byte, byte, byte) CalculateRGBWithKernel(int x, int y, Image<Rgb24> image, List<List<double>> kernel)
    {
        int widthRadius = kernel[0].Count / 2;
        int heightRadius = kernel.Count / 2;

        double RSum = 0;
        double GSum = 0;
        double BSum = 0;

        for(int i = 0; i < kernel.Count; i++)
        {
            for(int j = 0; j < kernel[0].Count; j++)
            {
                int pixelY = y + i - heightRadius;
                int pixelX = x + j - widthRadius;

                // if the filter exceeds the boundaries of the image some pixels are skipped
                if (!(pixelY < 0 || pixelX < 0 || pixelX >= image.Width || pixelY >= image.Height))
                {
                    RSum += kernel[j][i] * image[pixelX, pixelY].R;
                    BSum += kernel[j][i] * image[pixelX, pixelY].B;
                    GSum += kernel[j][i] * image[pixelX, pixelY].G;
                }
            }
        }

        return ((byte)Math.Round(RSum), (byte)Math.Round(GSum), (byte)Math.Round(BSum));        
    }
}
