using System;
using Redemption.Items.Accessories.HM;
using Redemption.Tiles.Ores;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class Plutonium : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plutonium");
			base.Tooltip.SetDefault("Holding this may cause radiation poisoning without proper equipment");
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
			base.item.rare = 9;
			base.item.value = 10000;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<PlutoniumTile>();
		}

		public override void HoldItem(Player player)
		{
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (!modPlayer.hazmatPower && !modPlayer.HEVPower)
			{
				if (player.GetModPlayer<MullerEffect>().effect && Main.rand.Next(100) == 0 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/Muller2").WithVolume(0.9f).WithPitchVariance(0.1f), player.position);
				}
				if (Main.rand.Next(6000) == 0 && player.GetModPlayer<RedePlayer>().irradiatedLevel < 3)
				{
					player.GetModPlayer<RedePlayer>().irradiatedLevel++;
				}
			}
		}
	}
}
