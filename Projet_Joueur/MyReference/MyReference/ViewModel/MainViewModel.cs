namespace MyReference.ViewModel;


public partial class MainViewModel : BaseViewModel
{
    //public ObservableCollection<User> MyUsers { get; set; } = new();
    UserGestionService MyDBService = new();

    public Boolean IsLoggedIn= false;


    [ObservableProperty]
    public string userNameLog;

    [ObservableProperty]
    public string userPasswordLog;
    
    public MainViewModel(UserGestionService MyDBService)
    {
        /////on cree les dataset, datatable en appelant le constructeur
        UserDonneesTables MyUserTables = new();
        this.MyDBService = MyDBService;
        MyDBService.ConfigOutils();

        //Globals.UserSet.Tables["Users"].Columns["UserName"] = "blabla";

    }

 
    async Task ChargerJson_AllerHomePage()
    {
       
        JoueurService MyService = new();

        try
        {
           
            Globals.MyJoueurList = await MyService.GetJoueurs();
        }
        catch (Exception ex)
        {
           
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
       
    }

    [RelayCommand]
    public async Task AllerInscriptionPage()
    {
        await Shell.Current.GoToAsync(nameof(InscriptionPage), true);
    }

    [RelayCommand]
    async Task Connexion_AllerHomePage()
    {
       
        await ReadAccess();
        RemplirInfoDepuisDB();

            foreach (var user in MyUsers)
            {
                if (UserNameLog == user.UserName )
                {

                    if(UserPasswordLog == user.UserPassword)
                    {
                    Globals.utilisateurConnecte = user;
                    Globals.connecte = true;
                    IsLoggedIn = true;
                    await ChargerJson_AllerHomePage();
                    await Shell.Current.GoToAsync(nameof(HomePage), true);

                    
                    }
                }

            }

            if (IsLoggedIn == false) 
        {
            await Shell.Current.DisplayAlert("Databse", "Utilisateur non trouvé", "OK");
        }




    }

    async Task ReadAccess()
    {

        Globals.UserSet.Tables["Users"].Clear();
        Globals.UserSet.Tables["Access"].Clear();
        try
        {
            await MyDBService.ReadAccessTable();
            await MyDBService.ReadUserTable();

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Databse", ex.Message, "OK");
        }
    }


   
}