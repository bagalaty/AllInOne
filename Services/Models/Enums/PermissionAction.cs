using System;

namespace Services.Models.Enums
{
    [Flags]
    public enum PermissionAction
    {
        CreateOrUpdate = 0,
        View = 1,
        Create = 2,
        Update = 4,
        Delete = 8
    }
}