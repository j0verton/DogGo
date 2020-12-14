using Doggo.Repositories.Utils;
using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly IConfiguration _config;

        public DogRepository(IConfiguration config)
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
        public List<Dog> GetAllDogs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [Id] ,[Name], [OwnerId], [Breed], [Notes], [ImageUrl]
                                        FROM Dog";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Dog> dogs = new List<Dog>();
                    while (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = ReaderUtils.GetNullableString(reader, "Notes"),
                            ImageUrl = ReaderUtils.GetNullableString(reader, "ImageUrl"),
                        };
                        dogs.Add(dog);
                    }
                    reader.Close();
                    return dogs;
                }
            }
        }

        public List<Dog> GetDogsByOwnerId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT [Id], [Name], [OwnerId], [Breed], [Notes], [ImageUrl]
                        FROM Dog
                        WHERE [OwnerId] = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Dog> dogs = new List<Dog>();
                    while (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = ReaderUtils.GetNullableString(reader, "Notes"),
                            ImageUrl = ReaderUtils.GetNullableString(reader, "ImageUrl"),
                        };
                        dogs.Add(dog);
                    }
                    reader.Close();
                    return dogs;

                }

            }


        }

        public Dog GetDogById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT [Id], [Name], [OwnerId], [Breed], [Notes], [ImageUrl]
                        FROM Dog
                        WHERE [Id] = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = ReaderUtils.GetNullableString(reader, "Notes"),
                            ImageUrl = ReaderUtils.GetNullableString(reader, "ImageUrl"),
                        };

                        reader.Close();
                        return dog;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
        public void AddDog(Dog dog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Dog ([Name], [OwnerId], [Breed], [Notes], [ImageUrl])
                    OUTPUT INSERTED.ID
                    VALUES (@name, @ownerId, @breed,  @notes, @ImageUrl);
                    ";
                    cmd.Parameters.AddWithValue("@name", dog.Name);
                    cmd.Parameters.AddWithValue("@breed", dog.Breed);
                    cmd.Parameters.AddWithValue("@ImageUrl", dog.ImageUrl);
                    cmd.Parameters.AddWithValue("@notes", dog.Notes);
                    cmd.Parameters.AddWithValue("@ownerId", dog.OwnerId);
                    int id = (int)cmd.ExecuteScalar();
                    dog.Id = id;
                }
            }

        }

        public void DeleteDog(int dogId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Dog
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", dogId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDog(Dog dog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Dog
                            SET 
                                [Name] = @name,
                                [OwnerId] = @ownerid,
                                [Breed] = @breed, 
                                [Notes] = @notes, 
                                [ImageUrl] = @imageurl
                            WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", dog.Id);
                    cmd.Parameters.AddWithValue("@name", dog.Name);
                    cmd.Parameters.AddWithValue("@ownerid", dog.OwnerId);
                    cmd.Parameters.AddWithValue("@breed", dog.Breed);

                    cmd.Parameters.AddWithValue("@notes", ReaderUtils.GetNullableParam(dog.Notes));
                    cmd.Parameters.AddWithValue("@imageurl", ReaderUtils.GetNullableParam(dog.ImageUrl));
                    if (!string.IsNullOrWhiteSpace(dog.Notes))
                    {
                        cmd.Parameters.AddWithValue("@notes", GetNullableParamdog.Notes);
                    }
                    else 
                    {
                        cmd.Parameters.AddWithValue("@notes", DBNull.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(dog.ImageUrl))
                    {
                        cmd.Parameters.AddWithValue("@imageurl", dog.ImageUrl);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@imageurl", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
