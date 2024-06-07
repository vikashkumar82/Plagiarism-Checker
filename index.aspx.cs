using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace plagGarism
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string inputText = txtInput.Text;
            List<PlagiarismResult> results = CheckPlagiarism(inputText);

            if (results.Count > 0)
            {
                rptResults.DataSource = results;
                rptResults.DataBind();
                lblResult.Text = "Potential plagiarism detected. Similar content found.";
            }
            else
            {
                lblResult.Text = "No plagiarism detected. Content is unique.";
            }
        }

        public List<PlagiarismResult> CheckPlagiarism(string text)
        {
            string apiKey = "AIzaSyAACsjtFAuk377IWjF23cDSBNjFyIGmwhU";
            string searchEngineId = "663aad86da6d34869";
            string query = System.Net.WebUtility.UrlEncode(text);
            string apiUrl = $"https://www.googleapis.com/customsearch/v1?q={query}&key={apiKey}&cx={searchEngineId}";

            List<PlagiarismResult> results = new List<PlagiarismResult>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    JObject searchResults = JObject.Parse(jsonResponse);
                    var items = searchResults["items"];
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            string link = item["link"].ToString();
                            string snippet = item["snippet"].ToString();

                            // Calculate similarity
                            string similarity = GetSimilarityPercentage(text, snippet);

                            results.Add(new PlagiarismResult { Link = link, Snippet = snippet, Similarity = similarity });
                        }
                    }
                }
            }

            return results;
        }

        private string GetSimilarityPercentage(string text1, string text2)
        {
            // Normalize the texts by converting to lower case and removing punctuation
            text1 = NormalizeText(text1);
            text2 = NormalizeText(text2);

            // Split the texts into words
            string[] words1 = text1.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] words2 = text2.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Calculate the number of exact word matches
            int matchCount = words1.Intersect(words2).Count();

            // Calculate the similarity percentage
            double similarityPercentage = (double)matchCount / words1.Length * 100;

            // Format the percentage to two decimal places
            return similarityPercentage.ToString("F2") + "%";
        }

        private string NormalizeText(string text)
        {
            // Convert to lower case and remove punctuation using regex
            text = text.ToLower();
            text = Regex.Replace(text, @"[^\w\s]", "");
            return text;
        }
    }

    public class PlagiarismResult
    {
        public string Link { get; set; }
        public string Snippet { get; set; }
        public string Similarity { get; set; }
    }
}