using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class RainbowCat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Legendary Rainbow Cat");
			base.Tooltip.SetDefault("'A recolour!? How have I still not resprited this yet?'\nSummons Hallam... If he feels like it\nOnly usable at day");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 32;
			base.item.maxStack = 20;
			base.item.rare = 11;
			base.item.expert = true;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("KingChickenT"));
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("KingChickenT"));
			Main.PlaySound(15, player.position, 0);
			string text = "Nah, don't feel like it...";
			Color rarityPink = Colors.RarityPink;
			byte r = rarityPink.R;
			Color rarityPink2 = Colors.RarityPink;
			byte g = rarityPink2.G;
			Color rarityPink3 = Colors.RarityPink;
			Main.NewText(text, r, g, rarityPink3.B, false);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 999);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
