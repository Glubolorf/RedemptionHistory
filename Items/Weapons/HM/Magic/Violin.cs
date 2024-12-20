using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class Violin : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Violin");
			base.Tooltip.SetDefault("'Music will destroy all...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 60;
			base.item.magic = true;
			base.item.mana = 6;
			base.item.width = 38;
			base.item.height = 40;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.rare = 7;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<ViolinProj1>();
			base.item.shootSpeed = 14f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!Main.dedServ)
			{
				float cursorPosFromPlayer = player.Distance(Main.MouseWorld) / (float)(Main.screenHeight / 2 / 24);
				if (cursorPosFromPlayer > 24f)
				{
					cursorPosFromPlayer = 1f;
				}
				else
				{
					cursorPosFromPlayer = cursorPosFromPlayer / 12f - 1f;
				}
				Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, base.mod.GetSoundSlot(2, "Sounds/Item/TheViolinSound"), 1f, cursorPosFromPlayer);
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<TheTrueViolin>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
