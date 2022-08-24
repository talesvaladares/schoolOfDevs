using Microsoft.EntityFrameworkCore;
using schoolOfDevs.Entities;
using schoolOfDevs.Exceptions;
using schoolOfDevs.Helpers;
using BC = BCrypt.Net.BCrypt;

namespace schoolOfDevs.Services { 

    public interface IUserService {
        public Task<List<User>> GetAll();
        public Task<User> Create(User User);
        public Task<User> GetById(int id);
        public Task Update(User User);
        public Task Delete(int id);

    }

    public class UserService : IUserService
    {
      
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
     

        public async Task<User> Create(User user)
        {

            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == user.UserName);

            if (userDb is not null)
            {
                throw new BadRequestException($"Username {user.UserName} already exist.");
            }

            if (!user.Password.Equals(user.ConfirmPassword))
            {
                throw new BadRequestException("Password does not match confirmPassword");
            }

            user.Password = BC.HashPassword(user.Password);
           
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            User UserDb = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (UserDb is null)
            {
                throw new KeyNotFoundException($"User {id} not found");
            }

            _context.Users.Remove(UserDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync(); 
     
        public async Task<User> GetById(int id)
        {
            User UserDb = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            if (UserDb is null)
            {
                throw new KeyNotFoundException($"User {id} not found");
            }

            return UserDb;
        }

        public async Task Update(User user)
        {

            if (!user.Password.Equals(user.ConfirmPassword))
            {
                throw new BadRequestException("Password does not match confirmPassword");
            }

            User UserDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == user.Id);

            if (UserDb is null)
            {
                throw new KeyNotFoundException($"User { user.Id} not found");
            }

            if (!BC.Verify(user.CurrentPassword, UserDb.Password))
            {
                throw new BadRequestException("Incorret Password");
            }

           
            //para nao perder a data de criação
            user.CreatedAt = UserDb.CreatedAt;
            user.Password = BC.HashPassword(user.Password);

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
