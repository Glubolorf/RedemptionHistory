using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class SoulHead : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lost Soul's Head");
			base.Tooltip.SetDefault("2% increased druidic damage\n2% damage reduction\n4% increased druidic critical strike chance\n[c/bdffff:Spirit Level +1]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 20;
			base.item.value = 50;
			base.item.rare = 1;
			base.item.defense = 4;
			this.spiritWeapon = false;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer2 = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			druidDamagePlayer.druidDamage += 0.02f;
			druidDamagePlayer.druidCrit += 4;
			player.endurance += 0.02f;
			modPlayer2.spiritLevel++;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("SoulBody") && legs.type == base.mod.ItemType("SoulLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases spirits summoned by 1";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritExtras++;
			redePlayer.lostSoulSet = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
