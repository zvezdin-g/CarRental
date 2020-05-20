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

    public class CarQh
    {
        public int Car_ID { get; set; }
        public string Car_Plate_Number { get; set; }
        public int Fuel_left { get; set; }
        public string Colour { get; set; }
        public bool is_Free { get; set; }
        public int Model_ID { get; set; }
    }

    public class ModelQh
    {
        public int Model_ID { get; set; }
        public string Model_Name { get; set; }
        public string Body_type { get; set; }
        public string Transmission { get; set; }
        public int HP { get; set; }
        public int Engine_capacity { get; set; }
        public int Class_ID { get; set; }
        public int Manufacturer_ID { get; set; }
    }

    public class RentalQh
    {
        public int Rental_ID { get; set; }
        public string Rental_Name { get; set; }
        public string Location { get; set; }
        
    }

    public class FineQh
    {
        public int Fine_ID { get; set; }
        public string Fine_description { get; set; }
        public int Fine_cost { get; set; }
    }
}
