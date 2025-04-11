using PropertyChanged;
using SG_RECU_AdolfoRodri.MVVM.Models;
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
    public class GestionEtiquetasViewModel
    {
        public ObservableCollection<Etiqueta> ListaEtiquetas { get; set; } = new ObservableCollection<Etiqueta>();

        public Etiqueta EtiquetaSeleccionada { get; set; } = new Etiqueta();

        private Command _guardarEtiqueta;

        private Command _eliminarEtiqueta;

        public string Nombre { get; set; } = string.Empty;
        public ICommand GuardarEtiquetaCommand => _guardarEtiqueta;

        public ICommand EliminarEtiquetaCommand => _eliminarEtiqueta;



        public GestionEtiquetasViewModel(){

            ListaEtiquetas.Clear();
            GetEtiquetas();

            _guardarEtiqueta = new Command(execute: () =>
            {
                GuardarEtiqueta();
            }, canExecute: () =>
            {
                return Nombre.Length > 0;
            });

            _eliminarEtiqueta = new Command(execute: () =>
            {
                EliminarEtiqueta();
            }, canExecute: () =>
            {
                return EtiquetaSeleccionada != null;
            });
        }
        public void GetEtiquetas()
        {
            ListaEtiquetas.Clear();
            var etiquetas = App.EtiquetaRepo.GetItems();
            foreach (var etiqueta in etiquetas)
            {
                ListaEtiquetas.Add(etiqueta);
            }
        }
        async public void GuardarEtiqueta()
        {
            EtiquetaSeleccionada.Nombre = Nombre;
            App.EtiquetaRepo.SaveItem(EtiquetaSeleccionada);
            await Application.Current.MainPage.DisplayAlert("Creada", "Etiqueta creada correctamente", "Ok");
            Nombre = string.Empty;
            GetEtiquetas();
        }

        async public void EliminarEtiqueta()
        {
           
            //Opcion para ver si tiene tareas y borrar la relacion

            App.EtiquetaRepo.DeleteItem(EtiquetaSeleccionada);
            await Application.Current.MainPage.DisplayAlert("Eliminada", "Etiqueta eliminada correctamente", "Ok");
            EtiquetaSeleccionada = new Etiqueta();
            GetEtiquetas();
        }
        public void NotificarCambio()
        {
            _guardarEtiqueta.ChangeCanExecute();
        }

    }

    

}
