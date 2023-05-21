using MyReference.View;

namespace MyReference.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    public ObservableCollection<User> MyUsers { get; set; } = new();
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
    public async Task AllerUtilisateurPage()
    {
        RemplirDB();
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

    public async void RemplirDB()
    {
        IsBusy = true;

        List<User> usersTemp = new();

        MyUsers.Clear();

        /////On va rajouter la connexion à la base de données



        /// On ajoute les éléments de la DB 
        try
        {
            usersTemp = Globals.UserSet.Tables["Users"].AsEnumerable().Select(m => new User()
            {
                User_ID = m.Field<Int16>("User_ID"),
                UserName = m.Field<string>("UserName"),
                UserPassword = m.Field<string>("UserPassword"),
                UserAccessType = m.Field<Int16>("UserAccessType"),
            }).ToList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Databse", ex.Message, "OK");
        }

        ///on remplit la liste avec les éléments de la DB
        foreach (var user in usersTemp)
        {
            MyUsers.Add(user);
        }
        IsBusy = false;
    }

}