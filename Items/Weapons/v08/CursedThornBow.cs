using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedThornBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Bow");
			base.Tooltip.SetDefault("Fires a short-ranged cursed thorn along with the arrow");
		}

		public override void SetDefaults()
		{
			base.item.damage = 14;
			base.item.ranged = true;
			base.item.width = 24;
			base.item.height = 54;
			base.item.useTime = 21;
			base.item.useAnimation = 21;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 0, 44, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = false;
			base.item.shoot = 10;
			base.item.shootSpeed = 10f;
			base.item.useAmmo = AmmoID.Arrow;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, base.mod.ProjectileType("CursedThornPro4"), damage / 3, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}
	}
}
