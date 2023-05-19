namespace MyReference.ViewModel;

public partial class AjouterJoueurViewModel : BaseViewModel
{
    /*[ObservableProperty]
   public string iD;
   [ObservableProperty]
   public string nom;
   [ObservableProperty]
   public string prenom;
   [ObservableProperty]
   public int age;
   [ObservableProperty]
   public string poste;
   [ObservableProperty]
   public string image;*/


    [ObservableProperty]
    Joueur joueurNvx;
    public AjouterJoueurViewModel()
    {
        this.joueurNvx = new Joueur();
    }

    [RelayCommand]
    async void AjouterJoueur()
    {
        if ((string.IsNullOrEmpty(JoueurNvx.ID)) || (string.IsNullOrWhiteSpace(JoueurNvx.ID))
            || (string.IsNullOrEmpty(JoueurNvx.Nom)) || (string.IsNullOrWhiteSpace(JoueurNvx.Nom))
            || (string.IsNullOrEmpty(JoueurNvx.Prenom)) || (string.IsNullOrWhiteSpace(JoueurNvx.Prenom))
            || (string.IsNullOrEmpty(JoueurNvx.Age.ToString())) || (string.IsNullOrWhiteSpace(JoueurNvx.Age.ToString()))
            || (string.IsNullOrEmpty(JoueurNvx.Poste)) || (string.IsNullOrWhiteSpace(JoueurNvx.Poste))
            || (string.IsNullOrEmpty(JoueurNvx.Image)) || (string.IsNullOrWhiteSpace(JoueurNvx.Image)))
        {
            await Shell.Current.DisplayAlert("Erreur Ajout Joueur", "Veuillez entrer de vrai valeurs", "OK");
        }
        else
        {
            Globals.MyJoueurList.Add(JoueurNvx);

            //Text = string.Empty;
            await Shell.Current.DisplayAlert("Joueur ajouté", "Vous pouvez revenir en arrière.", "OK");
        }
    }
}