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
        private void RefreshView()
        {
            Tareas = new ObservableCollection<Tarea>(App.TareaRepo.GetItemsCascada());
        }
    }
}
