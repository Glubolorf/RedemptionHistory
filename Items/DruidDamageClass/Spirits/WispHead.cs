using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class WispHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wisp's Head");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n12% increased druidic damage\n4% increased druidic critical strike chance\n4% damage reduction\nIncreased night vision\n[c/bdffff:Spirit Level +2]");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 36;
			base.item.value = Item.sellPrice(0, 4, 80, 0);
			base.item.rare = 8;
			base.item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer2 = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			druidDamagePlayer.druidDamage += 0.12f;
			druidDamagePlayer.druidCrit += 4;
			player.endurance += 0.04f;
			player.nightVision = true;
			modPlayer2.spiritLevel += 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("WispArmour") && legs.type == base.mod.ItemType("WispLegs");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases spirits summoned by 2\nYou emit an aura of light\nAny enemy slain has a chance to spawn a Dungeon Spirit";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritExtras += 2;
			redePlayer.wispSet = true;
			player.AddBuff(11, 2, true);
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SoulOfBloom", 20);
			modRecipe.AddIngredient(1508, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
