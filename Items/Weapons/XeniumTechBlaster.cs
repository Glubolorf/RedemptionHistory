using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XeniumTechBlaster : ModItem
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
				XeniumTechBlaster.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = XeniumTechBlaster.customGlowMask;
			base.DisplayName.SetDefault("Xenium Railgun");
			base.Tooltip.SetDefault("Rapidly fires Xenium Rail Blasts\nRight-clicking will shoot your normal bullets");
		}

		public override void SetDefaults()
		{
			base.item.damage = 220;
			base.item.ranged = true;
			base.item.width = 86;
			base.item.height = 42;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.knockBack = 5f;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/Launch2");
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.shoot = ModContent.ProjectileType<XenoShard5>();
			base.item.shootSpeed = 50f;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.glowMask = XeniumTechBlaster.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.shoot = 10;
				base.item.shootSpeed = 100f;
				base.item.useAmmo = AmmoID.Bullet;
			}
			else
			{
				base.item.shoot = ModContent.ProjectileType<XenoShard5>();
				base.item.shootSpeed = 50f;
				base.item.useAmmo = 0;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX * 0.99f, speedY * 0.99f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-10f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 20);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
