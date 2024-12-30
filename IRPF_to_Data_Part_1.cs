using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Text;
using System.Text.RegularExpressions; // Importe para usar Regex

public class LeitorPdf
{
    public static void Main(string[] args)
    {
        string caminhoDoPdf = "irpf.pdf";
        string textoDoPdf = LerPdf(caminhoDoPdf);

        if (textoDoPdf.StartsWith("Erro ao ler o PDF") || textoDoPdf == "Arquivo não encontrado.")
        {
            Console.WriteLine(textoDoPdf);
        }
        else
        {
            Pessoa pessoa = new Pessoa();

            // Usando Regex para extrair as informações de forma mais robusta
            string cpfPattern = @"CPF do declarante:\s*([0-9.]*-?[0-9]{2})"; // Expressão regular para CPF
            string modeloPattern = @"Modelo:\s*(.+)"; // Expressão regular para Modelo (captura tudo após "Modelo:")

            Match cpfMatch = Regex.Match(textoDoPdf, cpfPattern);
            Match modeloMatch = Regex.Match(textoDoPdf, modeloPattern);

            if (cpfMatch.Success)
            {
                pessoa.Usuario = cpfMatch.Groups[1].Value.Trim();
            }
            else
            {
                 Console.WriteLine("CPF não encontrado no PDF");
            }

            if (modeloMatch.Success)
            {
                pessoa.Modelo = modeloMatch.Groups[1].Value.Trim();
            }
            else
            {
                Console.WriteLine("Modelo não encontrado no PDF");
            }

            if(pessoa.Usuario != null && pessoa.Modelo != null) //verifica se os dois foram encontrados antes de imprimir.
            {
                 Console.WriteLine($"As informações extraídas são: {pessoa.Usuario} e {pessoa.Modelo}");
            }
        }

        Console.ReadKey();
    }

    public static string LerPdf(string caminhoArquivo)
    {
        // ... (código de leitura do PDF permanece o mesmo)
        try
        {
            // Verifica se o arquivo existe
            if (!System.IO.File.Exists(caminhoArquivo))
            {
                return "Arquivo não encontrado.";
            }

            StringBuilder texto = new StringBuilder();

            using (PdfReader leitor = new PdfReader(caminhoArquivo))
            using (PdfDocument pdfDocumento = new PdfDocument(leitor))
            {
                for (int i = 1; i <= pdfDocumento.GetNumberOfPages(); i++)
                {
                    // Usa o SimpleTextExtractionStrategy para extrair o texto
                    ITextExtractionStrategy estrategia = new SimpleTextExtractionStrategy();
                    string textoPagina = PdfTextExtractor.GetTextFromPage(pdfDocumento.GetPage(i), estrategia);
                    texto.Append(textoPagina);
                }
            }

            return texto.ToString();
        }
        catch (Exception ex)
        {
            return $"Erro ao ler o PDF: {ex.Message}";
        }
    }

    public class Pessoa
    {
        public string Usuario { get; set; }
        public string Modelo { get; set; }

        //Remova o construtor que lança exceção, ou implemente um construtor vazio caso precise.
        public Pessoa()
        {

        }
    }
}
