﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class ApartmentResponse
    {
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
