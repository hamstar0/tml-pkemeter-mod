using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace PKEMeter.Logic {
	public delegate (string text, Color color, float priority) PKEText(
				Player player,
				Vector2 position,
				(float b, float g, float y, float r) gauges );




	////////////////

	partial class PKEMeterLogic : ILoadable {
		private static (string text, Color color, float priority) DefaultTextDisplay() {
			Color color = Color.Red * ( 0.5f + ( Main.rand.NextFloat() * 0.5f ) );
			string text = "";

			NPC bossNpc = Main.npc.FirstOrDefault(
				n => n?.active == true
				&& ( n.boss || n.type == NPCID.EaterofWorldsHead )
			);

			float priority = 0f;

			switch( bossNpc?.type ?? -1 ) {
			case -1:
				break;
			case NPCID.EyeofCthulhu:
				text = "WARNING - CLASS V FREEROAM CORPOREAL FLOATER";
				priority = 0.5f;
				break;
			case NPCID.KingSlime:
				text = "WARNING - CLASS V COMPOSITE ANIMATE SEMISOLID";
				priority = 0.5f;
				break;
			case NPCID.EaterofWorldsHead:
				text = "WARNING - CLASS VI SEQUENCED NECROTIC ORGANIC";
				priority = 0.6f;
				break;
			case NPCID.BrainofCthulhu:
				text = "WARNING - CLASS VI TRANSDIM SWARMHOST FLOATER";
				priority = 0.6f;
				break;
			case NPCID.SkeletronHead:
				text = "WARNING - CLASS VI REACTIVE FREEROAM CORPOREAL POSSESSOR";
				priority = 0.6f;
				break;
			case NPCID.QueenBee:
				text = "WARNING - CLASS VI FREEROAM SWARMHOST ORGANIC";
				priority = 0.6f;
				break;
			case NPCID.WallofFlesh:
				text = "WARNING - CLASS VII VOLUMINOUS KINETIC ORGANIC AMALGAMATE";
				priority = 0.7f;
				break;
			case NPCID.TheDestroyer:
				text = "WARNING - CLASS VII SEQUENCED CONSTRUCT";
				priority = 0.7f;
				break;
			case NPCID.Eyezor:
			case NPCID.Retinazer:
				text = "WARNING - CLASS VII PAIRED FREEROAM FLOATER CONSTRUCTS";
				priority = 0.7f;
				break;
			case NPCID.SkeletronPrime:
				text = "WARNING - CLASS VII FREEROAM MULTIFACET CONSTRUCT";
				priority = 0.7f;
				break;
			//case NPCID.QueenSlime:
			//	text = "WARNING - CLASS VII FREEROAM COMPOSITE ANIMATE SEMISOLID";
			//	priority = 0.7f;
			//	break;
			case NPCID.Plantera:
				text = "WARNING - CLASS VIII REACTIVE FLORAL CRAWLER";
				priority = 0.8f;
				break;
			case NPCID.Golem:
				text = "WARNING - CLASS VIII POWERED MULTIFACET CONSTRUCT";
				priority = 0.8f;
				break;
			case NPCID.DukeFishron:
				text = "WARNING - CLASS IX FREEROAM AQUATIC TRANSDIM ORGANIC";
				priority = 0.9f;
				break;
			case NPCID.CultistBoss:
				text = "WARNING - CLASS VIII FREEROAM TRANSDIM ELEVATED MORTAL";
				priority = 0.8f;
				break;
			//case NPCID.EmpressOfLight:
			//	text = "WARNING - CLASS IX FREEROAM LUMINOUS DEITY";
			//	priority = 0.9f;
			//	break;
			case NPCID.MoonLordCore:
				text = "WARNING - CLASS X FULLTORSO ULTRADIM DEITY REMNANT";
				priority = 1f;
				break;
			default:
				text = "WARNING - UNKNOWN CLASS V+ PKE-EMITTING ENTITY";
				priority = 0.501f;
				break;
			}

			return (text, color, priority);
		}



		////////////////

		private void InitializeDefaultText() {
			if( this.TextSources == null ) {
				this.TextSources.Add( (_, __, ___) => PKEMeterLogic.DefaultTextDisplay() );
			}
		}

		private void PostInitializeDefaultText() {
			Timers.SetTimer( 3, true, () => {
				this.TextScrollPos += 2;
				return true;
			} );
		}


		////////////////

		public (string text, Color color, int offset) GetText( Player player, Vector2 position ) {
			IEnumerable<(string text, Color color, float priority)> msgs = this.TextSources.Select(
				m => m.Invoke( player, position, this.GaugeSnapshot )
			);

			(string text, Color color, float priority) msg;
			if( msgs.Count() > 0 ) {
				msg = msgs.Aggregate(
					( l, r ) => l.priority > r.priority ? l : r
				);
			} else {
				msg = ( "", Color.White, 0f );
			}

			if( msg != this.CurrentText ) {
				if( msg.priority > this.CurrentText.priority || this.CurrentTextTickDuration <= 0 ) {
					this.CurrentTextTickDuration = 60 * 3;
					this.CurrentText = msg;
				}
			}

			this.CurrentTextTickDuration--;

			int textWid = msg.text.Length * 8;
			if( this.TextScrollPos > textWid ) {
				this.TextScrollPos = -6;
			}

			return ( msg.text, msg.color, this.TextScrollPos );
		}
	}
}
