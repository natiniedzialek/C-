// working with collections

namespace lab3;
using System;

public class Program
{
    private static TweetList ?list;

    public static void Main(string[] args)
    {
        Task1();
        Task2();
        Task3And4();
        Task5();
        Task6();
        Task7();
        Task8();
    }

    // 1. Wczytaj dane z pliku favorite-tweets.json Po wczytaniu poszczególne tweety powinny znajdować się w osobnych obiektach a obiekty na liście. Możesz dowolnie modyfikować strukturę pliku, ale nie modyfikuj danych poszczególnych tweetów.
    public static void Task1() 
    {
        list = new TweetList("data/favorite-tweets.json");
        //Console.WriteLine(list);
    }

    // 2. Napisz funkcję, który pozwoli na przekonwertowanie wczytanych w punkcie 1 danych do formatu XML. Funkcja ma pozwalać zarówno na zapis do pliku w formacie XML danych o tweetach jak i wczytanie tych danych z pliku.
    public static void Task2()
    {
        list.ToXML("tweets.xml");
    }

    // 3. Napisz funkcję sortującą tweety po nazwie użytkowników oraz funkcję sortującą użytkowników po dacie utworzenie tweetu.
    // 4. Wypisz najnowszy i najstarszy tweet znaleziony względem daty jego utworzenia.
    public static void Task3And4() 
    {
        list.SortByUserName();
        //Console.WriteLine(list);
        list.SortByDate();
        //Console.WriteLine(String.Format("oldest: {0} \nlatest: {1}", list.Data.First(), list.Data.Last()));
    }

    // 5. Stwórz słownik, który będzie indeksowany po username i będzie przechowywał jako listę tweety użytkownika o danym username.
    public static void Task5()
    {
        Dictionary<string, List<Tweet>> dict = list.UsersTweets();

        foreach(string s in dict.Keys) 
        {
            foreach(Tweet t in dict[s])
                Console.WriteLine(t);
            Console.WriteLine();
        }
    }

    // 6. Oblicz częstość występowania słów, które występują w treści tweetów (w polu TEXT).
    public static void Task6()
    {
        Dictionary<string, int> d = list.WordsCount();
        
        foreach(var keyValuePair in d) 
        {
            Console.WriteLine(String.Format("Word: {0} Count: {1}", keyValuePair.Key, keyValuePair.Value));
        }
    }

    // 7. Znajdź i wypisz 10 najczęściej występujących w tweetach wyrazów o długości co najmniej 5 liter.
    public static void Task7()
    {
        foreach(var word in list.PopularWords(10)) 
        {
            Console.WriteLine(String.Format("Word: {0} Count: {1}", word.Key, word.Value));
        }
    }

    // 8. Policz IDF dla wszystkich słów w tweetach zgodnie z definicją podaną na stronie http://www.tfidf.com/ Posortuj IDF malejąco i wypisz 10 wyrazów o największej wartości IDF.
    public static void Task8() 
    {
        foreach (var word in list.TopIDF(10))
        {
            Console.WriteLine(String.Format("Word: {0} IDF: {1}", word.Key, word.Value));
        }
    }


}
