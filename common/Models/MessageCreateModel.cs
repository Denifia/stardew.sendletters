﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denifia.Stardew.SendLetters.Common.Models
{
    public class MessageCreateModel
    {
        public string ToPlayerId { get; set; }
        public string FromPlayerId { get; set; }
        public string Text { get; set; }
    }
}
