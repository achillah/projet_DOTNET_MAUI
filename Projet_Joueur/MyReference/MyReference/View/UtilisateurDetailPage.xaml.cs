namespace MyReference.View;

public partial class UtilisateurDetailPage : ContentPage
{
	public UtilisateurDetailPage(UtilisateurDetailViewModel utilisateurDetailViewModel)
	{
		InitializeComponent();
		BindingContext = utilisateurDetailViewModel;
	}
}