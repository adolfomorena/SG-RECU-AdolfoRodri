using PropertyChanged;
using SG_RECU_AdolfoRodri.MVVM.Models;
using SG_RECU_AdolfoRodri.MVVM.Views;
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

        public ICommand EditarTareaCommand => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(
                new GestionTareasView
                {
                    BindingContext = new GestionTareasViewModel
                    {
                        TareaSeleccionada = this.TareaSeleccionada
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

        public ICommand CambiarEstadoCommand => new Command(() =>
        {
            CambiarEstado();
        });

        public ListaTareasViewModel()
        {
            Tareas = new ObservableCollection<Tarea>(App.TareaRepo.GetItems());
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
        }

        async public void RefreshView()
        {
            Tareas.Clear();
            var tareas = App.TareaRepo.GetItemsCascada();
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }

        async public void BorrarTarea()
        {
            if (TareaSeleccionada != null)
            {
                App.TareaRepo.DeleteItem(TareaSeleccionada);
                await Application.Current.MainPage.DisplayAlert("Eliminada", "Tarea eliminada correctamente", "Ok");
                RefreshView();
            }
        }
    }
}
