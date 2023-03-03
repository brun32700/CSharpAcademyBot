﻿using System.ComponentModel.DataAnnotations;

namespace CSharpAcademyBot.Models;

public class Reputation
{
    [Key]
    public int UserId { get; set; } 
    public User User { get; set; }
    public int Amount { get; set; }
}
