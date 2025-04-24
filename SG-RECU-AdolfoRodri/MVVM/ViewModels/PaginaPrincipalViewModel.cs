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
        public ICommand EditarTareaCommand { get; private set; }
        public bool IsRefreshing { get; set; }
        public ICommand RefreshCommand { get; private set; }

        public Tarea TareaSeleccionada { get; set; } = new Tarea();
        public ObservableCollection<Tarea> Tareas { get; set; }

        public PaginaPrincipalViewModel() {
            BorrarTareaCommand = new Command(BorrarTarea);
            AgregarTareaCommand = new Command(CrearTarea);
            EditarTareaCommand = new Command(EditarTarea);

            RefreshCommand = new Command(Refrescar);
            Tareas = new ObservableCollection<Tarea>();
            RefreshView();
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
            else
            {
                App.Current.MainPage.DisplayAlert("Pendiente", "No hay tarea seleccionada", "Ok");
            }
        }
        private void CrearTarea()
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

        private void EditarTarea()
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionTareasView
            {
                BindingContext = new GestionTareasViewModel(TareaSeleccionada)
            });
        }

        private void Refrescar()
        {
            IsRefreshing = true;
            RefreshView();
            IsRefreshing = false;
        }

        private void RefreshView()
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
