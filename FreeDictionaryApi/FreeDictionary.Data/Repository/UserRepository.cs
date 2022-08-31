using FreeDictionary.Data.Context;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly FreeDictionaryContext _context;
        public UserRepository(FreeDictionaryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return default;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return default;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return default;

            return user;
        }

        public async Task<User> AddAsync(string name, string email, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public override Task<User?> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
