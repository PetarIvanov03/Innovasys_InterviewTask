using Dapper;
using InnovasysApp.ViewModels;
using Microsoft.Data.SqlClient;

namespace InnovasysApp.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task SaveUsersAsync(List<UserViewModel> users)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync("DELETE FROM Addresses");
            await connection.ExecuteAsync("DELETE FROM Users");

            foreach (var user in users)
            {
                var userId = await connection.ExecuteScalarAsync<int>(
                    @"INSERT INTO Users (Name, NotUsername, Email, Phone, Website, Note, IsActive, CreatedAt)
                      VALUES (@Name, @NotUsername, @Email, @Phone, @Website, @Note, @IsActive, @CreatedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int);",
                    new
                    {
                        Name = user.Name,
                        NotUsername = user.Username,
                        Email = user.Email,
                        Phone = user.Phone,
                        Website = user.Website,
                        Note = user.Note ?? "",
                        IsActive = user.IsActive ? 1 : 0,
                        CreatedAt = DateTime.Now
                    });

                await connection.ExecuteAsync(
                    @"INSERT INTO Addresses (Street, Suite, City, Zipcode, Lat, Lng, UserId)
                      VALUES (@Street, @Suite, @City, @Zipcode, @Lat, @Lng, @UserId);",
                    new
                    {
                        Street = user.Address.Street,
                        Suite = user.Address.Suite,
                        City = user.Address.City,
                        Zipcode = user.Address.Zipcode,
                        Lat = user.Address.Geo.Lat,
                        Lng = user.Address.Geo.Lng,
                        UserId = userId
                    });
            }
        }

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
        SELECT 
            u.Name,
            u.NotUsername AS Username,
            u.Email,
            u.Phone,
            u.Website,
            u.Note,
            u.IsActive,
            a.Street,
            a.Suite,
            a.City,
            a.Zipcode,
            a.Lat,
            a.Lng
        FROM Users u
        INNER JOIN Addresses a ON a.UserId = u.Id";

            var results = await connection.QueryAsync(query);

            var users = results.Select(row => new UserViewModel
            {
                Name = row.Name,
                Username = row.Username,
                Email = row.Email,
                Phone = row.Phone,
                Website = row.Website,
                Note = row.Note,
                IsActive = Convert.ToBoolean(row.IsActive),
                Address = new AddressViewModel
                {
                    Street = row.Street,
                    Suite = row.Suite,
                    City = row.City,
                    Zipcode = row.Zipcode,
                    Geo = new GeoViewModel
                    {
                        Lat = row.Lat,
                        Lng = row.Lng
                    }
                }
            }).ToList();

            return users;
        }
    }
}
