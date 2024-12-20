using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class GirusDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				GirusDagger.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = GirusDagger.customGlowMask;
			base.DisplayName.SetDefault("Girus' Dagger");
		}

		public override void SetDefaults()
		{
			base.item.damage = 65;
			base.item.melee = true;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 85f;
			base.item.shoot = ModContent.ProjectileType<GirusDaggerPro>();
			base.item.glowMask = GirusDagger.customGlowMask;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = (float)(4 + Main.rand.Next(2));
			float rotation = MathHelper.ToRadians(35f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int i = 0;
			while ((float)i < numberProjectiles)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				i++;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "GirusDaggerThrown", 1);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
