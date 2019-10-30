using System.ComponentModel;

namespace Entities
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
