using System.IO;
using System.Text.Json;
namespace MyReference.ViewModel;

[QueryProperty("Joueur", "Joueur")]
public partial class JoueurViewModel : BaseViewModel
{
    [ObservableProperty]
    Joueur joueur;

    [ObservableProperty]
    Boolean droitModifier = false;

    [ObservableProperty]
    Boolean droitSupprimer = false;

    public JoueurViewModel() 
    {
        VerifieDroit();
    }

    public void VerifieDroit() 
    {
        if(Globals.utilisateurConnecte.UserAccessType == 1) 
        {
            DroitModifier = true;
            DroitSupprimer=true;
        }
        else if (Globals.utilisateurConnecte.UserAccessType == 2)
        {
            DroitSupprimer = true;
        }





    }

    [RelayCommand]
    public async Task AllerModifierJoueurPage(Joueur joueur)
    {
        //await Shell.Current.DisplayAlert("Successfully Created!", "You can go back.", "OK");
        await Shell.Current.GoToAsync(nameof(ModifierJoueurPage), true, new Dictionary<string, object>
        {

            {"Joueur", joueur }

        });
    }

    [RelayCommand]
    async void SupprimerJoueur(Joueur joueur)
    {
        
        if (Globals.MyJoueurList.Contains(joueur))
        {
            Globals.MyJoueurList.Remove(joueur);

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ServerDonnees", "Joueurs.json");

            // Récupère le contenu JSON existant du fichier
            string jsonContent = File.ReadAllText(filePath);

            // Désérialise le contenu JSON en une liste
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var joueurs = JsonSerializer.Deserialize<List<Joueur>>(jsonContent, options);

            // Supprime le joueur de la liste désérialisée
            joueurs.RemoveAll(j => j.ID == joueur.ID);

            // Sérialise la liste mise à jour en JSON
            string updatedJsonContent = JsonSerializer.Serialize(joueurs, options);

            try
            {
                // Écrit le contenu JSON sérialisé dans le fichier
                File.WriteAllText(filePath, updatedJsonContent);
            }
            catch (Exception e)
            {

                await Shell.Current.DisplayAlert("Ecriture non réaliser", e.Message, "OK");

            }

        }

        await Shell.Current.DisplayAlert("Suppression effectuée", "Vous pouvez revenir en arrière.", "OK");
    }



}
