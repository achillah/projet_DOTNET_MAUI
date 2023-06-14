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

    // R�cup�re le contenu JSON existant du fichier
    string jsonContent = File.ReadAllText(filePath);

    // D�s�rialise le contenu JSON en une liste
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    var joueurs = JsonSerializer.Deserialize<List<Joueur>>(jsonContent, options);

    // Recherche le joueur � modifier
    Joueur joueurAModifier = joueurs.FirstOrDefault(joueur => joueur.ID == JoueurUpd.ID);

    if (joueurAModifier != null)
    {
        // V�rifie que les nouvelles valeurs ne sont pas vides ou ne contiennent que des espaces
        if (!string.IsNullOrEmpty(JoueurUpd.Nom.Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Prenom.Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Age.ToString().Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Poste.Trim()) &&
            !string.IsNullOrEmpty(JoueurUpd.Image.Trim()))
        {
            // Met � jour les propri�t�s du joueur avec les nouvelles valeurs
            joueurAModifier.Nom = JoueurUpd.Nom;
            joueurAModifier.Prenom = JoueurUpd.Prenom;
            joueurAModifier.Age = JoueurUpd.Age;
            joueurAModifier.Poste = JoueurUpd.Poste;
            joueurAModifier.Image = JoueurUpd.Image;

            // S�rialise la liste mise � jour en JSON
            string updatedJsonContent = JsonSerializer.Serialize(joueurs, options);


                try
                {
                    // �crit le contenu JSON s�rialis� dans le fichier
                    File.WriteAllText(filePath, updatedJsonContent);
                }
                catch (Exception e)
                {

                    await Shell.Current.DisplayAlert("Ecriture non r�aliser", e.Message, "OK");

                }
                
            await Shell.Current.DisplayAlert("Modification effectu�e", "Vous pouvez revenir en arri�re.", "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Erreur de modification", "Veuillez entrer de vraies valeurs", "OK");
        }
    }
    else
    {
        await Shell.Current.DisplayAlert("Erreur de modification", "Le joueur n'a pas �t� trouv�.", "OK");
    }
}

}