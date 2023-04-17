namespace lab3;
using Newtonsoft.Json;
using System.Globalization;

public class Tweet {
    public string Text;
    public string UserName;
    public string LinkToTweet;
    public string FirstLinkUrl;
    public DateTime CreatedAt;
    public string TweetEmbedCode;
    
    public Tweet(){}
    [JsonConstructor]
    public Tweet(string Text, string UserName, string LinkToTweet, string FirstLinkUrl, string CreatedAt, string TweetEmbedCode) {
        this.Text = Text;
        this.UserName = UserName;
        this.LinkToTweet = LinkToTweet;
        this.FirstLinkUrl = FirstLinkUrl;
        this.TweetEmbedCode = TweetEmbedCode;
        this.CreatedAt = DateTime.ParseExact(
            CreatedAt.Insert(CreatedAt.Length-2, " "),
            "MMMM dd, yyyy 'at' hh':'mm tt",
            CultureInfo.InvariantCulture
            );
    }

    public override string ToString() {
        return String.Format("Username: {0} Date: {1} Text: {2}", UserName, CreatedAt,Text);
    }
}