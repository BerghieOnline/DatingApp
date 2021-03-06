using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
            
        }

        public async Task<User> Login(string username, string password)
        {
            //Go to DB and Check if Username exist. If nothing, returns NULL without exception. Using FirstOrDefault.
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
            return null;

            //Create a new method. Send Password, and DB Stored Hash and Salt to check if user password is correct. User Ctrl + . to create Method.
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return null;
                
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {                
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if(ComputedHash[i] != passwordHash[i]) return false;
                }

                return true;
            }
        }

        public async Task<User> Register(User user, string password)
    {
        byte[] passwordHash, passwordSalt;

        CreatePasswordHash(password, out passwordHash, out passwordSalt);
        
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;


    }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username == username))
            return true;

            return false;
        }
    }
}