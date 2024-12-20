using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Cooldowns;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class Pommisauva : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pommisauva");
			base.Tooltip.SetDefault("'A magic wand that summons bombs that destroy ground efficiently'\nCasts a bomb, only 3 can be cast in a row\nBombs can destroy tiles");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 100;
			base.item.magic = true;
			base.item.mana = 12;
			base.item.width = 44;
			base.item.height = 44;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.reuseDelay = 20;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 9f;
			base.item.value = Item.buyPrice(0, 9, 25, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<PommisauvaBomb>();
			base.item.shootSpeed = 6f;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<NoitaBombCooldown>());
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			this.bombCount++;
			if (this.bombCount >= 3)
			{
				player.AddBuff(ModContent.BuffType<NoitaBombCooldown>(), 1800, true);
				this.bombCount = 0;
			}
			return true;
		}

		public int bombCount;
	}
}
