using System.ComponentModel;

namespace EscalaSistema.API.Enum;

public enum UserRoleEnum
{
    [Description("Leader")]
    Leader = 1,
    [Description("Musician")]
    Musician = 2,
    [Description("Administrator")]
    Administrator = 3,

}
