using SG_RECU_AdolfoRodri.MVVM.ViewModels;

namespace SG_RECU_AdolfoRodri.MVVM.Views;

public partial class PaginaPrincipalView : ContentPage
{
	public PaginaPrincipalView()
	{
		InitializeComponent();
		BindingContext = new PaginaPrincipalViewModel();
    }
}