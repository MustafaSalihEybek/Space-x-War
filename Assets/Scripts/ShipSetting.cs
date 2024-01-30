public class ShipSetting
{
    private readonly int shipHealth;
    private readonly int shipDefense;
    private readonly int shipAttack;
    private readonly int shipCoinAmount;

    public ShipSetting(int shipHealth, int shipDefense, int shipAttack, int shipCoinAmount)
    {
        this.shipHealth = shipHealth;
        this.shipDefense = shipDefense;
        this.shipAttack = shipAttack;
        this.shipCoinAmount = shipCoinAmount;
    }

    public int GetShipHealth() => shipHealth;

    public int GetShipDefense() => shipDefense;

    public int GetShipAttack() => shipAttack;

    public int GetShipCoinAmount() => shipCoinAmount;
}
