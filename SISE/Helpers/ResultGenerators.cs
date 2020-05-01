using System;
using System.Collections.Generic;
using System.Text;

namespace SISE.Helpers
{
    public class ResultGenerators
    {
        public void WriteResultState(string resultStateFilePath, string solution)
        {
            int resultLenght = solution != "No solution found!" ? solution.Length : -1;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(resultStateFilePath))
            {
                sw.WriteLine(resultLenght);
                if (resultLenght != -1)
                    sw.WriteLine(solution.ToUpper());
            }
        }

        public void WriteAdditionalInformation(string additionalInformationFilePath, int resultLength, int numberOfVisitedStates, int numberOfProcessedStates, int maxDepth, long durationOfCalculationProcess)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(additionalInformationFilePath))
            {
                sw.WriteLine(resultLength);
                sw.WriteLine(numberOfVisitedStates);
                sw.WriteLine(numberOfProcessedStates);
                sw.WriteLine(maxDepth);
                sw.WriteLine(durationOfCalculationProcess);
            }
        }
    }
}
