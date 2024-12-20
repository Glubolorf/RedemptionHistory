using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class FDruidStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Druid's Stave");
			base.Tooltip.SetDefault("Shoots a leaf");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 42;
			base.item.width = 56;
			base.item.height = 60;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.crit = 4;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 2, 35, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = base.mod.ProjectileType("KingsOakShot2");
			base.item.shootSpeed = 16f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = base.mod.ProjectileType("KingsOakShot2");
			this.guardianBuffID = base.mod.BuffType("NatureGuardian2Buff");
			this.guardianProjectileID = base.mod.ProjectileType("NatureGuardian2");
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 48.2f;
			this.guardianName = "Nature Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Dryad's Blessing/Druidic Embrace";
			this.guardianEffects = "Staves cast a lot faster, Defence Boost, Druidic Enhancement";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ForestCore", 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
