using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class HauntedIceStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Haunted Ice Stave");
			base.Tooltip.SetDefault("'Frees haunted souls that were doomed in the cold'\nShoots a Chilling Soul");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 20;
			base.item.width = 52;
			base.item.height = 52;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 1, 25, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("IceSoulPro1");
			base.item.shootSpeed = 10f;
			this.defaultShoot = base.mod.ProjectileType("IceSoulPro1");
			this.guardianBuffID = base.mod.BuffType("NatureGuardian26Buff");
			this.guardianProjectileID = base.mod.ProjectileType("NatureGuardian26");
			this.guardianTime = 600;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 52.2f;
			this.guardianName = "Boreal Statuette";
			this.guardianType = "Mystic";
			this.guardianAbility = "Swift-Cast/Ice Shield";
			this.guardianEffects = "Staves cast a lot faster, lethal hits will be ignored";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2503, 14);
			modRecipe.AddIngredient(null, "LostSoul", 1);
			modRecipe.AddIngredient(664, 20);
			modRecipe.AddIngredient(86, 10);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(2503, 14);
			modRecipe2.AddIngredient(null, "LostSoul", 1);
			modRecipe2.AddIngredient(664, 20);
			modRecipe2.AddIngredient(1329, 10);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
