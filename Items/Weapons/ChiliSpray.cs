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
			int ShotAmt = 2;
			int spread = 2;
			float spreadMult = 0.8f;
			Vector2 vector2 = default(Vector2);
			for (int i = 0; i < ShotAmt; i++)
			{
				float vX = 8f * speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
				float vY = 8f * speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
				float angle = (float)Math.Atan((double)(vY / vX));
				vector2 = new Vector2(position.X + 75f * (float)Math.Cos((double)angle), position.Y + 75f * (float)Math.Sin((double)angle));
				if ((float)Main.mouseX + Main.screenPosition.X < player.position.X)
				{
					vector2 = new Vector2(position.X - 75f * (float)Math.Cos((double)angle), position.Y - 75f * (float)Math.Sin((double)angle));
				}
				Projectile.NewProjectile(vector2.X, vector2.Y, vX, vY, base.mod.ProjectileType("ChiliPowder"), damage, knockBack, Main.myPlayer, 0f, 0f);
			}
			return false;
		}
	}
}
