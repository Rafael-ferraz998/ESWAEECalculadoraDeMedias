namespace OOPFoundation
{
    /// <summary>
    /// Valida pesos de cálculo no intervalo fechado [0,0; 1,0].
    /// </summary>
    public class WeightValidation : ADoubleValidation
    {
        /// <summary>
        /// Inicializa a validação de pesos com intervalo [0,0; 1,0].
        /// </summary>
        public WeightValidation()
        {
            LowerLimit = 0.0;
            UpperLimit = 1.0;
        }
    }
}
