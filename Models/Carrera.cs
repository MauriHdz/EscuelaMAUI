using System;
using System.Collections.Generic;

namespace EscuelaApp.Models;

public partial class Carrera
{
    public int Id { get; set; }

    public string Clave { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Alumno> Alumno { get; set; } = new List<Alumno>();
}
