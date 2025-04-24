using PropertyChanged;
using SG_RECU_AdolfoRodri.MVVM.Models;
using SG_RECU_AdolfoRodri.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SG_RECU_AdolfoRodri.MVVM.ViewModels
{ 
    public class GestionTareasViewModel
    {
        public Tarea TareaSeleccionada { get; set; }
        public ObservableCollection<ItemEtiqueta> EtiquetasDisponibles { get; set; } = new ObservableCollection<ItemEtiqueta>();

        public ICommand VolverCommand { get; private set; }

        private Command _guardarCommand;
        public ICommand GuardarCommand => _guardarCommand;
        public ICommand GestionEtiquetasCommand { get; private set; }

        public GestionTareasViewModel(Tarea tarea)
        {
            TareaSeleccionada = tarea;
            VolverCommand = new Command(Volver);

            _guardarCommand = new Command(execute: () =>
            {
                Guardar();
            }, canExecute: () =>
            {
                return !string.IsNullOrWhiteSpace(TareaSeleccionada.Titulo)
                && !string.IsNullOrWhiteSpace(TareaSeleccionada.Descripcion)
                && !string.IsNullOrWhiteSpace(TareaSeleccionada.Prioridad);
            });

            GestionEtiquetasCommand = new Command(GestionEtiquetas);
            RefreshView();
        }

        public GestionTareasViewModel()
        {
            TareaSeleccionada = new Tarea();
            VolverCommand = new Command(Volver);
            _guardarCommand = new Command(execute: () =>
            {
                Guardar();
            }, canExecute: () =>
            {
                return !string.IsNullOrWhiteSpace(TareaSeleccionada.Titulo)
                && !string.IsNullOrWhiteSpace(TareaSeleccionada.Descripcion)
                && !string.IsNullOrWhiteSpace(TareaSeleccionada.Prioridad);
            });
            
            GestionEtiquetasCommand = new Command(GestionEtiquetas);
            RefreshView();
        }

        private void Volver()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        private void Guardar()
        {
            if (TareaSeleccionada.Etiquetas == null)
            {
                TareaSeleccionada.Etiquetas = new ObservableCollection<Etiqueta>();
            }
            else
            {
                TareaSeleccionada.Etiquetas.Clear();
            }

            foreach ( var item in EtiquetasDisponibles)
            {
                if (item.Seleccionado)
                {
                    TareaSeleccionada.Etiquetas.Add(item.Etiqueta);
                }
            }
            App.TareaRepo.SaveItemCascada(TareaSeleccionada);
            App.Current.MainPage.DisplayAlert("Tarea guardada", "La tarea se ha guardada correctamente", "Ok");
            App.Current.MainPage.Navigation.PopAsync();

        }
        private void GestionEtiquetas()
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionEtiquetasView
            {
                BindingContext = new GestionEtiquetasViewModel()
            });
        }

        public void NotificarCanExecute()
        {
            _guardarCommand.ChangeCanExecute();
        }

        private void RefreshView()
        {
            var etiquetasDisponibles = App.EtiquetaRepo.GetItems();
            EtiquetasDisponibles.Clear();

            foreach (var etiqueta in etiquetasDisponibles)
            {
                EtiquetasDisponibles.Add(new ItemEtiqueta
                {
                    Etiqueta = etiqueta,
                    //Si es null, devuelve automáticamente null y evita lanzar una excepción.
                    Seleccionado = TareaSeleccionada?.Etiquetas.Any(e => e.Id == etiqueta.Id) == true
                });
            }
        }
    }
}

