using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiCore.Models
{
    public class RoomDetails
    {
        public string? Id { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public int ParticipantCount { get; set; }
        public int MaxParticipants { get; set; }

        // modify to make P2P
        public string? RoomType { get; set; } = null!;
    }
}
