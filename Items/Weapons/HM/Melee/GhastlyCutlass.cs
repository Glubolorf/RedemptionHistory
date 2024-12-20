using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class GhastlyCutlass : ModItem
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
				GhastlyCutlass.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = GhastlyCutlass.customGlowMask;
			base.DisplayName.SetDefault("Ghastly Cutlass");
			base.Tooltip.SetDefault("'Summons the great spirits of this world'\nHitting an enemy has a chance to summon a Ghastly Spirit");
		}

		public override void SetDefaults()
		{
			base.item.damage = 91;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 74;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 10, 50, 50);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.alpha = 100;
			base.item.glowMask = GhastlyCutlass.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "GhostCutlass", 1);
			modRecipe.AddIngredient(150, 30);
			modRecipe.AddIngredient(154, 20);
			modRecipe.AddIngredient(1508, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<SpiritGhast>(), base.item.damage, base.item.knockBack, player.whoAmI, 0f, 1f);
			}
		}

		public static short customGlowMask;
	}
}
