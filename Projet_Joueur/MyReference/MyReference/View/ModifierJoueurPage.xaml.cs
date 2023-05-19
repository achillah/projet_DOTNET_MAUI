namespace MyReference.View;

public partial class ModifierJoueurPage : ContentPage
{
	public ModifierJoueurPage(ModifierJouerViewModel modifierJouerViewModel)
	{
		InitializeComponent();
		BindingContext = modifierJouerViewModel;
	}
}