using MyReference.View;

namespace MyReference;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        Routing.RegisterRoute(nameof(JoueurPage), typeof(JoueurPage));
        Routing.RegisterRoute(nameof(AjouterJoueurPage), typeof(AjouterJoueurPage));
        Routing.RegisterRoute(nameof(ModifierJoueurPage), typeof(ModifierJoueurPage));
        Routing.RegisterRoute(nameof(RechercheJoueurPage), typeof(RechercheJoueurPage));
        Routing.RegisterRoute(nameof(InscriptionPage), typeof(InscriptionPage));
        Routing.RegisterRoute(nameof(UtilisateurPage), typeof(UtilisateurPage));
        
        Routing.RegisterRoute(nameof(UtilisateurDetailPage), typeof(UtilisateurDetailPage));
    }
}
