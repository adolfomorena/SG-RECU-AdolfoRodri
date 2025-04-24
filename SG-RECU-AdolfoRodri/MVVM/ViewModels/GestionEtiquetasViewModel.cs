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
    public class GestionEtiquetasViewModel
    {
        public ObservableCollection<Etiqueta> ListaEtiquetas { get; set; } = new ObservableCollection<Etiqueta>();

        public ObservableCollection<TareaEtiqueta> ListaTareaEtiqueta { get; set; } = new ObservableCollection<TareaEtiqueta>();

        public Etiqueta EtiquetaSeleccionada { get; set; } = new Etiqueta();

        private Command _guardarEtiqueta;

        private Command _eliminarEtiqueta;

       
        public ICommand GuardarEtiquetaCommand => _guardarEtiqueta;

        public ICommand EliminarEtiquetaCommand => _eliminarEtiqueta;


        public ICommand VolverCommand => new Command(() =>
        {
            Volver();
        }); 



        public GestionEtiquetasViewModel(){

            ListaTareaEtiqueta.Clear();
            ListaEtiquetas.Clear();
            GetTareasEtiquetas();
            GetEtiquetas();

            _guardarEtiqueta = new Command(execute: () =>
            {
                GuardarEtiqueta();
            }, canExecute: () =>
            {
                return EtiquetaSeleccionada.Nombre.Length > 0;
            });

            _eliminarEtiqueta = new Command(execute: () =>
            {
                EliminarEtiqueta();
            }, canExecute: () =>
            {
                return EtiquetaSeleccionada.Id != 0;
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
        void Volver()
        {
            App.Current.MainPage.Navigation.PopToRootAsync();

        }
        public void GetTareasEtiquetas()
        {
            ListaTareaEtiqueta.Clear();
            var tareasEtiquetas = App.TareaEtiquetaRepo.GetItems();
            foreach (var tareaEtiqueta in tareasEtiquetas)
            {
                ListaTareaEtiqueta.Add(tareaEtiqueta);
            }
        }
        async public void GuardarEtiqueta()
        {
            App.EtiquetaRepo.SaveItem(EtiquetaSeleccionada);
            await Application.Current.MainPage.DisplayAlert("Creada", "Etiqueta creada correctamente", "Ok");
           
            EtiquetaSeleccionada = new Etiqueta();
            GetEtiquetas();
        }

        async public void EliminarEtiqueta()
        {
          
            var tareas= from ta in ListaTareaEtiqueta
                        where ta.EtiquetaId == EtiquetaSeleccionada.Id
                        select ta;

            if (tareas.Any())
            {
                App.EtiquetaRepo.DeleteItem(EtiquetaSeleccionada);
            }   

            App.EtiquetaRepo.DeleteItem(EtiquetaSeleccionada);
            await Application.Current.MainPage.DisplayAlert("Eliminada", "Etiqueta eliminada correctamente", "Ok");
            EtiquetaSeleccionada = new Etiqueta();
            GetEtiquetas();
            

        }
        public void NotificarCambio()
        {
            _guardarEtiqueta.ChangeCanExecute();  
            _eliminarEtiqueta.ChangeCanExecute();
        }

    }

    

}
