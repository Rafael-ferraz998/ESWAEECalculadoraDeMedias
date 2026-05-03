namespace OOPFoundation
{
    /// <summary>
    /// Implementação concreta de AText para uso geral.
    /// </summary>
    public class Text : AText
    {
        /// <summary>
        /// Construtor padrão. Cria instância com texto e padrão vazios.
        /// </summary>
        public Text() : base() { }

        /// <summary>
        /// Construtor que inicializa o texto com um padrão de sanitização.
        /// </summary>
        /// <param name="text">Texto a armazenar.</param>
        /// <param name="validPattern">Padrão de caracteres permitidos.</param>
        public Text(string text, string validPattern) : base()
        {
            SetText(text, validPattern);
        }
    }
}
