using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class KingSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cyber Tech");
			base.Tooltip.SetDefault("'He won't go easy on you...'\nSummons King Slayer III\nOnly usable at day\nCan only be used after the Keeper has been defeated\nNot consumable");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.rare = 9;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("KSEntrance")) && RedeWorld.downedTheKeeper;
		}

		public override bool UseItem(Player player)
		{
			int num = NPC.NewNPC((int)(player.position.X + (float)Main.rand.Next(100, 200)), (int)(player.position.Y - 0f), base.mod.NPCType("KSEntrance"), 0, 0f, 0f, 0f, 0f, 255);
			if (Main.netMode == 2 && num < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
			}
			Main.PlaySound(15, player.position, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk2Plating", 4);
			modRecipe.AddIngredient(null, "Mk2Capacitator", 2);
			modRecipe.AddIngredient(549, 5);
			modRecipe.AddIngredient(548, 5);
			modRecipe.AddIngredient(547, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
