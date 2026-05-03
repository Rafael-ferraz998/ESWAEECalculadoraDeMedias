namespace OOPFoundation
{
    /// <summary>
    /// Valida notas de alunos no intervalo fechado [0,0; 10,0].
    /// </summary>
    public class NoteValidation : ADoubleValidation
    {
        /// <summary>
        /// Inicializa a validação de notas com intervalo [0,0; 10,0].
        /// </summary>
        public NoteValidation()
        {
            LowerLimit = 0.0;
            UpperLimit = 10.0;
        }
    }
}
