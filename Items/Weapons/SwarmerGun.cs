using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SwarmerGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Swarmer Cannon");
			base.Tooltip.SetDefault("Fires a barrage of Swarm Growths that explode onto enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 225;
			base.item.ranged = true;
			base.item.width = 64;
			base.item.height = 36;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 1f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.UseSound = SoundID.NPCDeath13;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("SwarmGrowthPro");
			base.item.shootSpeed = 10f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3 + Main.rand.Next(6);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(8f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-8f, 0f));
		}
	}
}
