using SG_RECU_AdolfoRodri.MVVM.ViewModels;

namespace SG_RECU_AdolfoRodri.MVVM.Views;

public partial class GestionEtiquetasView : ContentPage
{
	public GestionEtiquetasView()
	{
		InitializeComponent();
        BindingContext=new GestionEtiquetasViewModel();
	}

    void Entry_TextChangued(object sender, TextChangedEventArgs e)
    {
        ((GestionEtiquetasViewModel)BindingContext).NotificarCambio();
    }
}