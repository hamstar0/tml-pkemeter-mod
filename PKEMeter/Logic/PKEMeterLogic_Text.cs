using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Services.Timers;


namespace PKEMeter.Logic {
	public delegate (string text, Color color) PKEText(
				Player player,
				Vector2 position,
				(float b, float g, float y, float r) gauges );




	partial class PKEMeterLogic : ILoadable {
		private void InitializeDefaultText() {
			if( this.CurrentText != null ) { return; }

			this.CurrentText = ( _, __, ____ ) => {
				Color color = Color.Red * (0.5f + (Main.rand.NextFloat() * 0.5f));
				string text = "";

				NPC bossNpc = Main.npc.FirstOrDefault(
					n=>n?.active == true
					&& (n.boss || n.type == NPCID.EaterofWorldsHead)
				);

				switch( bossNpc?.type ?? -1 ) {
				case -1:
					break;
				case NPCID.EyeofCthulhu:
					text = "WARNING - CLASS V FREEROAM CORPOREAL FLOATER";
					break;
				case NPCID.KingSlime:
					text = "WARNING - CLASS V COMPOSITE ANIMATE SEMISOLID";
					break;
				case NPCID.EaterofWorldsHead:
					text = "WARNING - CLASS VI SEQUENCED NECROTIC ORGANIC";
					break;
				case NPCID.BrainofCthulhu:
					text = "WARNING - CLASS VI TRANSDIM SWARMHOST FLOATER";
					break;
				case NPCID.SkeletronHead:
					text = "WARNING - CLASS VI REACTIVE FREEROAM CORPOREAL POSSESSOR";
					break;
				case NPCID.QueenBee:
					text = "WARNING - CLASS VI FREEROAM SWARMHOST ORGANIC";
					break;
				case NPCID.WallofFlesh:
					text = "WARNING - CLASS VII VOLUMINOUS KINETIC ORGANIC AMALGAMATE";
					break;
				case NPCID.TheDestroyer:
					text = "WARNING - CLASS VII SEQUENCED CONSTRUCT";
					break;
				case NPCID.Eyezor:
				case NPCID.Retinazer:
					text = "WARNING - CLASS VII PAIRED FREEROAM FLOATER CONSTRUCTS";
					break;
				case NPCID.SkeletronPrime:
					text = "WARNING - CLASS VII FREEROAM MULTIFACET CONSTRUCT";
					break;
				//case NPCID.QueenSlime:
				//	text = "WARNING - CLASS VII FREEROAM COMPOSITE ANIMATE SEMISOLID";
				//	break;
				case NPCID.Plantera:
					text = "WARNING - CLASS VIII REACTIVE FLORAL CRAWLER";
					break;
				case NPCID.Golem:
					text = "WARNING - CLASS VII POWERED MULTIFACET CONSTRUCT";
					break;
				case NPCID.DukeFishron:
					text = "WARNING - CLASS IX FREEROAM AQUATIC TRANSDIM ORGANIC";
					break;
				case NPCID.CultistBoss:
					text = "WARNING - CLASS VIII FREEROAM TRANSDIM ELEVATED MORTAL";
					break;
				//case NPCID.EmpressOfLight:
				//	text = "WARNING - CLASS IX FREEROAM LUMINOUS DEITY";
				//	break;
				case NPCID.MoonLordCore:
					text = "WARNING - CLASS X FULLTORSO ULTRADIM DEITY REMNANT";
					break;
				default:
					text = "WARNING - UNKNOWN CLASS V+ PKE-EMITTING ENTITY";
					break;
				}

				return (text, color);
			};

			Timers.SetTimer( 4, true, () => {
				this.TextScrollPos += 2;
				return true;
			} );
		}


		////

		public (string text, Color color, int offset) GetText( Player player, Vector2 position ) {
			(string text, Color color) msg = this.CurrentText?.Invoke( player, position, this.GaugeSnapshot )
				?? ("", Color.White);
			int textWid = msg.text.Length * 8;

			if( this.TextScrollPos > textWid ) {
				this.TextScrollPos = -6;
			}

			return (msg.text, msg.color, this.TextScrollPos);
		}
	}
}
