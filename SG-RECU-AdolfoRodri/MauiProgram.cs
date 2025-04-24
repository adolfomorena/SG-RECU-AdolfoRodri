using Microsoft.Extensions.Logging;
using SG_RECU_AdolfoRodri.MVVM.Models;
using SQLiteEjemplo.Repositories;

namespace SG_RECU_AdolfoRodri
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<BaseRepository<Tarea>>();
            builder.Services.AddSingleton<BaseRepository<Etiqueta>>();
            builder.Services.AddSingleton<BaseRepository<TareaEtiqueta>>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
