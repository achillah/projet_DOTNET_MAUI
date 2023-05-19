namespace MyReference.View;

public partial class AjouterJoueurPage : ContentPage
{
	public AjouterJoueurPage(AjouterJoueurViewModel addJoueurViewModel)
	{
		InitializeComponent();
		BindingContext = addJoueurViewModel;
	}
}