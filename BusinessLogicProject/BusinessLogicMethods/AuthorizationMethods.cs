using BusinessLogicProject.Contexts;
using BusinessLogicProject.Models;
using System.Linq;
using System.Web.Security;

namespace BusinessLogicProject.BusinessLogicMethods
{
    public class AuthorizationMethods
    {
        UserContext db = new UserContext();

        public User Login(LoginModel loginModel)
        {

            string pass = SHA.GenerateSHA256String(loginModel.Password);
            User user = null;
            user = db.Users.FirstOrDefault(e => e.Name == loginModel.Login &&
            e.PassWord == pass);
            return user;

        }
        public bool RegisterMethod(RegisterModel registerModel)
        {
            User user = null;
            user = db.Users.FirstOrDefault(e => e.Name == registerModel.Login);
            if (user == null && registerModel.Age > 0 && !string.IsNullOrEmpty(registerModel.Login) && !string.IsNullOrEmpty(registerModel.Sex.ToString())) 
            {
                db.Users.Add(new User
                {
                    Name = registerModel.Login,
                    PassWord = SHA.GenerateSHA256String(registerModel.Password),
                    Age = registerModel.Age,
                    Sex = registerModel.Sex.ToString()

                });
                db.SaveChanges();
                string pass = SHA.GenerateSHA256String(registerModel.Password);
                user = db.Users.Where(e => e.Name == registerModel.Login
                   && e.PassWord == pass)
                   .FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(registerModel.Login, true);
                    return true;
                }
            }
            return false;
        }
        public User UserInfo(string name)
        {
            User user = db.Users.FirstOrDefault(e => e.Name == name);
            return user;

        }

    }
}
