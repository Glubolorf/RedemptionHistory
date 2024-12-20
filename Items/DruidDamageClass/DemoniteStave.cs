using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DemoniteStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Demonite Stave");
			base.Tooltip.SetDefault("Shoots a Corrupt Blast");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 19;
			base.item.width = 38;
			base.item.height = 38;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 27, 25);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = base.mod.ProjectileType("CorruptBlast2");
			base.item.shootSpeed = 16f;
			base.item.autoReuse = true;
			this.defaultShoot = base.mod.ProjectileType("CorruptBlast2");
			this.guardianBuffID = base.mod.BuffType("NatureGuardian8Buff");
			this.guardianProjectileID = base.mod.ProjectileType("NatureGuardian8");
			this.guardianTime = 1500;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 38.2f;
			this.guardianName = "Corrupt Fairy";
			this.guardianType = "Fairy";
			this.guardianAbility = "Swift-Cast/Corrupt Aura";
			this.guardianEffects = "Staves cast a lot faster, Mobility Enhancement while in Corruption";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(57, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
