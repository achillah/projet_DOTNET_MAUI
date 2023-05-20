namespace MyReference.ViewModel;

public partial class InscriptionViewModel : BaseViewModel
{

    public ObservableCollection<User> MyUsers { get; set; } = new();
    UserGestionService MyDBService = new();

    [ObservableProperty]
    string userNameInscription;

    [ObservableProperty]
    string userPasswordInscription;

    [ObservableProperty]
    Int32 userAccessInscription;


    public InscriptionViewModel(UserGestionService MyDBService)
    {
        this.MyDBService = MyDBService;
        MyDBService.ConfigOutils();
    }

[RelayCommand]
async Task Inscription()
{
    //if (IsBusy) return;
    await ReadAccess();
    RemplirDB();
    await MyDBService.InsertUser(UserNameInscription, UserPasswordInscription, UserAccessInscription);
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

    ///on remplit la liste avec les �l�ments de la DB
    foreach (var user in usersTemp)
    {
        MyUsers.Add(user);
    }
    IsBusy = false;
}


}