using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Summon
{
	public class UkonRuno : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukon Runo");
			base.Tooltip.SetDefault("'An ancient poem for Ukko to cause rain and bless crops'\nSummons an Ukkonen to fight for you.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1000;
			base.item.summon = true;
			base.item.mana = 16;
			base.item.width = 36;
			base.item.height = 30;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<Ukkonen>();
			base.item.shootSpeed = 10f;
			base.item.buffType = ModContent.BuffType<UkkonenBuff>();
			base.item.buffTime = 3600;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
	}
}
