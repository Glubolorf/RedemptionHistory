using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedThornPickaxeAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Pickaxe Axe");
			base.Tooltip.SetDefault("Right-clicking will shoot an illusion of the pickaxe axe, dealing magic damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 590;
			base.item.melee = true;
			base.item.width = 62;
			base.item.height = 64;
			base.item.useTime = 6;
			base.item.useAnimation = 10;
			base.item.pick = 300;
			base.item.axe = 35;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = base.mod.ProjectileType("CursedThornPickaxeAxePro");
			base.item.shootSpeed = 15f;
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
				base.item.useTime = 6;
				base.item.pick = 0;
				base.item.axe = 0;
				base.item.shoot = base.mod.ProjectileType("CursedThornPickaxeAxePro");
				base.item.magic = true;
			}
			else
			{
				base.item.useTime = 6;
				base.item.pick = 300;
				base.item.axe = 35;
				base.item.shoot = 0;
				base.item.melee = true;
			}
			return base.CanUseItem(player);
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

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse == 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
