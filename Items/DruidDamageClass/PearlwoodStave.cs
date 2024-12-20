using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PearlwoodStave : DruidStave
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/DruidDamageClass/" + base.GetType().Name + "_Glow");
				PearlwoodStave.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Pearlwood Stave");
			base.Tooltip.SetDefault("Shoots a pink bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 27;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 4, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.glowMask = PearlwoodStave.customGlowMask;
			base.item.shoot = 121;
			base.item.shootSpeed = 8f;
			this.defaultShoot = 121;
			this.guardianBuffID = ModContent.BuffType<NatureGuardian7Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian7>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 64.2f;
			this.guardianName = "Hallowed Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Healing Aura/Druidic Embrace";
			this.guardianEffects = "Staves cast a lot faster, Life Boost, Druidic Enhancement";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(621, 8);
			modRecipe.AddIngredient(502, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
