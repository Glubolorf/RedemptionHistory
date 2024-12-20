using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Critters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Critters
{
	public class Fly : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fly");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 10;
			base.npc.height = 10;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0f;
			base.npc.npcSlots = 0f;
			base.npc.aiStyle = 24;
			this.aiType = 74;
			base.npc.friendly = true;
			this.animationType = 355;
			base.npc.catchItem = (short)ModContent.ItemType<FlyBait>();
		}

		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return new bool?(true);
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return new bool?(true);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
	}
}
