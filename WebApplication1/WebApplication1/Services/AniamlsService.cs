using System.Data.SqlClient;
using cwiczenia4.Models;

namespace WebApplication1.Services
{
    public class AnimalDbService : IAnimalDbService
    {
        private readonly string _connectionString = "Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            var animals = new List<Animal>();

            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy}";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var animal = new Animal
                    {
                        IdAnimal = (int)dr["IdAnimal"],
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    };

                    animals.Add(animal);
                }
            }

            return animals;
        }
        
        
        public Animal GetAnimalById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Animal WHERE IdAnimal = @IdAnimal";
                com.Parameters.AddWithValue("@IdAnimal", id);

                con.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var animal = new Animal
                    {
                        IdAnimal = (int)dr["IdAnimal"],
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    };

                    return animal;
                }

                return null;
            }
        }

        public void AddAnimal(Animal newAnimal)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "INSERT INTO Animals(Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";

                com.Parameters.AddWithValue("@Name", newAnimal.Name);
                com.Parameters.AddWithValue("@Description", newAnimal.Description);
                com.Parameters.AddWithValue("@Category", newAnimal.Category);
                com.Parameters.AddWithValue("@Area", newAnimal.Area);

                con.Open();
                com.ExecuteNonQuery();
            }
        }

        public void UpdateAnimal(Animal updatedAnimal)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "UPDATE Animals SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";

                com.Parameters.AddWithValue("@IdAnimal", updatedAnimal.IdAnimal);
                com.Parameters.AddWithValue("@Name", updatedAnimal.Name);
                com.Parameters.AddWithValue("@Description", updatedAnimal.Description);
                com.Parameters.AddWithValue("@Category", updatedAnimal.Category);
                com.Parameters.AddWithValue("@Area", updatedAnimal.Area);

                con.Open();
                com.ExecuteNonQuery();
            }
        }

        public void DeleteAnimal(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "DELETE FROM Animals WHERE IdAnimal = @IdAnimal";

                com.Parameters.AddWithValue("@IdAnimal", id);

                con.Open();
                com.ExecuteNonQuery();
            }
        }
    }
}