using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Druid.Staves
{
	public class ShadeStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Stave of Shadows");
			base.Tooltip.SetDefault("Summons Shade Portals at cursor point\nIf 2 Shade Portals are active, the user can travel from one portal to the other\nTravelling through a portal will give a temporary buff and spawn a swarm of shadesouls to attack enemies\nRight-click to deactivate a Shade Portal\nTeleportation won't work while there's a boss alive");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 475;
			base.item.width = 46;
			base.item.height = 46;
			base.item.useTime = 50;
			base.item.useAnimation = 50;
			base.item.crit = 4;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.UseSound = SoundID.Item8;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<ShadePortalSummon>();
			base.item.shootSpeed = 16f;
			base.item.rare = 11;
			this.defaultShoot = ModContent.ProjectileType<ShadePortalSummon>();
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 46.2f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Main.MouseWorld;
			if (player.altFunctionUse == 2)
			{
				for (int g = 0; g < 1000; g++)
				{
					if (Main.projectile[g].active && (Main.projectile[g].type == type || Main.projectile[g].type == ModContent.ProjectileType<ShadeStavePortal>()))
					{
						Main.projectile[g].Kill();
						break;
					}
				}
			}
			else
			{
				if (player.ownedProjectileCounts[type] < 2 && player.ownedProjectileCounts[ModContent.ProjectileType<ShadeStavePortal>()] < 2)
				{
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<ShadeStavePortal>()] >= 2)
				{
					for (int g2 = 0; g2 < 1000; g2++)
					{
						if (Main.projectile[g2].active && (Main.projectile[g2].type == type || Main.projectile[g2].type == ModContent.ProjectileType<ShadeStavePortal>()))
						{
							Main.projectile[g2].Kill();
							break;
						}
					}
					Projectile.NewProjectile(position.X, position.Y, speedX, speedX, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
				}
			}
			return false;
		}
	}
}
