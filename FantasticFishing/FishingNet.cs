using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Objects;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using System.Xml.Serialization;

namespace FantasticFishing
{
    internal class FishingNet
    {
        MoikUtils moikUtils;
        public FishingNet()
        {
            moikUtils = new MoikUtils();
        }
        public void isNetSpot(Farmer player, GameLocation l, Vector2 tile)
        {
            //Fishing Net:
            //Can only be placed in water
            //Has an inventory
            //Fed bait - different kinds?
            //Start of next day: add fish
            //Get list of fish
            //Fish season: only current game season
            //Other requirements from bait (like legendary fish, etc)
            //Randomly choose amount of fish based on fishing level, luck, randint
            //Place fish in inventory
            if (CrabPot.IsValidCrabPotLocationTile(l, (int)tile.X, (int)tile.Y))
            {
                //something something place
            }
        }

        public void fillFishingNet(Farmer player, StardewValley.Object object1)
        {
            if (player.CurrentItem.Name.Equals("Bait"))
            {
                object1.heldObject.Value = new StardewValley.Object(685, 1);
                moikUtils.removeItemsFromInventoryFF(player, player.CurrentItem, 1);
            }
        }
    }
}