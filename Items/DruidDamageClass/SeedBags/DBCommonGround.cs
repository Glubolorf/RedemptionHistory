using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class DBCommonGround : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Duality Seedbag - Common Ground");
			base.Tooltip.SetDefault("Tosses an seed that forms a big cluster of thorns with the tips being empowered deathweeds");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 60;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 42;
			base.item.useAnimation = 42;
			base.item.useStyle = 1;
			base.item.mana = 12;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("Seed27");
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.EvilTiles;
			this.nativeText = "Corruption/Crimson";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "VilethornBushBag", 1);
			modRecipe.AddIngredient(null, "DeathweedBag", 1);
			modRecipe.AddIngredient(null, "SoulOfBloom", 15);
			modRecipe.AddTile(null, "BotanistStationTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "CrimthornBushBag", 1);
			modRecipe2.AddIngredient(null, "DeathweedBag", 1);
			modRecipe2.AddIngredient(null, "SoulOfBloom", 15);
			modRecipe2.AddTile(null, "BotanistStationTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
