using System.ComponentModel;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        #region UI Properties
        public string Destaque
        {
            get => destaque;
            set
            {
                destaque = value;
                OnPropertyChanged();
            }
        }

        public List<char> Letras
        {
            get => letras;
            set
            {
                letras = value;
                OnPropertyChanged();
            }
        }

        public string Mensagem
        {
            get => mensagem;
            set
            {
                mensagem = value;
                OnPropertyChanged();
            }
        }

        public string StatusJogo
        {
            get => statusJogo;
            set
            {
                statusJogo = value;
                OnPropertyChanged();
            }
        }

        public string ImagemAtual
        {
            get => imagemAtual;
            set
            {
                imagemAtual = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Fields
        List<string> palavras = new List<string>()
        {
            "python",
            "java",
            "javascript",
            "csharp",
            "ruby",
            "php",
            "swift",
            "kotlin",
            "typescript",
            "go",
            "scala",
            "r",
            "perl",
            "rust",
            "elixir",
            "haskell",
            "clojure",
            "erlang",
            "lua",
            "cobol",
            "fortran",
            "pascal",
            "lisp",
            "ada",
            "scheme",
            "prolog",
            "smalltalk",
            "forth",
            "apex",
            "abap",
            "dart",
            "julia",
            "groovy",
            "powershell",
            "bash",
            "shell",
            "racket",
            "fsharp",
            "ocaml",
            "visualbasic",
            "delphi",
            "actionscript",
            "assembly",
            "objectiveC",
            "sql",
            "mongodb"
        };

        string resposta = "";
        private string destaque;
        List<char> palpite = new List<char>();
        private List<char> letras = new List<char>();
        private string mensagem;
        private string statusJogo;
        private string imagemAtual = "image0.png";
        int erros = 0;
        int maximoErrors = 6;
        #endregion

        public MainPage()
        {
            InitializeComponent();
            Letras.AddRange("abcdefghijklmnopqrstuvwxyz");
            BindingContext = this;
            EscolherPalavra();
            CalcularPalavra(resposta, palpite);
        }
        #region Jogo

        private void EscolherPalavra()
        {
            resposta = palavras[new Random().Next(0, palavras.Count)];
            Debug.WriteLine(resposta);
        }

        private void CalcularPalavra(string resposta, List<char> palpite)
        {
            var temp = resposta.Select
                (x => (palpite.IndexOf(x) >= 0 ? x : '_')).ToArray();

            Destaque = string.Join(' ', temp);

        }

        private void VerificarPalpite(char letra)
        {
            if (palpite.IndexOf(letra) == -1)
            {
                palpite.Add(letra);
            }

            if(resposta.IndexOf(letra) >= 0)
            {
                CalcularPalavra(resposta, palpite);
                VerificarSeGanhou();
            }
            else if (resposta.IndexOf(letra) == -1)
            {
                erros++;
                ImagemAtual = $"image{erros}.png";
                VerificarSePerdeu();
            }
        }

        private void VerificarSeGanhou()
        {
            if (Destaque.Replace(" ", "") == resposta)
            {
                Mensagem = "Parabéns, você ganhou!";
                DesabilitarLetras();
            }
        }

        private void VerificarSePerdeu()
        {
            if (erros == maximoErrors)
            {
                StatusJogo = "Você perdeu!";
                Mensagem = "A palavra era: " + resposta;
                DesabilitarLetras();
            }
        }

        private void DesabilitarLetras()
        {
            foreach (var children in LetrasContainer.Children)
            {
                var btn = children as Button;
                if (btn != null)
                {
                    btn.IsEnabled = false;
                }
            }
        }
        private void HabilitarLetras()
        {
            foreach (var children in LetrasContainer.Children)
            {
                var btn = children as Button;
                if (btn != null)
                {
                    btn.IsEnabled = true;
                }
            }
        }

        private void AtualizarStatus()
        {
            StatusJogo = $"Erros: {erros}/{maximoErrors}";
        }

        #endregion

        private void Reset_Clicked(object sender, EventArgs e)
        {
            erros = 0;
            palpite = new List<char>();
            ImagemAtual = "image0.png";
            EscolherPalavra();
            CalcularPalavra(resposta, palpite);
            Mensagem = "";
            AtualizarStatus();
            HabilitarLetras();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var letra = btn.Text;
                btn.IsEnabled = false;
                VerificarPalpite(letra[0]);
            }
        }

       
    }

}
