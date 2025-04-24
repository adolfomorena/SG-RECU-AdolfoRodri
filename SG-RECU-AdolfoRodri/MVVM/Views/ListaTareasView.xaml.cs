using SG_RECU_AdolfoRodri.MVVM.ViewModels;

namespace SG_RECU_AdolfoRodri.MVVM.Views;

public partial class ListaTareasView : ContentPage
{
	public ListaTareasView()
	{
        InitializeComponent();
        BindingContext = new ListaTareasViewModel();
    }

}