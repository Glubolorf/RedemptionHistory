using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ChiliSpray : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chilli Powder Spray");
			base.Tooltip.SetDefault("Burn your enemies with spicy spice");
		}

		public override void SetDefaults()
		{
			base.item.damage = 43;
			base.item.magic = true;
			base.item.mana = 3;
			base.item.width = 16;
			base.item.height = 30;
			base.item.useAnimation = 4;
			base.item.useTime = 4;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 1f;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item13;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("ChiliPowder");
			base.item.shootSpeed = 4f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 2;
			int num2 = 2;
			float num3 = 0.8f;
			Vector2 vector = default(Vector2);
			for (int i = 0; i < num; i++)
			{
				float num4 = 8f * speedX + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num5 = 8f * speedY + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num6 = (float)Math.Atan((double)(num5 / num4));
				vector..ctor(position.X + 75f * (float)Math.Cos((double)num6), position.Y + 75f * (float)Math.Sin((double)num6));
				float num7 = (float)Main.mouseX + Main.screenPosition.X;
				if (num7 < player.position.X)
				{
					vector..ctor(position.X - 75f * (float)Math.Cos((double)num6), position.Y - 75f * (float)Math.Sin((double)num6));
				}
				Projectile.NewProjectile(vector.X, vector.Y, num4, num5, base.mod.ProjectileType("ChiliPowder"), damage, knockBack, Main.myPlayer, 0f, 0f);
			}
			return false;
		}
	}
}
