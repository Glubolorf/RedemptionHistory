using System;
using Microsoft.Xna.Framework;
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
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
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
			return Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("KSEntrance")) && RedeWorld.downedTheKeeper && !NPC.AnyNPCs(base.mod.NPCType("KSEntranceClone")) && !NPC.AnyNPCs(base.mod.NPCType("KSNope"));
		}

		public override bool UseItem(Player player)
		{
			Mod AAMod = ModLoader.GetMod("AAMod");
			if (RedeWorld.KSRajahInteraction && NPC.AnyNPCs(AAMod.NPCType("Rajah")))
			{
				string text = "Nope. You deal with the oversized rabbit.";
				Color rarityCyan = Colors.RarityCyan;
				byte r = rarityCyan.R;
				rarityCyan = Colors.RarityCyan;
				byte g = rarityCyan.G;
				rarityCyan = Colors.RarityCyan;
				Main.NewText(text, r, g, rarityCyan.B, false);
			}
			if (RedeWorld.girusTalk3)
			{
				Main.NewText("King Slayer III emerges... ?", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Redemption.SpawnBoss(player, "KSEntranceClone", false, new Vector2(player.position.X + (float)Main.rand.Next(100, 200), player.position.Y - 80f), " ", false);
				Main.PlaySound(15, player.position, 0);
			}
			else
			{
				if (AAMod != null && NPC.AnyNPCs(AAMod.NPCType("Rajah")))
				{
					Main.NewText("King Slayer III emerges!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
					Redemption.SpawnBoss(player, "KSNope", false, new Vector2(player.position.X + (float)Main.rand.Next(100, 200), player.position.Y - 80f), " ", false);
					Main.PlaySound(15, player.position, 0);
					RedeWorld.KSRajahInteraction = true;
					return true;
				}
				Main.NewText("King Slayer III emerges!", Color.MediumPurple.R, Color.MediumPurple.G, Color.MediumPurple.B, false);
				Redemption.SpawnBoss(player, "KSEntrance", false, new Vector2(player.position.X + (float)Main.rand.Next(100, 200), player.position.Y - 80f), " ", false);
				Main.PlaySound(15, player.position, 0);
			}
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
