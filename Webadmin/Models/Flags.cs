using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Webadmin.Models;

namespace Webadmin.Models
{
    public class Flags
    {
        public int ID { get; set; }
        public string FlagTitle { get; set; }
        public string FlagLocationPage { get; set; }
        public string FlagCategory { get; set; }
        public bool FlagPersistent { get; set; }
        public int FlagUrgency { get; set; }
        public string FlagDesc { get; set; }
        public int FlagVenueID { get; set; } //possibly have 'Public Venues FlagVenueID' --- foreign key
        public DateTime FlagDate { get; set; }
        public bool FlagResolved { get; set; }
        


    }



}
