using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

        // Add these to allow the Migration to add a cascading effect to delete the photos per user as well. Before doing migrations
        public User User { get; set; }
        public int UserId { get; set; }

        
    }
}