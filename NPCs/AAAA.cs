using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class AAAA : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Remnants");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 84;
			base.npc.height = 84;
			base.npc.friendly = false;
			base.npc.damage = 40;
			base.npc.defense = 0;
			base.npc.lifeMax = 650;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 22;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 316;
			this.animationType = 316;
			base.npc.alpha = 75;
		}

		public override void AI()
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
