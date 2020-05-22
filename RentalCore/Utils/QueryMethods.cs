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

        public List<ModelQh> QueryModels(SqlConnection conn)
        {
            var models = new List<ModelQh>();
            var sql = "select * from Model";
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
                        var modelId = reader.GetInt32(0);
                        var modelName = reader.GetString(1);
                        var bodyType = reader.GetString(2);
                        var transmission = reader.GetString(3);
                        var hp = reader.GetInt32(4);
                        var engineCapacity = reader.GetInt32(5);
                        var classId = reader.GetInt32(6);
                        var manufacturerId = reader.GetInt32(7);

                        var tempModel = new ModelQh()
                        {
                            Model_ID = modelId,
                            Model_Name = modelName,
                            Body_type = bodyType,
                            Transmission = transmission,
                            HP = hp,
                            Engine_capacity = engineCapacity,
                            Class_ID = classId,
                            Manufacturer_ID = manufacturerId
                        };

                        models.Add(tempModel);
                    }
                }
            }

            return models;
        }

        public List<RentalQh> QueryRentals(SqlConnection conn)
        {
            var rentals = new List<RentalQh>();
            var sql = "select * from Rental_place";
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
                        var rentalId = reader.GetInt32(0);
                        var rentalName = reader.GetString(2);
                        var location = reader.GetString(1);


                        var tempRental = new RentalQh()
                        {
                            Rental_ID = rentalId,
                            Rental_Name = rentalName,
                            Location = location
                        };

                        rentals.Add(tempRental);
                    }
                }
            }

            return rentals;
        }
        public List<FineQh> QueryFines(SqlConnection conn)
        {
            var fines = new List<FineQh>();
            var sql = "select * from Fine";
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
                        var fId = reader.GetInt32(0);
                        var fDesc = reader.GetString(1);
                        var fCost = reader.GetInt32(2);


                        var tempFine = new FineQh()
                        {
                            Fine_ID = fId,
                            Fine_description = fDesc,
                            Fine_cost = fCost
                        };

                        fines.Add(tempFine);
                    }
                }
            }

            return fines;
        }
        public void QueryCloseOrder(SqlConnection conn, SessionQh session, FineQh fine)
        {
            string sql;
            if (fine != null)
            {
                sql = "update Session set End_datetime = @dateclosed where Session_ID = @sessionid;" +
                          "insert into Car_Return(Session_ID, Fine_ID) Values (@sessionid, @fineid);" +
                          "insert into Payments(Session_ID, Date_time) Values (@sessionid, @dateclosed);";
            }
            else
            {
                sql = "update Session set End_datetime = @dateclosed where Session_ID = @sessionid;" +
                      "insert into Car_Return(Session_ID) Values (@sessionid);" +
                      "insert into Payments(Session_ID, Date_time) Values (@sessionid, @dateclosed);";
            }
            
            var cmd = new SqlCommand()
            {
                Connection = conn,
                CommandText = sql
            };
            if (fine != null)
                cmd.Parameters.Add("@fineid", SqlDbType.Int).Value = fine.Fine_ID;
            cmd.Parameters.Add("@dateclosed", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@sessionid", SqlDbType.Int).Value = session.Session_ID;
            cmd.ExecuteNonQuery();
        }
        public List<FinancesQh> QueryFinances(SqlConnection conn)
        {
            var finances = new List<FinancesQh>();
            var sql = "select p.Session_ID, " +
                      "p.Date_time, " +
                      "Rental_cost, " +
                      "Fine_description, " +
                      "Fine_cost, " +
                      "Total_cost " +
                      "from Payments p left join Car_Return cr on p.Session_ID=cr.Session_ID " +
                      "left join Fine f on cr.Fine_ID=f.Fine_ID";
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
                        var sId = reader.GetInt32(0);
                        var dt = reader.GetDateTime(1);
                        var rCost = reader.GetInt32(2);
                        var fDesc = reader.GetValue(3) as string;
                        if (fDesc != null)
                            fDesc = reader.GetString(3);
                        else
                            fDesc = "---";
                        var fCost = reader.GetValue(4) as int?;
                        if (fCost != null)
                            fCost = reader.GetInt32(4);
                        else
                            fCost = 0;
                        var tCost = reader.GetInt32(5);

                        var tempFinances = new FinancesQh()
                        {
                            Session_ID = sId,
                            DateTime = dt,
                            Rental_cost = rCost,
                            Fine_description = fDesc,
                            Fine_cost = fCost,
                            Total_cost = tCost
                        };

                        finances.Add(tempFinances);
                    }
                }
            }

            return finances;
        }
        public List<ClientQh> QueryClients(SqlConnection conn)
        {
            var clients = new List<ClientQh>();
            var sql = "select * from Client";
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
                        var cId = reader.GetInt32(0);
                        var ln = reader.GetString(1);
                        var fn = reader.GetString(2);
                        var dob = reader.GetDateTime(3);
                        var doi = reader.GetDateTime(4);
                        var exp = reader.GetInt32(5);
                        var ins_class = reader.GetInt32(6);
                        var phone = reader.GetString(7);
                        var licence = reader.GetString(8);
                        
                        var temp= new ClientQh()
                        {
                            Client_ID = cId,
                            Last_Name = ln,
                            First_Name = fn,
                            DOB = dob,
                            DOI = doi,
                            Driving_experience = exp,
                            Insurance_class = ins_class,
                            Phone = phone,
                            Licence_number = licence
                        };

                        clients.Add(temp);
                    }
                }
            }

            return clients;
        }
    }
}
