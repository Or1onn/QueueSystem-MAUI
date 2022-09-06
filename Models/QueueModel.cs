using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSystem.Models
{
    public class QueueModel
    {
        public int Id { get; set; }
        public int Queue { get; set; }
        public string FIN { get; set; }
        public string FullName { get; set; }
        public bool IsPaid { get; set; }
    }
}
