namespace lab3;
using Newtonsoft.Json;
public class TweetList 
{
    List<Tweet> data = new List<Tweet>();
    public List<Tweet> Data 
    {
        get => data;
    }

    TweetList(){}

    public TweetList(string fileName) 
    {
        StreamReader r = new StreamReader(fileName);
        string jsonString;

        while((jsonString = r.ReadLine()) != null) 
        {    
            Tweet tweet = JsonConvert.DeserializeObject<Tweet>(jsonString);
            data.Add(tweet);
        }
    }

    public override string ToString() 
    {
        return string.Join("\n", data);
    }

    public void SortByUserName() 
    {
        data.Sort((a,b) => a.UserName.CompareTo(b.UserName));
    }

    public void SortByDate() 
    {
        data.Sort((a,b) => a.CreatedAt.CompareTo(b.CreatedAt));
    }

    public Dictionary<string, List<Tweet>> UsersTweets() 
    {
        Dictionary<string, List<Tweet>> d = new Dictionary<string, List<Tweet>>();

        foreach(Tweet tweet in data) 
        {
            if(d.ContainsKey(tweet.UserName)) 
            {
                d[tweet.UserName].Add(tweet);
            } else {
                d.Add(tweet.UserName, new List<Tweet>(){tweet});
            }
        }

        return d;
    }

    public Dictionary<string, int> WordsCount() 
    {
        Dictionary<string, int> d = new Dictionary<string, int>();

        foreach(Tweet tweet in data) 
        {
            foreach(string word in tweet.Text.Split(' ')) 
            {
                if(d.ContainsKey(word)) 
                {
                    d[word] += 1;
                } else {
                    d.Add(word, 1);
                }
            }
        }

        return d;
    }

    public IEnumerable<KeyValuePair<string, int>> PopularWords(int n) 
    {
        Dictionary<string, int> d = WordsCount();
        List<KeyValuePair<string, int>> l = d.ToList();

        l.Sort((a,b) => b.Value.CompareTo(a.Value));

        return l.FindAll(x => x.Key.Length >= 5).Take(n);     
    }

    public IEnumerable<KeyValuePair<string, double>> TopIDF(int n) 
    {
        Dictionary<string, int> numOfTweets = new();

        // count the occurrences in tweets of every word
        foreach(Tweet tweet in data) 
        {
            string [] line = tweet.Text.Split(' ');
            line = line.Distinct().ToArray();
            foreach(string word in line) 
            {
                if(numOfTweets.ContainsKey(word)) 
                {
                    numOfTweets[word] += 1;
                } else {
                    numOfTweets.Add(word, 1);
                }
            }
        }

        Dictionary<string, double> IDF = new();

        foreach(string word in numOfTweets.Keys) 
        {
            IDF.Add(word, Math.Log(data.Count / numOfTweets[word], Math.E));
        }

        List<KeyValuePair<string, double>> l = IDF.ToList();
        l.Sort((a,b) => b.Value.CompareTo(a.Value));
        
        return l.Take(n);
    }

    public void ToXML(string fileName)
    {
        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(data[0].GetType());

        using (StreamWriter writer = File.CreateText(fileName)) 
        {
            foreach(Tweet tweet in data) 
            {
                xmlSerializer.Serialize(writer, tweet);
            }
        }
    }

    /*public List<Tweet> FromXML(string fileName) 
    {
        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(Tweet));
        
        string? xlmString;
        using (StreamReader reader = new StreamReader(fileName)) {
            while((var i = reader.ReadLine()) != null) { 
                list2 = (TweetList)x.Deserialize(reader);
            }
        }
    }*/
 
}