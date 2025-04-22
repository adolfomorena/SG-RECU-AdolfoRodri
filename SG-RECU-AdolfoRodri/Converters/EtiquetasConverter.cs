using SG_RECU_AdolfoRodri.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SG_RECU_AdolfoRodri.Converters
{
    public class EtiquetasConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var etiquetas = value as ObservableCollection<Etiqueta>;

            if (etiquetas == null || etiquetas.Count == 0)
                return "Sin etiquetas";

            else
            {
                return string.Join(", ", etiquetas.Select(e => e.Nombre));
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
