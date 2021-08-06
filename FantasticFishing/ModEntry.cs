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
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
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


        private void OnUpdateTicked(object sender, EventArgs e)
        {

            //True Fisher Hat
            if (Game1.player.hat.Value != null && Game1.player.hat.Get().displayName == "True Fisher Hat" && !trueHatEvent)
            {
                this.Monitor.Log($"Hat put on!", LogLevel.Debug);
                Game1.player.addedFishingLevel.Value += 1;
                this.trueHatEvent = true;
            }
            if (Game1.player.hat.Value == null && trueHatEvent)
            {
                this.Monitor.Log($"Hat taken off!", LogLevel.Debug);
                Game1.player.addedFishingLevel.Value -= 1;
                this.trueHatEvent = false;
            }

            //Angler Boots
            if (Game1.player.boots.Value != null && Game1.player.boots.Get().displayName == "Angler Boots" && !anglerBootsEvent)
            {
                this.Monitor.Log($"Boots put on!", LogLevel.Debug);
                Game1.player.addedFishingLevel.Value += 1;
                this.anglerBootsEvent = true;
            }
            if (Game1.player.boots.Value == null && anglerBootsEvent)
            {
                this.Monitor.Log($"Boots taken off!", LogLevel.Debug);
                Game1.player.addedFishingLevel.Value -= 1;
                this.anglerBootsEvent = false;
            }

        }

    }
}
