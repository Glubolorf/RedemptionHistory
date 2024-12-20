using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class OrichalcumStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Orichalcum Stave");
			base.Tooltip.SetDefault("Shoots a pink bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 46;
			base.item.width = 44;
			base.item.height = 44;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 2, 53, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = 121;
			base.item.shootSpeed = 14f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = 121;
			this.guardianBuffID = ModContent.BuffType<NatureGuardian2Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian2>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 44.2f;
			this.guardianName = "Nature Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Dryad's Blessing/Druidic Embrace";
			this.guardianEffects = "Staves cast a lot faster, Defence Boost, Druidic Enhancement";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(1191, 8);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
