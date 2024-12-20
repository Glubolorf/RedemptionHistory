using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedThornBow2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Withering Rose");
			base.Tooltip.SetDefault("Fires a cursed thorn along with two arrows");
		}

		public override void SetDefaults()
		{
			base.item.damage = 400;
			base.item.ranged = true;
			base.item.width = 38;
			base.item.height = 102;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 45, 0, 0);
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 30f;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().thornCrown)
			{
				flat += 50f;
			}
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX / 2f, speedY / 2f, ModContent.ProjectileType<CursedThornPro5>(), damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
