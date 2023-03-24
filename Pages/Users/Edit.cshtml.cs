using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyDetails.Pages.Users
{
    
    public class EditModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
            String id= Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mydetails;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlRead = "SELECT * FROM userlist WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sqlRead, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.userName = reader.GetString(1);
                                userInfo.name = reader.GetString(2);
                                userInfo.email = reader.GetString(3);
                                userInfo.phone = reader.GetString(4);
                                userInfo.address = reader.GetString(5);
                                userInfo.manager_name = reader.GetString(6);
                                userInfo.created_at = reader.GetDateTime(7).ToString();
                                userInfo.status = reader.GetString(8);

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            Console.WriteLine(userInfo);
        }

        public void OnPost() {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];
            userInfo.manager_name = Request.Form["manager_name"];
            userInfo.status = Request.Form["status"];
            userInfo.userName = Request.Form["userName"];

            if (userInfo.name.Length == 0 || userInfo.userName.Length == 0 ||
               userInfo.phone.Length == 0 || userInfo.email.Length == 0 || userInfo.address.Length == 0 ||
               userInfo.manager_name.Length == 0 || userInfo.status.Length == 0)
            {
                errorMsg = "All the fields are mandatory!";
                return;
            }


            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mydetails;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sqlUpdate = "UPDATE userlist SET user_name=@user_name, name=@name, email=@email, address=@address, phone=@phone, manager_name=@manager_name, status=@status WHERE id=@id;";
                    Console.WriteLine(userInfo.id);
                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@user_name", userInfo.userName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@name", userInfo.name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@email", userInfo.email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@address", userInfo.address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@phone", userInfo.phone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@manager_name", userInfo.manager_name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@status", userInfo.status ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@id", userInfo.id ?? (object)DBNull.Value);

                        command.ExecuteNonQuery();

                        Console.WriteLine("SuccessFully Edited!");
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message + " SQL Edit Error";
                return;
            }

            successMsg = "User's Details Updated Successfully.";

            Response.Redirect("/Users/UsersList");
           
        }
    }
}
