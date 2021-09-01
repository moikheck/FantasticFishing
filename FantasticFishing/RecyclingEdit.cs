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
    internal class RecyclingEdit
    {
        MoikUtils moikUtils;
        public RecyclingEdit()
        {
            moikUtils = new MoikUtils();
        }

        
        public void dropItemInRecycling(Farmer player, StardewValley.Object object1)
        {
            bool recycling = false;
            int timer = 0;
            if (player.CurrentItem.Name.Equals("Old Boot") && player.CurrentItem.Stack >= 2)
            {
                object1.heldObject.Value = new StardewValley.Object(moikUtils.getItemIDByName("Angler Boots", "Data//Boots"), 1);
                moikUtils.removeItemsFromInventoryFF(player, player.CurrentItem, 2);
                recycling = true;
                timer = 10;//set to 120 on final
            }
            if (recycling)
            {
                player.currentLocation.playSound("trashcan");
                object1.minutesUntilReady.Value = timer;
                ++Game1.stats.PiecesOfTrashRecycled;
            }
        }
    }
}