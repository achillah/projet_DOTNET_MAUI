namespace MyReference.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class RechercheJoueurViewModel : BaseViewModel
{
    DeviceOrientationServices MyDeviceOrientationService;

    [ObservableProperty]
    string monTxt;

    [ObservableProperty]
    Joueur joueurRechercher;


    public RechercheJoueurViewModel()
	{
        this.MyDeviceOrientationService = new DeviceOrientationServices();
        MyDeviceOrientationService.ConfigureScanner();

        MyDeviceOrientationService.MyQueueBuffer.Changed += SerialBuffer_Changed;
    }

    private async void SerialBuffer_Changed(object sender, EventArgs e)
    {
        DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

        MonTxt = myQueue.Dequeue().ToString(); //ActiveTarget = nom du label a changer!!!!

        foreach (Joueur joueur in Globals.MyJoueurList)
        {
            if (MonTxt == joueur.ID)
            {
                JoueurRechercher = joueur;
            }

            else
            {
                await Shell.Current.DisplayAlert("L'ID entrer n'a pas été retrouvé", "Veuillez vous diriger vers la page d'ajout et créer un joueur avec cet ID", "OK");
            }
        }

    }

    [RelayCommand]
    public async Task AllerAjouterJoueurPage()
    {
        await Shell.Current.GoToAsync(nameof(AjouterJoueurPage), true);
    }

    
}