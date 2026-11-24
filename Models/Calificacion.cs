using System;
using System.Collections.Generic;

namespace EscuelaApp.Models;

public partial class Calificacion
{
    public int CalificacionId { get; set; }

    public string NoControl { get; set; } = null!;

    public string Materia { get; set; } = null!;

    public int Parcial { get; set; }

    public decimal Valor { get; set; }

    public virtual Alumno NoControlNavigation { get; set; } = null!;
}
