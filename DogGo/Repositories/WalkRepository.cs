﻿using Doggo.Repositories.Utils;
using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {

        private readonly IConfiguration _config;

        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public Walk GetWalkById(int walkId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT w.Id, w.Date, w.Duration, w.DogId, w.WalkerId, w.WalkStatusId, d.Name
                                        FROM Walks w
                                        JOIN Dog d ON d.Id = w.DogId
                                        WHERE w.Id = @id";
                    cmd.Parameters.AddWithValue("@id", walkId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Walk walk = new Walk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            WalkStatusId = reader.GetInt32(reader.GetOrdinal("WalkStatusId")),
                            Dog = new Dog
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };

                        reader.Close();
                        return walk;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
            public List<Walk> GetWalksByWalkerId(int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Duration, w.Date, w.WalkerId, w.WalkStatusId, DogId, d.Name, d.OwnerId, d.Breed, d.Notes, d.ImageURL, o.[Name] AS oName
                        FROM Walks w
                        JOIN Dog d ON w.DogId = d.Id
                        JOIN Owner o ON d.OwnerId = o.Id
                        WHERE w.WalkerId = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            WalkStatusId = reader.GetInt32(reader.GetOrdinal("WalkStatusId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Owner = new Owner()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("oName"))
                                },
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Notes = ReaderUtils.GetNullableString(reader, "Notes"),
                                ImageUrl = ReaderUtils.GetNullableString(reader, "ImageUrl"),
                            },
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration"))
                        };
                        walks.Add(walk);

                    }
                    reader.Close();
                    return walks;
                }

            }
        }

        public List<Walk> GetWalksByOwnerId(int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Duration, w.Date, w.WalkerId, w.WalkStatusId, DogId, d.Name, d.OwnerId, d.Breed, d.Notes, d.ImageURL, o.[Name] AS oName
                        FROM Walks w
                        JOIN Dog d ON w.DogId = d.Id
                        JOIN Owner o ON d.OwnerId = o.Id
                        WHERE d.OwnerId = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            WalkStatusId = reader.GetInt32(reader.GetOrdinal("WalkStatusId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Owner = new Owner()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("oName"))
                                },
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Notes = ReaderUtils.GetNullableString(reader, "Notes"),
                                ImageUrl = ReaderUtils.GetNullableString(reader, "ImageUrl"),
                            },
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration"))
                        };
                        walks.Add(walk);

                    }
                    reader.Close();
                    return walks;
                }

            }
        }
        public List<Walk> GetWalksByDogId(int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT w.Id, w.Duration, w.Date, w.WalkerId, w.WalkStatusId, DogId, d.Name, d.OwnerId, d.Breed, d.Notes, d.ImageURL, o.[Name] AS oName
                        FROM Walks w
                        JOIN Dog d ON w.DogId = d.Id
                        JOIN Owner o ON d.OwnerId = o.Id
                        WHERE w.DogId = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            WalkStatusId = reader.GetInt32(reader.GetOrdinal("WalkStatusId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Owner = new Owner()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("oName"))
                                },
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                Notes = ReaderUtils.GetNullableString(reader, "Notes"),
                                ImageUrl = ReaderUtils.GetNullableString(reader, "ImageUrl"),
                            },
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration"))
                        };
                        walks.Add(walk);

                    }
                    reader.Close();
                    return walks;
                }

            }
        }

        public void RequestWalk(Walk walk)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Walks (Date,Duration, WalkStatusId, DogId, WalkerId)
                    OUTPUT INSERTED.ID
                    VALUES (@date, @duration, @walkstatusid, @dogid, @walkerid)
                    ";
                    cmd.Parameters.AddWithValue("@date", walk.Date);
                    cmd.Parameters.AddWithValue("@duration", walk.Duration);
                    cmd.Parameters.AddWithValue("@walkstatusid", walk.WalkStatusId);
                    cmd.Parameters.AddWithValue("@dogid", walk.DogId);
                    cmd.Parameters.AddWithValue("@walkerid", walk.WalkerId);
                    int id = (int)cmd.ExecuteScalar();
                    walk.Id = id;

                }
            }
        }

        public void ConfirmWalkRequest(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Walks
                        SET WalkStatusId = 2
                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            
            }
        }

        public void CompleteWalk(int id, int duration)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    int walkduration = duration * 60;
                    cmd.CommandText = @"
                        UPDATE Walks
                        SET WalkStatusId = 3, Duration = @duration
                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@duration", walkduration);

                    cmd.ExecuteNonQuery();
                }

            }
        }


    }
}
