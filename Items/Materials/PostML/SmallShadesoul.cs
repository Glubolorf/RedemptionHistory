using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class SmallShadesoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Small Shadesoul");
			base.Tooltip.SetDefault("'A soul of pure chaos'\nCan emerge from killing soulless creatures in the caverns");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			new Item().SetDefaults(549, false);
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 999;
			base.item.value = 100;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Shadesoul", 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 10);
			modRecipe.AddRecipe();
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.GhostWhite.ToVector3() * 0.55f * Main.essScale);
		}
	}
}
