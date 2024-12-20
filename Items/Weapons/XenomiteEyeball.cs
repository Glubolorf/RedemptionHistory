using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XenomiteEyeball : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Eyeball");
			base.Tooltip.SetDefault("Rapidly fires green lasers");
		}

		public override void SetDefaults()
		{
			base.item.damage = 14;
			base.item.magic = true;
			base.item.mana = 3;
			base.item.width = 32;
			base.item.height = 30;
			base.item.useAnimation = 3;
			base.item.useTime = 3;
			base.item.reuseDelay = 2;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 1f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 3;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/XenoEyeLaser1");
			base.item.autoReuse = true;
			base.item.shoot = 20;
			base.item.shootSpeed = 5f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 2;
			int num2 = 2;
			float num3 = 0.3f;
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
				Projectile.NewProjectile(vector.X, vector.Y, num4, num5, 20, damage, knockBack, Main.myPlayer, 0f, 0f);
			}
			return false;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
		}
	}
}
