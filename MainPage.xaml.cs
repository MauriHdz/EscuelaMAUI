using EscuelaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApp;

public partial class MainPage : ContentPage
{
	public MainPage() => InitializeComponent();

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await CargarAsync();
	}

	private async Task CargarAsync()
	{
		try
		{
			LblEstado.Text = "Cargando...";

			using var db = new EscuelaMauiContext();

			var alumnos = await db.Alumno
								  .Include(a => a.IdCarreraNavigation)
								  .OrderBy(a => a.Apellidos)
								  .ToListAsync();

			Lista.ItemsSource = alumnos;
			LblEstado.Text = $"{alumnos.Count} alumnos";
		}
		catch (Exception ex)
		{
			LblEstado.Text = "Error de conexion";
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	private async void OnAlumnoSeleccionado(object? sender,SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.FirstOrDefault() is not Alumno alumno) return;
		Lista.SelectedItem = null;
		await Navigation.PushAsync(new DetallePage(alumno.NoControl));
	}
}