using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class GeigerMuller : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Geiger-Muller");
			base.Tooltip.SetDefault("Lab issued Geiger counter. The louder it gets, the higher the chance of you getting irradiated.");
		}

		public override void SetDefaults()
		{
			base.item.value = Item.buyPrice(0, 20, 50, 0);
			base.item.rare = 7;
			base.item.width = 34;
			base.item.height = 28;
			base.item.accessory = true;
		}

		public override void UpdateInventory(Player player)
		{
			player.GetModPlayer<MullerEffect>().effect = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MullerEffect>().effect = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			string rad = "No";
			string rad2 = "nothing to note.";
			if (player.GetModPlayer<RedePlayer>().irradiatedLevel == 0)
			{
				rad = "No";
				rad2 = "nothing to note.";
			}
			else if (player.GetModPlayer<RedePlayer>().irradiatedLevel == 1)
			{
				rad = "Low";
				rad2 = "nothing to note.";
			}
			else if (player.GetModPlayer<RedePlayer>().irradiatedLevel == 2)
			{
				rad = "Medium";
				rad2 = "have teochrome-issued pills on hand just in case.";
			}
			else if (player.GetModPlayer<RedePlayer>().irradiatedLevel == 3)
			{
				rad = "High";
				rad2 = "have teochrome-issued pills on hand just in case.";
			}
			else if (player.GetModPlayer<RedePlayer>().irradiatedLevel == 4)
			{
				rad = "Very high";
				rad2 = "high chance of irradiation and suffering ARS.";
			}
			else if (player.GetModPlayer<RedePlayer>().irradiatedLevel == 5)
			{
				rad = "Extreme";
				rad2 = "Acute Radiation Syndrome detected.";
			}
			string text = rad + " doses of radiation detected on self, " + rad2;
			TooltipLine line = new TooltipLine(base.mod, "text1", text)
			{
				overrideColor = new Color?(Color.LimeGreen)
			};
			tooltips.Insert(2, line);
		}
	}
}
