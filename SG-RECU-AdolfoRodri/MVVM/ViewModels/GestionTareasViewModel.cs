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
    public class GestionTareasViewModel
    {
        public Tarea TareaSeleccionada { get; set; }
        public ObservableCollection<Etiqueta> EtiquetasDisponibles { get; set; } = new ObservableCollection<Etiqueta>();

        public ICommand VolverCommand { get; private set; }
        public ICommand GuardarCommand { get; private set; }
        public ICommand GestionEtiquetasCommand { get; private set; }

        public GestionTareasViewModel(Tarea tarea)
        {
            TareaSeleccionada = tarea;
            VolverCommand = new Command(Volver);
            GuardarCommand = new Command(Guardar);
            GestionEtiquetasCommand = new Command(GestionEtiquetas);
            RefreshView();
        }
        public GestionTareasViewModel()
        {
            TareaSeleccionada = new Tarea();
            VolverCommand = new Command(Volver);
            GuardarCommand = new Command(Guardar);
            GestionEtiquetasCommand = new Command(GestionEtiquetas);
            RefreshView();
        }

        private void Volver()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        // can execute y guardar bien las etiquetas
        private void Guardar()
        {
            App.TareaRepo.SaveItemCascada(TareaSeleccionada);
            App.Current.MainPage.DisplayAlert("Creada", "Etiqueta creada correctamente", "Ok");

            App.Current.MainPage.Navigation.PopAsync();
        }
        private void GestionEtiquetas()
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionEtiquetasView
            {
                BindingContext = new GestionEtiquetasViewModel()
            });
        }

        private void RefreshView()
        {
            EtiquetasDisponibles = new ObservableCollection<Etiqueta>(App.EtiquetaRepo.GetItems());
        }
    }
}

