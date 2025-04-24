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

    //deshabilitar el btn para volver atras
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}