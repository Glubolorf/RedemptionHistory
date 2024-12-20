using System;
using Redemption.Buffs.Wasteland;
using Redemption.Items.Materials.HM;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.Ammo
{
	public class BioweaponFlask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flask of Corrosion");
			base.Tooltip.SetDefault("Melee attacks inflict Cellular Destruction");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 15;
			base.item.maxStack = 30;
			base.item.consumable = true;
			base.item.width = 22;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 7;
			base.item.buffType = ModContent.BuffType<BioweaponFlaskBuff>();
			base.item.buffTime = 52000;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(126, 1);
			modRecipe.AddIngredient(ModContent.ItemType<Bioweapon>(), 2);
			modRecipe.AddTile(243);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
