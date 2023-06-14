using MyReference.View;

namespace MyReference.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    //public ObservableCollection<User> MyUsers { get; set; } = new();
    UserGestionService MyDBService = new();
    public ObservableCollection<Joueur> MyJoueurs { get; set; } = new();

    [ObservableProperty]
    Boolean droit = false;

    public Joueur joueur;


    [ObservableProperty]
    string monTexte = "Go To Recherche Page";



    public HomeViewModel(UserGestionService MyDBService)
    {
        VerifieDroit();

        this.MyDBService = MyDBService;
        MyDBService.ConfigOutils();
    }

    public void VerifieDroit()
    {
        if (Globals.utilisateurConnecte.UserAccessType == 1)
        {
            Droit = true;
        }
        

    }

    ///Routing de Page
    [RelayCommand]
    public async Task AllerRecherchePage(string data)
    {
        await Shell.Current.GoToAsync(nameof(RechercheJoueurPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    async Task AllerJoueurPage(Joueur joueur)
    {
        if (joueur is null)
            return;

        await Shell.Current.GoToAsync(nameof(JoueurPage), true, new Dictionary<string, object>
        {

            {"Joueur", joueur }

        });
    }

    [RelayCommand]
    public async Task AllerAjouterJoueurPage()
    {
        await Shell.Current.GoToAsync(nameof(AjouterJoueurPage), true);
    }

    [RelayCommand]
    public async Task AllerExemplePage()
    {
        await Shell.Current.GoToAsync(nameof(ExemplePage), true);
    }

    [RelayCommand]
    public async Task AllerUtilisateurPage()
    {
        RemplirInfoDepuisDB();
        await Shell.Current.GoToAsync(nameof(UtilisateurPage), true);
    }

    public void RefreshList()
    {
        MyJoueurs.Clear();

        foreach (Joueur joueur in Globals.MyJoueurList)
        {
            MyJoueurs.Add(joueur);
        }
    }


}