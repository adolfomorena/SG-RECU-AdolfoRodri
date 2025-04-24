using PropertyChanged;
using SG_RECU_AdolfoRodri.MVVM.Models;
using SG_RECU_AdolfoRodri.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SG_RECU_AdolfoRodri.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ListaTareasViewModel
    {
        public Tarea TareaSeleccionada { get; set; } = new Tarea();
        public ObservableCollection<Tarea> Tareas { get; set; }

        public ICommand CambiarEstadoCommand { get; private set; }
        public ICommand EditarTareaCommand { get; private set; }
        public ICommand CrearTareaCommand { get; private set; }

        public ICommand AgregarTareaCommand => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionTareasView());
        });

      

        public ListaTareasViewModel()
        {
            CambiarEstadoCommand = new Command(CambiarEstado);
            EditarTareaCommand = new Command(EditarTarea);
            CrearTareaCommand = new Command(CrearTarea);


            Tareas = new ObservableCollection<Tarea>();
            CargarTareas();
            
        }

        async private void CambiarEstado()
        {
            if (TareaSeleccionada != null)
            {
                if (!TareaSeleccionada.Estado)
                {
                    TareaSeleccionada.Estado = true;
                    App.TareaRepo.SaveItemCascada(TareaSeleccionada);
                    Application.Current.MainPage.DisplayAlert("Completada", "Tarea completada", "Ok");
                }
                else
                {
                    TareaSeleccionada.Estado = false;
                    App.TareaRepo.SaveItemCascada(TareaSeleccionada);
                    Application.Current.MainPage.DisplayAlert("Pendiente", "Tarea marcada como pendiente", "Ok");
                }
            }
        }

        private void EditarTarea()
        {
            if (TareaSeleccionada != null)
            {
                App.Current.MainPage.Navigation.PushAsync(new GestionTareasView
                {
                    BindingContext = new GestionTareasViewModel(TareaSeleccionada)
                });
            }
            
        }

        private void CrearTarea()
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionTareasView
            {
                BindingContext = new GestionTareasViewModel()
            });
        }

        async public void RefreshView()
        {
            Tareas.Clear();
            List<Tarea> tareasDesdeBD = App.TareaRepo.GetItems();
            foreach (var tarea in tareasDesdeBD)
            {
                Tareas.Add(tarea);
            }
        }
        private void CargarTareas()
        {
            var tareasDesdeDb = App.TareaRepo.GetItemsCascada();

            Tareas.Clear();

            foreach (var tarea in tareasDesdeDb)
            {
                Tareas.Add(tarea);
            }
        }
    }
}
