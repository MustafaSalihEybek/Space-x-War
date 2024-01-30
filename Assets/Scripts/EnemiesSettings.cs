using System.Collections;
using System.Collections.Generic;

public class EnemiesSettings
{
    private readonly Dictionary<int, ShipSetting> enemiesProperties = new Dictionary<int, ShipSetting>()
    {
        { 1, new ShipSetting(50, 0, 6, 25) },
        { 2, new ShipSetting(50, 0, 6, 50) },
        { 3, new ShipSetting(50, 0, 6, 75) },
        { 4, new ShipSetting(50, 0, 6, 100) },
        { 5, new ShipSetting(100, 5, 12, 125) },
        { 6, new ShipSetting(100, 5, 12, 150) },
        { 7, new ShipSetting(100, 5, 12, 175) },
        { 8, new ShipSetting(100, 5, 12, 200) },
        { 9, new ShipSetting(150, 10, 18, 225) },
        { 10, new ShipSetting(150, 10, 18, 250) },
        { 11, new ShipSetting(200, 5, 6, 250) },
        { 12, new ShipSetting(200, 5, 6, 400) },
        { 13, new ShipSetting(200, 5, 6, 550) },
        { 14, new ShipSetting(200, 5, 6, 700) },
        { 15, new ShipSetting(250, 10, 30, 850) },
        { 16, new ShipSetting(250, 10, 30, 1000) },
        { 17, new ShipSetting(250, 10, 30, 1150) },
        { 18, new ShipSetting(250, 10, 30, 1300) },
        { 19, new ShipSetting(300, 15, 48, 1450) },
        { 20, new ShipSetting(300, 15, 48, 1600) },
        { 21, new ShipSetting(400, 10, 48, 1600) },
        { 22, new ShipSetting(400, 10, 48, 2000) },
        { 23, new ShipSetting(400, 10, 48, 2400) },
        { 24, new ShipSetting(400, 10, 48, 2800) },
        { 25, new ShipSetting(500, 15, 72, 3200) },
        { 26, new ShipSetting(500, 15, 72, 3600) },
        { 27, new ShipSetting(500, 15, 72, 4000) },
        { 28, new ShipSetting(500, 15, 72, 4400) },
        { 29, new ShipSetting(600, 20, 96, 4800) },
        { 30, new ShipSetting(600, 20, 96, 5200) },
    };

    public Dictionary<int, ShipSetting> GetEnemiesProperties() => enemiesProperties;
}
