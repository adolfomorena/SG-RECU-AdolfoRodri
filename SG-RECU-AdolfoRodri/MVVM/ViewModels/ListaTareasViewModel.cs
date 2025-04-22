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
    public class ListaTareasViewModel
    {
        public Tarea TareaSeleccionada { get; set; } = new Tarea();
        public ObservableCollection<Tarea> Tareas { get; set; } = new ObservableCollection<Tarea>();

       

        public ListaTareasViewModel()
        {
            RefreshView();
           
        }
        public ICommand EditarTareaCommand => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(
                new GestionTareasView
                {
                    BindingContext = new GestionTareasViewModel
                    {
                        TareaSeleccionada = TareaSeleccionada
                    }
                }
            );
        });
        public ICommand AgregarTareaCommand => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionTareasView());
        });
        public ICommand RefrescarCommand => new Command(() =>
        {
            RefreshView();
        });

        public ICommand BorrarTareaCommand => new Command(() =>
        {
            BorrarTarea();
        });
        public void RefreshView()
        {
            Tareas = new ObservableCollection<Tarea>(App.TareaRepo.GetItemsCascada());
        }
        async public void BorrarTarea()
        {
            App.TareaRepo.DeleteItem(TareaSeleccionada);
            await Application.Current.MainPage.DisplayAlert("Eliminada", "Tarea eliminada correctamente", "Ok");
        }
    }
}
