using System;

namespace OOPFoundation
{
    /// <summary>
    /// Status possíveis do aluno.
    /// </summary>
    public enum StudentStatus
    {
        EmAndamento,
        Aprovado,
        EmExame,
        Reprovado
    }

    /// <summary>
    /// Realiza o cálculo das médias Semestral e Final e define o status do aluno.
    /// Regras:
    ///   MS = (4*NP1 + 4*NP2 + 2*PIM) / 10
    ///   Se MS >= 7,0 → Aprovado
    ///   Se MS <  7,0 → Em Exame
    ///   MF = (MS + EX) / 2
    ///   Se MF >= 5,0 → Aprovado
    ///   Se MF <  5,0 → Reprovado
    /// Arredondamento: MidpointRounding.AwayFromZero para 1 casa decimal.
    /// </summary>
    public class GradeCalculator
    {
        /// <summary>
        /// Calcula a Média Semestral.
        /// </summary>
        public static double CalculateSemestralAverage(double np1, double np2, double pim)
        {
            double ms = (4 * np1 + 4 * np2 + 2 * pim) / 10.0;
            return Math.Round(ms, 1, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calcula a Média Final.
        /// </summary>
        public static double CalculateFinalAverage(double semestral, double exam)
        {
            double mf = (semestral + exam) / 2.0;
            return Math.Round(mf, 1, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Determina o status com base na Média Semestral.
        /// </summary>
        public static StudentStatus GetSemestralStatus(double semestral)
        {
            return semestral >= 7.0 ? StudentStatus.Aprovado : StudentStatus.EmExame;
        }

        /// <summary>
        /// Determina o status com base na Média Final.
        /// </summary>
        public static StudentStatus GetFinalStatus(double finalAverage)
        {
            return finalAverage >= 5.0 ? StudentStatus.Aprovado : StudentStatus.Reprovado;
        }
    }
}
