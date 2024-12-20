using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class CursedThornDart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Dart");
			base.Tooltip.SetDefault("Hitting an enemy will cause a burst of more Cursed Thorn Dart");
		}

		public override void SetDefaults()
		{
			base.item.damage = 35;
			base.item.ranged = true;
			base.item.width = 10;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.knockBack = 2.3f;
			base.item.value = 70;
			base.item.shoot = base.mod.ProjectileType("CursedThornDartPro");
			base.item.shootSpeed = 4f;
			base.item.ammo = AmmoID.Dart;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 1);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
