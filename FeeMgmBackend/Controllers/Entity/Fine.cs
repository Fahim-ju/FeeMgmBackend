using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class Fine
    {
        public Guid Id { get; set; }
        public int LawId { get; set; }
        public int FineAmount { get; set; }
    }
}