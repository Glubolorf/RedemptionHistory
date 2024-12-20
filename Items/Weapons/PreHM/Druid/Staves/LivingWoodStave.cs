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
	public class LivingWoodStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Stave");
			base.Tooltip.SetDefault("Shoots a leaf");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 6;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 1, 25);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<KingsOakShot2>();
			base.item.shootSpeed = 7f;
			this.defaultShoot = ModContent.ProjectileType<KingsOakShot2>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardianBuff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian1>();
			this.guardianTime = 1500;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 48.2f;
			this.guardianName = "Nature Pixie";
			this.guardianType = "Pixie";
			this.guardianAbility = "Swift-Cast";
			this.guardianEffects = "Staves cast a lot faster";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 8);
			modRecipe.AddIngredient(27, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
