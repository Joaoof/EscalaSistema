using System.ComponentModel;

namespace EscalaSistema.API.Enum;

public enum ScaleConfirmedEnum
{
    [Description("Rascunho")]
    Draft = 0,
    [Description("Publicado")]
    Published = 1,
    [Description("Cancelado")]
    Canceled = 2
}
