using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XeniumBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				XeniumBlade.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = XeniumBlade.customGlowMask;
			base.DisplayName.SetDefault("Xenium Blade");
			base.Tooltip.SetDefault("Shoots Xeno Blasts");
		}

		public override void SetDefaults()
		{
			base.item.width = 62;
			base.item.height = 68;
			base.item.damage = 165;
			base.item.melee = true;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.useTurn = true;
			base.item.useStyle = 1;
			base.item.rare = 11;
			base.item.knockBack = 7f;
			base.item.UseSound = SoundID.Item60;
			base.item.autoReuse = true;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.shoot = base.mod.ProjectileType("XenoShard4");
			base.item.shootSpeed = 24f;
			base.item.glowMask = XeniumBlade.customGlowMask;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 22);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
