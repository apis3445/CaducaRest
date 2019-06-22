using System;
using System.Data;

namespace CaducaRest.Core
{
    public class EvaluaExpresion
    {
        private int recursividad;
        private readonly int MaximaRecursividad = 10;

        /// <summary>
        /// Contiene el valor de la fórmula
        /// </summary>
        public decimal? valor;
        //private string FormulaResumida;

        /// <summary>
        /// Indica el error de la fórmula
        /// </summary>
        public string error;

        /// <summary>
        /// Indica que hay un error en la expresión de la fórmula
        /// </summary>
        public bool errorEnFormula;

        /// <summary>
        /// Constructor
        /// </summary>
        public EvaluaExpresion()
        {
            recursividad = 0;
        }



        /// <summary>
        /// Permite validar una fórmula
        /// </summary>
        /// <remarks>
        /// Valida que la clave o nombre del indicador existan
        /// </remarks>
        /// <param name="formula">Fórmula a validar</param>
        /// <param name="empresaId">Id de la empresa</param>
        /// <returns></returns>
        public string Validar(string formula, int empresaId)
        {

            if (string.IsNullOrEmpty(formula))
            {
                error += "No se definió la fórmula";
                return String.Empty;
            }

            char[] caracteres = formula.ToCharArray();
            var FormulaResumida = string.Empty;
            var indicador = String.Empty;
            this.error = string.Empty;
            var tipoValor = String.Empty;
            var expresion = String.Empty;
            errorEnFormula = false;
            var inicial = string.Empty;
            TipoCaracterEnum tipoCaracter = TipoCaracterEnum.ValorInicio;
            bool esInicio = true;
             string[] valoresPosibles = new string[] { "base", "pb", "real", "meta", "valor1", "valor2", "valor3", "valor4", "valor 1", "valor 2", "valor 3", "valor 4" };
            for (int i = 0; i < caracteres.Length; i++)
            {
                char caracter = caracteres[i];
                switch (caracter)
                {
                    case '{':
                    case '[':
                        esInicio = true;
                        if (tipoCaracter == TipoCaracterEnum.ValorInicio)
                        {
                            tipoCaracter = TipoCaracterEnum.IndicadorInicio;
                        }
                        else
                        {
                            tipoCaracter = TipoCaracterEnum.ValorInicio;
                        }
                        break;
                    case '}':
                    case ']': //Se termina la fórmula para el indicador
                        if (esInicio)
                        {
                            esInicio = false;

                        }

                        break;
                    case '.':
                        if (esInicio)
                            indicador += caracter;
                        if (string.IsNullOrEmpty(indicador) && string.IsNullOrEmpty(tipoValor))
                        {
                            expresion += caracter;
                            FormulaResumida += caracter;
                        }
                        tipoValor = string.Empty;

                        break;
                    case '+':
                        if (!esInicio)
                        {
                            expresion += caracter;
                            FormulaResumida += caracter;
                        }
                        else
                        {
                            if (tipoCaracter == TipoCaracterEnum.IndicadorInicio)
                                indicador += caracter;
                            else
                                tipoValor += caracter;
                        }
                        break;
                    case '-':
                        if (!esInicio)
                        {
                            expresion += caracter;
                            FormulaResumida += caracter;
                        }
                        else
                        {
                            if (tipoCaracter == TipoCaracterEnum.IndicadorInicio)
                                indicador += caracter;
                            else
                                tipoValor += caracter;
                        }
                        break;
                    case '*':
                        if (!esInicio)
                        {
                            expresion += caracter;
                            FormulaResumida += caracter;
                        }
                        else
                        {
                            if (tipoCaracter == TipoCaracterEnum.IndicadorInicio)
                                indicador += caracter;
                            else
                                tipoValor += caracter;
                        }
                        break;
                    case '/':
                        if (!esInicio)
                        {
                            expresion += caracter;
                            FormulaResumida += caracter;
                        }
                        else
                        {
                            if (tipoCaracter == TipoCaracterEnum.IndicadorInicio)
                                indicador += caracter;
                            else
                                tipoValor += caracter;
                        }
                        break;
                    case '(':
                        if (!esInicio)
                        {
                            expresion += caracter;
                            FormulaResumida += caracter;
                        }
                        else
                        {
                            if (tipoCaracter == TipoCaracterEnum.IndicadorInicio)
                                indicador += caracter;
                            else
                                tipoValor += caracter;
                        }
                        break;
                    case ')':
                        if (!esInicio)
                        {
                            expresion += caracter;
                            FormulaResumida += caracter;
                        }
                        else
                        {
                            if (tipoCaracter == TipoCaracterEnum.IndicadorInicio)
                                indicador += caracter;
                            else
                                tipoValor += caracter;
                        }
                        break;
                    default:
                        if (char.IsLetterOrDigit(caracter) || caracter == ' ')
                        {
                            if (esInicio)
                            {
                                if (tipoCaracter == TipoCaracterEnum.IndicadorInicio)
                                    indicador += caracter;

                                if (tipoCaracter == TipoCaracterEnum.ValorInicio)
                                    tipoValor += caracter;
                            }
                            else
                            {
                                if (char.IsDigit(caracter))
                                {
                                    expresion += caracter;
                                    FormulaResumida += caracter;
                                }
                            }
                        }
                        break;
                }
            }

            if (string.IsNullOrEmpty(expresion))
            {
                this.errorEnFormula = true;
                this.error = "La expresión del indicador es incorrecta";
                return string.Empty;
            }

            if (string.IsNullOrEmpty(this.error))
            {
                try
                {
                    valor = Convert.ToDecimal(new DataTable().Compute(expresion.Replace("-", "+"), null));
                    return FormulaResumida;
                }
                catch (Exception ex)
                {
                    this.errorEnFormula = true;
                    this.error = ex.Message;
                    return string.Empty;
                }
            }
            return string.Empty;
        }


    }
}
