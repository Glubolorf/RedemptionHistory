using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Domain
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class NightshadeHead2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Pickelhaube");
			base.Tooltip.SetDefault("10% increased magic critical strike chance\nAttackers also take damage, and get inflicted by poison");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 0, 18, 0);
			base.item.rare = 1;
			base.item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 10;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.vendetta = true;
			player.thorns += 0.4f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("NightshadeBody") && legs.type == base.mod.ItemType("NightshadeLegs");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "10% increased damage reduction";
			player.endurance += 0.1f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyIronBar = true;
			modRecipe.AddIngredient(92, 1);
			modRecipe.AddIngredient(22, 10);
			modRecipe.AddIngredient(null, "Nightshade", 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(696, 1);
			modRecipe.AddIngredient(22, 10);
			modRecipe.AddIngredient(null, "Nightshade", 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
