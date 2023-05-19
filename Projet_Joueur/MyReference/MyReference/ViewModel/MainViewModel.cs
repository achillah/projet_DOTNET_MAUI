namespace MyReference.ViewModel;

/*[QueryProperty("userName", "userName")]
[QueryProperty("userPassword", "userPassword")]*/
public partial class MainViewModel : BaseViewModel
{
    public ObservableCollection<User> MyUsers { get; set; } = new();
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

   // [RelayCommand]
    async Task ChargerJson_AllerHomePage()
    {
        //if (IsBusy) return;

        JoueurService MyService = new();

        try
        {
            //   IsBusy = true;
            Globals.MyJoueurList = await MyService.GetJoueurs();
        }
        catch (Exception ex)
        {
            //Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        //finally { IsBusy = false; }

        //await Shell.Current.GoToAsync(nameof(HomePage), true);

    }

    [RelayCommand]
    async Task Connexion_AllerHomePage()
    {
        //if (IsBusy) return;
        await ReadAccess();
        RemplirDB();

            foreach (var user in MyUsers)
            {
                if (UserNameLog == user.UserName )
                {

                    if(UserPasswordLog == user.UserPassword)
                    {
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


    async void RemplirDB()
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