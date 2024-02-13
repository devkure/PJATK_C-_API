
using Microsoft.Data.SqlClient;

public interface IAnimalsRepository
{
    Task<ICollection<Animal>> GetAnimals(string orderBy);
    Task AddAnimal(AnimalPOST animalPost);
    Task<bool> DoesAnimalExist(int ID);
    Task<bool> UpdateAnimal(Animal animal);
    Task<Animal> GetAnimal(int animalID);
    Task<bool> DeleteAnimal(int animalID);
}

public class AnimalsRepository : IAnimalsRepository
{
    private readonly string _connectionString;

    //Po³¹czenie z bazk¹ danych
    public AnimalsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")
            ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task AddAnimal(AnimalPOST animalPost)
    {
        var query = $"INSERT INTO [dbo].[Animal] ([ID], [Name], [Description], [Category], [Area]) VALUES (@ID, @Name, @Description, @Category, @Area)";
        
        using (var connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", animalPost.ID);
            command.Parameters.AddWithValue("@Name", animalPost.Name);
            command.Parameters.AddWithValue("@Description", animalPost.Description);
            command.Parameters.AddWithValue("@Category", animalPost.Category);
            command.Parameters.AddWithValue("@Area", animalPost.Area);

            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<bool> DoesAnimalExist(int ID)
    {
        var query = $"SELECT COUNT(*) FROM Animal WHERE ID = @ID";

        using (var connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            await connection.OpenAsync();

            int count = (int)await command.ExecuteScalarAsync();

            return count > 0;
        }

    }

    public async Task<ICollection<Animal>> GetAnimals(string orderBy)
    {
        var query = $"SELECT * FROM Animal ORDER BY {orderBy}";
        var animals = new List<Animal>();

        using (var connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);

            await connection.OpenAsync();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                int ID = reader.GetOrdinal("ID");
                int Name = reader.GetOrdinal("Name");
                int Description = reader.GetOrdinal("Description");
                int Category = reader.GetOrdinal("Category");
                int Area = reader.GetOrdinal("Area");

                while(await reader.ReadAsync())
                {
                    animals.Add(new Animal
                    {
                        ID = reader.GetInt32(ID),
                        Name = reader.GetString(Name),
                        Description = reader.GetString(Description),
                        Category = reader.GetString(Category),
                        Area = reader.GetString(Area)
                    });
                }
            }
        }
        return animals;
    }

    public async Task<bool> UpdateAnimal(Animal animal)
    {
        var query = @"UPDATE Animal SET Name = @Name, Description = @Description, 
                  Category = @Category, Area = @Area WHERE ID = @ID";

        using (var connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", animal.ID);
            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Description", animal.Description);
            command.Parameters.AddWithValue("@Category", animal.Category);
            command.Parameters.AddWithValue("@Area", animal.Area);

            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
    }

    public async Task<Animal> GetAnimal(int animalID)
    {
        var query = $"SELECT * FROM [dbo].[Animal] WHERE [ID] = @ID";

        using (var connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", animalID);

            await connection.OpenAsync();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return new Animal
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Category = reader.GetString(reader.GetOrdinal("Category")),
                        Area = reader.GetString(reader.GetOrdinal("Area"))
                    };
                }
            }
        }

        return null;
    }

    public async Task<bool> DeleteAnimal(int animalID)
    {
        var query = $"DELETE FROM [dbo].[Animal] WHERE [ID] = @ID";

        using (var connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", animalID);

            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
    }
}