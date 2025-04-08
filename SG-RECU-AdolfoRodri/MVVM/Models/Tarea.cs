using PropertyChanged;
using SG_RECU_AdolfoRodri.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RECU_AdolfoRodri.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    [Table("Tareas")]
    
    public class Tarea: TableData
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;
        public string Prioridad { get; set; } = string.Empty;

        //[ManyToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Etiqueta> Etiquetas { get; set; } = new ObservableCollection<Etiqueta>();
    }
}
