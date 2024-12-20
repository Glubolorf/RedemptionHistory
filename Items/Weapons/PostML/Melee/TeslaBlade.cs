using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class TeslaBlade : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PostML/Melee/" + base.GetType().Name + "_Glow");
				TeslaBlade.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = TeslaBlade.customGlowMask;
			base.DisplayName.SetDefault("Tesla Blade");
			base.Tooltip.SetDefault("Shoots out 3 Holoblades");
		}

		public override void SetDefaults()
		{
			base.item.damage = 150;
			base.item.melee = true;
			base.item.width = 70;
			base.item.height = 82;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 20f;
			base.item.shoot = ModContent.ProjectileType<TeslaBladePro>();
			base.item.glowMask = TeslaBlade.customGlowMask;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX * 0.8f, speedY * 0.8f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 0.6f, speedY * 0.6f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 0.8f, speedY * 0.8f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 0.6f, speedY * 0.6f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(2880, 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
