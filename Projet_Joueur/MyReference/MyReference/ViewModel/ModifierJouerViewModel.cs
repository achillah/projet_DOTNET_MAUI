namespace MyReference.ViewModel;


[QueryProperty(nameof(JoueurUpd), "Joueur")]
public partial class ModifierJouerViewModel : BaseViewModel
{
    [ObservableProperty]
     Joueur joueurUpd;


    public ModifierJouerViewModel()
    {

    }

    [RelayCommand]
    async void ModifierJoueur()
    {

        foreach (Joueur joueur in Globals.MyJoueurList)
        {
            if(joueur.ID == JoueurUpd.ID) 
            {
                JoueurUpd.Nom = joueur.Nom;
                JoueurUpd.Prenom = joueur.Prenom;
                JoueurUpd.Age = joueur.Age;
                JoueurUpd.Poste = joueur.Poste;
                JoueurUpd.Image = joueur.Image;
            }
        }

        await Shell.Current.DisplayAlert("Modification éffectué!", "Vous pouvez retournez en arrière.", "OK");
    }
}