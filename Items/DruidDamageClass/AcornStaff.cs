using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class AcornStaff : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Acorn Stave");
			base.Tooltip.SetDefault("Shoots an acorn");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 4;
			base.item.width = 46;
			base.item.height = 50;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 0, 75);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("Acorn1Pro");
			base.item.shootSpeed = 7f;
			this.defaultShoot = base.mod.ProjectileType("Acorn1Pro");
			this.guardianBuffID = base.mod.BuffType("NatureGuardianBuff");
			this.guardianProjectileID = base.mod.ProjectileType("NatureGuardian1");
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 50.2f;
			this.guardianName = "Nature Pixie";
			this.guardianType = "Pixie";
			this.guardianAbility = "Swift-Cast";
			this.guardianEffects = "Staves cast a lot faster";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(9, 8);
			modRecipe.AddIngredient(27, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
