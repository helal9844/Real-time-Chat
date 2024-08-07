using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat_DAL;

public class Seed
{
    public static async Task SeedUsers(AppDbContext dbContext)
    {
        if (await dbContext.Users.AnyAsync()) return;

        var userData = await System.IO.File.ReadAllTextAsync(@"E:\ChatApp\RealTimeChat\Chat_RealTime\Chat_DAL\Data\UserSeedData.json");

        var users = JsonSerializer.Deserialize<List<ChatUser>>(userData);
        if (users == null || users.Count == 0) return;

        foreach (var user in users)
        {
            using var hmac = new HMACSHA256();

            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            dbContext.Users.Add(user);
        }
        await dbContext.SaveChangesAsync();
    }
}
