public class SettingsService : ISettingsService
{
    private readonly ISaveService save;

    public SettingsService(ISaveService save)
    {
        this.save = save;
    }

    public bool VibrationOn
    {
        get => save.GetInt("vibrationOn", 1) != 0;
        set => save.SetInt("vibrationOn", value ? 1 : 0);
    }
}
