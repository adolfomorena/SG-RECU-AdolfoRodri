using SG_RECU_AdolfoRodri.MVVM.Models;
using SG_RECU_AdolfoRodri.MVVM.Views;
using SQLiteEjemplo.Repositories;

namespace SG_RECU_AdolfoRodri
{
    public partial class App : Application
    {
        public static BaseRepository<Tarea> TareaRepo { get; private set; }
        public static BaseRepository<Etiqueta> EtiquetaRepo { get; private set; }
        public App(BaseRepository<Tarea> objTareaRepo, BaseRepository<Etiqueta> objEtiquetaRepo)
        {
            TareaRepo = objTareaRepo;
            EtiquetaRepo = objEtiquetaRepo;
            InitializeComponent();

            MainPage = new NavigationPage(new ListaTareasView());
        }
    }
}
