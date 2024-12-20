using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedGrassSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Grass Blade");
			base.Tooltip.SetDefault("Shoots a spread of stingers");
		}

		public override void SetDefaults()
		{
			base.item.damage = 13;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 44, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.rare = 2;
			base.item.shootSpeed = 16f;
			base.item.shoot = 55;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 2 + Main.rand.Next(2);
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float num2 = 1f - Utils.NextFloat(Main.rand) * 0.4f;
				vector *= num2;
				int num3 = Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[num3].hostile = false;
				Main.projectile[num3].friendly = true;
				Main.projectile[num3].timeLeft = 60;
			}
			return false;
		}
	}
}
