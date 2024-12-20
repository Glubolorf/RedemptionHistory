using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class KingsGreatstave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King's Oak Greatstave");
			base.Tooltip.SetDefault("Rapidly shoots either Acorns, Leaves, Pine Needles, Nature Orbs, or Spores");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 18;
			base.item.height = 78;
			base.item.width = 78;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.crit = 18;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1));
			base.item.shootSpeed = 12f;
			this.defaultShoot = base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1));
			this.guardianBuffID = base.mod.BuffType("NatureGuardian25Buff");
			this.guardianProjectileID = base.mod.ProjectileType("NatureGuardian25");
			this.guardianTime = 600;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 78.2f;
			this.guardianName = "Pixie Trinity";
			this.guardianType = "Pixie";
			this.guardianAbility = "Swift-Cast/Stream-Shot/Druidic Embrace/Warmth";
			this.guardianEffects = "Combined effects of all pixies";
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1));
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AcornStaff", 1);
			modRecipe.AddIngredient(null, "AncientWoodStave", 1);
			modRecipe.AddIngredient(null, "BorealStave", 1);
			modRecipe.AddIngredient(null, "EbonwoodStave", 1);
			modRecipe.AddIngredient(null, "LivingWoodStave", 1);
			modRecipe.AddIngredient(null, "MahoganyStave", 1);
			modRecipe.AddIngredient(null, "PalmStave", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "AcornStaff", 1);
			modRecipe2.AddIngredient(null, "AncientWoodStave", 1);
			modRecipe2.AddIngredient(null, "BorealStave", 1);
			modRecipe2.AddIngredient(null, "ShadewoodStave", 1);
			modRecipe2.AddIngredient(null, "LivingWoodStave", 1);
			modRecipe2.AddIngredient(null, "MahoganyStave", 1);
			modRecipe2.AddIngredient(null, "PalmStave", 1);
			modRecipe2.AddIngredient(null, "SmallLostSoul", 5);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
