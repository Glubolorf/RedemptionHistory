using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class Synthesizer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xeno Synthesizer");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 32;
			base.item.useTime = 32;
			base.item.knockBack = 0f;
			base.item.rare = 7;
			base.item.damage = 52;
			base.item.shoot = ModContent.ProjectileType<XenoSynthPro>();
			base.item.shootSpeed = 11f;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/SynthSound");
			base.item.magic = true;
			base.item.mana = 4;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 20);
			modRecipe.AddIngredient(null, "Biohazard", 8);
			modRecipe.AddIngredient(549, 15);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
