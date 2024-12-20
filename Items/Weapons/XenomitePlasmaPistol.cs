using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XenomitePlasmaPistol : ModItem
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
				XenomitePlasmaPistol.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = XenomitePlasmaPistol.customGlowMask;
			base.DisplayName.SetDefault("Xenomite Plasma Pistol");
		}

		public override void SetDefaults()
		{
			base.item.damage = 60;
			base.item.magic = true;
			base.item.width = 44;
			base.item.height = 30;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = 10000;
			base.item.rare = 7;
			base.item.mana = 5;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<TiedProjectile>();
			base.item.shootSpeed = 30f;
			base.item.glowMask = XenomitePlasmaPistol.customGlowMask;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-4f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 25);
			modRecipe.AddIngredient(null, "StarliteBar", 5);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(10, 20), true);
		}

		public static short customGlowMask;
	}
}
