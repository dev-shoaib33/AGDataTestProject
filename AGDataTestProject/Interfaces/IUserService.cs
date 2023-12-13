using AGDataTestProject.Models;

namespace AGDataTestProject.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        (bool IsSuccess, string Message) UpdateUser(User user);
        (bool IsSuccess, string Message) AddUser(User user);
        (bool IsSuccess, string Message) DeleteUser(string name);
    }
}
