using SG_RECU_AdolfoRodri.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RECU_AdolfoRodri.MVVM.Models
{
    [Table("TareaEtiqueta")]
    public class TareaEtiqueta: TableData
    {
        [ForeignKey(typeof(Tarea))]
        public int TareaId { get; set; }

        [ForeignKey(typeof(Etiqueta))]
        public int EtiquetaId { get; set; }
    }
}
