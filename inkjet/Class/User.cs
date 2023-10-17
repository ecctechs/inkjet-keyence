using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void setUserNameOp(String UserNameOp)
        {
            strUser_name_op = UserNameOp;
        }
        public void setUserPasswordOp(String UserPasswordOp)
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

    }
}
