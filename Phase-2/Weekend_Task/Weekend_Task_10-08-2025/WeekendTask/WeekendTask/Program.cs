using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

namespace FetchPostsApp
{
    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://jsonplaceholder.typicode.com/posts";

            try
            {
                List<Post> posts = await FetchPostsAsync(url);

                foreach (var post in posts)
                {
                    Console.WriteLine($"Post ID: {post.Id}");
                    Console.WriteLine($"User ID: {post.UserId}");
                    Console.WriteLine($"Title: {post.Title}");
                    Console.WriteLine($"Body: {post.Body}");
                    Console.WriteLine(new string('-', 50));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task<List<Post>> FetchPostsAsync(string url)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); 

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Post>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
