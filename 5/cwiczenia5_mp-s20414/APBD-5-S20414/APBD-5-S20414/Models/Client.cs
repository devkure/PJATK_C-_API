﻿using System;
using System.Collections.Generic;

namespace APBD_5_S20414.Models;

public partial class Client
{
    public int IdClient { get; private set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Pesel { get; set; } = null!;

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}
