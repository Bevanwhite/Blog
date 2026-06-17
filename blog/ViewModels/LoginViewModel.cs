using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel;

public class LoginViewModel
{
    public string Username { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}