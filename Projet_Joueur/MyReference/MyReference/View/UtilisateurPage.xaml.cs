namespace MyReference.View;

public partial class UtilisateurPage : ContentPage
{
    UtilisateurViewModel viewModel;
	public UtilisateurPage(UtilisateurViewModel utilisateurViewModel)
	{
		InitializeComponent();
		BindingContext = utilisateurViewModel;
        viewModel = utilisateurViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        viewModel.RemplirInfoDepuisDB();    // R�initialise la observablecollection
        BindingContext = viewModel;
    }
}