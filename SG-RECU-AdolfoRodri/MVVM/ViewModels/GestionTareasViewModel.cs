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

        public GestionTareasViewModel(Tarea tarea)
        {
            TareaSeleccionada = tarea;
            RefreshView();
        }
        public GestionTareasViewModel()
        {
            TareaSeleccionada = new Tarea();
            RefreshView();
        }
        private void RefreshView()
        {
            EtiquetasDisponibles = new ObservableCollection<Etiqueta>(App.EtiquetaRepo.GetItems());
        }
    }
}

