using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("C# Programming Basics", "John Smith", 600);
        video1.AddComment(new Comment("Mary", "Very helpful!"));
        video1.AddComment(new Comment("David", "Thanks for sharing."));
        video1.AddComment(new Comment("Sarah", "Excellent tutorial."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Object-Oriented Programming", "Jane Doe", 750);
        video2.AddComment(new Comment("Michael", "Great explanation."));
        video2.AddComment(new Comment("Grace", "Easy to understand."));
        video2.AddComment(new Comment("Peter", "I learned a lot."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Learning C#", "Code Academy", 900);
        video3.AddComment(new Comment("James", "Awesome video!"));
        video3.AddComment(new Comment("Linda", "Very informative."));
        video3.AddComment(new Comment("Daniel", "Keep it up!"));
        videos.Add(video3);

        // Display all videos and comments
        foreach (Video video in videos)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine();

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"{comment.GetName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}