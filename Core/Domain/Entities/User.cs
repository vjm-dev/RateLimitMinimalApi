﻿namespace RateLimitMinimalApi.Core.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User() { }

    public User(string username, string email)
    {
        Username = username;
        Email = email;
    }
}
