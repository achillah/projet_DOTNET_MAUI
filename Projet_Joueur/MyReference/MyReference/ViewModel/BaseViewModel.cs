namespace MyReference.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    public ObservableCollection<User> MyUsers { get; set; } = new();

    [ObservableProperty]
	public string title;

	[ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;
    public BaseViewModel()
	{

	}

    public async void RemplirInfoDepuisDB()
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