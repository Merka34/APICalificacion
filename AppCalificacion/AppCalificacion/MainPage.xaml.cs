using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppCalificacion.Models;
using Xamarin.Forms;
using System.Threading;
using System.Diagnostics;

namespace AppCalificacion
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
        }

        public List<Calificacion> Calificacions = new List<Calificacion>();
        ObservableCollection<Calificacion> calificaciones = new ObservableCollection<Calificacion>();
        public ObservableCollection<Calificacion> Calificaciones { get { return calificaciones; } set { calificaciones = value; OnPropertyChanged(); } }
        public string JSON { get; set; } = "";
        List<string> ca = new List<string>();
        public string Enlace { get; set; }
        Stopwatch TiempoEspera = new Stopwatch();
        WebClient webClient = new WebClient();
        public bool Desconectar { get; set; }

        private void btnAcceder_Clicked(object sender, EventArgs e)
        {
        
        }

        private void Envio()
        {
            webClient.DownloadStringAsync(new Uri(Enlace));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JSON = e.Result;
                btnAcceder.IsEnabled = true;
            }
            catch (Exception)
            {
                txtError.TextColor = Color.Red;
                txtError.Text = "Problema de red, verifique su conexión a Internet o el servidor no este disponible en este momento";
            }

        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtError.Text = "";
        }

        private async void btnAcceder_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                txtNombre.IsReadOnly = true;
                txtPassword.IsReadOnly = true;
                btnAcceder.IsVisible = false;
                btnCancelar.IsVisible = true;
                Enlace = $"https://apicalificacion.conveyor.cloud/api/Account/alumno/{txtNombre.Text}/{txtPassword.Text}";
                JSON = "";
                txtError.TextColor = Color.Yellow;
                txtError.Text = "Validando logueo";
                Envio();
                TiempoEspera.Start();
                while (JSON == "" && !Desconectar)
                {
                    if (TiempoEspera.Elapsed.TotalSeconds >= 10)
                    {
                        TiempoEspera.Reset();
                        txtError.TextColor = Color.Red;
                        txtError.Text = "Se superio el tiempo de espera";
                        break;
                    }
                    await Task.Delay(10);
                }
                btnCancelar.IsVisible = false;
                if (Desconectar)
                {
                    txtError.TextColor = Color.Red;
                    txtError.Text = "Proceso cancelado por el usuario";
                    Desconectar = !Desconectar;
                }
                TiempoEspera.Reset();
                if (JSON != "")
                {
                    Usuarioalumno al = JsonConvert.DeserializeObject<Usuarioalumno>(JSON);
                    if (al.IdAlumno != 0)
                    {
                        Enlace = $"https://apicalificacion.conveyor.cloud/api/calificaciones/list/{al.IdAlumno}";
                        JSON = "";
                        txtError.Text = "Logueo con éxito, obteniendo calificaciones";
                        Envio();
                        while (JSON == "")
                        {
                            await Task.Delay(10);
                        }
                        List<Calificacion> listCalificaciones = JsonConvert.DeserializeObject<List<Calificacion>>(JSON).ToList();
                        txtError.TextColor = Color.Green;
                        txtError.Text = "Calificaciones obtenidas con éxito";

                        ca.Add($"|    P1    |    P2    |    P3    |    Pf    |    Materia    |");
                        foreach (var item in listCalificaciones)
                        {
                            ca.Add($"|    {item.P1}    |    {item.P2}    |    {item.P3}    |    {item.Pf}   |   {item.IdNavigation.IdNombreMateriaNavigation.NombreMateria1}   |");

                        }
                        lstCalificaciones.ItemsSource = ca;
                        btnDesconectar.IsVisible = true;
                    }
                    else
                    {
                        txtError.TextColor = Color.Red;
                        txtError.Text = "Nombre y/o contraseña incorrectos";
                    }
                }
                if (!btnDesconectar.IsVisible)
                {
                    btnAcceder.IsVisible = true;
                    txtNombre.IsReadOnly = false;
                    txtPassword.IsReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Desconectar = !Desconectar;
        }

        private void btnDesconectar_Clicked(object sender, EventArgs e)
        {
            btnDesconectar.IsVisible = false;
            btnAcceder.IsVisible = true;
            txtNombre.IsReadOnly = false;
            txtPassword.IsReadOnly = false;
            ca.Clear();
            lstCalificaciones.ItemsSource = null;
            txtError.TextColor = Color.Green;
            txtError.Text = "Se ha deslogueado con éxito";
        }
    }
}
