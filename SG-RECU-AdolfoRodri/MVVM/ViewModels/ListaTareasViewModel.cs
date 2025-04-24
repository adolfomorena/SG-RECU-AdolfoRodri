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
        public ObservableCollection<Tarea> Tareas { get; set; } = new ObservableCollection<Tarea>();

        public ICommand CambiarEstadoCommand { get; private set; }
        public ICommand EditarTareaCommand { get; private set; }
        public ICommand CrearTareaCommand { get; private set; }


        public ListaTareasViewModel()
        {
            RefreshView();
            CambiarEstadoCommand = new Command(CambiarEstado);
            EditarTareaCommand = new Command(EditarTarea);
            CrearTareaCommand = new Command(CrearTarea);
        }

        private void CambiarEstado()
        {
            if (TareaSeleccionada != null)
            {
                if (!TareaSeleccionada.Estado)
                {
                    TareaSeleccionada.Estado = true;
                    App.TareaRepo.SaveItemCascada(TareaSeleccionada);
                    App.Current.MainPage.DisplayAlert("Completada", "Tarea completada", "Ok");
                }
                else
                {
                    TareaSeleccionada.Estado = false;
                    App.TareaRepo.SaveItemCascada(TareaSeleccionada);
                    App.Current.MainPage.DisplayAlert("Pendiente", "Tarea marcada como pendiente", "Ok");
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

        private void RefreshView()
        {
            if (Tareas == null)
            {
                Tareas = new ObservableCollection<Tarea>();
            }
            else
            {
                Tareas.Clear();
            }

            var tareas = App.TareaRepo.GetItemsCascada();
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }
    }
}
