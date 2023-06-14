namespace MyReference.ViewModel;

[QueryProperty("User", "User")]
public partial class UtilisateurDetailViewModel : BaseViewModel
{

    //public ObservableCollection<User> MyUsers { get; set; } = new();
    UserGestionService MyDBService = new();

    [ObservableProperty]
    User user;

    public UtilisateurDetailViewModel(UserGestionService MyDBService)
    {
        this.MyDBService = MyDBService;
        MyDBService.ConfigOutils();
    }

    


    [RelayCommand]
    async Task ModifierUtilisateur()
    {
         await ReadAccess();
        try
        {
            await MyDBService.UpdateUser(User.User_ID, User.UserName, User.UserPassword, User.UserAccessType);
            await Shell.Current.DisplayAlert("Utilisateur Modifié", "Les données de l'utilisateur ont été modifiées avec succès", "OK");

        }
        catch (Exception e)
        {

            await Shell.Current.DisplayAlert("Utilisateur non Modifié", e.Message, "OK");
        }

        await ReadAccess();
        RemplirInfoDepuisDB();

    }

    [RelayCommand]
    async Task SupprimerUtilisateur()
    {
        try
        {
            await MyDBService.DeletetUser(User.UserName);
            await Shell.Current.DisplayAlert("Utilisateur Supprimé", "L'utilisateur ont été supprimer avec succès", "OK");

        }
        catch (Exception e)
        {

            await Shell.Current.DisplayAlert("Utilisateur non supprimé", e.Message, "OK");
        }
        
        await ReadAccess();
        RemplirInfoDepuisDB();
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