using SG_RECU_AdolfoRodri.MVVM.Models;
using SG_RECU_AdolfoRodri.MVVM.Views;
using SQLiteEjemplo.Repositories;

namespace SG_RECU_AdolfoRodri
{
    public partial class App : Application
    {
        public static BaseRepository<Tarea> TareaRepo { get; private set; }
        public static BaseRepository<Etiqueta> EtiquetaRepo { get; private set; }

        public static BaseRepository<TareaEtiqueta> TareaEtiquetaRepo { get; private set; }
        public App(BaseRepository<Tarea> objTareaRepo, BaseRepository<Etiqueta> objEtiquetaRepo,
            BaseRepository<TareaEtiqueta> objTareaEtiquetaRepo)
        {
            InitializeComponent();
            TareaRepo = objTareaRepo;
            EtiquetaRepo = objEtiquetaRepo;
            TareaEtiquetaRepo = objTareaEtiquetaRepo;
            
            MainPage = new NavigationPage(new ListaTareasView());
        }
    }
}
