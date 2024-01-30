using System.Collections.Generic;

public class ShipSettings
{
    private readonly Dictionary<string, ShipSetting[]> shipsProperties = new Dictionary<string, ShipSetting[]>()
   {
       {
           "ShipType1", new ShipSetting[]
           {
               new ShipSetting(50, 5, 10, 0),
               new ShipSetting(60, 6, 12 ,150),
               new ShipSetting(70, 7, 14, 300),
               new ShipSetting(80, 8, 16, 600),
               new ShipSetting(90, 9, 18, 1200),
               new ShipSetting(100, 10, 20 ,2400),
               new ShipSetting(110, 11, 22, 4800),
               new ShipSetting(120, 12, 24, 9600),
               new ShipSetting(130, 13, 26, 19200),
               new ShipSetting(140, 14, 28, 38400),
           }
       },
       {
           "ShipType2", new ShipSetting[]
           {
               new ShipSetting(75, 5, 15, 300),
               new ShipSetting(90, 6, 17, 600),
               new ShipSetting(105, 7, 19, 1200),
               new ShipSetting(120, 8, 21, 2400),
               new ShipSetting(135, 9, 23, 4800),
               new ShipSetting(150, 10, 25 ,9600),
               new ShipSetting(165, 11, 27, 19200),
               new ShipSetting(180, 12, 29, 38400),
               new ShipSetting(195, 13, 31, 76800),
               new ShipSetting(210, 14, 33, 153600),
           }
       },
       {
           "ShipType3", new ShipSetting[]
           {
               new ShipSetting(150, 10, 20, 1200),
               new ShipSetting(175, 12, 23, 2400),
               new ShipSetting(200, 14, 26, 4800),
               new ShipSetting(225, 16, 29, 9600),
               new ShipSetting(250, 18, 32, 19200),
               new ShipSetting(275, 20, 35, 38400),
               new ShipSetting(300, 22, 38, 76800),
               new ShipSetting(325, 24, 41, 153600),
               new ShipSetting(350, 26, 43, 307200),
               new ShipSetting(375, 28, 47, 614400),
           }
       },
       {
           "ShipType4", new ShipSetting[]
           {
               new ShipSetting(250, 15, 25, 4800),
               new ShipSetting(280, 18, 29, 9600),
               new ShipSetting(310, 21, 33, 19200),
               new ShipSetting(340, 24, 37, 38400),
               new ShipSetting(370, 27, 41, 76800),
               new ShipSetting(400, 30, 45, 153600),
               new ShipSetting(430, 33, 49, 307200),
               new ShipSetting(460, 36, 53, 614400),
               new ShipSetting(490, 39, 57, 1228800),
               new ShipSetting(520, 42, 61, 2457600),
           }
       },
       {
           "ShipType5", new ShipSetting[]
           {
               new ShipSetting(400, 20, 30, 19200),
               new ShipSetting(450, 24, 35, 36400),
               new ShipSetting(500, 28, 40, 76800),
               new ShipSetting(550, 32, 45, 153600),
               new ShipSetting(600, 36, 50, 307200),
               new ShipSetting(650, 40, 55, 614400),
               new ShipSetting(700, 44, 60, 1228800),
               new ShipSetting(750, 48, 65, 2457600),
               new ShipSetting(800, 52, 70, 4915200),
               new ShipSetting(850, 56, 75, 9830400),
           }
       }
   };

    public Dictionary<string, ShipSetting[]> GetShipsProperties() => shipsProperties;
}