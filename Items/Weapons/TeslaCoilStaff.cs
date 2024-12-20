using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TeslaCoilStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Coil Staff");
			base.Tooltip.SetDefault("Focuses a tesla beam");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 190;
			base.item.magic = true;
			base.item.mana = 15;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.reuseDelay = 5;
			base.item.useStyle = 5;
			base.item.UseSound = SoundID.Item13;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.channel = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.shoot = base.mod.ProjectileType("TeslaCoilStaffPro");
			base.item.shootSpeed = 30f;
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
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(3541, 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
