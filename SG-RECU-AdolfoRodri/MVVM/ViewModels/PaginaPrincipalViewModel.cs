using PropertyChanged;
using SG_RECU_AdolfoRodri.MVVM.Models;
using SG_RECU_AdolfoRodri.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SG_RECU_AdolfoRodri.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PaginaPrincipalViewModel
    
   {

        public ICommand CambiarEstadoCommand => new Command(() =>
        {
            CambiarEstado();
        });

        public ICommand BorrarTareaCommand { get; set; }

        public ICommand AgregarTareaCommand { get; private set; }
        public ICommand EditarTareaCommand => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionTareasView
            {
                BindingContext = new GestionTareasViewModel(TareaSeleccionada)
            });
        });
        public ICommand RefreshCommand => new Command(() =>
        {
            RefreshView();
        });

        public Tarea TareaSeleccionada { get; set; } = new Tarea();

        public ObservableCollection<Tarea> Tareas { get; set; }
        public PaginaPrincipalViewModel() {
            BorrarTareaCommand = new Command(BorrarTarea);
            AgregarTareaCommand = new Command(CrearTarea);
            Tareas = new ObservableCollection<Tarea>();
            CargarTareas();
        }
        private void CargarTareas()
        {
            var tareas = App.TareaRepo.GetItemsCascada();

            Tareas.Clear();

            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }
        async public void CambiarEstado()
        {
            if (TareaSeleccionada != null)
            {
                if (!TareaSeleccionada.Estado)
                {
                    TareaSeleccionada.Estado = true;
                    App.TareaRepo.SaveItemCascada(TareaSeleccionada);
                    await Application.Current.MainPage.DisplayAlert("Completada", "Tarea completada", "Ok");
                }
                else
                {
                    TareaSeleccionada.Estado = false;
                    App.TareaRepo.SaveItemCascada(TareaSeleccionada);
                    await Application.Current.MainPage.DisplayAlert("Pendiente", "Tarea marcada como pendiente", "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Pendiente", "No hay tarea seleccionada", "Ok");
            }
        }
        async public void CrearTarea()
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionTareasView());
        }
        async public void BorrarTarea()
        {
            Tarea existe = App.TareaRepo.GetItem(t => t.Id.Equals(TareaSeleccionada.Id));
            if (existe != null)
            {
                App.TareaRepo.DeleteItem(TareaSeleccionada);
                await Application.Current.MainPage.DisplayAlert("Borrada", "Tarea borrada correctamente", "Ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Esta tarea ya no existe", "Ok");
            }
        }
        async public void RefreshView()
        {
            var tareas = App.TareaRepo.GetItemsCascada();

            Tareas.Clear();

            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }
    }

}
