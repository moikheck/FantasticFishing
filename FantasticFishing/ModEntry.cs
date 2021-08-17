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

namespace FantasticFishing
{
    public class ModEntry : StardewModdingAPI.Mod
    {
        public static Mod instance;

        private bool trueHatEvent = false;
        private bool anglerBootsEvent = false;
        private bool anglerVestEvent = false;
        public override void Entry(IModHelper helper)
        {
            instance = this;
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
            helper.Events.GameLoop.DayStarted += this.OnWearingGearOvernight;
            helper.Events.GameLoop.SaveLoaded += this.OnWearingGearOnLogin;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
            if (e.Button.ToString().Equals("MouseRight"))
            {
                StardewValley.Object object1;
                Game1.currentLocation.Objects.TryGetValue(new Vector2(e.Cursor.Tile.X, e.Cursor.Tile.Y), out object1);
                if (object1 != null
                        && object1.displayName.Equals("Recycling Machine")
                        && object1.heldObject.Value == null
                        && Game1.player.CurrentItem != null)
                {
                    dropItemInRecycling(Game1.player, object1);
                }
            }
                
            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }

        private void OnWearingGearOvernight(object sender, EventArgs e)
        {
            if(trueHatEvent)
                Game1.player.addedFishingLevel.Value += 1;
            if(anglerBootsEvent)
                Game1.player.addedFishingLevel.Value += 1;
            if(anglerVestEvent)
                Game1.player.addedFishingLevel.Value += 1;
        }
        private void OnWearingGearOnLogin(object sender, EventArgs e)
        {
            if (trueHatEvent)
                Game1.player.addedFishingLevel.Value -= 1;
            if (anglerBootsEvent)
                Game1.player.addedFishingLevel.Value -= 1;
            if (anglerVestEvent)
                Game1.player.addedFishingLevel.Value -= 1;
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {

            //True Fisher Hat
            if (Game1.player.hat.Value != null && Game1.player.hat.Get().displayName == "True Fisher Hat" && !trueHatEvent)
            {
                Game1.player.addedFishingLevel.Value += 1;
                this.trueHatEvent = true;
            }
            if ((Game1.player.hat.Value == null || (Game1.player.hat.Value != null && Game1.player.hat.Get().displayName != "True Fisher Hat")) && trueHatEvent)
            {
                Game1.player.addedFishingLevel.Value -= 1;
                this.trueHatEvent = false;
            }

            //Angler Boots
            if (Game1.player.boots.Value != null && Game1.player.boots.Get().displayName == "Angler Boots" && !anglerBootsEvent)
            {
                Game1.player.addedFishingLevel.Value += 1;
                this.anglerBootsEvent = true;
            }
            if ((Game1.player.boots.Value == null || (Game1.player.boots.Value != null && Game1.player.boots.Get().displayName != "Angler Boots")) && anglerBootsEvent)
            {
                Game1.player.addedFishingLevel.Value -= 1;
                this.anglerBootsEvent = false;
            }

            //Angler Vest
            if (Game1.player.shirtItem.Value != null && Game1.player.shirtItem.Get().displayName == "Angler Vest" && !anglerVestEvent)
            {
                Game1.player.addedFishingLevel.Value += 1;
                this.anglerVestEvent = true;
            }
            if ((Game1.player.shirtItem.Value == null || (Game1.player.shirtItem.Value != null && Game1.player.shirtItem.Get().displayName != "Angler Vest")) && anglerVestEvent)
            {
                Game1.player.addedFishingLevel.Value -= 1;
                this.anglerVestEvent = false;
            }

        }

        public IDictionary<string, int> switchDictionaryValues(IDictionary<int, string> toSwitch)
        {
            this.Monitor.Log($"{toSwitch.Keys} {toSwitch.Values}", LogLevel.Debug);
            IDictionary<string, int> returnDictionary = null;
            foreach(int key in toSwitch.Keys)
            {
                returnDictionary.Add(toSwitch.TryGetValue(key, out string name) ? name : null, key);
            }
            if(toSwitch.Values.Contains("Angler Boots"))
            {

            }
            this.Monitor.Log($"{returnDictionary}", LogLevel.Debug);
            return returnDictionary;
        }

        private int GetId(IDictionary<int, string> toSwitch, string name)
        {
            IDictionary<string, int> ids = switchDictionaryValues(toSwitch);
            return ids != null && ids.TryGetValue(name, out int id)
                ? id
                : -1;
        }
        public void dropItemInRecycling(Farmer player, StardewValley.Object object1)
        {
            bool recycling = false;
            int timer = 0;
            this.Monitor.Log($"{player.CurrentItem.Name}", LogLevel.Debug);
            if (player.CurrentItem.Name.Equals("Old Boot") && player.CurrentItem.Stack >= 2)
            {
                object1.heldObject.Value = new StardewValley.Object(3700, 1);
                removeItemsFromInventoryFF(player, player.CurrentItem, 2);
                recycling = true;
                timer = 120;
            }
            if (recycling)
            {
                player.currentLocation.playSound("trashcan");
                object1.minutesUntilReady.Value = timer;
                ++Game1.stats.PiecesOfTrashRecycled;
            }
        }

        public void removeItemsFromInventoryFF(Farmer player, Item which, int count)
        {
            int index = player.items.IndexOf(which);
            int currentStack = which.Stack;
            currentStack -= count;
            if (currentStack == 0)
                player.items[index] = (Item)null;
            else
                which.Stack = currentStack;
        }

    }
}
