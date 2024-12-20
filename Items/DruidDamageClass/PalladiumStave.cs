using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PalladiumStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Palladium Stave");
			base.Tooltip.SetDefault("Shoots an orange bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 40;
			base.item.width = 42;
			base.item.height = 42;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 84, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = 122;
			base.item.shootSpeed = 14f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = 122;
			this.guardianBuffID = ModContent.BuffType<NatureGuardian2Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian2>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 42.2f;
			this.guardianName = "Nature Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Dryad's Blessing/Druidic Embrace";
			this.guardianEffects = "Staves cast a lot faster, Defence Boost, Druidic Enhancement";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(1184, 8);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
