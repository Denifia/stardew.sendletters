﻿namespace Denifia.Stardew.SendItems.Api.Models
{
    public class CreateMailModel
    {
        public string FromFarmerId { get; set; }
        public string ToFarmerId { get; set; }
        public string Text { get; set; }
    }
}