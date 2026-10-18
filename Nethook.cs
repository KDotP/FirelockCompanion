using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace FirelockCompanion;

// Reminder: move this to gamedata.cs
public class UnitSpawn
{
    public string UnitId { get; set; }
    public string DisplayName { get; set; }
    public string CustomName { get; set; }

    public UnitSpawn(string unitId, string displayName, string customName)
    {
        UnitId = unitId;
        DisplayName = displayName;
        CustomName = customName;
    }
}

public static class Nethook
{
    private const string Host = "127.0.0.1";
    private const int TtsPort = 39999; // TTS listens here for external messages
    private const int CustomMessageId = 2;

    public static bool SpawnPieces(List<UnitSpawn> units)
    {
        if (units == null || units.Count == 0)
        {
            return false;
        }

        List<string> entries = new List<string>();
        foreach (UnitSpawn unit in units)
        {
            string id = Clean(unit.UnitId);
            if (id == "")
            {
                continue;
            }
            entries.Add(id + "|" + Clean(unit.DisplayName) + "|" + Clean(unit.CustomName));
        }

        if (entries.Count == 0)
        {
            return false;
        }

        string joined = string.Join(",", entries);

        var message = new
        {
            messageID = CustomMessageId,
            customMessage = new
            {
                names = joined
            }
        };

        string json = JsonSerializer.Serialize(message);
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        try
        {
            using (TcpClient client = new TcpClient(Host, TtsPort))
            using (NetworkStream stream = client.GetStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            return true;
        }
        catch (SocketException)
        {
            // TTS isn't running, or no game is loaded
            return false;
        }
    }

    // Removes the delimiter characters so a name can't break the message format
    private static string Clean(string text)
    {
        if (text == null)
        {
            return "";
        }
        return text.Replace(",", " ").Replace("|", " ").Trim();
    }
}