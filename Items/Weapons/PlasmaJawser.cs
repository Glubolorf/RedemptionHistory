using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class PlasmaJawser : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plasma Jawser");
			base.Tooltip.SetDefault("\nFires an Omega Laser\nRight-clicking fires 3 Omega Plasma Balls");
		}

		public override void SetDefaults()
		{
			base.item.damage = 100;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.channel = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("OmegaLaser");
			base.item.shootSpeed = 14f;
			base.item.UseSound = SoundID.Item91;
			base.item.magic = true;
			base.item.mana = 5;
			base.item.width = 68;
			base.item.height = 32;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 10;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.damage = 250;
				base.item.useTime = 23;
				base.item.UseSound = SoundID.Item125;
				base.item.useAnimation = 23;
				base.item.channel = false;
				base.item.autoReuse = true;
				base.item.shoot = base.mod.ProjectileType("OmegaPlasmaBall2");
				base.item.shootSpeed = 20f;
			}
			else
			{
				base.item.damage = 100;
				base.item.useTime = 20;
				base.item.useAnimation = 20;
				base.item.channel = true;
				base.item.autoReuse = false;
				base.item.shoot = base.mod.ProjectileType("OmegaLaser");
				base.item.shootSpeed = 14f;
				base.item.UseSound = SoundID.Item91;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				float num = 3f;
				float num2 = MathHelper.ToRadians(15f);
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
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-8f, 0f));
		}
	}
}
