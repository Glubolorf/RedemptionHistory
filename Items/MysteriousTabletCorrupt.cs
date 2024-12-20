using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MysteriousTabletCorrupt : ModItem
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
			base.item.value = Item.sellPrice(0, 2, 0, 0);
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
				Redemption.SpawnBoss(player, "TheKeeper", true, new Vector2(player.position.X + (float)Main.rand.Next(1100, 1300), player.position.Y - 0f), "The Keeper", false);
				Main.PlaySound(15, player.position, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddIngredient(57, 6);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
