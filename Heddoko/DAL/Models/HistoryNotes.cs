using System;

namespace DAL.Models
{
    public class HistoryNotes
    {
        public User User { get; set; }

        public string Notes { get; set; }

        public DateTime Created { get; set; }
    }
}