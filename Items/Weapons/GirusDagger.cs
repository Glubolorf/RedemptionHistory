using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class GirusDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus' Dagger");
		}

		public override void SetDefaults()
		{
			base.item.damage = 165;
			base.item.melee = true;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 85f;
			base.item.shoot = base.mod.ProjectileType("GirusDaggerPro");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num = (float)(4 + Main.rand.Next(2));
			float num2 = MathHelper.ToRadians(35f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int num3 = 0;
			while ((float)num3 < num)
			{
				Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				num3++;
			}
			return false;
		}
	}
}
