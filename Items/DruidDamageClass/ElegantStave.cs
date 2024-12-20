using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class ElegantStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Elegant Marble Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Medusa won't like this...'\nWhile holding this, you are immune to Petrification");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 21;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.useStyle = 1;
			base.item.crit = 16;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 40, 30);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 20;
				base.item.useAnimation = 20;
			}
			else
			{
				base.item.useTime = 24;
				base.item.useAnimation = 24;
			}
			return true;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (target.type == 480)
			{
				damage *= 200;
			}
		}

		public override void HoldItem(Player player)
		{
			player.buffImmune[156] = true;
		}
	}
}
