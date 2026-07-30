using System.Text.Json; // para usar métodos de manipulação de json.

namespace Sistema_De_Aplicativos_Simples__.NET.appsForms
{
    public class Booksearch : Form
    {
        private readonly HttpClient httpClient = new HttpClient();

        private Label lblPesquisa;
        private TextBox txtPesquisa;
        private Button btnPesquisar;

        private PictureBox picCapa;

        private GroupBox grpLivro;

        private Label lblTitulo;
        private Label lblAutor;
        private Label lblEditora;
        private Label lblAno;
        private Label lblISBN;

        private RichTextBox txtDescricao;

        public Booksearch()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            Text = "Open Library API - Pesquisa de Livros";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(820, 560);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            lblPesquisa = new Label
            {
                Text = "Pesquisar livro:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtPesquisa = new TextBox
            {
                Location = new Point(20, 45),
                Width = 560
            };

            txtPesquisa.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await Pesquisar();
                }
            };

            btnPesquisar = new Button
            {
                Text = "Pesquisar",
                Location = new Point(600, 43),
                Width = 150
            };

            btnPesquisar.Click += async (s, e) => await Pesquisar();

            picCapa = new PictureBox
            {
                Location = new Point(20, 90),
                Size = new Size(180, 260),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            grpLivro = new GroupBox
            {
                Text = "Informações",
                Location = new Point(220, 90),
                Size = new Size(560, 190)
            };

            lblTitulo = CriarLabel(240, 120);
            lblAutor = CriarLabel(240, 160);
            lblEditora = CriarLabel(240, 200);
            lblAno = CriarLabel(240, 240);
            lblISBN = CriarLabel(240, 280);

            txtDescricao = new RichTextBox
            {
                Location = new Point(20, 365),
                Size = new Size(760, 90),
                ReadOnly = true,
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(lblPesquisa);
            Controls.Add(txtPesquisa);
            Controls.Add(btnPesquisar);
            Controls.Add(picCapa);

            Controls.Add(lblTitulo);
            Controls.Add(lblAutor);
            Controls.Add(lblEditora);
            Controls.Add(lblAno);
            Controls.Add(lblISBN);

            Controls.Add(grpLivro);
            Controls.Add(txtDescricao);

            AcceptButton = btnPesquisar;
        }

        private Label CriarLabel(int x, int y)
        {
            return new Label
            {
                Location = new Point(x, y),
                Size = new Size(560, 30),
                Font = new Font("Segoe UI", 10)
            };
        }

        private async Task Pesquisar()
        {
            // pequena validação de campo, pra não fazer requisição inútil
            if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
            {
                MessageBox.Show("Informe um livro para pesquisar.");
                return;
            }

            btnPesquisar.Enabled = false; // o botão de pesquisar deve permanecer desativado até que a resposta da requisição retorne.

            try
            {
                LimparTela();

                string url = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(txtPesquisa.Text)}";

                string json = await httpClient.GetStringAsync(url);

                SearchResponse resposta = JsonSerializer.Deserialize<SearchResponse>(json);

                if (resposta?.docs == null || resposta.docs.Count == 0)
                {
                    MessageBox.Show("Nenhum livro encontrado.");
                    return;
                }

                Book livro = resposta.docs.First();

                string descricao = await ObterDescricao(livro.key);

                lblTitulo.Text = $"Título: {livro.title}";

                lblAutor.Text = $"Autor(es): {TextoLista(livro.author_name)}";

                lblEditora.Text = $"Editora: {TextoLista(livro.publisher)}";

                lblAno.Text = $"Primeira publicação: {livro.first_publish_year}";

                lblISBN.Text = $"ISBN: {TextoLista(livro.isbn, 1)}";

                if (livro.cover_i.HasValue)
                {
                    string capa = $"https://covers.openlibrary.org/b/id/{livro.cover_i}-L.jpg";

                    picCapa.LoadAsync(capa);
                }

                txtDescricao.Text = descricao;
            }
            catch (Exception ex) // o erro que pode capturar aqui provavelmente envolve 400 Bad Request, 404 Not Found... etc, caso a requisição falhe.
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnPesquisar.Enabled = true; // independente do resultado da requisição, ao final, o botão de pesquisa deve ser reativado.
            }
        }

        // o método deve ser assíncrono (async) para que não ocorra congelamento de interface ao fazer a requisição pela API
        // "Task" indica uma operação que retorna um valor de certo tipo, basicamente, ao fazer a requisição pela descrição de um livro, isto é uma task que retorna algo como resposta, no caso uma string.
        private async Task<string> ObterDescricao(string workKey)
        {
            try
            {
                string url = $"https://openlibrary.org{workKey}.json";

                string json = await httpClient.GetStringAsync(url);

                using JsonDocument doc = JsonDocument.Parse(json);

                // lembrete: 'out' cria a variável 'desc' em escopo local, é por esta razão que os próximos 'ifs' conseguem acessar ela com o valor que ela tiver no momento, desde que estes 'ifs' estejam no mesmo escopo/hierarquia deste primeiro if.
                if (!doc.RootElement.TryGetProperty("description", out JsonElement desc))
                    return "Descrição não disponível.";

                if (desc.ValueKind == JsonValueKind.String)
                    return desc.GetString();

                if (desc.ValueKind == JsonValueKind.Object &&
                    desc.TryGetProperty("value", out JsonElement value))
                    return value.GetString();

                return "Descrição não disponível.";
            }
            catch
            {
                return "Descrição não disponível.";
            }
        }

        private void LimparTela()
        {
            lblTitulo.Text = "";
            lblAutor.Text = "";
            lblEditora.Text = "";
            lblAno.Text = "";
            lblISBN.Text = "";

            txtDescricao.Clear();

            picCapa.Image = null;
        }

        private string TextoLista(List<string> lista, int max = 3)
        {
            if (lista == null || lista.Count == 0)
                return "Não informado";

            return string.Join(", ", lista.Take(max));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                httpClient.Dispose();

            base.Dispose(disposing);
        }
    }

    public class SearchResponse
    {
        public List<Book> docs { get; set; }
    }

    // os resultados da requisição deverão ser concatenados a uma instância dessa classe aqui, Assim, a interface acessa os valores armazenados num objeto, não diretamente da resposta da requisião.
    public class Book
    {
        public string title { get; set; }

        public List<string> author_name { get; set; }

        public List<string> publisher { get; set; }

        public List<string> isbn { get; set; }

        public int? first_publish_year { get; set; }

        public int? cover_i { get; set; }

        public string key { get; set; }
    }
}