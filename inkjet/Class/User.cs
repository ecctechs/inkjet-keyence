using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace inkjet.Class
{
    public class User
    {
        [Name(name: "User ID")]
        public int UserId { get; set; }

        [Name(name: "User Name")]
        public string UserName { get; set; }

        [Name(name: "User Password")]
        public string UserPassword { get; set; }

        [Name(name: "User Role")]
        public string UserRole { get; set; }

        [Name(name: "User Name Operator")]
        public string UserNameOp { get; set; }

        [Name(name: "User Password Operator")]
        public string UserPasswordOp { get; set; }

        private static string strUser = "";
        private static string strUser_role = "";
        private static string strUser_password = "";
        private static int strUserID;
        private static string strUser_name_op = "";
        private static string strUser_password_op = "";

        public void setUser(String UserName)
        {
            strUser = UserName;
        }
        public void setUserRole(String UserRole)
        {
            strUser_role = UserRole;
        }
        public void setUserPassword(String UserPassword)
        {
            strUser_password = UserPassword;
        }
        public void setUserID(int UserId)
        {
            strUserID = UserId;
        }
        public static void setUserNameOp(String UserNameOp)
        {
            strUser_name_op = UserNameOp;
        }
        public static void setUserPasswordOp(String UserPasswordOp)
        {
            strUser_password_op = UserPasswordOp;
        }

        public static string getUser()
        {
            return strUser;
        }
        public static string getUserRole()
        {
            return strUser_role;
        }
        public static string getUserPassword()
        {
            return strUser_password;
        }
        public static int getUserID()
        {
            return strUserID;
        }
        public static string getUserNameOp()
        {
            return strUser_name_op;
        }
        public static string getUserPasswordOp()
        {
            return strUser_password_op;
        }

        public void clearUser()
        {
            strUser = null;
        }

 

        public static List<User> ListUser()
        {
            List<User> list_user = new List<User>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\Inkjet\Data\user.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_user = csv.GetRecords<User>().ToList();
                    for (int i = 0; i < list_user.Count; i++)
                    {
                        if (User.getUser() == list_user[i].UserName)
                        {
                            setUserNameOp(list_user[i].UserNameOp);
                            setUserPasswordOp(list_user[i].UserPasswordOp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return list_user;
        }

        public static void Update_User(List<User> records)
        {
            try
            {
                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\Inkjet\Data\user.csv"))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(records);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
        }
    }
}
