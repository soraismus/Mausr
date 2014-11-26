﻿using System.Diagnostics.Contracts;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Mausr.Core.NeuralNet {
	public class NetEvaluator {

		public Net Net { get; private set; }


		public NetEvaluator(Net network) {
			Net = network;
		}

		/// <param name="inputs">Matrix with data in rows.</param>
		public Matrix<double> Evaluate(Matrix<double> inputs) {
			Contract.Requires(inputs.ColumnCount == Net.Layout.InputSize);
			Contract.Ensures(Contract.Result<Matrix<double>>().ColumnCount == Net.Layout.OutputSize);

			Matrix<double> result = inputs;

			for (int i = 0; i < Net.Layout.CoefsCount; ++i) {
				result = evalStep(i, result);
			}

			return result;
		}

		protected Matrix<double> evalStep(int coefIndex, Matrix<double> input) {
			var result = input.InsertColumn(0, DenseVector.Create(input.RowCount, 1));
			result = result.Multiply(Net.GetCoefsMatrix(coefIndex));
			result.Map(Net.NeuronActivationFunc.Evaluate, result);
			return result;
		}

	}
}