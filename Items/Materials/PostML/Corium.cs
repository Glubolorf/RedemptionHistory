using System;
using Redemption.Items.Accessories.HM;
using Redemption.Tiles.Ores;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class Corium : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corium");
			base.Tooltip.SetDefault("Holding this will cause severe radiation poisoning without proper equipment");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 10;
			base.item.value = 40000;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<SolidCoriumTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 4;
		}

		public override void HoldItem(Player player)
		{
			if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller5").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
			}
			if (Main.rand.Next(50) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 5)
			{
				player.GetModPlayer<RedePlayer>().irradiatedLevel += 2;
			}
		}
	}
}
