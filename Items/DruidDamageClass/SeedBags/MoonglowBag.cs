using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class MoonglowBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moonglow Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a Moonglow\nDeals more damage at nightime");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 8;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 1;
			base.item.mana = 3;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 70, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed14>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.JungleTiles;
			this.nativeText = "Jungle";
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (!Main.dayTime)
			{
				flat += 8f;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(176, 5);
			modRecipe.AddIngredient(314, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
