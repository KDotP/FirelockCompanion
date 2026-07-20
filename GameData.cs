using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirelockCompanion
{
    public enum EmbarkStatus { None, Embarked, Desanted }
    public enum EmbarkRole { None, Carrier, Passenger }

    internal class GameData
    {
        public Dictionary<string, string> keywords { get; set; }
        public Dictionary<string, List<UnitTemplate>> factions { get; set; }
    }

    public class UnitTemplate
    {
        public string id { get; set; }
        public string name { get; set; }
        public string subname { get; set; }
        public string type { get; set; }
        public int cost { get; set; }
        public string unit_stats { get; set; }
        public List<string> keywords { get; set; }
        public List<Weapon> weapons { get; set; }
        public string bonus_traits { get; set; }
    }

    public class Weapon
    {
        public string name { get; set; }
        public string weapon_stats { get; set; }
        public List<string> keywords { get; set; }
        public List<Ammo> ammos { get; set; }
    }

    public class Ammo
    {
        public string name { get; set; }
        public string ammo_stats { get; set; }
        public List<string> keywords { get; set; }
    }

    public class ActiveUnitEntry
    {
        public UnitTemplate Unit { get; }
        public string CustomName { get; set; }

        // As a passenger (Inf-tagged)
        public EmbarkStatus Status { get; set; } = EmbarkStatus.None;
        public bool HasCarrierAbove { get; set; }
        public bool HasDesantCarrierAbove { get; set; }
        public bool CanEmbark { get; set; }
        public bool CanDesant { get; set; }

        // As a towee (Vec-tagged)
        public bool IsTowed { get; set; }
        public bool HasTowProviderAbove { get; set; }
        public bool CanTow { get; set; }

        // As a carrier (own PC capacity, if applicable)
        public int? EmbarkCapacityDisplay { get; set; }
        public int EmbarkUsedDisplay { get; set; }
        public bool ShowDesantSuffix { get; set; }
        public int DesantUsedDisplay { get; set; }

        // As a tow provider (own Tow(X) capacity, if applicable)
        public int? TowCapacityDisplay { get; set; }
        public int TowUsedDisplay { get; set; }

        public TreeNode RelationParentNode { get; set; } // the vehicle/provider this unit is currently nested under, or null
        public int IndentLevel { get; set; }

        public ActiveUnitEntry(UnitTemplate unit, string customName)
        {
            Unit = unit;
            CustomName = customName;
        }
    }
}
