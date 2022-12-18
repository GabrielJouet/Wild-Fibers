/// <summary>
/// Tower info button used in library.
/// </summary>
public class TowerInfo : ObjectInfo
{
    /// <summary>
    /// Tower data buffered in this tower info.
    /// </summary>
    private Tower _tower;
    public Tower Tower
    {
        get => _tower;

        set
        {
            _tower = value;
            _screenShot.sprite = _tower.ScreenShot;
        }
    }
}