namespace MyReference.View;

public partial class ConnexionPage : ContentPage
{
	public ConnexionPage(ConnexionViewModel connexionViewModel)
	{
		InitializeComponent();
		BindingContext = connexionViewModel;
	}
}