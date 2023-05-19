namespace MyReference.View;

public partial class RechercheJoueurPage : ContentPage
{
	public RechercheJoueurPage(RechercheJoueurViewModel rechercheJoueurViewModel)
	{
		InitializeComponent();
		BindingContext= rechercheJoueurViewModel;
	}
}