using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatsManager
{
    [Header("Player")]
    private List<Player> Players = new List<Player>();

    

    private bool PlayerStatsRead(Player _player)
    {
        string DataPath = "Asset/csv/PlayerData.csv";
        //List CsvReader(DataPath);
        return false;
    }

    private static IEnumerable<string[]> CsvReader(string assetName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(assetName);
        using var stringReader = new StringReader(textAsset.text);
        var row =stringReader.ReadLine();//ˆês“Ç

        //‚·‚×‚Ä‚Ìs‚ğReadLine‚·‚é‚Ü‚Å
        while (row != null)
        {
            var columns = row.Split(',');

            yield return columns;

            row = stringReader.ReadLine();
        }
    }
}
