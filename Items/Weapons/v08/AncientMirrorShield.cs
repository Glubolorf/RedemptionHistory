using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientMirrorShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Mirror Shield");
			base.Tooltip.SetDefault("Summons a mirror to reflect most projectiles\nIf the projectile is bigger than the mirror, the mirror will break");
		}

		public override void SetDefaults()
		{
			base.item.mana = 100;
			base.item.width = 28;
			base.item.height = 50;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.value = Item.sellPrice(0, 45, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("AncientMirrorShieldPro");
			base.item.shootSpeed = 0f;
			base.item.buffType = base.mod.BuffType("AncientMirrorBuff");
			base.item.buffTime = 36000;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.ownedProjectileCounts[base.mod.ProjectileType("AncientMirrorShieldPro")] == 0;
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
			modRecipe.AddIngredient(null, "AncientPowerCore", 14);
			modRecipe.AddIngredient(170, 6);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
