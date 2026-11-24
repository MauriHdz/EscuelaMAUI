namespace EscuelaApp.Models; 
public partial class Alumno 
{ 
public string NombreCompleto => $"{Nombre} {Apellidos}"; 
}