using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyDetails.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> listUsers=new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mydetails;Integrated Security=True";
                using (SqlConnection connection=new SqlConnection (connectionString))
                {
                    connection.Open ();
                    String sqlRead = "SELECT * FROM userlist";
                    using (SqlCommand command = new SqlCommand(sqlRead, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader ())
                        {
                            while(reader.Read ())
                            {
                                UserInfo userInfo = new UserInfo();

                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.userName=reader.GetString(1);
                                userInfo.name = reader.GetString(2);
                                userInfo.email = reader.GetString(3);
                                userInfo.phone = reader.GetString(4);
                                userInfo.address = reader.GetString(5);
                                userInfo.manager_name = reader.GetString(6);
                                userInfo.created_at=reader.GetDateTime(7).ToString();
                                userInfo.status=reader.GetString(8);
                                
                                listUsers.Add(userInfo);
                            }
                        }
                    }
                
                }
            }
            catch(Exception exc) {
                Console.WriteLine("Exception: " + exc.ToString());
            }
        }
    }


    public class UserInfo
    {
        public String id;
        public String userName;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String manager_name;
        public String created_at;
        public String status;

    }
}
