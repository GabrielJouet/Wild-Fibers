public class Shield
{
    /// <summary>
    /// Default shield value.
    /// </summary>
    protected float _baseShieldValue;

    protected Enemy _reference;

    protected bool _stopWhileShielding;


    public Shield(float newArmorMax, bool stopWhileShielding, Enemy newReference)
    {
        _baseShieldValue = newArmorMax;
        _stopWhileShielding = stopWhileShielding;
        _reference = newReference;
    }


    /// <summary>
    /// Method used to change shield value.
    /// </summary>
    /// <param name="shieldValue">New shield value</param>
    public void ActivateShield(float shieldValue, bool dotApplied)
    {
        _reference.Moving = !_stopWhileShielding;

        if (dotApplied)
            _reference.Armor = shieldValue - (_reference.ArmorMax - _reference.Armor);
        else
            _reference.Armor = shieldValue;

        _reference.ArmorMax = shieldValue;
    }


    public void ResetShield(bool dotApplied)
    {
        if (dotApplied)
            _reference.Armor = _baseShieldValue - (_reference.ArmorMax - _reference.Armor);
        else
            _reference.Armor = _baseShieldValue;

        _reference.ArmorMax = _baseShieldValue;

        _reference.Moving = true;
    }
}
