using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientPickaxeAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Pickaxe Axe");
			base.Tooltip.SetDefault("Right-clicking will throw the pickaxe axe, dealing ranged damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 600;
			base.item.melee = true;
			base.item.width = 68;
			base.item.height = 64;
			base.item.useTime = 6;
			base.item.useAnimation = 10;
			base.item.pick = 300;
			base.item.axe = 35;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 0;
			base.item.shootSpeed = 18f;
			base.item.autoReuse = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.noUseGraphic = true;
				base.item.noMelee = true;
				base.item.useTime = 6;
				base.item.pick = 0;
				base.item.axe = 0;
				base.item.shoot = base.mod.ProjectileType("AncientPickaxeAxePro");
				base.item.ranged = true;
			}
			else
			{
				base.item.noUseGraphic = false;
				base.item.noMelee = false;
				base.item.useTime = 6;
				base.item.pick = 300;
				base.item.axe = 35;
				base.item.shoot = 0;
				base.item.melee = true;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse == 2;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
