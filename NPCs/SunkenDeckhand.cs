using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SunkenDeckhand : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sunken Pirate");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 24;
			base.npc.height = 40;
			base.npc.friendly = false;
			base.npc.damage = 28;
			base.npc.defense = 0;
			base.npc.lifeMax = 55;
			base.npc.HitSound = SoundID.NPCHit54;
			base.npc.DeathSound = SoundID.NPCDeath52;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 22;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 82;
			this.animationType = 316;
			base.npc.alpha = 255;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 89, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		public override void AI()
		{
			this.sunkenTimer++;
			if (this.sunkenTimer <= 120)
			{
				base.npc.velocity.X = 0f;
				base.npc.alpha--;
				base.npc.dontTakeDamage = true;
			}
			if (this.sunkenTimer > 120)
			{
				base.npc.dontTakeDamage = false;
			}
		}

		private int sunkenTimer;
	}
}
