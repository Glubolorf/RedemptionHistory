using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class Bindeklinge : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Melee/" + base.GetType().Name + "_Glow");
				Bindeklinge.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = Bindeklinge.customGlowMask;
			base.DisplayName.SetDefault("Bindeklinge");
			base.Tooltip.SetDefault("'For those we must protect, onward!'\nCasts a pillar of rising flames upon hitting an enemy\nRight-clicking casts flames at cursor position occasionally");
		}

		public override void SetDefaults()
		{
			base.item.damage = 44;
			base.item.melee = true;
			base.item.width = 54;
			base.item.height = 54;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 1;
			base.item.knockBack = 6.5f;
			base.item.value = Item.sellPrice(0, 7, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = 0;
			base.item.shootSpeed = 0f;
			base.item.glowMask = Bindeklinge.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.shoot = 85;
				base.item.autoReuse = false;
			}
			else
			{
				base.item.shoot = 0;
				base.item.autoReuse = true;
			}
			return base.CanUseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (player.altFunctionUse <= 1)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, -3f, 85, 33, 15f, Main.myPlayer, 0f, 0f);
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2 && Main.rand.Next(2) == 0)
			{
				position = Main.MouseWorld;
				return true;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(368, 1);
			modRecipe.AddIngredient(121, 1);
			modRecipe.AddIngredient(19, 20);
			modRecipe.AddIngredient(178, 5);
			modRecipe.AddIngredient(177, 6);
			modRecipe.AddIngredient(548, 5);
			modRecipe.AddIngredient(549, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
