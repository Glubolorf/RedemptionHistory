using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Cooldowns;
using Redemption.Projectiles.Druid.Stave;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Staves
{
	public class AncientWorldStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient World Stave");
			base.Tooltip.SetDefault("Summon an Ancient Obelisk at your cursor point\nThe obelisk emits a force-field that increases player's defence and endurance when near\nCan only place one at a time");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 44;
			base.item.height = 46;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 1;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<WorldTree1>();
			base.item.shootSpeed = 0f;
			base.item.potion = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<WorldStaveCooldownDebuff>());
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(ModContent.BuffType<WorldStaveCooldownDebuff>(), 1200, true);
			position = Main.MouseWorld;
			return true;
		}
	}
}
