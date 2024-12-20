using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class OldXenomiteHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Old Xenomite Headgear");
			base.Tooltip.SetDefault("'Comes from a simpler time...'\n+25 max mana\n10% increased magic damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 7;
			base.item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 25;
			player.magicDamage *= 1.1f;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return (body.type == ModContent.ItemType<OldXenomiteBody>() && legs.type == ModContent.ItemType<OldXenomiteLeggings>()) || (body.type == ModContent.ItemType<XenomiteBody>() && legs.type == ModContent.ItemType<XenomiteLeggings>());
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "You are immune to the Xenomite Infection and Radioactive Fallout\nThe armour emits light\nCritical strikes have a chance to release a burst of xenomite shards at your cursor";
			Lighting.AddLight(player.Center, 0.5f, 1f, 0.5f);
			player.buffImmune[ModContent.BuffType<XenomiteDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff2>()] = true;
			player.buffImmune[ModContent.BuffType<RadioactiveFalloutDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<HeavyRadiationDebuff>()] = true;
			player.GetModPlayer<RedePlayer>().xenomiteSet = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
