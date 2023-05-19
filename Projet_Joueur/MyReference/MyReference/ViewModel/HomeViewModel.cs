using MyReference.View;

namespace MyReference.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    //DeviceOrientationServices MyDeviceOrientationService;
    public ObservableCollection<Joueur> MyJoueurs { get; set; } = new();

    public HomeViewModel()
    {
        //Items = new ObservableCollection<string>();
        //Queue Serialbuffer = new();
    }

    public Joueur joueur;


    [ObservableProperty]
    string monTexte = "Go To Recherche Page";

    /*[ObservableProperty]
    string text;

    [ObservableProperty]
    ObservableCollection<string> items;*/




    /* [RelayCommand]
     public async Task GoToDetailPage(string data)
     {
         await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
         {
             {"Databc", data }
         });
     }*/

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
    public async Task AllerConnexionPage()
    {
        await Shell.Current.GoToAsync(nameof(ConnexionPage), true);
    }

    [RelayCommand]
    public async Task AllerInscriptionPage()
    {
        await Shell.Current.GoToAsync(nameof(InscriptionPage), true);
    }



    public void RefreshList()
    {
        MyJoueurs.Clear();

        foreach (Joueur joueur in Globals.MyJoueurList)
        {
            MyJoueurs.Add(joueur);
        }
    }

    /*[RelayCommand]
    async Task JoueursDepuisJSON()
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

        RefreshList();
    }*/
}