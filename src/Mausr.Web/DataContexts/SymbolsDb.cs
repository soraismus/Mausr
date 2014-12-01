﻿using System;
using System.Data.Entity;
using Mausr.Core;
using Mausr.Web.Models;
using Newtonsoft.Json;

namespace Mausr.Web.DataContexts {
	
	public class SymbolsDb : DbContext {

		public SymbolsDb() : base("SymbolsConnection") {

		}


		public DbSet<Symbol> Symbols { get; set; }

		public DbSet<SymbolDrawing> SymbolDrawings { get; set; }


		public SymbolDrawing InsertSymbolDrawingFromJson(string jsonData, Symbol symbol, bool drawnUsingTouch) {

			RawDrawing drawing;
			try {
				var lines = JsonConvert.DeserializeObject<RawPoint[][]>(jsonData);
				drawing = new RawDrawing() { Lines = lines };
			}
			catch (Exception ex) {
				return null;
			}

			new RawDataProcessor().CleanData(drawing);

			if (drawing.LinesCount == 0) {
				return null;
			}

			var sd = new SymbolDrawing() {
				Symbol = symbol,
				RawDrawing = drawing,
				CreatedDateTime = DateTime.UtcNow,
				DrawnUsingTouch = drawnUsingTouch,
			};


			sd = SymbolDrawings.Add(sd);
			SaveChanges();

			return sd;
		}

	}
}