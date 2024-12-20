﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco
{
	public class HiveSpawnerTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = base.mod.DustType("SludgeSpoonDust");
			this.minPick = 300;
			this.mineResist = 10f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(40, 120, 40), null);
			this.animationFrameHeight = 18;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (Main.netMode != 1)
			{
				float dist = Vector2.Distance(Main.LocalPlayer.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
				if (dist <= 10f && dist > 1f && Main.rand.Next(100) == 0 && NPC.CountNPCS(base.mod.NPCType("InfectionHive")) == 0)
				{
					i *= 16;
					j *= 16;
					int k = NPC.NewNPC(i, j, base.mod.NPCType("InfectionHive"), 0, 0f, 0f, 0f, 0f, 255);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
						return;
					}
				}
			}
			else
			{
				float dist2 = Vector2.Distance(Main.LocalPlayer.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
				if (dist2 <= 10f && dist2 > 1f && Main.rand.Next(100) == 0 && NPC.CountNPCS(base.mod.NPCType("InfectionHive")) == 0)
				{
					Main.tile[i, j];
					i *= 16;
					j *= 16;
					Projectile.NewProjectile((float)i, (float)j, 0f, 0f, base.mod.ProjectileType("InfectionHiveSummonPro"), 0, 0f, 255, 0f, 0f);
				}
			}
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 30)
			{
				frameCounter = 0;
				frame++;
				if (frame > 1)
				{
					frame = 0;
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
