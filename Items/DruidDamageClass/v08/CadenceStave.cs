using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class CadenceStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cadence");
			base.Tooltip.SetDefault("Shoots erratic-moving hearts");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 41;
			base.item.width = 44;
			base.item.height = 44;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.crit = 4;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item109;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<CadencePro>();
			base.item.shootSpeed = 16f;
			this.guardianBuffID = ModContent.BuffType<NatureGuardian7Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian7>();
			this.guardianTime = 1200;
			this.defaultShoot = ModContent.ProjectileType<CadencePro>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 44.2f;
			this.guardianName = "Hallowed Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Healing Aura/Druidic Embrace";
			this.guardianEffects = "Staves cast a lot faster, Life Boost, Druidic Enhancement";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "OrichalcumStave", 1);
			modRecipe.AddIngredient(526, 2);
			modRecipe.AddIngredient(501, 8);
			modRecipe.AddIngredient(621, 24);
			modRecipe.AddIngredient(520, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "MythrilStave", 1);
			modRecipe2.AddIngredient(526, 2);
			modRecipe2.AddIngredient(501, 8);
			modRecipe2.AddIngredient(621, 24);
			modRecipe2.AddIngredient(520, 8);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
