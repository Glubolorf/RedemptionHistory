using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TeslaCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Cannon");
			base.Tooltip.SetDefault("Fires Lightning Bolts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 250;
			base.item.useTime = 12;
			base.item.useAnimation = 12;
			base.item.autoReuse = true;
			base.item.shoot = 580;
			base.item.shootSpeed = 8f;
			base.item.UseSound = SoundID.Item92;
			base.item.ranged = true;
			base.item.width = 80;
			base.item.height = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(8, 50, 0, 0);
			base.item.rare = 11;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = -Main.player[Main.myPlayer].Center + Main.MouseWorld;
			float num = (float)Main.rand.Next(100);
			Vector2 vector2 = Vector2.Normalize(vector) * base.item.shootSpeed;
			Projectile.NewProjectile(player.Center.X, player.Center.Y, vector2.X, vector2.Y, type, damage, 0.5f, player.whoAmI, Utils.ToRotation(vector), num);
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-18f, 2f));
		}
	}
}
