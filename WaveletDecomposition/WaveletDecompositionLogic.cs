using System.Collections.Generic;

namespace WaveletDecomposition
{
    public class WaveletDecompositionLogic
    {
        #region WaveletDecompositionConstants

        private float[] _analysisL = new float[]
        {
            0.026748757411f,
            -0.016864118443f,
            -0.078223266529f,
            0.266864118443f,
            0.602949018236f,
            0.266864118443f,
            -0.078223266529f,
            -0.016864118443f,
            0.026748757411f
        }, 
            _analysisH = new float[]
        {
            0.000000000000f,
            0.091271763114f,
            -0.057543526229f,
            -0.591271763114f,
            1.115087052457f,
            -0.591271763114f,
            -0.057543526229f,
            0.091271763114f,
            0.000000000000f
        };

        private float[] _synthesisL = new float[]
        {
            0.000000000000f,
            -0.091271763114f,
            -0.057543526229f,
            0.591271763114f,
            1.115087052457f,
            0.591271763114f,
            -0.057543526229f,
            -0.091271763114f,
            0.000000000000f
        },
            _synthesisH = new float[]
        {
            0.026748757411f,
            0.016864118443f,
            -0.078223266529f,
            -0.266864118443f,
            0.602949018236f,
            -0.266864118443f,
            -0.078223266529f,
            0.016864118443f,
            0.026748757411f
        };

        #endregion

        public List<float> AnalysisH(float[] line, int length)
        {
            return GetAnalysisVector(line, length);
        }

        public List<float> AnalysisV(float[] col, int length)
        {
            return GetAnalysisVector(col, length);
        }

        public List<float> SynthesisH(float[] line, int length)
        {
            return GetSynthesisVector(line, length);
        }

        public List<float> SynthesisV(float[] col, int length)
        {
            return GetSynthesisVector(col, length);
        }

        private List<float> GetAnalysisVector(float[] line, int length)
        {
            var linePad = new List<float>()
            {
                line[4], line[3], line[2], line[1],
            };

            linePad.AddRange(line);
            linePad.Add(line[length - 2]);
            linePad.Add(line[length - 3]);
            linePad.Add(line[length - 4]);
            linePad.Add(line[length - 5]);

            var lowAnalysisList = new List<float>();
            var highAnalysisList = new List<float>();
            for (int lineIndex = 0; lineIndex < length; lineIndex++)
            {
                float lowVal = 0.0f, highVal = 0.0f;

                for (int analyzingIndex = 0; analyzingIndex < 9; analyzingIndex++)
                {
                    lowVal += linePad[lineIndex + analyzingIndex] * _analysisL[analyzingIndex];
                    highVal += linePad[lineIndex + analyzingIndex] * _analysisH[analyzingIndex];
                }

                lowAnalysisList.Add(lowVal);
                highAnalysisList.Add(highVal);
            }

            var result = new List<float>();
            for (int i = 0; i < lowAnalysisList.Count; i += 2)
            {
                result.Add(lowAnalysisList[i]);
            }
            for (int i = 1; i < highAnalysisList.Count; i += 2)
            {
                result.Add(highAnalysisList[i]);
            }

            return result;
        }

        private List<float> GetSynthesisVector(float[] line, int length)
        {
            var lowTemp = new List<float>();
            for (int i = 0; i < length / 2; i++)
            {
                lowTemp.Add(line[i]);
                lowTemp.Add(0f);
            }

            var lowLinePad = new List<float>()
            {
                lowTemp[4], lowTemp[3], lowTemp[2], lowTemp[1]
            };

            lowLinePad.AddRange(lowTemp);
            lowLinePad.Add(lowTemp[lowTemp.Count - 2]);
            lowLinePad.Add(lowTemp[lowTemp.Count - 3]);
            lowLinePad.Add(lowTemp[lowTemp.Count - 4]);
            lowLinePad.Add(lowTemp[lowTemp.Count - 5]);

            var highTemp = new List<float>();
            for (int i = length / 2; i < length; i++)
            {
                highTemp.Add(0f);
                highTemp.Add(line[i]);
            }

            var highLinePad = new List<float>()
            {
                highTemp[4], highTemp[3], highTemp[2], highTemp[1]
            };

            highLinePad.AddRange(highTemp);
            highLinePad.Add(highTemp[highTemp.Count - 2]);
            highLinePad.Add(highTemp[highTemp.Count - 3]);
            highLinePad.Add(highTemp[highTemp.Count - 4]);
            highLinePad.Add(highTemp[highTemp.Count - 5]);

            var lowSynthesisList = new List<float>();
            var highSynthesisList = new List<float>();
            for (int lineIndex = 0; lineIndex < length; lineIndex++)
            {
                float lowVal = 0.0f, highVal = 0.0f;

                for (int synthesisIndex = 0; synthesisIndex < 9; synthesisIndex++)
                {
                    lowVal += lowLinePad[lineIndex + synthesisIndex] * _synthesisL[synthesisIndex];
                    highVal += highLinePad[lineIndex + synthesisIndex] * _synthesisH[synthesisIndex];
                }

                lowSynthesisList.Add(lowVal);
                highSynthesisList.Add(highVal);
            }

            var result = new List<float>();
            for (int i = 0; i < lowSynthesisList.Count; i++)
            {
                result.Add(lowSynthesisList[i] + highSynthesisList[i]);
            }

            return result;
        }
    }
}
