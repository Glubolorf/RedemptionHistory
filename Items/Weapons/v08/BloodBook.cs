using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class BloodBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crimson Storm");
			base.Tooltip.SetDefault("Rapidly shoots Blood Crystals that stick to enemies, draining life");
		}

		public override void SetDefaults()
		{
			base.item.damage = 200;
			base.item.magic = true;
			base.item.width = 32;
			base.item.height = 36;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.mana = 6;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = SoundID.Item101;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("BloodBookPro1");
			base.item.shootSpeed = 5f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int ShotAmt = 2;
			int spread = 2;
			float spreadMult = 0.3f;
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
				Projectile.NewProjectile(vector2.X, vector2.Y, vX, vY, base.mod.ProjectileType("BloodBookPro1"), damage, knockBack, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(vector2.X, vector2.Y, vX * 0.6f, vY * 0.6f, base.mod.ProjectileType("BloodBookPro1"), damage, knockBack, Main.myPlayer, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteBlood", 7);
			modRecipe.AddIngredient(172, 15);
			modRecipe.AddIngredient(518, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
