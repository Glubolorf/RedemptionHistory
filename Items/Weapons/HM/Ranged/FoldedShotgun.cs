using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Ranged
{
	public class FoldedShotgun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Folded Shotgun");
		}

		public override void SetDefaults()
		{
			base.item.damage = 19;
			base.item.ranged = true;
			base.item.width = 48;
			base.item.height = 20;
			base.item.useTime = 34;
			base.item.useAnimation = 34;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item36;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 16f;
			base.item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.1f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
