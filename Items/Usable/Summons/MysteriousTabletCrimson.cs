using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.TheKeeper;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
{
	public class MysteriousTabletCrimson : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Tablet");
			base.Tooltip.SetDefault("Summons the Keeper\nOnly usable at night\nNot consumable");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 36;
			base.item.maxStack = 1;
			base.item.rare = 2;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			player.GetModPlayer<RedePlayer>();
			return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<Keeper>()) && !NPC.AnyNPCs(ModContent.NPCType<KeeperSpirit>()) && !SLWorld.subworld;
		}

		public override bool UseItem(Player player)
		{
			if (RedeWorld.keeperSaved)
			{
				Redemption.SpawnBoss(player, "KeeperSpirit", true, new Vector2(player.position.X + (float)Main.rand.Next(400, 500), player.position.Y - 0f), "The Keeper's Spirit", false);
				Main.PlaySound(15, player.position, 0);
			}
			else
			{
				Redemption.SpawnBoss(player, "Keeper", true, new Vector2(player.position.X + (float)Main.rand.Next(400, 500), player.position.Y - 0f), "The Keeper", false);
				Main.PlaySound(15, player.position, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddIngredient(1257, 6);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
