using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Services.Timers;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public static int MaxScanTicks { get; private set; } = 45;



		////////////////

		public static bool CanScanAt( int screenX, int screenY ) {
			bool Scan( PKEScannable data ) {
				Rectangle rect = data.ScreenAreaGetter.Invoke();
				return rect.Contains( screenX, screenY );
			}

			return PKEScannable.Scannables?.Values
				.Any( Scan ) ?? false;
		}


		////////////////

		public static void RunScanAt( int screenX, int screenY ) {
			var mymod = PKEMeterMod.Instance;

			var scanned = new List<string>();

			//

			foreach( (string name, PKEScannable scannable) in PKEScannable.Scannables ) {
				Rectangle area = scannable.ScreenAreaGetter.Invoke();
				
				if( !area.Contains(screenX, screenY) ) {
					continue;
				}

				//

				float addPerc = 1f / (float)PKEMeterItem.MaxScanTicks;

				if( (scannable.ScanPercent + addPerc) >= 1f ) {
					scanned.Add( name );
				} else {
					scannable.ScanPercent += addPerc;
				}
			}
			
			//
			
			foreach( string scanName in scanned ) {
				PKEScannable.CompleteScan( scanName );
			}

			//

			if( mymod.PKEScanLoop.State != SoundState.Playing ) {
				mymod.PKEScanLoop.Volume = 0.2f;
				mymod.PKEScanLoop.IsLooped = true;
				mymod.PKEScanLoop.Play();
			}

			Timers.SetTimer( "PKEScanSoundLoopCutoff", 2, false, () => {
				mymod.PKEScanLoop.Stop();
				return false;
			} );

			//

			if( scanned.Count > 0 ) {
				if( mymod.PKEScanDone.State != SoundState.Playing ) {
					mymod.PKEScanDone.Volume = 0.2f;
					mymod.PKEScanDone.Play();
				}
			}
		}
	}
}