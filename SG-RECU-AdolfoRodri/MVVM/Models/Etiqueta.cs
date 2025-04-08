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
    [Table("Etiquetas")]
    public class Etiqueta : TableData
    {
        [Unique]
        public string Nombre { get; set; } = string.Empty;

        [ManyToMany(typeof(TareaEtiqueta))]
        public int TareaId { get; set; }
    }
}
