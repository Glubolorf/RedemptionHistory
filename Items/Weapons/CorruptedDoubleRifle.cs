using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedDoubleRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Double Rifle");
			base.Tooltip.SetDefault("33% chance not to consume ammo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 265;
			base.item.ranged = true;
			base.item.width = 58;
			base.item.height = 42;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item36;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 90f;
			base.item.useAmmo = AmmoID.Bullet;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Utils.NextFloat(Main.rand) >= 0.33f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 14)
			{
				type = 242;
			}
			float num = 2f;
			float num2 = MathHelper.ToRadians(3f);
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

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("EmpoweredBuff"), Main.rand.Next(50, 60), true);
		}
	}
}
