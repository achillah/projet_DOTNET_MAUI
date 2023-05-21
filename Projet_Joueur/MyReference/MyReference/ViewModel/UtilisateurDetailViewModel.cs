namespace MyReference.ViewModel;

[QueryProperty("User", "User")]
public partial class UtilisateurDetailViewModel : BaseViewModel
{

    public ObservableCollection<User> MyUsers { get; set; } = new();
    UserGestionService MyDBService = new();

    public UtilisateurDetailViewModel(UserGestionService MyDBService)
    {
        this.MyDBService = MyDBService;
        MyDBService.ConfigOutils();
    }

    [ObservableProperty]
    User user;



    /*[RelayCommand]
    public async Task AllerModifierUtilisateurPage(User user)
    {
        //await Shell.Current.DisplayAlert("Successfully Created!", "You can go back.", "OK");
        await Shell.Current.GoToAsync(nameof(ModifierUtilisateurPage), true, new Dictionary<string, object>
        {

            {"User", user }

        });
    }*/

    [RelayCommand]
    async Task ModifierUtilisateur()
    {
        // await ReadAccess();
        try
        {
            await MyDBService.UpdateUser(User.User_ID, User.UserName, User.UserPassword, User.UserAccessType);
            await Shell.Current.DisplayAlert("Utilisateur Modifié", "gooooo", "OK");

        }
        catch (Exception e)
        {

            await Shell.Current.DisplayAlert("Utilisateur non Modifié", e.Message, "OK");
        }

        await ReadAccess();
        RemplirDB();

    }

    [RelayCommand]
    async Task SupprimerUtilisateur()
    {
        await MyDBService.DeletetUser(User.UserName);
        await ReadAccess();
        RemplirDB();
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

        /////On va rajouter la connexion � la base de donn�es



        /// On ajoute les �l�ments de la DB 
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

        //MyUsers.Clear();

        ///on remplit la liste avec les �l�ments de la DB
        foreach (var user in usersTemp)
        {
            MyUsers.Add(user);
        }
        IsBusy = false;
    }


   
}