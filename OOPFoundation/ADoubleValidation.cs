namespace OOPFoundation
{
    /// <summary>
    /// Classe abstrata base para validação de valores double com limites inferior e superior.
    /// </summary>
    public abstract class ADoubleValidation : IDoubleValidation
    {
        /// <summary>Limite inferior do intervalo válido (inclusive).</summary>
        protected double LowerLimit { get; set; }

        /// <summary>Limite superior do intervalo válido (inclusive).</summary>
        protected double UpperLimit { get; set; }

        /// <summary>
        /// Verifica se o valor está dentro do intervalo fechado [LowerLimit, UpperLimit].
        /// </summary>
        /// <param name="doubleToValidate">Valor a ser validado.</param>
        /// <returns>True se válido, false caso contrário.</returns>
        public bool DoubleIsValid(double doubleToValidate)
        {
            return doubleToValidate >= LowerLimit && doubleToValidate <= UpperLimit;
        }
    }
}
