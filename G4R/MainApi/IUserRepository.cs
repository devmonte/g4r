public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User> Add(User user);
}