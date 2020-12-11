using DogGo.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class DogRepository
    {
        //public SqlConnection Connection
        //{
        //    get
        //    {
        //        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        //    }
        //}

        //public List<Dog> GetDogsByOwnerId(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                SELECT [Id], [Name], [OwnerId], [Breed], [Notes], [ImageUrl]
        //                FROM Dog
        //                WHERE [OwnerId] = @id
        //            ";
        //            cmd.Parameters.AddWithValue("@id", id);

        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                Dog dog = new Dog
        //                {


        //                };


        //            }

        //        }
            
            
        //    }
        //            }
    }
}
