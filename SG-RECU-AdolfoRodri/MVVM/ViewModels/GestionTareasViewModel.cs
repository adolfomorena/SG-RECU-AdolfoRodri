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

    [AddINotifyPropertyChangedInterface]
    public class GestionTareasViewModel
    {

        public Tarea TareaSeleccionada { get; set; } = new Tarea();
        public ObservableCollection<Tarea> Tareas { get; set; } = new ObservableCollection<Tarea>();
        public List<Etiqueta> Etiquetas { get; set; } = new List<Etiqueta>();

        public ObservableCollection<ItemEtiqueta> ItemsEtiqueta { get; set; } = new ObservableCollection<ItemEtiqueta>();

        public ICommand NavegarEtiquetasCommand => new Command(() =>
        {
            NavegarEtiquetas();
        });
        public ICommand VolverCommand => new Command(() =>
        {
            Volver();
        });
        public Collection<string> Prioridades { get; set; } = new Collection<string>
        {
            "ALTA",
            "MEDIA",
            "BAJA"
        };
        public string Prioridad { get; set; }





        private Command _guardarTarea;

        public ICommand GuardarTareaCommand => _guardarTarea;
        public GestionTareasViewModel()
        {
            GetEtiquetas();
            GetTareas();
            _guardarTarea = new Command(execute: () =>
            {
                GuardarTarea();
            }, canExecute: () =>
            {
                return TareaSeleccionada.Titulo != string.Empty && TareaSeleccionada.Prioridad != string.Empty;
            });
        }

        async public void GuardarTarea()
        {
            Tarea existe = App.TareaRepo.GetItem(t => t.Id == TareaSeleccionada.Id);



            if (existe == null)
            {
                Tarea nuevaTarea = new Tarea
                {
                    Titulo = TareaSeleccionada.Titulo,
                    Prioridad = TareaSeleccionada.Prioridad,
                    Etiquetas = new ObservableCollection<Etiqueta>()
                };
                foreach (var item in ItemsEtiqueta.Where(x => x.Seleccionado))
                {
                    Etiqueta etiquetaExistente = App.EtiquetaRepo.GetItem(e => e.Nombre == item.Etiqueta.Nombre);
                    nuevaTarea.Etiquetas.Add(etiquetaExistente);
                }
                App.TareaRepo.SaveItemCascada(nuevaTarea);

                await Application.Current.MainPage.DisplayAlert("Guardado", "Tarjeta guardada", "OK");
            }
            else
            {
                existe.Prioridad = TareaSeleccionada.Prioridad;
                existe.Etiquetas = new ObservableCollection<Etiqueta>();
                foreach (var item in ItemsEtiqueta.Where(x => x.Seleccionado))
                {
                    Etiqueta etiquetaExistente = App.EtiquetaRepo.GetItem(e => e.Nombre == item.Etiqueta.Nombre);
                    existe.Etiquetas.Add(etiquetaExistente);
                }

                App.TareaRepo.SaveItemCascada(existe);
                await Application.Current.MainPage.DisplayAlert("Actualizada", "Tarea actualizada correctamente", "OK");

            }
            TareaSeleccionada = new Tarea();
            foreach (var item in ItemsEtiqueta) item.Seleccionado = false;
        }
             public void NavegarEtiquetas()
        {
            App.Current.MainPage.Navigation.PushAsync(new GestionEtiquetasView());

        }
        void Volver()
        {
            App.Current.MainPage.Navigation.PushAsync(new ListaTareasView());

        }
        public void GetEtiquetas()
        {
            Etiquetas.Clear();
            ItemsEtiqueta.Clear();
            var etiquetas = App.EtiquetaRepo.GetItems();
            foreach (var etiqueta in etiquetas)
            {
                Etiquetas.Add(etiqueta);
                ItemsEtiqueta.Add(new ItemEtiqueta { Etiqueta = etiqueta, Seleccionado = false });
            }
        }
        public void GetTareas()
        {
            Tareas.Clear();
            var tareas = App.TareaRepo.GetItems();
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }
        public void NotificarCanExecute()
        {
            _guardarTarea.ChangeCanExecute();
        }
        }
    }

