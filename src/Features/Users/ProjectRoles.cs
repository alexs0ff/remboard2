using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Users
{
    public enum ProjectRoles:long
    {
        [Description("Администратор")]
        Admin=1,
        [Description("Менеджер")]
        Manager = 2,
        [Description("Инженер")]
        Engineer = 3
        
    }
}
