using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class FrostyStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chillblood Staff");
			base.Tooltip.SetDefault("Shoots inaccurate Chilling Spheres");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 74;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.width = 68;
			base.item.height = 68;
			base.item.useTime = 14;
			base.item.useAnimation = 14;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = 5500;
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item27;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<SnowyBall>();
			base.item.shootSpeed = 18f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
}
