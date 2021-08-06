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
    public class ModEntry : Mod
    {
        private bool trueHatEvent = false;
        private bool anglerBootsEvent = false;
        private bool anglerVestEvent = false;
        public override void Entry(IModHelper helper)
        {
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

    }
}
