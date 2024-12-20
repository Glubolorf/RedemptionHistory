using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedRocketLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Annihilator");
			base.Tooltip.SetDefault("Launches a powerful red Plasma Blast\nRight-Clicking will launch 4-6 weaker Plasma Blasts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 450;
			base.item.ranged = true;
			base.item.width = 106;
			base.item.height = 38;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 5;
			base.item.knockBack = 6f;
			base.item.UseSound = SoundID.Item61;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.shoot = base.mod.ProjectileType("PlasmaBlast1");
			base.item.shootSpeed = 16f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.damage = 100;
				base.item.useTime = 28;
				base.item.useAnimation = 28;
				base.item.shootSpeed = 16f;
				base.item.shoot = base.mod.ProjectileType("PlasmaBlast2");
			}
			else
			{
				base.item.damage = 450;
				base.item.useTime = 28;
				base.item.useAnimation = 28;
				base.item.shoot = base.mod.ProjectileType("PlasmaBlast1");
				base.item.shootSpeed = 16f;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				int num = 4 + Main.rand.Next(2);
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float num2 = 1f - Utils.NextFloat(Main.rand) * 0.1f;
					vector *= num2;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-40f, 0f));
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("EmpoweredBuff"), Main.rand.Next(50, 60), true);
		}
	}
}
