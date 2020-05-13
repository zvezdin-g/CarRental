using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace RentalCore.Utils
{
    public class QueryMethods
    {
        public List<SessionQh> QueryCurrentSessions(SqlConnection conn)
        {
            var sessions = new List<SessionQh>();
            var sql = "select * from Session where End_datetime is null";
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = sql
            };
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var sessionID = reader.GetInt32(0);
                        var startDt = reader.GetDateTime(1);
                        var clientID = reader.GetInt32(3);
                        var carID = reader.GetInt32(4);
                        var endLocationID = reader.GetInt32(5);
                        var startLocationID = reader.GetInt32(6);
                        var managerID = reader.GetInt32(7);

                        var tempSession = new SessionQh()
                        {
                            Session_ID = sessionID,
                            Start_datetime = startDt,
                            Client_ID = clientID,
                            Car_ID = carID,
                            End_location_ID = endLocationID,
                            Start_location_ID = startLocationID,
                            Manager_ID = managerID
                        };

                        sessions.Add(tempSession);
                    }
                }
            }

            return sessions;
        }
        public List<SessionQh> QueryPastSessions(SqlConnection conn)
        {
            var sessions = new List<SessionQh>();
            var sql = "select * from Session where End_datetime is not null";
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = sql
            };
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var sessionID = reader.GetInt32(0);
                        var startDt = reader.GetDateTime(1);
                        var endDt = reader.GetValue(2) as DateTime?;
                        if (endDt != null)
                            endDt = reader.GetDateTime(2);
                        var clientID = reader.GetInt32(3);
                        var carID = reader.GetInt32(4);
                        var endLocationID = reader.GetInt32(5);
                        var startLocationID = reader.GetInt32(6);
                        var managerID = reader.GetInt32(7);

                        var tempSession = new SessionQh()
                        {
                            Session_ID = sessionID,
                            Start_datetime = startDt,
                            End_datetime = endDt,
                            Client_ID = clientID,
                            Car_ID = carID,
                            End_location_ID = endLocationID,
                            Start_location_ID = startLocationID,
                            Manager_ID = managerID
                        };

                        sessions.Add(tempSession);
                    }
                }
            }

            return sessions;
        }

        public List<CarQh> QueryCars(SqlConnection conn)
        {
            var cars = new List<CarQh>();
            var sql = "select * from Car";
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = sql
            };
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var carID = reader.GetInt32(0);
                        var carPlateNumber = reader.GetString(1);
                        var fuelLeft = reader.GetInt32(2);
                        var colour = reader.GetString(3);
                        var isFree = reader.GetBoolean(4);
                        var modelID = reader.GetInt32(5);
                        
                        var tempCar = new CarQh()
                        {
                            Car_ID = carID,
                            Car_Plate_Number = carPlateNumber,
                            Fuel_left = fuelLeft,
                            Colour = colour,
                            is_Free = isFree,
                            Model_ID = modelID
                        };

                        cars.Add(tempCar);
                    }
                }
            }

            return cars;
        }
    }
}
