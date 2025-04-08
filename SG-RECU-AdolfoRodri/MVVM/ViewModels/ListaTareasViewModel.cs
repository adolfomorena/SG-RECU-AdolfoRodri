using SG_RECU_AdolfoRodri.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void RefreshView()
        {
            Tareas = new ObservableCollection<Tarea>(App.TareaRepo.GetItemsCascada());
        }
    }
}
