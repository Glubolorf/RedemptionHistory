using System;
using Redemption.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class ElectrifiedPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Insulatium Potion");
			base.Tooltip.SetDefault("Provides immunity to the Electrified debuff\nElectrifies nearby enemies");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.width = 20;
			base.item.height = 34;
			base.item.maxStack = 30;
			base.item.value = Item.sellPrice(0, 0, 85, 0);
			base.item.rare = 11;
			base.item.buffType = ModContent.BuffType<InsulatiumPotionBuff>();
			base.item.buffTime = 36000;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.alchemy = true;
			modRecipe.AddIngredient(null, "AbyssStinger", 1);
			modRecipe.AddIngredient(126, 1);
			modRecipe.AddTile(13);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
