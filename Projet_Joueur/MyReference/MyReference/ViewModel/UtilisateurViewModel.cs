namespace MyReference.ViewModel;

public partial class UtilisateurViewModel : BaseViewModel
{
    //public ObservableCollection<User> MyUsers { get; set; } = new();
	UserGestionService MyDBService = new();


    public UtilisateurViewModel(UserGestionService MyDBService)
	{
		
		this.MyDBService = MyDBService;
		MyDBService.ConfigOutils();
		RemplirInfoDepuisDB();
	}

    [RelayCommand]
    async Task AllerUtilisateurDetailPage(User user)
    {
        if (user is null)
            return;

        await Shell.Current.GoToAsync(nameof(UtilisateurDetailPage), true, new Dictionary<string, object>
        {

            {"User", user }

        });
    }

    //[RelayCommand]
    /*public async void RemplirDB()
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
	}*/

}