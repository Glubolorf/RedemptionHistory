using System;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class DruidsCharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Master Druid's Charm");
			base.Tooltip.SetDefault("5% increased druidic damage\nNegates fall damage\nGrants immunity to fire blocks\nForms an spirit skull to aid you upon being attacked\nStaves will burn targets\nSeedbags will inflict Frostburn\nSeedbags that throw only one seed will throw 2-3 seeds instead\nStaves cast faster\nThrows seedbags faster\nSpirits shoot faster\nSpirit summoning weapons will summon 2 extra spirits\nSpirits pierce through more targets\nSpirits home in on enemies\n95% decrease to all other damage types");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 52;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidsCharmDawn>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidsCharmDusk>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidsCharmMidnight>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidEmblem>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<LargeSeedPouch>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.frostburnSeedbag = true;
			redePlayer.burnStaves = true;
			redePlayer.spiritSkull1 = true;
			redePlayer.moreSeeds = true;
			redePlayer.staveSpeed += 0.05f;
			redePlayer.fasterSeedbags = true;
			redePlayer.fasterSpirits = true;
			redePlayer.moreSpirits = true;
			redePlayer.spiritHoming = true;
			redePlayer.spiritPierce = true;
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.25f;
			player.noFallDmg = true;
			player.buffImmune[67] = true;
			player.magicDamage *= 0.05f;
			player.meleeDamage *= 0.05f;
			player.minionDamage *= 0.05f;
			player.rangedDamage *= 0.05f;
			player.thrownDamage *= 0.05f;
		}
	}
}
