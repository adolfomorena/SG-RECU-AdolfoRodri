using PropertyChanged;
using SG_RECU_AdolfoRodri.MVVM.ViewModels;

namespace SG_RECU_AdolfoRodri.MVVM.Views;

public partial class GestionTareasView : ContentPage
{
	
	public GestionTareasView()
	{
		InitializeComponent();
        BindingContext = new GestionTareasViewModel();
	}
}