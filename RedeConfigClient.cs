using System;
using Terraria.ModLoader.Config;

namespace Redemption
{
	public class RedeConfigClient : ModConfig
	{
		public override ConfigScope Mode
		{
			get
			{
				return 1;
			}
		}

		public static RedeConfigClient Instance;

		[Label("Classic Vlitch Cleaver Sprite")]
		public bool classicRedeVC;

		[Label("Previous Infected Eye Sprite")]
		public bool classicRedeIE;

		[Label("No Combat Text")]
		[Tooltip("Disables combat text, the text that appears above an enemies head, e.g. when a chicken pecks the ground")]
		public bool NoCombatText;

		[Label("Disable Empty Core")]
		[Tooltip("New players won't have an Empty Core in their inventory")]
		public bool NoStarterCore;

		[Label("SHUT UP KING SLAYER NO ONE CARES")]
		[Tooltip("Disables all boss dialogue from the mod (Don't use while a boss is alive)")]
		public bool NoBossText;

		[Label("Anti-Antti (Disables Antti's music)")]
		[Tooltip("Youtubers will get claimed when using that music, so if you're a Youtuber, enable this. (Getting claimed will NOT affect your channel, unlike copyright strikes)")]
		public bool AntiAntti;

		[Label("Music Replacements for Claimable Music")]
		[Tooltip("Requires enabling 'Anti-Antti' config option. Replaces some of the music that would claim YouTube videos with free-to-use alternatives, to still make fights enjoyable.")]
		public bool MusicReplacements;

		[Label("I'm Arachnophobic!")]
		[Tooltip("Disables Forest Spiders and other spiders from this mod from spawning, won't disable vanilla game's spiders.")]
		public bool NoSpidersInMyTerrariaMod;

		[Label("Disable Druid Class Tag")]
		[Tooltip("Disables the ---Druid Class--- tooltip that is under druid item names")]
		public bool NoDruidClassTag;

		[Label("Disable Nature Guardian Info")]
		[Tooltip("Disables the ---Guardian Info--- tooltip and contents under it, so you won't know what guardians do what!")]
		public bool NoGuardianInfo;
	}
}
