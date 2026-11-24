using EscuelaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApp;

public partial class DetallePage : ContentPage
{
    private readonly string _noControl;

    public DetallePage(string noControl)
    {
        InitializeComponent();
        _noControl = noControl;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarAsync();
    }

    private async Task CargarAsync()
    {
        using var db = new EscuelaMauiContext();

        // Las tres tablas en una sola consulta 
        var alumno = await db.Alumno
                             .Include(a => a.IdCarreraNavigation)
                             .Include(a => a.Calificacion)
                             .FirstOrDefaultAsync(a => a.NoControl == _noControl);
        if (alumno is null) return;

        LblNombre.Text = alumno.NombreCompleto;
        LblCarrera.Text = alumno.IdCarrera.CompareTo(0) == 0
            ? "Sin carrera"
            : alumno.IdCarreraNavigation.Nombre;

        var califs = alumno.Calificacion.OrderBy(c => c.Materia).ToList();
        ListaCalif.ItemsSource = califs;

        LblPromedio.Text = califs.Count == 0
            ? "Sin promedio"
            : $"Promedio: {califs.Average(c => c.Valor):0.0}";
    }

    private async void OnAgregarClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtMateria.Text)
            || !int.TryParse(TxtParcial.Text, out int parcial)
            || !decimal.TryParse(TxtValor.Text, out decimal valor))
        {
            await DisplayAlert("Faltan datos", "Llena los tres campos.", "OK");
            return;
        }

        using var db = new EscuelaMauiContext();

        db.Calificacion.Add(new Calificacion
        {
            NoControl = _noControl,
            Materia = TxtMateria.Text.Trim(),
            Parcial = parcial,
            Valor = valor
        });

        await db.SaveChangesAsync();

        TxtMateria.Text = TxtParcial.Text = TxtValor.Text = "";
        await CargarAsync();
    }
}