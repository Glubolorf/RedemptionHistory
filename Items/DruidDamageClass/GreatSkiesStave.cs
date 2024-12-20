using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class GreatSkiesStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Great Skies Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nLunges the user forward with great force");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 57;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 38;
			base.item.useAnimation = 38;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 11f;
			base.item.value = Item.sellPrice(0, 2, 25, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
		}

		public override bool UseItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("GSSBuff"), 20, true);
			Vector2 vector = Utils.SafeNormalize(Main.MouseWorld - player.Center, -Vector2.UnitY) * 8f;
			player.velocity += vector;
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 34;
				base.item.useAnimation = 34;
			}
			else
			{
				base.item.useTime = 38;
				base.item.useAnimation = 38;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(320, 10);
			modRecipe.AddIngredient(575, 20);
			modRecipe.AddIngredient(9, 30);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
