﻿using System;
using Mausr.Core.Optimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mausr.Tests.Optimization {
	[TestClass]
	public class ImprovedRpropMinusOptmizerTests {
		[TestMethod]
		public void OptimizeTest() {
			var instance = new ImprovedRpropMinusOptmizer(0.05, 10, 1.2, 0.5, 256);
			OptimizerTestsHelper.PerformOptimizerTestOnSinCosCrazyFunction(instance);
		}
	}
}
