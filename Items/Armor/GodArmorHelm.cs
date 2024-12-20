using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GodArmorHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kamite Helmet");
			base.Tooltip.SetDefault("'Blessed with Oysus' power, this helm holds overwhelming energy...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 30;
			base.item.value = Item.sellPrice(1, 75, 0, 0);
			base.item.rare = 9;
			base.item.defense = 82;
		}

		public override void UpdateEquip(Player player)
		{
			player.lifeRegen += 30;
			player.manaRegen += 30;
			player.statLifeMax2 += 500;
			player.nightVision = true;
			player.findTreasure = true;
			player.meleeSpeed *= 1.5f;
			player.thrownVelocity *= 1.25f;
			player.manaCost *= 0.5f;
			player.maxMinions += 9;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadowLokis = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("GodArmor") && legs.type == base.mod.ItemType("GodArmorLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Oysus has empowered you";
			player.AddBuff(11, 2, true);
			player.armorPenetration += 50;
			player.aggro += 5;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(208, 255, 255));
			}
		}
	}
}
