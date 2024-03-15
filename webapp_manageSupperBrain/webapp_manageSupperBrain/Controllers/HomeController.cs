using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using webapp_manageSupperBrain.Controllers;
using webapp_manageSupperBrain.Controllers.Config;
using webapp_manageSupperBrain.Models;

namespace webapp_manageSupperBrain.Views.Login
{
    public class HomeController : Controller
    {
        // GET: Home
        ModelsWebDataContext db = new ModelsWebDataContext();
        public class userrespone
        {
          
        }
        public ActionResult Home()
        {
            return View();
        }
    
        public ActionResult ManageUser() {
        
      




            return View();
        }
        [HttpGet]
        public ActionResult getdata()
        {
            List<UserAccount> user = new List<UserAccount>();

            user = SettingConnect.Select<UserAccount>("select * from UserAccount");





            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult search(string keyword)
        {
            List<UserAccount> userAccounts = SettingConnect.Select<UserAccount>("SELECT * FROM UserAccount WHERE Name LIKE '%"+keyword+"%'");
            Session["datasearch"] = userAccounts;
           return Json(userAccounts,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getid(int idInput) {
            Console.WriteLine(idInput);
            Session["iduseraccount"] = idInput;
            List<UserPermission> userPermissions = db.UserPermissions.Where(x => x.IdUser == idInput).ToList();
            List<Innerjoin> Model = new List<Innerjoin>();
          
                if (userPermissions.Count > 0)
                {
               Model = GETDATEWITHID(idInput);
                   

                }
                else
                {
                    List<Permission> permissions = db.Permissions.ToList();
                    // Create a new UserPermission object and set its properties
                    foreach (Permission per in permissions)
                    {
                        UserPermission newPermission = new UserPermission
                        {
                            IdUser = idInput, // Assuming IdUser is a property in your UserPermission entity
                            IdPermission = per.id, // Assuming IdPermission is a property in your UserPermission entity
                            IsRead = false,
                            IsDelete = false,
                            IsCreate = false,
                            IsEdit = false,
                        };
                        // Add the new permission to the DataContext
                        db.UserPermissions.InsertOnSubmit(newPermission);
                        // Submit changes to persist the new record to the database
                        db.SubmitChanges();
                    }
                Model = GETDATEWITHID(idInput);
            }
            // Sử dụng model ở đây (ví dụ: lưu vào session, truyền vào view, ...)



            TempData["data"] = Model;
            return Json(Model, JsonRequestBehavior.AllowGet);
        }
       public List<Innerjoin> GETDATEWITHID(int idInput)
        {
            List<Innerjoin> Model = new List<Innerjoin>();
            List<PermissionCategory> permissionCategories = db.PermissionCategories.ToList();
            if (permissionCategories != null)
            {
                foreach (PermissionCategory permissionCategory in permissionCategories)
                {
                    Innerjoin innerjoin = new Innerjoin();
                    innerjoin.Name = permissionCategory.Code;
                    List<Permissionse> permissionseslist = new List<Permissionse>();
                    List<Permission> permissions = db.Permissions.Where(x => x.IdPermissionCategory == permissionCategory.Id).ToList();
                    foreach (Permission permission in permissions)
                    {
                        Permissionse permissionse = new Permissionse();
                        UserPermission userPermission = db.UserPermissions.Where(x => x.IdUser == idInput && x.IdPermission == permission.id).SingleOrDefault();
                        permissionse.IsRead = (bool)userPermission.IsRead;
                        permissionse.IsCreate = (bool)userPermission.IsCreate;
                        permissionse.IsEdit = (bool)userPermission.IsEdit;
                        permissionse.IsDelete = (bool)userPermission.IsDelete;
                        permissionse.code = permission.Code;
                        permissionse.Name = permission.Name;
                        permissionse.id = permission.id;
                        permissionseslist.Add(permissionse);
                    }

                    innerjoin.Permissions = permissionseslist;
                    Model.Add(innerjoin);
                }
            }
            return Model;
        }

        [HttpPost]
        public ActionResult SaveChange(List<UserpermissionRes> Permissions)
        {
            int id = Convert.ToInt32(Session["iduseraccount"]);
            List<UserPermission> users = db.UserPermissions.Where(x=>x.IdUser == id).ToList();
            if (users != null)
            {
              foreach(UserpermissionRes userPermission in Permissions)
                {
                    UserPermission userPermissionUpdate = db.UserPermissions.FirstOrDefault(x=>x.IdPermission == userPermission.IdPermission && x.IdUser == id);
                    userPermissionUpdate.IsRead = userPermission.IsRead == 1 ? true:false;
                    userPermissionUpdate.IsDelete = userPermission.IsDelete == 1 ? true : false;
                    userPermissionUpdate.IsCreate = userPermission.IsCreate == 1 ? true : false;
                    userPermissionUpdate.IsEdit = userPermission.IsEdit == 1 ? true : false;
                    db.SubmitChanges();
                }
            }
          
            
         
            return View();
        }
      
    }
}