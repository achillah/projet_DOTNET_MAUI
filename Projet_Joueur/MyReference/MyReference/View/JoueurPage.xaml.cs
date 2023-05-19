namespace MyReference.View;

public partial class JoueurPage : ContentPage
{
    JoueurViewModel viewModel;
    public JoueurPage(JoueurViewModel joueurViewModel)
	{
        this.viewModel = joueurViewModel;
		InitializeComponent();
		BindingContext = joueurViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        BindingContext = null;
        BindingContext = viewModel;
    }


}