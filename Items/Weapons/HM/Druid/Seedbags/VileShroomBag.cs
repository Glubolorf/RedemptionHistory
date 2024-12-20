using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Seedbags
{
	public class VileShroomBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Shroom Seedbag");
			base.Tooltip.SetDefault("Throws a capsule that grows a fume-spitting Cursed Shroom\nThe fumes inflict Cursed Inferno for a long duration");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 1;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 31;
			base.item.useAnimation = 31;
			base.item.useStyle = 1;
			base.item.mana = 8;
			base.item.crit = 4;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 6, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed31>();
			base.item.shootSpeed = 18f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(522, 10);
			modRecipe.AddIngredient(60, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
