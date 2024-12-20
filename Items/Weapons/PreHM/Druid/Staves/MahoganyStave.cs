using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Staves
{
	public class MahoganyStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mahogany Stave");
			base.Tooltip.SetDefault("Shoots a spore");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 8;
			base.item.width = 42;
			base.item.height = 46;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 1, 25);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<KingsOakShot5>();
			base.item.shootSpeed = 7f;
			this.defaultShoot = ModContent.ProjectileType<KingsOakShot5>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian24Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian24>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 42.2f;
			this.guardianName = "Spore Pixie";
			this.guardianType = "Pixie";
			this.guardianAbility = "Swift-Cast";
			this.guardianEffects = "Staves cast a lot faster";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(620, 8);
			modRecipe.AddIngredient(331, 3);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
