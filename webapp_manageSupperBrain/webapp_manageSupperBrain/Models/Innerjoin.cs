using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp_manageSupperBrain.Models
{
    public class Innerjoin
    {
        public string Name { get; set; }
        public string code { get; set; }
        public int iduserAccount { get; set; }
        public List<Permissionse> Permissions { get; set; }
       
    }
    public class Permissionse
    {
        public int id {  get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public bool IsRead { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
    public class UserpermissionRes
    {
        public int IdPermission { get; set; }
        public int IsRead { get; set; }
        public int IsCreate { get; set; }
        public int IsDelete { get; set; }

        public int IsEdit { get; set; }
    }
}