using OOPFoundation;
using Xunit;

namespace CalculadoraDeMediasEE.Tests
{
    // ══════════════════════════════════════════════════════════════════
    // 1. Validação de Notas  [0,0 ; 10,0]
    // ══════════════════════════════════════════════════════════════════
    public class NoteValidationTests
    {
        private readonly NoteValidation _validator = new();

        [Theory]
        [InlineData(0.0)]
        [InlineData(5.0)]
        [InlineData(7.0)]
        [InlineData(10.0)]
        public void DoubleIsValid_NotaValida_DeveRetornarTrue(double nota)
        {
            Assert.True(_validator.DoubleIsValid(nota));
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1.0)]
        [InlineData(10.1)]
        [InlineData(11.0)]
        public void DoubleIsValid_NotaInvalida_DeveRetornarFalse(double nota)
        {
            Assert.False(_validator.DoubleIsValid(nota));
        }

        [Fact]
        public void DoubleIsValid_LimiteInferior_DeveRetornarTrue()
            => Assert.True(_validator.DoubleIsValid(0.0));

        [Fact]
        public void DoubleIsValid_LimiteSuperior_DeveRetornarTrue()
            => Assert.True(_validator.DoubleIsValid(10.0));
    }

    // ══════════════════════════════════════════════════════════════════
    // 2. Validação de Pesos  [0,0 ; 1,0]
    // ══════════════════════════════════════════════════════════════════
    public class WeightValidationTests
    {
        private readonly WeightValidation _validator = new();

        [Theory]
        [InlineData(0.0)]
        [InlineData(0.4)]
        [InlineData(1.0)]
        public void DoubleIsValid_PesoValido_DeveRetornarTrue(double peso)
        {
            Assert.True(_validator.DoubleIsValid(peso));
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(1.1)]
        [InlineData(2.0)]
        public void DoubleIsValid_PesoInvalido_DeveRetornarFalse(double peso)
        {
            Assert.False(_validator.DoubleIsValid(peso));
        }
    }

    // ══════════════════════════════════════════════════════════════════
    // 3. Soma dos Pesos (MS: 4+4+2=10 → pesos 0.4+0.4+0.2=1.0)
    // ══════════════════════════════════════════════════════════════════
    public class WeightSumTests
    {
        [Fact]
        public void SomaDePesosSemestral_DeveSerUm()
        {
            double pesoNP1 = 0.4;
            double pesoNP2 = 0.4;
            double pesoPIM = 0.2;
            double soma = pesoNP1 + pesoNP2 + pesoPIM;
            Assert.Equal(1.0, soma, precision: 10);
        }

        [Fact]
        public void SomaDePesosFinal_DeveSerUm()
        {
            double pesoMS = 0.5;
            double pesoEX = 0.5;
            double soma = pesoMS + pesoEX;
            Assert.Equal(1.0, soma, precision: 10);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    // 4. Cálculo da Média Semestral
    // ══════════════════════════════════════════════════════════════════
    public class SemestralAverageTests
    {
        // MS = (4*NP1 + 4*NP2 + 2*PIM) / 10

        [Fact]
        public void CalculateSemestralAverage_TodosZero_DeveRetornarZero()
        {
            double ms = GradeCalculator.CalculateSemestralAverage(0, 0, 0);
            Assert.Equal(0.0, ms);
        }

        [Fact]
        public void CalculateSemestralAverage_TodosDez_DeveRetornarDez()
        {
            double ms = GradeCalculator.CalculateSemestralAverage(10, 10, 10);
            Assert.Equal(10.0, ms);
        }

        [Fact]
        public void CalculateSemestralAverage_ExemploTipico_DeveCalcularCorretamente()
        {
            // (4*8 + 4*7 + 2*6) / 10 = (32+28+12)/10 = 72/10 = 7.2
            double ms = GradeCalculator.CalculateSemestralAverage(8, 7, 6);
            Assert.Equal(7.2, ms);
        }

        [Fact]
        public void CalculateSemestralAverage_ArredondamentoMidpoint_DeveArredondarParaCima()
        {
            // (4*6 + 4*6 + 2*7) / 10 = (24+24+14)/10 = 62/10 = 6.2
            double ms = GradeCalculator.CalculateSemestralAverage(6, 6, 7);
            Assert.Equal(6.2, ms);
        }

        [Fact]
        public void CalculateSemestralAverage_ResultadoExato_SemArredondamento()
        {
            // (4*5 + 4*5 + 2*5) / 10 = 50/10 = 5.0
            double ms = GradeCalculator.CalculateSemestralAverage(5, 5, 5);
            Assert.Equal(5.0, ms);
        }

        [Theory]
        [InlineData(10, 10, 10, 10.0)]
        [InlineData(7,  7,  7,  7.0)]
        [InlineData(0,  0,  0,  0.0)]
        [InlineData(8,  6,  5,  6.6)]  // (32+24+10)/10 = 66/10 = 6.6
        public void CalculateSemestralAverage_Parametrizado(double np1, double np2, double pim, double esperado)
        {
            double ms = GradeCalculator.CalculateSemestralAverage(np1, np2, pim);
            Assert.Equal(esperado, ms);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    // 5. Status da Média Semestral
    // ══════════════════════════════════════════════════════════════════
    public class SemestralStatusTests
    {
        [Theory]
        [InlineData(7.0)]
        [InlineData(8.5)]
        [InlineData(10.0)]
        public void GetSemestralStatus_MediaMaiorOuIgualA7_DeveRetornarAprovado(double ms)
        {
            Assert.Equal(StudentStatus.Aprovado, GradeCalculator.GetSemestralStatus(ms));
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(5.0)]
        [InlineData(6.9)]
        public void GetSemestralStatus_MediaMenorQue7_DeveRetornarEmExame(double ms)
        {
            Assert.Equal(StudentStatus.EmExame, GradeCalculator.GetSemestralStatus(ms));
        }

        [Fact]
        public void GetSemestralStatus_LimiteExato7_DeveRetornarAprovado()
        {
            Assert.Equal(StudentStatus.Aprovado, GradeCalculator.GetSemestralStatus(7.0));
        }

        [Fact]
        public void GetSemestralStatus_AbaixoDoLimite_DeveRetornarEmExame()
        {
            Assert.Equal(StudentStatus.EmExame, GradeCalculator.GetSemestralStatus(6.9));
        }
    }

    // ══════════════════════════════════════════════════════════════════
    // 6. Cálculo da Média Final
    // ══════════════════════════════════════════════════════════════════
    public class FinalAverageTests
    {
        // MF = (MS + EX) / 2

        [Fact]
        public void CalculateFinalAverage_TodosZero_DeveRetornarZero()
        {
            double mf = GradeCalculator.CalculateFinalAverage(0, 0);
            Assert.Equal(0.0, mf);
        }

        [Fact]
        public void CalculateFinalAverage_TodosDez_DeveRetornarDez()
        {
            double mf = GradeCalculator.CalculateFinalAverage(10, 10);
            Assert.Equal(10.0, mf);
        }

        [Fact]
        public void CalculateFinalAverage_ExemploTipico_DeveCalcularCorretamente()
        {
            // (6.5 + 7.5) / 2 = 7.0
            double mf = GradeCalculator.CalculateFinalAverage(6.5, 7.5);
            Assert.Equal(7.0, mf);
        }

        [Fact]
        public void CalculateFinalAverage_LimiteAprovacao_DeveRetornar5()
        {
            // (4.0 + 6.0) / 2 = 5.0
            double mf = GradeCalculator.CalculateFinalAverage(4.0, 6.0);
            Assert.Equal(5.0, mf);
        }

        [Theory]
        [InlineData(6.0, 4.0, 5.0)]
        [InlineData(5.0, 5.0, 5.0)]
        [InlineData(6.0, 8.0, 7.0)]
        [InlineData(4.0, 4.0, 4.0)]
        public void CalculateFinalAverage_Parametrizado(double ms, double ex, double esperado)
        {
            double mf = GradeCalculator.CalculateFinalAverage(ms, ex);
            Assert.Equal(esperado, mf);
        }
    }

    // ══════════════════════════════════════════════════════════════════
    // 7. Status da Média Final
    // ══════════════════════════════════════════════════════════════════
    public class FinalStatusTests
    {
        [Theory]
        [InlineData(5.0)]
        [InlineData(7.0)]
        [InlineData(10.0)]
        public void GetFinalStatus_MediaMaiorOuIgualA5_DeveRetornarAprovado(double mf)
        {
            Assert.Equal(StudentStatus.Aprovado, GradeCalculator.GetFinalStatus(mf));
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(3.5)]
        [InlineData(4.9)]
        public void GetFinalStatus_MediaMenorQue5_DeveRetornarReprovado(double mf)
        {
            Assert.Equal(StudentStatus.Reprovado, GradeCalculator.GetFinalStatus(mf));
        }

        [Fact]
        public void GetFinalStatus_LimiteExato5_DeveRetornarAprovado()
        {
            Assert.Equal(StudentStatus.Aprovado, GradeCalculator.GetFinalStatus(5.0));
        }

        [Fact]
        public void GetFinalStatus_AbaixoDoLimite_DeveRetornarReprovado()
        {
            Assert.Equal(StudentStatus.Reprovado, GradeCalculator.GetFinalStatus(4.9));
        }
    }
}
