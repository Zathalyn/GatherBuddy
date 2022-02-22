using System.Linq;
using Dalamud.Logging;
using GatherBuddy.Enums;

namespace GatherBuddy.Data;

public static class HiddenItems
{
    public const int DarkMatterClusterId = 10335;
    public const int UnaspectedCrystalId = 10099;

    public static readonly (uint ItemId, uint NodeId)[] Items =
    {
        (7758, 203),  // Grade 1 La Noscean Topsoil
        (7761, 200),  // Grade 1 Shroud Topsoil   
        (7764, 201),  // Grade 1 Thanalan Topsoil 
        (7759, 150),  // Grade 2 La Noscean Topsoil
        (7762, 209),  // Grade 2 Shroud Topsoil   
        (7765, 151),  // Grade 2 Thanalan Topsoil 
        (10092, 210), // Black Limestone          
        (10094, 177), // Little Worm              
        (10097, 133), // Yafaemi Wildgrass        
        (12893, 295), // Dark Chestnut            
        (15865, 30),  // Firelight Seeds          
        (15866, 39),  // Icelight Seeds           
        (15867, 21),  // Windlight Seeds          
        (15868, 31),  // Earthlight Seeds         
        (15869, 25),  // Levinlight Seeds         
        (15870, 14),  // Waterlight Seeds
        (12534, 285), // Mythrite Ore             
        (12535, 353), // Hardsilver Ore           
        (12537, 286), // Titanium Ore             
        (12579, 356), // Birch Log                
        (12878, 297), // Cyclops Onion            
        (12879, 298), // Emerald Beans            
    };

    public static void Apply(GameData data)
    {
        foreach (var (itemId, nodeId) in Items)
        {
            if (!data.Gatherables.TryGetValue(itemId, out var item))
            {
                PluginLog.Error($"Could not find item {itemId}.");
                continue;
            }

            if (!data.GatheringNodes.TryGetValue(nodeId, out var node))
            {
                PluginLog.Error($"Could not find gathering node {nodeId}.");
                continue;
            }

            node.AddItem(item);
        }
    }

    public static void ApplyDarkMatter(GameData data)
    {
        if (!data.Gatherables.TryGetValue(DarkMatterClusterId, out var darkMatter))
        {
            PluginLog.Error("Could not find Dark Matter Cluster.");
            return;
        }

        if (!data.Gatherables.TryGetValue(UnaspectedCrystalId, out var crystal))
        {
            PluginLog.Error("Could not find Unaspected Crystal.");
            return;
        }

        foreach (var node in data.GatheringNodes.Values
                     .Where(n => n.NodeType == NodeType.Unspoiled && n.Level == 50))
        {
            node.AddItem(darkMatter);
            node.AddItem(crystal);
        }
    }
}
