using System;
using Redemption.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class XenomiteDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Drill");
		}

		public override void SetDefaults()
		{
			base.item.damage = 28;
			base.item.melee = true;
			base.item.width = 56;
			base.item.height = 26;
			base.item.useTime = 7;
			base.item.useAnimation = 15;
			base.item.channel = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.pick = 195;
			base.item.useStyle = 5;
			base.item.knockBack = 4f;
			base.item.value = 550000;
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item23;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<XenomiteDrillPro>();
			base.item.shootSpeed = 40f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 10);
			modRecipe.AddIngredient(null, "StarliteBar", 15);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
