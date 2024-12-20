using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class lol : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("3RD VL1TCH 0VERL0RD!?!?!!???!!");
		}

		public override void SetDefaults()
		{
			base.npc.width = 2000;
			base.npc.height = 2000;
			base.npc.defense = 999;
			base.npc.damage = 999;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
		}

		public override void AI()
		{
			this.lolol++;
			if (this.lolol >= 1)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BAZINGA").WithVolume(1.4f).WithPitchVariance(0.1f), -1, -1);
				base.npc.timeLeft = 0;
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
		}

		private int lolol;
	}
}
