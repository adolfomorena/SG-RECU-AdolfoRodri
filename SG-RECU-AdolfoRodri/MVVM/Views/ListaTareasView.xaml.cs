using SG_RECU_AdolfoRodri.MVVM.ViewModels;

namespace SG_RECU_AdolfoRodri.MVVM.Views;

public partial class ListaTareasView : ContentPage
{
	public ListaTareasView()
	{
		InitializeComponent();
        BindingContext = new ListaTareasViewModel();
    }


    private void EditarTarea_Clicked(object sender, EventArgs e)
    {
    //    var tarea = ((ListaTareasViewModel)BindingContext).TareaSeleccionada;

    //    App.Current.MainPage.Navigation.PushAsync(new GestionTareaView
    //    {
    //        BindingContext = new GestionTareaViewModel
    //        {
    //            TareaSeleccionada = tarea
    //        }
    //    });
    }

    private void CrearTarea_Clicked(object sender, EventArgs e)
    {
        //App.Current.MainPage.Navigation.PushAsync(new GestionTareaView
        //{
        //    BindingContext = new GestionTareaViewModel()
        //});
    }

}