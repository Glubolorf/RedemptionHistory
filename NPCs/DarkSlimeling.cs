using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class DarkSlimeling : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Slimeling");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 70;
			base.npc.height = 46;
			base.npc.friendly = false;
			base.npc.damage = 85;
			base.npc.defense = 0;
			base.npc.lifeMax = 1250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.alpha = 50;
			base.npc.aiStyle = 1;
			this.aiType = 85;
			this.animationType = 302;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
		}
	}
}
