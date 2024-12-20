using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GreatChickenShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Greater Chickman Escutcheon");
			base.Tooltip.SetDefault("+12 defense and knockback immunity while holding this\nLeft-Click to thrust the shield forward, bashing onto enemies and dealing heavy damage and knockback");
		}

		public override void SetDefaults()
		{
			base.item.damage = 2000;
			base.item.melee = true;
			base.item.useTime = 50;
			base.item.useAnimation = 50;
			base.item.useStyle = 5;
			base.item.knockBack = 18f;
			base.item.autoReuse = false;
			base.item.useTurn = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("GreatChickenShieldPro2");
			base.item.shootSpeed = 0f;
			base.item.width = 30;
			base.item.height = 38;
			base.item.value = Item.buyPrice(0, 12, 0, 0);
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("ChickenShieldBuff"), 4, true);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("GreatChickenShieldPro1")] == 0)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, base.mod.ProjectileType("GreatChickenShieldPro1"), 0, 0f, player.whoAmI, 0f, 0f);
			}
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
	}
}
