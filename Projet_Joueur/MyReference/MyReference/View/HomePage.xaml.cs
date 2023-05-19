namespace MyReference.View;

public partial class HomePage : ContentPage
{
	HomeViewModel viewModel;
	public HomePage(HomeViewModel viewModel)
	{
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;

    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        viewModel.RefreshList();    // Réinitialise la observablecollection
        BindingContext = viewModel;
    }
}