using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class RedemptionTeller : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chalice of Alignment");
			base.Tooltip.SetDefault("Tells you your current alignment\n[c/ffea9b:A sentient treasure, cursed with visions of what is yet to come]");
		}

		public override void SetDefaults()
		{
			base.item.width = 10;
			base.item.height = 10;
			base.item.maxStack = 1;
			base.item.noUseGraphic = true;
			base.item.value = 22000;
			base.item.rare = 3;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.consumable = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedGem", 1);
			modRecipe.AddIngredient(2258, 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool UseItem(Player player)
		{
			Main.NewText(string.Concat(RedeWorld.redemptionPoints) ?? "", Color.Gold, false);
			if (RedeWorld.redemptionPoints < 10 && RedeWorld.downedNebuleus)
			{
				Main.NewText("<Chalice of Alignment> ... What have you done...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints == 0)
			{
				Main.NewText("<Chalice of Alignment> You are truely neutral...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= -1 && RedeWorld.redemptionPoints <= 1)
			{
				Main.NewText("<Chalice of Alignment> You are safe for now...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= -3 && RedeWorld.redemptionPoints <= -2)
			{
				Main.NewText("<Chalice of Alignment> Be wary, you are straying from the path of good...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= 2 && RedeWorld.redemptionPoints <= 3)
			{
				Main.NewText("<Chalice of Alignment> You are choosing the right path. Please, continue.", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= -5 && RedeWorld.redemptionPoints <= -4)
			{
				Main.NewText("<Chalice of Alignment> You are really pushing it aren't you... If you continue this road, he will come...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= 4 && RedeWorld.redemptionPoints <= 5)
			{
				Main.NewText("<Chalice of Alignment> I am proud of you for keeping the light within you bright...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= -7 && RedeWorld.redemptionPoints <= -6)
			{
				Main.NewText("<Chalice of Alignment> ... Listen, you are following the wrong path here... Please, go back.", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= 6 && RedeWorld.redemptionPoints <= 7)
			{
				Main.NewText("<Chalice of Alignment> Vanquishing the evil of the world... You really are something.", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= -9 && RedeWorld.redemptionPoints <= -8)
			{
				Main.NewText("<Chalice of Alignment> I am sorry, you can't go back now...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= 8 && RedeWorld.redemptionPoints <= 9)
			{
				Main.NewText("<Chalice of Alignment> Light shines within you, but I am sure more dangerous foes lie ahead...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints <= -10)
			{
				Main.NewText("<Chalice of Alignment> You are past redemption...", Color.DarkGoldenrod, false);
			}
			else if (RedeWorld.redemptionPoints >= 10)
			{
				Main.NewText("<Chalice of Alignment> You are the redemption this world needed...", Color.DarkGoldenrod, false);
			}
			return true;
		}
	}
}
