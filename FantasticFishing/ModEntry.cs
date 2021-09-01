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
    public class ModEntry : StardewModdingAPI.Mod
    {
        public static Mod instance;
        RecyclingEdit recyclingEdit = new RecyclingEdit();
        MoikUtils moikUtils = new MoikUtils();
        FishingNet fishingNet = new FishingNet();

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
            helper.Events.GameLoop.DayStarted += this.OnFishingNetCheck;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
            if (e.Button.ToString().Equals("MouseRight"))
            {
                StardewValley.Object object1;
                Game1.currentLocation.Objects.TryGetValue(new Vector2(e.Cursor.Tile.X, e.Cursor.Tile.Y), out object1);
                //Player clicking Recycling Machine - run machine function
                if (object1 != null
                        && object1.displayName.Equals("Recycling Machine")
                        && object1.heldObject.Value == null
                        && Game1.player.CurrentItem != null)
                {
                    recyclingEdit.dropItemInRecycling(Game1.player, object1);
                }
                //Player has fishing net - run fishing net function
                else if (Game1.player.CurrentItem != null 
                        && Game1.player.CurrentItem.Name.Equals("Fishing Net"))
                {
                    fishingNet.isNetSpot(Game1.player, Game1.currentLocation, new Vector2(e.Cursor.Tile.X, e.Cursor.Tile.Y));
                }
            }
                
            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }
        private void OnWearingGearOvernight(object sender, EventArgs e)
        {
            if (trueHatEvent)
                Game1.player.addedFishingLevel.Value += 1;
            if (anglerBootsEvent)
                Game1.player.addedFishingLevel.Value += 1;
            if (anglerVestEvent)
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

        private void OnFishingNetCheck(object sender, EventArgs e)
        {
            //something something find nets and replace items
            //function: find all fish and sort out which can be in each net
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            //Fix Error Boots
            foreach (Item item in Game1.player.Items)
            {
                if (item != null && item.DisplayName == "Error Item")
                {
                    moikUtils.removeItemsFromInventoryFF(Game1.player, item, -1);
                    Game1.player.addItemByMenuIfNecessary((Item)new Boots(moikUtils.getItemIDByName("Angler Boots", "Data//Boots")));
                }
            }

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
    }
}
