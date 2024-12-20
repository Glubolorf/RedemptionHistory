using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class PlasmaJawser : ModItem
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
				PlasmaJawser.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = PlasmaJawser.customGlowMask;
			base.DisplayName.SetDefault("Plasma Jawser");
			base.Tooltip.SetDefault("\nFires an Omega Laser\nRight-clicking fires 3 Omega Plasma Balls");
		}

		public override void SetDefaults()
		{
			base.item.damage = 120;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.channel = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<OmegaLaser>();
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
			base.item.glowMask = PlasmaJawser.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.damage = 200;
				base.item.useTime = 50;
				base.item.UseSound = SoundID.Item125;
				base.item.useAnimation = 50;
				base.item.channel = false;
				base.item.autoReuse = true;
				base.item.shoot = ModContent.ProjectileType<OmegaPlasmaBall2>();
				base.item.shootSpeed = 20f;
			}
			else
			{
				base.item.damage = 120;
				base.item.useTime = 20;
				base.item.useAnimation = 20;
				base.item.channel = true;
				base.item.autoReuse = false;
				base.item.shoot = ModContent.ProjectileType<OmegaLaser>();
				base.item.shootSpeed = 14f;
				base.item.UseSound = SoundID.Item91;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				float numberProjectiles = 3f;
				float rotation = MathHelper.ToRadians(15f);
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
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-8f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "OblitBrain", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 8);
			modRecipe.AddIngredient(null, "VlitchScale", 22);
			modRecipe.AddIngredient(null, "VlitchBattery", 2);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
