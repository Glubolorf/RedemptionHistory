using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DungeonHammer2 : ModItem
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
				DungeonHammer2.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = DungeonHammer2.customGlowMask;
			base.DisplayName.SetDefault("Three-Headed Skeletal Warhammer");
			base.Tooltip.SetDefault("'I've got a bone to pick with you!'\nHitting an enemy will unleash 3 homing Lost Souls from the user");
		}

		public override void SetDefaults()
		{
			base.item.damage = 100;
			base.item.melee = true;
			base.item.width = 90;
			base.item.height = 90;
			base.item.useTime = 11;
			base.item.useAnimation = 35;
			base.item.hammer = 90;
			base.item.useStyle = 1;
			base.item.knockBack = 9f;
			base.item.value = Item.buyPrice(0, 8, 65, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
			base.item.tileBoost += 2;
			base.item.glowMask = DungeonHammer2.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DungeonHammer", 1);
			modRecipe.AddIngredient(154, 50);
			modRecipe.AddIngredient(1508, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(31, 160, false);
			}
			Projectile.NewProjectile(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), 297, base.item.damage, base.item.knockBack, player.whoAmI, 0f, 1f);
			Projectile.NewProjectile(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), 297, base.item.damage, base.item.knockBack, player.whoAmI, 0f, 1f);
			Projectile.NewProjectile(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), 297, base.item.damage, base.item.knockBack, player.whoAmI, 0f, 1f);
		}

		public static short customGlowMask;
	}
}
