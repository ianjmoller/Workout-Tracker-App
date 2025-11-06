using Microsoft.Extensions.Configuration; // Needed to read the appsettings.json
using Npgsql; // Needed for PostgreSQL
using WorkoutTracker.Data.Entities; // Needed to use MuscleGroup
using System.Data; // Needed for CommandType

namespace WorkoutTracker.Data.Repositories
{
    // This is our "Repository" (our "modular level")
    // Its ONLY job is to talk to the database.
    public class MuscleGroupRepository
    {
        private readonly string _connectionString;

        // 1. The constructor gets the "Configuration" (your appsettings.json)
        public MuscleGroupRepository(IConfiguration configuration)
        {
            // 2. It finds your password and saves it in a private variable.
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 3. This is the simple function our Controller will call.
        public async Task<IEnumerable<MuscleGroup>> GetAllAsync()
        {
            var muscleGroups = new List<MuscleGroup>();

            // 4. We use "await using" to create a connection.
            //    It automatically opens and closes the connection for us.
            await using (var db = new NpgsqlConnection(_connectionString))
            {
                // 5. This is our SQL query. It uses the correct column names.
                var sql = "SELECT muscle_group_id, name FROM MuscleGroups";
                
                await using (var cmd = new NpgsqlCommand(sql, db))
                {
                    await db.OpenAsync(); // Open the connection
                    var reader = await cmd.ExecuteReaderAsync();

                    // 6. Loop through every row the database gives us
                    while (await reader.ReadAsync())
                    {
                        // 7. Create a new MuscleGroup object and fill it
                        var muscleGroup = new MuscleGroup
                        {
                            MuscleGroupId = (int)reader["muscle_group_id"],
                            Name = (string)reader["name"]
                        };
                        muscleGroups.Add(muscleGroup);
                    }
                }
            }
            // 8. Return the final list!
            return muscleGroups;
        }
    }
}