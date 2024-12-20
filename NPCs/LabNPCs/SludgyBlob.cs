using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class SludgyBlob : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sludge Blob");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 16;
			base.npc.height = 14;
			base.npc.friendly = false;
			base.npc.damage = 45;
			base.npc.defense = 0;
			base.npc.lifeMax = 200;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.6f;
			base.npc.aiStyle = 1;
			this.aiType = 183;
			this.animationType = 302;
			base.npc.lavaImmune = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
			}
			this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
			this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 0, default(Color), 1f);
		}

		private int dust;
	}
}
