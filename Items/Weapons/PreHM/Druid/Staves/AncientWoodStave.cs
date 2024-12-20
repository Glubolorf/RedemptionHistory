using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Staves
{
	public class AncientWoodStave : DruidStave
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PreHM/Druid/Staves/" + base.GetType().Name + "_Glow");
				AncientWoodStave.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Ancient Wood Stave");
			base.Tooltip.SetDefault("Shoots a Nature Orb");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 19;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 4, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.glowMask = AncientWoodStave.customGlowMask;
			base.item.shoot = ModContent.ProjectileType<KingsOakShot4>();
			base.item.shootSpeed = 14f;
			this.defaultShoot = ModContent.ProjectileType<KingsOakShot4>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian5Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian5>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 48.2f;
			this.guardianName = "Ancient Pixie";
			this.guardianType = "Pixie";
			this.guardianAbility = "Swift-Cast/Druidic Embrace";
			this.guardianEffects = "Staves cast a lot faster, Druidic Enhancement";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 8);
			modRecipe.AddIngredient(179, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
