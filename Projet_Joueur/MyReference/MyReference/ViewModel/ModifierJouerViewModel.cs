using System.IO;
using System.Text.Json;
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
    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ServerDonnees", "Joueurs.json");

    // Récupère le contenu JSON existant du fichier
    string jsonContent = File.ReadAllText(filePath);

    // Désérialise le contenu JSON en une liste
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    var joueurs = JsonSerializer.Deserialize<List<Joueur>>(jsonContent, options);

    // Recherche le joueur à modifier
    Joueur joueurAModifier = joueurs.FirstOrDefault(joueur => joueur.ID == JoueurUpd.ID);

    if (joueurAModifier != null)
    {
        // Vérifie que les nouvelles valeurs ne sont pas vides ou ne contiennent que des espaces
        if (!string.IsNullOrEmpty(JoueurUpd.Nom.Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Prenom.Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Age.ToString().Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Poste.Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Image.Trim()))
        {
            // Met à jour les propriétés du joueur avec les nouvelles valeurs
            joueurAModifier.Nom = JoueurUpd.Nom;
            joueurAModifier.Prenom = JoueurUpd.Prenom;
            joueurAModifier.Age = JoueurUpd.Age;
            joueurAModifier.Poste = JoueurUpd.Poste;
            joueurAModifier.Image = JoueurUpd.Image;

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
                
            await Shell.Current.DisplayAlert("Modification effectuée", "Vous pouvez revenir en arrière.", "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Erreur de modification", "Veuillez entrer de vraies valeurs", "OK");
        }
    }
    else
    {
        await Shell.Current.DisplayAlert("Erreur de modification", "Le joueur n'a pas été trouvé.", "OK");
    }
}

}