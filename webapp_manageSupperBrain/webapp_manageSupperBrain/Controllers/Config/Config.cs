using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using webapp_manageSupperBrain.Models;

namespace webapp_manageSupperBrain.Controllers.Config
{
   
    public class Config
    {
        public static string ConnectionString = @"Data Source=dungct\SQLEXPRESS;Initial Catalog=webapp_manageSupperBrain;Integrated Security=True;";
        public static ModelsWebDataContext db = new ModelsWebDataContext();
      
        public static string checkRole(string code, int IdPermission)
        {
           
            Permission permission = SettingConnect.SelectSingle<Permission>("select * from Permission per where per.Code = '"+code+"'");

             List<UserPermission> userpermission = SettingConnect.Select<UserPermission>("select * from UserPermission");
                if (permission != null)
                {
                    UserPermission userPermission = SettingConnect.SelectSingle<UserPermission>("select * from UserPermission where IdUser = '"+IdPermission+"' and IdPermission='"+permission.id+"'");

                    if (userPermission != null)
                    {
                        bool? isRead = userPermission.IsRead;
                        bool? isCreate = userPermission.IsCreate;
                        bool? isDelete = userPermission.IsDelete;
                        bool? isEdit = userPermission.IsEdit;

                        if ((isRead == true) ||
                            (isCreate == true) ||
                            (isDelete == true) ||
                            (isEdit == true ))
                        {
                            return ""; // Trả về chuỗi rỗng nếu người dùng có bất kỳ quyền nào được cấp hoặc quyền không xác định
                        }
                    }
                }

                return "hideof"; // Trả về chuỗi "hideof" nếu không có quyền truy cập
            
        }


    }
}