namespace MyReference.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class DetailViewModel : BaseViewModel
{
     DeviceOrientationServices MyDeviceOrientationService;

    [ObservableProperty]
	string monTxt;
	[ObservableProperty]
	string myButton;

    public DetailViewModel()
	{
        this.MyDeviceOrientationService = new DeviceOrientationServices();
        MyDeviceOrientationService.ConfigureScanner();

		MyDeviceOrientationService.MyQueueBuffer.Changed += SerialBuffer_Changed;

    }

	private void SerialBuffer_Changed(object sender, EventArgs e)
	{
		DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

        MonTxt = myQueue.Dequeue().ToString(); //ActiveTarget = nom du label a changer!!!!
	}

	[RelayCommand]
	void SelectButton(string data)
	{
		MyButton = data;
		//if(Globals.SerialBuffer.Count > 0) { MonTxt= Globals.SerialBuffer.Dequeue(); }
	}
}