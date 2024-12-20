using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Staves
{
	public class TerraStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terra Stave");
			base.Tooltip.SetDefault("");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 98;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 20, 0, 30);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<TerraBallPro>();
			base.item.shootSpeed = 17f;
			this.defaultShoot = ModContent.ProjectileType<TerraBallPro>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian19Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian19>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 56f;
			this.guardianName = "Terra Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Scatter-Shot/Terra's Embrace";
			this.guardianEffects = "Staves cast a lot faster, Staves that shoot a single projectile will instead shoot a cluster, Druidic Enhancement+++";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TrueLunarCrescentStave", 1);
			modRecipe.AddIngredient(null, "TrueHallowedStave", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
