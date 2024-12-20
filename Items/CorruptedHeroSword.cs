using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CorruptedHeroSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Hero Sword");
			base.Tooltip.SetDefault("Summons one of Vlitch's Overlords\n'The corrupted blade draws near the power, thus beginning the final hour'\nOnly usable at night");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 38;
			base.item.maxStack = 20;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.rare = 10;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("VlitchCleaver"));
		}

		public override bool UseItem(Player player)
		{
			int num = NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(200, 300)), (int)(player.position.Y - 0f), base.mod.NPCType("VlitchCleaver"), 0, 0f, 0f, 0f, 0f, 255);
			if (Main.netMode == 2 && num < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
			}
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0, 1f, 0f);
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
			{
				Main.NewText("Vlitch Cleaver has awoken!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Main.NewText("... An Omega Android challenges me? So be it, traitor!", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).chickenPower)
			{
				Main.NewText("Vlitch Cleaver has awoken!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Main.NewText("Let's se- wait... Is that a chicken!?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
			}
			else
			{
				Main.NewText("Vlitch Cleaver has awoken!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Main.NewText("Let's see how long you last...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1570, 1);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1570, 1);
			modRecipe.AddIngredient(1508, 10);
			modRecipe.AddTile(null, "CorruptorTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
