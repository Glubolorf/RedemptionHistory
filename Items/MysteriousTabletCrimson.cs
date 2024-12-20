using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MysteriousTabletCrimson : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Tablet");
			base.Tooltip.SetDefault("Summons the Keeper\nOnly usable at night");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 36;
			base.item.maxStack = 20;
			base.item.rare = 2;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("TheKeeper"));
		}

		public override bool UseItem(Player player)
		{
			if (RedeWorld.keeperSaved)
			{
				Main.NewText("But nobody came...", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
			}
			else
			{
				Main.NewText("The Keeper has awoken!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				int num = NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(1100, 1300)), (int)(player.position.Y - 0f), base.mod.NPCType("TheKeeper"), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2 && num < 200)
				{
					NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
				}
				Main.PlaySound(15, player.position, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddIngredient(1257, 6);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
