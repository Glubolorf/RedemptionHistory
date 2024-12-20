using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class NoblesSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Noble's Sword");
			base.Tooltip.SetDefault("Guaranteed critical strikes after 5 consecutive hits");
		}

		public override void SetDefaults()
		{
			base.item.damage = 14;
			base.item.melee = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			this.hitCounter++;
			player.AddBuff(ModContent.BuffType<ConsecutiveStrikesBuff>(), 60, true);
		}

		public override void UseStyle(Player player)
		{
			if (!player.GetModPlayer<RedePlayer>().consecutiveStrikes)
			{
				this.hitCounter = 0;
			}
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (this.hitCounter >= 5 && player.GetModPlayer<RedePlayer>().consecutiveStrikes)
			{
				crit = true;
			}
		}

		public int hitCounter;
	}
}
