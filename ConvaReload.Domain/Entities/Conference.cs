﻿using System;
using System.ComponentModel.DataAnnotations;
using ConvaReload.Domain.Enum;

namespace ConvaReload.Domain.Entities;

public class Conference
{
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public string Organizer { get; set; }

    [EmailAddress] public string? Email { get; set; }
    [Phone] public string? Phone { get; set; }

    [Required] public string Addition { get; set; }

    [Required] public StatusConference status { get; set; }
}

