using System;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
{
	public class AncientSigil : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Sigil");
			base.Tooltip.SetDefault("'A sigil with lost power...'\nSummons the empowered Eaglecrest Golem\nOnly usable at day\nNot consumable");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool CanUseItem(Player player)
		{
			return Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<EaglecrestGolemPZ>()) && !NPC.AnyNPCs(ModContent.NPCType<EaglecrestGolem>()) && !NPC.AnyNPCs(ModContent.NPCType<Ukko>()) && !SLWorld.subworld;
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<EaglecrestGolemPZ>());
			Main.PlaySound(15, player.position, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientCoreF", 12);
			modRecipe.AddIngredient(null, "GolemEye", 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
