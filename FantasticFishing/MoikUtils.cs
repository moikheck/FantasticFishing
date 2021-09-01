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
    internal class MoikUtils
    {
        public void removeItemsFromInventoryFF(Farmer player, Item which, int count)
        {
            int index = player.items.IndexOf(which);
            int currentStack = which.Stack;
            if (count == -1)
                currentStack = 0;
            else
                currentStack -= count;
            if (currentStack == 0)
                player.items[index] = (Item)null;
            else
                which.Stack = currentStack;
        }
        public int getItemIDByName(string name, string dictionaryName)
        {
            Dictionary<int, string> dictionary = Game1.content.Load<Dictionary<int, string>>(dictionaryName);
            foreach (int key in dictionary.Keys)
            {
                string[] strArray = Game1.content.Load<Dictionary<int, string>>(dictionaryName)[key].Split('/');
                if (name.Equals(strArray[strArray.Length - 1]))
                {
                    return key;
                }
            }
            return -1;
        }
    }
}