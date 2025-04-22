using PropertyChanged;
using SG_RECU_AdolfoRodri.MVVM.ViewModels;

namespace SG_RECU_AdolfoRodri.MVVM.Views;

public partial class GestionTareasView : ContentPage
{
	public GestionTareasView()
	{
		InitializeComponent();
        BindingContext=new GestionTareasViewModel();
	}
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {

        ((GestionTareasViewModel)BindingContext).NotificarCanExecute();
    }
    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        ((GestionTareasViewModel)BindingContext).NotificarCanExecute();

    }
}