using System;
using System.Collections.Generic;

namespace EscuelaApp.Models;

public partial class Alumno
{
    public string NoControl { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public int Semestre { get; set; }

    public int IdCarrera { get; set; }

    public virtual ICollection<Calificacion> Calificacion { get; set; } = new List<Calificacion>();

    public virtual Carrera IdCarreraNavigation { get; set; } = null!;
}
