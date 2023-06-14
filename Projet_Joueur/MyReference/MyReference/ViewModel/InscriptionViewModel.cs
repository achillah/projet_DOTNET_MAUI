namespace MyReference.ViewModel;

public partial class InscriptionViewModel : BaseViewModel
{

    //public ObservableCollection<User> MyUsers { get; set; } = new();
    UserGestionService MyDBService = new();

    [ObservableProperty]
    string userNameInscription;

    [ObservableProperty]
    string userPasswordInscription;

     


    public InscriptionViewModel(UserGestionService MyDBService)
    {
        this.MyDBService = MyDBService;
        MyDBService.ConfigOutils();
    }

[RelayCommand]
async Task Inscription()
{

        try
        {
            await ReadAccess();
            RemplirInfoDepuisDB();
            await MyDBService.InsertUser(UserNameInscription, UserPasswordInscription, 3);
            RemplirInfoDepuisDB();
           
        }
        catch (Exception ex)
        {

            await Shell.Current.DisplayAlert("Databse", ex.Message, "OK");
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