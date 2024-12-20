using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class PiercingNebulaWeapon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Piercing Nebula");
			base.Tooltip.SetDefault("Throw a Piercing Nebula that pierces through anything");
		}

		public override void SetDefaults()
		{
			base.item.damage = 444;
			base.item.melee = true;
			base.item.width = 82;
			base.item.height = 82;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.knockBack = 8f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = SoundID.Item117;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("PiercingNebWeaponPro");
			base.item.shootSpeed = 18f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(RedeColor.NebColour);
				}
			}
		}

		public override bool Shoot(Player player, ref Vector2 shootPos, ref float speedX, ref float speedY, ref int projType, ref int damage, ref float knockback)
		{
			Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX, speedY, projType, damage, knockback, player.whoAmI, 0f, 0f);
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX * 1f, speedY * 1f, (i == 0) ? base.mod.ProjectileType("PNebulaWep2") : base.mod.ProjectileType("PNebulaWep3"), damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
