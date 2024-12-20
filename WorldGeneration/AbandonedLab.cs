﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace Redemption.WorldGeneration
{
	public class AbandonedLab : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 255, 0)] = mod.TileType("LabTileUnsafe");
			colorToTile[new Color(0, 100, 0)] = mod.TileType("OvergrownLabTile");
			colorToTile[new Color(0, 255, 100)] = mod.TileType("LabTile");
			colorToTile[new Color(100, 100, 100)] = mod.TileType("LabSupportBeamTile");
			colorToTile[new Color(0, 255, 255)] = 214;
			colorToTile[new Color(50, 50, 50)] = 1;
			colorToTile[new Color(50, 0, 50)] = 123;
			colorToTile[new Color(90, 60, 30)] = 198;
			colorToTile[new Color(0, 0, 255)] = mod.TileType("XenomiteOreBlock");
			colorToTile[new Color(255, 255, 200)] = mod.TileType("UraniumTile");
			colorToTile[new Color(255, 200, 255)] = mod.TileType("PlutoniumTile");
			colorToTile[new Color(255, 100, 0)] = mod.TileType("SolidCoriumTile");
			colorToTile[new Color(255, 255, 0)] = mod.TileType("HardenedSludgeTile");
			colorToTile[new Color(187, 255, 90)] = mod.TileType("HardenedSludge2Tile");
			colorToTile[new Color(17, 54, 17)] = mod.TileType("HardenedSludge3Tile");
			colorToTile[new Color(120, 255, 255)] = mod.TileType("LabTubeTile");
			colorToTile[new Color(255, 120, 255)] = mod.TileType("LabTankTile");
			colorToTile[new Color(220, 255, 255)] = mod.TileType("HalogenLampTile");
			colorToTile[new Color(255, 100, 100)] = mod.TileType("RedLaserTile");
			colorToTile[new Color(100, 255, 100)] = mod.TileType("GreenLaserTile");
			colorToTile[new Color(100, 0, 100)] = mod.TileType("ElectricHazardTile");
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(0, 255, 0)] = mod.WallType("LabWallTileUnsafe");
			colorToWall[new Color(255, 255, 0)] = mod.WallType("VentWallTile");
			colorToWall[new Color(0, 0, 255)] = mod.WallType("HardenedSludgeWallTile");
			colorToWall[new Color(100, 0, 0)] = mod.WallType("HardenedlyHardenedSludgeWallTile");
			colorToWall[new Color(0, 255, 255)] = mod.WallType("MossyLabWall");
			colorToWall[new Color(255, 0, 255)] = mod.WallType("MossyLabWallFull");
			colorToWall[new Color(150, 150, 150)] = -2;
			colorToWall[Color.Black] = -1;
			BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/TrueLab2"), colorToTile, mod.GetTexture("WorldGeneration/TrueLab2Walls"), colorToWall, mod.GetTexture("WorldGeneration/TrueLab2Liquids"), null).Generate(origin.X, origin.Y, true, true);
			WorldGen.PlaceObject(origin.X + 125, origin.Y + 14, (int)((ushort)mod.TileType("TurretTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 125, origin.Y + 14, (int)((ushort)mod.TileType("TurretTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 135, origin.Y + 19, (int)((ushort)mod.TileType("LabDoor3TileClosed")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 135, origin.Y + 19, (int)((ushort)mod.TileType("LabDoor3TileClosed")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 143, origin.Y + 19, (int)((ushort)mod.TileType("LabDoor3TileClosed")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 143, origin.Y + 19, (int)((ushort)mod.TileType("LabDoor3TileClosed")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 169, origin.Y + 35, (int)((ushort)mod.TileType("LabDoor3TileClosed")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 169, origin.Y + 35, (int)((ushort)mod.TileType("LabDoor3TileClosed")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 109, origin.Y + 35, (int)((ushort)mod.TileType("LabDoor3TileClosed")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 109, origin.Y + 35, (int)((ushort)mod.TileType("LabDoor3TileClosed")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 156, origin.Y + 7, (int)((ushort)mod.TileType("LabBossDoorTile1H")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 156, origin.Y + 7, (int)((ushort)mod.TileType("LabBossDoorTile1H")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 62, origin.Y + 72, (int)((ushort)mod.TileType("LabBossDoorTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 62, origin.Y + 72, (int)((ushort)mod.TileType("LabBossDoorTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 200, origin.Y + 56, (int)((ushort)mod.TileType("LabBossDoorTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 200, origin.Y + 56, (int)((ushort)mod.TileType("LabBossDoorTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 223, origin.Y + 176, (int)((ushort)mod.TileType("LabBossDoorTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 223, origin.Y + 176, (int)((ushort)mod.TileType("LabBossDoorTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 128, origin.Y + 103, (int)((ushort)mod.TileType("LabBossDoorTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 128, origin.Y + 103, (int)((ushort)mod.TileType("LabBossDoorTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 104, origin.Y + 165, (int)((ushort)mod.TileType("LabBossDoorTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 104, origin.Y + 165, (int)((ushort)mod.TileType("LabBossDoorTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 42, origin.Y + 165, (int)((ushort)mod.TileType("LabBossDoorTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 42, origin.Y + 165, (int)((ushort)mod.TileType("LabBossDoorTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 118, origin.Y + 173, (int)((ushort)mod.TileType("LabBossDoorTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 118, origin.Y + 173, (int)((ushort)mod.TileType("LabBossDoorTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 91, origin.Y + 15, (int)((ushort)mod.TileType("Sign1Tile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 91, origin.Y + 15, (int)((ushort)mod.TileType("Sign1Tile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 188, origin.Y + 16, (int)((ushort)mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 188, origin.Y + 16, (int)((ushort)mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 61, origin.Y + 56, (int)((ushort)mod.TileType("SignElectricTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 61, origin.Y + 56, (int)((ushort)mod.TileType("SignElectricTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 65, origin.Y + 70, (int)((ushort)mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 65, origin.Y + 70, (int)((ushort)mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 206, origin.Y + 55, (int)((ushort)mod.TileType("SignBoiTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 206, origin.Y + 55, (int)((ushort)mod.TileType("SignBoiTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 203, origin.Y + 55, (int)((ushort)mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 203, origin.Y + 55, (int)((ushort)mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 210, origin.Y + 175, (int)((ushort)mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 210, origin.Y + 175, (int)((ushort)mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 204, origin.Y + 175, (int)((ushort)mod.TileType("SignBoiTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 204, origin.Y + 175, (int)((ushort)mod.TileType("SignBoiTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 75, origin.Y + 137, (int)((ushort)mod.TileType("SignElectricTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 75, origin.Y + 137, (int)((ushort)mod.TileType("SignElectricTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 107, origin.Y + 164, (int)((ushort)mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 107, origin.Y + 164, (int)((ushort)mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 108, origin.Y + 173, (int)((ushort)mod.TileType("SignBoiTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 108, origin.Y + 173, (int)((ushort)mod.TileType("SignBoiTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 226, origin.Y + 128, (int)((ushort)mod.TileType("SignDeathTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 226, origin.Y + 128, (int)((ushort)mod.TileType("SignDeathTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 220, origin.Y + 117, (int)((ushort)mod.TileType("SignRadioactiveTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 220, origin.Y + 117, (int)((ushort)mod.TileType("SignRadioactiveTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 252, origin.Y + 114, (int)((ushort)mod.TileType("SignRadioactiveTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 252, origin.Y + 114, (int)((ushort)mod.TileType("SignRadioactiveTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 171, origin.Y + 20, (int)((ushort)mod.TileType("JanitorSpawner")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 171, origin.Y + 20, (int)((ushort)mod.TileType("JanitorSpawner")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 44, origin.Y + 85, (int)((ushort)mod.TileType("Stage3ScientistSummon2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 44, origin.Y + 85, (int)((ushort)mod.TileType("Stage3ScientistSummon2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 203, origin.Y + 67, (int)((ushort)mod.TileType("BehemothSummon2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 203, origin.Y + 67, (int)((ushort)mod.TileType("BehemothSummon2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 207, origin.Y + 177, (int)((ushort)mod.TileType("BlisterfaceSummon2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 207, origin.Y + 177, (int)((ushort)mod.TileType("BlisterfaceSummon2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 206, origin.Y + 187, (int)((ushort)mod.TileType("BlisterHoleTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 206, origin.Y + 187, (int)((ushort)mod.TileType("BlisterHoleTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 85, origin.Y + 120, (int)((ushort)mod.TileType("LabWideConsoleVolt")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 85, origin.Y + 120, (int)((ushort)mod.TileType("LabWideConsoleVolt")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 60, origin.Y + 176, (int)((ushort)mod.TileType("MACESummon2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 60, origin.Y + 176, (int)((ushort)mod.TileType("MACESummon2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 142, origin.Y + 209, (int)((ushort)mod.TileType("PatientZeroSummon2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 142, origin.Y + 209, (int)((ushort)mod.TileType("PatientZeroSummon2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 183, origin.Y + 100, (int)((ushort)mod.TileType("JanitorNPCSpawner")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 183, origin.Y + 100, (int)((ushort)mod.TileType("JanitorNPCSpawner")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 118, origin.Y + 14, (int)((ushort)mod.TileType("IntercomTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 118, origin.Y + 14, (int)((ushort)mod.TileType("IntercomTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 121, origin.Y + 14, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 121, origin.Y + 14, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 189, origin.Y + 13, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 189, origin.Y + 13, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 122, origin.Y + 21, (int)((ushort)mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 122, origin.Y + 21, (int)((ushort)mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 124, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 124, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 128, origin.Y + 19, (int)((ushort)mod.TileType("LabReceptionDeskTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 128, origin.Y + 19, (int)((ushort)mod.TileType("LabReceptionDeskTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 128, origin.Y + 14, (int)((ushort)mod.TileType("ReceptionDeskMonitorsTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 128, origin.Y + 14, (int)((ushort)mod.TileType("ReceptionDeskMonitorsTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 139, origin.Y + 35, (int)((ushort)mod.TileType("Corpse1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 139, origin.Y + 35, (int)((ushort)mod.TileType("Corpse1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 147, origin.Y + 83, (int)((ushort)mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 147, origin.Y + 83, (int)((ushort)mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 147, origin.Y + 71, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 147, origin.Y + 71, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 187, origin.Y + 18, (int)((ushort)mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 187, origin.Y + 18, (int)((ushort)mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 150, origin.Y + 9, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 150, origin.Y + 9, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 184, origin.Y + 9, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 184, origin.Y + 9, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 152, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 152, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 157, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 157, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 181, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 181, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 176, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 176, origin.Y + 20, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 155, origin.Y + 21, (int)((ushort)mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 155, origin.Y + 21, (int)((ushort)mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 179, origin.Y + 21, (int)((ushort)mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 179, origin.Y + 21, (int)((ushort)mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 145, origin.Y + 14, (int)((ushort)mod.TileType("IntercomTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 145, origin.Y + 14, (int)((ushort)mod.TileType("IntercomTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 151, origin.Y + 37, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 151, origin.Y + 37, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 153, origin.Y + 37, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 153, origin.Y + 37, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 152, origin.Y + 35, (int)((ushort)mod.TileType("RadPillTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 152, origin.Y + 35, (int)((ushort)mod.TileType("RadPillTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 153, origin.Y + 34, (int)((ushort)mod.TileType("ComputerTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 153, origin.Y + 34, (int)((ushort)mod.TileType("ComputerTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 156, origin.Y + 37, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 156, origin.Y + 37, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 158, origin.Y + 37, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 158, origin.Y + 37, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 158, origin.Y + 34, (int)((ushort)mod.TileType("ComputerTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 158, origin.Y + 34, (int)((ushort)mod.TileType("ComputerTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 161, origin.Y + 37, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 161, origin.Y + 37, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 163, origin.Y + 37, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 163, origin.Y + 37, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 163, origin.Y + 34, (int)((ushort)mod.TileType("ComputerTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 163, origin.Y + 34, (int)((ushort)mod.TileType("ComputerTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 164, origin.Y + 25, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 164, origin.Y + 25, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 187, origin.Y + 28, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 187, origin.Y + 28, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 114, origin.Y + 33, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 114, origin.Y + 33, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 131, origin.Y + 32, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 131, origin.Y + 32, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 102, origin.Y + 32, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 102, origin.Y + 32, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 123, origin.Y + 34, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 123, origin.Y + 34, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 118, origin.Y + 36, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 118, origin.Y + 36, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 113, origin.Y + 36, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 113, origin.Y + 36, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 96, origin.Y + 44, (int)((ushort)mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 96, origin.Y + 44, (int)((ushort)mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 116, origin.Y + 37, (int)((ushort)mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 116, origin.Y + 37, (int)((ushort)mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 88, origin.Y + 41, (int)((ushort)mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 88, origin.Y + 41, (int)((ushort)mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 85, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 85, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 82, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 82, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 81, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 81, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 77, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 77, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 68, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 68, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 64, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 64, origin.Y + 34, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 59, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 59, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 61, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 61, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 60, origin.Y + 42, (int)((ushort)mod.TileType("RadPillTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 60, origin.Y + 42, (int)((ushort)mod.TileType("RadPillTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 63, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 63, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 66, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 66, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 68, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 68, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 70, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 70, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 73, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 73, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 75, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 75, origin.Y + 44, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 77, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 77, origin.Y + 44, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 52, origin.Y + 41, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 52, origin.Y + 41, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 50, origin.Y + 34, (int)((ushort)mod.TileType("IntercomTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 50, origin.Y + 34, (int)((ushort)mod.TileType("IntercomTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 82, origin.Y + 42, (int)((ushort)mod.TileType("RadPillTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 82, origin.Y + 42, (int)((ushort)mod.TileType("RadPillTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 98, origin.Y + 44, (int)((ushort)mod.TileType("HiveSpawnerTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 98, origin.Y + 44, (int)((ushort)mod.TileType("HiveSpawnerTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 44, origin.Y + 58, (int)((ushort)mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 44, origin.Y + 58, (int)((ushort)mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 44, origin.Y + 56, (int)((ushort)mod.TileType("ComputerTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 44, origin.Y + 56, (int)((ushort)mod.TileType("ComputerTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 35, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 35, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 40, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 40, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 47, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 47, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 52, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 52, origin.Y + 55, (int)((ushort)mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 45, origin.Y + 49, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 45, origin.Y + 49, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 76, origin.Y + 53, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 76, origin.Y + 53, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 83, origin.Y + 53, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 83, origin.Y + 53, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 79, origin.Y + 54, (int)((ushort)mod.TileType("ReceptionDeskMonitorsTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 79, origin.Y + 54, (int)((ushort)mod.TileType("ReceptionDeskMonitorsTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 79, origin.Y + 64, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 79, origin.Y + 64, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 102, origin.Y + 64, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 102, origin.Y + 64, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 97, origin.Y + 58, (int)((ushort)mod.TileType("HiveSpawnerTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 97, origin.Y + 58, (int)((ushort)mod.TileType("HiveSpawnerTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 66, origin.Y + 83, (int)((ushort)mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 66, origin.Y + 83, (int)((ushort)mod.TileType("XenoTank1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 73, origin.Y + 83, (int)((ushort)mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 73, origin.Y + 83, (int)((ushort)mod.TileType("XenoTank1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 80, origin.Y + 83, (int)((ushort)mod.TileType("XenoTank1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 80, origin.Y + 83, (int)((ushort)mod.TileType("XenoTank1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 126, origin.Y + 82, (int)((ushort)mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 126, origin.Y + 82, (int)((ushort)mod.TileType("VentTile4")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 163, origin.Y + 55, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 163, origin.Y + 55, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 186, origin.Y + 52, (int)((ushort)mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 186, origin.Y + 52, (int)((ushort)mod.TileType("VentTile4")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 243, origin.Y + 55, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 243, origin.Y + 55, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 236, origin.Y + 101, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 236, origin.Y + 101, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 244, origin.Y + 87, (int)((ushort)mod.TileType("BotanistStationTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 244, origin.Y + 86, (int)((ushort)mod.TileType("BotanistStationTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 246, origin.Y + 81, (int)((ushort)mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 246, origin.Y + 81, (int)((ushort)mod.TileType("VentTile4")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 249, origin.Y + 58, (int)((ushort)mod.TileType("MossyLabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 249, origin.Y + 58, (int)((ushort)mod.TileType("MossyLabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 251, origin.Y + 58, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 251, origin.Y + 58, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 251, origin.Y + 83, (int)((ushort)mod.TileType("MossTubeTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 251, origin.Y + 83, (int)((ushort)mod.TileType("MossTubeTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 278, origin.Y + 83, (int)((ushort)mod.TileType("MossTubeTile2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 278, origin.Y + 83, (int)((ushort)mod.TileType("MossTubeTile2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 240, origin.Y + 69, (int)((ushort)mod.TileType("MossTubeTile2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 240, origin.Y + 69, (int)((ushort)mod.TileType("MossTubeTile2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 187, origin.Y + 69, (int)((ushort)mod.TileType("DeadHazmatTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 187, origin.Y + 69, (int)((ushort)mod.TileType("DeadHazmatTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 153, origin.Y + 97, (int)((ushort)mod.TileType("LabForgeTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 153, origin.Y + 97, (int)((ushort)mod.TileType("LabForgeTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 182, origin.Y + 114, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 182, origin.Y + 114, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 182, origin.Y + 123, (int)((ushort)mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 182, origin.Y + 123, (int)((ushort)mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 188, origin.Y + 70, (int)((ushort)mod.TileType("DeadHazmatTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 188, origin.Y + 70, (int)((ushort)mod.TileType("DeadHazmatTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 188, origin.Y + 114, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 188, origin.Y + 114, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 180, origin.Y + 117, (int)((ushort)mod.TileType("CorruptorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 180, origin.Y + 117, (int)((ushort)mod.TileType("CorruptorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 231, origin.Y + 113, (int)((ushort)mod.TileType("LabReactorTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 231, origin.Y + 113, (int)((ushort)mod.TileType("LabReactorTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 266, origin.Y + 113, (int)((ushort)mod.TileType("LabReactorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 266, origin.Y + 113, (int)((ushort)mod.TileType("LabReactorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 233, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 233, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 236, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 236, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 267, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 267, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 270, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 270, origin.Y + 126, (int)((ushort)mod.TileType("LabFanTile1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 167, origin.Y + 139, (int)((ushort)mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 167, origin.Y + 139, (int)((ushort)mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 232, origin.Y + 131, (int)((ushort)mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 232, origin.Y + 131, (int)((ushort)mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 233, origin.Y + 159, (int)((ushort)mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 233, origin.Y + 159, (int)((ushort)mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 238, origin.Y + 173, (int)((ushort)mod.TileType("SewerHoleTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 238, origin.Y + 173, (int)((ushort)mod.TileType("SewerHoleTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 194, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 194, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 203, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 203, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 211, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 211, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 220, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 220, origin.Y + 177, (int)((ushort)mod.TileType("VentTile3")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 19, origin.Y + 74, (int)((ushort)mod.TileType("Corpse2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 19, origin.Y + 74, (int)((ushort)mod.TileType("Corpse2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 4, origin.Y + 84, (int)((ushort)mod.TileType("Corpse1")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 4, origin.Y + 84, (int)((ushort)mod.TileType("Corpse1")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 24, origin.Y + 85, (int)((ushort)mod.TileType("LabReceptionCouchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 24, origin.Y + 85, (int)((ushort)mod.TileType("LabReceptionCouchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 5, origin.Y + 102, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 5, origin.Y + 102, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 21, origin.Y + 99, (int)((ushort)mod.TileType("IntercomTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 21, origin.Y + 99, (int)((ushort)mod.TileType("IntercomTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 23, origin.Y + 105, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 23, origin.Y + 105, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 27, origin.Y + 102, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 27, origin.Y + 102, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 31, origin.Y + 102, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 31, origin.Y + 102, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 38, origin.Y + 105, (int)((ushort)mod.TileType("LabBedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 38, origin.Y + 105, (int)((ushort)mod.TileType("LabBedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 40, origin.Y + 105, (int)((ushort)mod.TileType("LabWorkbenchTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 40, origin.Y + 105, (int)((ushort)mod.TileType("LabWorkbenchTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 41, origin.Y + 104, (int)((ushort)mod.TileType("RadPillTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 41, origin.Y + 104, (int)((ushort)mod.TileType("RadPillTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 5, origin.Y + 118, (int)((ushort)mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 5, origin.Y + 118, (int)((ushort)mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 5, origin.Y + 137, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 5, origin.Y + 137, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 16, origin.Y + 150, (int)((ushort)mod.TileType("Corpse1")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 16, origin.Y + 150, (int)((ushort)mod.TileType("Corpse1")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 20, origin.Y + 151, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 20, origin.Y + 151, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 24, origin.Y + 137, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 24, origin.Y + 137, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 46, origin.Y + 140, (int)((ushort)mod.TileType("Corpse1")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 46, origin.Y + 140, (int)((ushort)mod.TileType("Corpse1")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 63, origin.Y + 140, (int)((ushort)mod.TileType("Corpse2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 63, origin.Y + 140, (int)((ushort)mod.TileType("Corpse2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 87, origin.Y + 137, (int)((ushort)mod.TileType("LabBookshelfTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 87, origin.Y + 137, (int)((ushort)mod.TileType("LabBookshelfTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 105, origin.Y + 140, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 105, origin.Y + 140, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 125, origin.Y + 132, (int)((ushort)mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 125, origin.Y + 132, (int)((ushort)mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 46, origin.Y + 140, (int)((ushort)mod.TileType("Corpse1")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 46, origin.Y + 140, (int)((ushort)mod.TileType("Corpse1")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 121, origin.Y + 146, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 121, origin.Y + 146, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 138, origin.Y + 156, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 138, origin.Y + 156, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 121, origin.Y + 164, (int)((ushort)mod.TileType("LabDoorBrokenTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 121, origin.Y + 164, (int)((ushort)mod.TileType("LabDoorBrokenTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 26, origin.Y + 116, (int)((ushort)mod.TileType("VentTile4")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 26, origin.Y + 116, (int)((ushort)mod.TileType("VentTile4")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 52, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 52, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 55, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 55, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 60, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 60, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 65, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 65, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 70, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 70, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 80, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 80, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 84, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 84, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 87, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 87, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 92, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 92, origin.Y + 91, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 102, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 102, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 105, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 105, origin.Y + 94, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 113, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 113, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 117, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 117, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 120, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 120, origin.Y + 92, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 66, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 66, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 70, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 70, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 101, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 101, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerOccupiedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 106, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerEmptyTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 106, origin.Y + 109, (int)((ushort)mod.TileType("BotHangerEmptyTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 108, origin.Y + 167, (int)((ushort)mod.TileType("Corpse2")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 108, origin.Y + 167, (int)((ushort)mod.TileType("Corpse2")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 38, origin.Y + 165, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 38, origin.Y + 165, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 113, origin.Y + 175, (int)((ushort)mod.TileType("LabTableTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 113, origin.Y + 175, (int)((ushort)mod.TileType("LabTableTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 112, origin.Y + 173, (int)((ushort)mod.TileType("RadPillTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 112, origin.Y + 173, (int)((ushort)mod.TileType("RadPillTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 113, origin.Y + 172, (int)((ushort)mod.TileType("ComputerTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 113, origin.Y + 172, (int)((ushort)mod.TileType("ComputerTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 105, origin.Y + 154, (int)((ushort)mod.TileType("OperatorSpawner")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 105, origin.Y + 154, (int)((ushort)mod.TileType("OperatorSpawner")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 109, origin.Y + 156, (int)((ushort)mod.TileType("LabChairTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 109, origin.Y + 156, (int)((ushort)mod.TileType("LabChairTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 112, origin.Y + 153, (int)((ushort)mod.TileType("LabDoorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 112, origin.Y + 153, (int)((ushort)mod.TileType("LabDoorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 104, origin.Y + 151, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 104, origin.Y + 151, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 108, origin.Y + 151, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 108, origin.Y + 151, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 102, origin.Y + 151, (int)((ushort)mod.TileType("IntercomTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 102, origin.Y + 151, (int)((ushort)mod.TileType("IntercomTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 116, origin.Y + 179, (int)((ushort)mod.TileType("CeilingMonitorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 116, origin.Y + 179, (int)((ushort)mod.TileType("CeilingMonitorTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 114, origin.Y + 187, (int)((ushort)mod.TileType("LabBedTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 114, origin.Y + 187, (int)((ushort)mod.TileType("LabBedTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 173, origin.Y + 184, (int)((ushort)mod.TileType("LabCabinet")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 173, origin.Y + 184, (int)((ushort)mod.TileType("LabCabinet")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 118, origin.Y + 209, (int)((ushort)mod.TileType("Corpse2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 118, origin.Y + 209, (int)((ushort)mod.TileType("Corpse2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 156, origin.Y + 208, (int)((ushort)mod.TileType("Corpse2")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 156, origin.Y + 208, (int)((ushort)mod.TileType("Corpse2")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 163, origin.Y + 209, (int)((ushort)mod.TileType("Corpse2")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 163, origin.Y + 209, (int)((ushort)mod.TileType("Corpse2")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 127, origin.Y + 210, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 127, origin.Y + 210, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 138, origin.Y + 207, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 138, origin.Y + 207, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 148, origin.Y + 210, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, 1);
			NetMessage.SendObjectPlacment(-1, origin.X + 148, origin.Y + 210, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, 1);
			WorldGen.PlaceObject(origin.X + 168, origin.Y + 210, (int)((ushort)mod.TileType("Corpse3")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 168, origin.Y + 210, (int)((ushort)mod.TileType("Corpse3")), 0, 0, -1, -1);
			this.LabChest(origin.X + 183, origin.Y + 31);
			this.LabChest(origin.X + 204, origin.Y + 31);
			this.LabChest(origin.X + 199, origin.Y + 27);
			this.LabChest(origin.X + 204, origin.Y + 27);
			this.LabChest(origin.X + 24, origin.Y + 49);
			this.LabChest(origin.X + 145, origin.Y + 86);
			this.LabChest(origin.X + 167, origin.Y + 131);
			this.LabChest(origin.X + 172, origin.Y + 131);
			this.LabChest(origin.X + 180, origin.Y + 126);
			this.LabChest(origin.X + 145, origin.Y + 74);
			this.LabChest(origin.X + 77, origin.Y + 75);
			this.LabChest(origin.X + 82, origin.Y + 75);
			this.LabChest(origin.X + 11, origin.Y + 74);
			this.LabChest(origin.X + 14, origin.Y + 74);
			this.LabChest(origin.X + 9, origin.Y + 105);
			this.LabChest(origin.X + 34, origin.Y + 105);
			this.LabChest(origin.X + 9, origin.Y + 140);
			this.LabChest(origin.X + 23, origin.Y + 151);
			this.LabChest(origin.X + 67, origin.Y + 140);
			this.LabChest(origin.X + 24, origin.Y + 163);
			this.LabChest(origin.X + 27, origin.Y + 74);
			this.LabChest(origin.X + 30, origin.Y + 74);
			this.LabChest(origin.X + 243, origin.Y + 73);
			this.LabChest(origin.X + 231, origin.Y + 101);
			this.LabChest(origin.X + 159, origin.Y + 157);
			this.LabChest(origin.X + 156, origin.Y + 157);
			this.LabChest(origin.X + 184, origin.Y + 178);
			this.LabChest(origin.X + 187, origin.Y + 178);
			this.LabChest(origin.X + 276, origin.Y + 164);
			this.LabChest(origin.X + 279, origin.Y + 164);
			this.SpecialLabChest(origin.X + 151, origin.Y + 154);
			this.DeadWoodChest(origin.X + 130, origin.Y + 37);
			this.DeadWoodChest(origin.X + 27, origin.Y + 49);
			this.DeadWoodChest(origin.X + 91, origin.Y + 86);
			this.DeadWoodChest(origin.X + 100, origin.Y + 86);
			this.DeadWoodChest(origin.X + 122, origin.Y + 86);
			this.DeadWoodChest(origin.X + 122, origin.Y + 74);
			this.DeadWoodChest(origin.X + 167, origin.Y + 87);
			this.DeadWoodChest(origin.X + 198, origin.Y + 86);
			this.DeadWoodChest(origin.X + 202, origin.Y + 87);
			return true;
		}

		public void LabChest(int x, int y)
		{
			Mod mod = Redemption.inst;
			int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)mod.TileType("LabChestTileLocked"), false, 0);
			int[] LabChestLoot = new int[]
			{
				mod.ItemType("FloppyDisk5"),
				mod.ItemType("FloppyDisk5_1"),
				mod.ItemType("FloppyDisk5_2"),
				mod.ItemType("FloppyDisk5_3")
			};
			int[] LabChestLoot2 = new int[]
			{
				mod.ItemType("ScrapMetal"),
				mod.ItemType("AIChip"),
				mod.ItemType("Mk3Capacitator"),
				mod.ItemType("Mk3Plating"),
				mod.ItemType("RawXenium")
			};
			int[] LabChestLoot3 = new int[]
			{
				mod.ItemType("Starlite"),
				mod.ItemType("XenomiteShard"),
				mod.ItemType("Electronade"),
				3460
			};
			int[] LabChestLoot4 = new int[]
			{
				mod.ItemType("TerraBombaPart1"),
				mod.ItemType("TerraBombaPart2"),
				mod.ItemType("TerraBombaPart3")
			};
			if (PlacementSuccess >= 0)
			{
				Chest chest = Main.chest[PlacementSuccess];
				Item item0 = chest.item[0];
				UnifiedRandom genRand0 = WorldGen.genRand;
				int[] array0 = new int[]
				{
					mod.ItemType("HazmatSuit"),
					mod.ItemType("SuspiciousXenomiteShard"),
					mod.ItemType("Petridish"),
					mod.ItemType("DNAgger"),
					mod.ItemType("EmptyMutagen"),
					mod.ItemType("TeslaManipulatorPrototype")
				};
				item0.SetDefaults(Utils.Next<int>(genRand0, array0), false);
				chest.item[1].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot2), false);
				chest.item[1].stack = WorldGen.genRand.Next(1, 3);
				chest.item[2].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot3), false);
				chest.item[2].stack = WorldGen.genRand.Next(8, 12);
				if (Main.rand.Next(2) == 0)
				{
					chest.item[3].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot), false);
				}
				if (Main.rand.Next(4) == 0)
				{
					chest.item[4].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot4), false);
				}
			}
		}

		public void DeadWoodChest(int x, int y)
		{
			Mod mod = Redemption.inst;
			int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)mod.TileType("DeadWoodChestTile"), false, 0);
			int[] LabChestLoot = new int[]
			{
				mod.ItemType("FloppyDisk1"),
				mod.ItemType("FloppyDisk3"),
				mod.ItemType("FloppyDisk3_1")
			};
			int[] LabChestLoot2 = new int[]
			{
				mod.ItemType("ScrapMetal"),
				mod.ItemType("AIChip"),
				mod.ItemType("Mk1Capacitator"),
				mod.ItemType("Mk2Capacitator"),
				mod.ItemType("Mk3Capacitator"),
				mod.ItemType("Mk1Plating"),
				mod.ItemType("Mk2Plating"),
				mod.ItemType("Mk3Plating")
			};
			int[] LabChestLoot3 = new int[]
			{
				mod.ItemType("AntiXenomiteApplier"),
				mod.ItemType("CarbonMyofibre"),
				mod.ItemType("Starlite"),
				mod.ItemType("XenomiteShard"),
				73
			};
			if (PlacementSuccess >= 0)
			{
				Chest chest = Main.chest[PlacementSuccess];
				Item item0 = chest.item[0];
				UnifiedRandom genRand0 = WorldGen.genRand;
				int[] array0 = new int[]
				{
					mod.ItemType("GasMask"),
					mod.ItemType("PlasmaShield"),
					mod.ItemType("MiniNuke"),
					mod.ItemType("XenoEye"),
					mod.ItemType("PlasmaSaber"),
					mod.ItemType("RadioactiveLauncher"),
					mod.ItemType("SludgeSpoon")
				};
				item0.SetDefaults(Utils.Next<int>(genRand0, array0), false);
				chest.item[1].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot2), false);
				chest.item[1].stack = WorldGen.genRand.Next(1, 3);
				chest.item[2].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot3), false);
				chest.item[2].stack = WorldGen.genRand.Next(8, 12);
				if (Main.rand.Next(4) == 0)
				{
					chest.item[3].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot), false);
				}
			}
		}

		public void SpecialLabChest(int x, int y)
		{
			Mod mod = Redemption.inst;
			int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)mod.TileType("LabChestTileLocked2"), false, 0);
			int[] LabChestLoot = new int[]
			{
				mod.ItemType("RawXenium")
			};
			int[] LabChestLoot2 = new int[]
			{
				mod.ItemType("Starlite")
			};
			if (PlacementSuccess >= 0)
			{
				Chest chest = Main.chest[PlacementSuccess];
				Item item0 = chest.item[0];
				UnifiedRandom genRand0 = WorldGen.genRand;
				int[] array0 = new int[]
				{
					mod.ItemType("NanoAxe")
				};
				item0.SetDefaults(Utils.Next<int>(genRand0, array0), false);
				chest.item[1].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot), false);
				chest.item[1].stack = WorldGen.genRand.Next(68, 92);
				chest.item[2].SetDefaults(Utils.Next<int>(WorldGen.genRand, LabChestLoot2), false);
				chest.item[2].stack = WorldGen.genRand.Next(20, 40);
			}
		}
	}
}