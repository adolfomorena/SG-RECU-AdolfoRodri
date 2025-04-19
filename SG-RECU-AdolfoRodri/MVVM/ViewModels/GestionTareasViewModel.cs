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
    public class GestionTareasViewModel
    {
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


        private string _nomTarea = string.Empty;


        public string desc { get; set; } = string.Empty;

        public bool Estado { get; set; } = false;

        public string prio { get; set; } = string.Empty;


        public string NomTarea { get; set; } = string.Empty;


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
                return !string.IsNullOrWhiteSpace(NomTarea) && !string.IsNullOrWhiteSpace(prio);
            });
        }

        async public void GuardarTarea()
        {
            Tarea existe = App.TareaRepo.GetItem(tarea => tarea.Titulo == NomTarea);

            Tarea nuevaTarea = new Tarea
            {
                Titulo = NomTarea,
                Descripcion = desc,
                Prioridad = prio,
                Estado = Estado
            };

            List<Etiqueta> etiquetasList = new List<Etiqueta>();



            if (existe == null)
            {
                App.TareaRepo.SaveItem(nuevaTarea);

                foreach (var item in ItemsEtiqueta.Where(x => x.Seleccionado))
                {
                    TareaEtiqueta nuevaEtiqueta = new TareaEtiqueta
                    {
                        TareaId = nuevaTarea.Id,
                        EtiquetaId = item.Etiqueta.Id
                    };

                    App.TareaEtiquetaRepo.SaveItem(nuevaEtiqueta);
                    nuevaTarea.Etiquetas.Add(item.Etiqueta);

                }

                await Application.Current.MainPage.DisplayAlert("Guardado", "Tarjeta guardada", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La tarea ya existe", "Ok");
            }
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

