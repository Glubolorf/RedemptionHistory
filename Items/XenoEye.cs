using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class XenoEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xeno Eye");
			base.Tooltip.SetDefault("Summons the Infected Eye\nOnly usable at night\n[c/67ff3e:Grows the Infection]");
			ItemID.Sets.SortingPriorityBossSpawns[base.item.type] = 13;
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 20;
			base.item.maxStack = 20;
			base.item.rare = 2;
			base.item.useAnimation = 45;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(base.mod.NPCType("InfectedEye"));
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("InfectedEye"));
			Main.PlaySound(15, player.position, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "InfectedLens", 1);
			modRecipe.AddIngredient(null, "XenomiteShard", 5);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
		}
	}
}
