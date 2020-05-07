using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCore.Utils
{
    public class QueryHolders
    {
    }
    public class SessionQh
    {
        public int Session_ID { get; set; }
        public DateTime Start_datetime { get; set; }
        public DateTime? End_datetime { get; set; }
        public int Client_ID { get; set; }
        public int Car_ID { get; set; }
        public int Manager_ID { get; set; }
        public int Start_location_ID { get; set; }
        public int End_location_ID { get; set; }

    }
}
