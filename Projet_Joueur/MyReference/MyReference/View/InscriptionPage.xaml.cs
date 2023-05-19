using MyReference.ViewModel;

namespace MyReference.View;

public partial class InscriptionPage : ContentPage
{
	public InscriptionPage(InscriptionViewModel inscriptionViewModel)
	{
		InitializeComponent();
		BindingContext = inscriptionViewModel;
	}
}