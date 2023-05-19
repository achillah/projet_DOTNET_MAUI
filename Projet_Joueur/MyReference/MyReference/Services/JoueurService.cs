namespace MyReference.Services;

public class JoueurService : ContentPage
{
	public JoueurService()
	{
	
	}

	public async Task<List<Joueur>> GetJoueurs()
    {
        List<Joueur> joueurs;

        using var stream = await FileSystem.OpenAppPackageFileAsync("csvjson.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        joueurs = JsonSerializer.Deserialize<List<Joueur>>(contents);

        return joueurs;
    }
}