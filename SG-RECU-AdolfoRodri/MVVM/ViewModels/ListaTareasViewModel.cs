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

        public ICommand CambiarEstadoCommand { get; private set; }
        public ICommand EditarTareaCommand { get; private set; }
        public ICommand CrearTareaCommand { get; private set; }

        public ListaTareasViewModel()
        {
            CambiarEstadoCommand = new Command<Tarea>(CambiarEstado);
            EditarTareaCommand = new Command(EditarTarea);
            CrearTareaCommand = new Command(CrearTarea);
            RefreshView();
        }

        private void CambiarEstado(Tarea tarea)
        {
            if (tarea != null && !tarea.Estado)
            {
                tarea.Estado = true;
                App.TareaRepo.SaveItem(tarea);
                RefreshView();
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
            Tareas = new ObservableCollection<Tarea>(App.TareaRepo.GetItemsCascada());
        }
    }
}
