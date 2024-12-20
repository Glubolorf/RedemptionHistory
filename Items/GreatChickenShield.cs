using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.v08;
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
			base.item.shoot = ModContent.ProjectileType<GreatChickenShieldPro2>();
			base.item.shootSpeed = 0f;
			base.item.width = 30;
			base.item.height = 38;
			base.item.value = Item.buyPrice(0, 12, 0, 0);
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<ChickenShieldBuff>(), 4, true);
			if (player.ownedProjectileCounts[ModContent.ProjectileType<GreatChickenShieldPro1>()] == 0)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<GreatChickenShieldPro1>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
