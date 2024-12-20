using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MythrilStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Mythril Stave");
			base.Tooltip.SetDefault("Shoots a green bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 43;
			base.item.width = 52;
			base.item.height = 52;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 2, 7, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = 124;
			base.item.shootSpeed = 14f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = 124;
			this.guardianBuffID = ModContent.BuffType<NatureGuardian2Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian2>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 52.2f;
			this.guardianName = "Nature Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Dryad's Blessing/Druidic Embrace";
			this.guardianEffects = "Staves cast a lot faster, Defence Boost, Druidic Enhancement";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(382, 8);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
