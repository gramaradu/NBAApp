﻿using System;

namespace NBA.Utils.Models
{
    public class Meta
    {
        public int Total_pages { get; set; }
        public int Current_page { get; set; }
        public int? Next_page { get; set; }
        public int Per_page { get; set; }
        public int Total_count { get; set; }
    }
}