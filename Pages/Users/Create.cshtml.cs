using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyDetails.Pages.Users
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost() {
            userInfo.userName = Request.Form["userName"];
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];
            userInfo.manager_name = Request.Form["manager_name"];
            userInfo.status = Request.Form["status"];
            Console.WriteLine(userInfo);

            if(userInfo.userName.Length == 0 ||  userInfo.name.Length == 0 || 
                userInfo.phone.Length==0 || userInfo.email.Length==0 || userInfo.address.Length==0 ||
                userInfo.manager_name.Length==0 || userInfo.status.Length==0)
            {
                errorMsg = "All the fields are mandatory!";
                return;
            }
                
            //Saving the data into DB

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mydetails;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlInsert = "INSERT INTO userlist (user_name, pass, name, email, phone, address, manager_name, status) " +
                        "VALUES (@user_name, @password, @name, @email, @phone, @address, @manager_name, @status);";

                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        command.Parameters.AddWithValue("@user_name", userInfo.userName.ToString());
                        command.Parameters.AddWithValue("@password", userInfo.email.Split('@')[0] + "1234");
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@address", userInfo.address);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@manager_name", userInfo.manager_name);
                        command.Parameters.AddWithValue("@status", userInfo.status);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.Message+" SQL Error";
                return;
            }

            userInfo.userName = "";
            userInfo.name = "";
            userInfo.email = "";
            userInfo.phone = "";
            userInfo.address = "";
            userInfo.manager_name = "";
            userInfo.status = "";
            successMsg = "New User added successfully.";

            Response.Redirect("/Users/UsersList");

        }
    }
}
