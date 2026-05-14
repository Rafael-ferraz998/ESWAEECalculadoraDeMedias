using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using OOPFoundation;

namespace CalculadoraDeMediasEE
{
    public class FormCalculadora : Form
    {
        // ── Campos de entrada ──────────────────────────────────────────
        private TextBox txtNP1   = new();
        private TextBox txtNP2   = new();
        private TextBox txtPIM   = new();
        private TextBox txtExame = new();

        // ── Rótulos de resultado ───────────────────────────────────────
        private Label lblSemestral = new();
        private Label lblFinal     = new();
        private Label lblStatus    = new();

        // ── Botões ─────────────────────────────────────────────────────
        private Button btnLimparSemestral = new();
        private Button btnSemestral       = new();
        private Button btnLimparFinal     = new();
        private Button btnFinal           = new();

        // ── Validadores ────────────────────────────────────────────────
        private readonly NoteValidation _noteValidator = new();

        public FormCalculadora()
        {
            BuildUI();
            ResetAll();
        }

        // ──────────────────────────────────────────────────────────────
        // Construção da UI
        // ──────────────────────────────────────────────────────────────
        private void BuildUI()
        {
            Text            = "Cálculo de Médias e Status | ESWA+POO";
            Size            = new Size(400, 420);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox     = false;
            StartPosition   = FormStartPosition.CenterScreen;
            BackColor       = Color.White;

            // STATUS
            lblStatus = MakeLabel("Em Andamento", 20, 15, 340, 30, bold: true, fontSize: 12);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.BorderStyle = BorderStyle.FixedSingle;

            // NP1
            Controls.Add(MakeLabel("NP1", 20, 60, 100, 25));
            txtNP1 = MakeTextBox(130, 60, 230);

            // NP2
            Controls.Add(MakeLabel("NP2", 20, 95, 100, 25));
            txtNP2 = MakeTextBox(130, 95, 230);

            // PIM
            Controls.Add(MakeLabel("PIM", 20, 130, 100, 25));
            txtPIM = MakeTextBox(130, 130, 230);

            // Semestral
            Controls.Add(MakeLabel("Semestral", 20, 165, 100, 25, bold: true));
            lblSemestral = MakeLabel("0,0", 130, 165, 230, 25, bold: true);
            lblSemestral.BorderStyle = BorderStyle.FixedSingle;
            lblSemestral.TextAlign   = ContentAlignment.MiddleRight;

            // Botões semestral
            btnLimparSemestral = MakeButton("Limpar",    150, 200, 90);
            btnSemestral       = MakeButton("Semestral", 250, 200, 100);
            btnLimparSemestral.Click += BtnLimparSemestral_Click;
            btnSemestral.Click       += BtnSemestral_Click;

            // Separador visual
            var sep = new Panel { Location = new Point(20, 240), Size = new Size(340, 1), BackColor = Color.LightGray };
            Controls.Add(sep);

            // Exame
            Controls.Add(MakeLabel("Exame", 20, 255, 100, 25));
            txtExame = MakeTextBox(130, 255, 230);

            // Final
            Controls.Add(MakeLabel("Final", 20, 290, 100, 25, bold: true));
            lblFinal = MakeLabel("0,0", 130, 290, 230, 25, bold: true);
            lblFinal.BorderStyle = BorderStyle.FixedSingle;
            lblFinal.TextAlign   = ContentAlignment.MiddleRight;

            // Botões final
            btnLimparFinal = MakeButton("Limpar", 150, 325, 90);
            btnFinal       = MakeButton("Final",  250, 325, 90);
            btnLimparFinal.Click += BtnLimparFinal_Click;
            btnFinal.Click       += BtnFinal_Click;

            // Sanitização: só dígitos e vírgula
            foreach (var tb in new[] { txtNP1, txtNP2, txtPIM, txtExame })
                tb.KeyPress += NotaTextBox_KeyPress;

            Controls.AddRange(new Control[]
            {
                lblStatus,
                txtNP1, txtNP2, txtPIM,
                lblSemestral,
                btnLimparSemestral, btnSemestral,
                txtExame, lblFinal,
                btnLimparFinal, btnFinal
            });
        }

        // ──────────────────────────────────────────────────────────────
        // Helpers de UI
        // ──────────────────────────────────────────────────────────────
        private static Label MakeLabel(string text, int x, int y, int w, int h,
                                       bool bold = false, float fontSize = 9f)
        {
            return new Label
            {
                Text      = text,
                Location  = new Point(x, y),
                Size      = new Size(w, h),
                Font      = new Font("Segoe UI", fontSize, bold ? FontStyle.Bold : FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static TextBox MakeTextBox(int x, int y, int w)
        {
            return new TextBox
            {
                Location  = new Point(x, y),
                Size      = new Size(w, 25),
                TextAlign = HorizontalAlignment.Right,
                Font      = new Font("Segoe UI", 9f)
            };
        }

        private static Button MakeButton(string text, int x, int y, int w)
        {
            return new Button
            {
                Text     = text,
                Location = new Point(x, y),
                Size     = new Size(w, 28),
                Font     = new Font("Segoe UI", 9f)
            };
        }

        // ──────────────────────────────────────────────────────────────
        // Sanitização via KeyPress
        // ──────────────────────────────────────────────────────────────
        private void NotaTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Permite: dígitos, vírgula, backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
                e.Handled = true;

            // Apenas uma vírgula por campo
            if (e.KeyChar == ',' && sender is TextBox tb && tb.Text.Contains(','))
                e.Handled = true;
        }

        // ──────────────────────────────────────────────────────────────
        // Leitura e validação de nota
        // ──────────────────────────────────────────────────────────────
        private bool TryGetNota(TextBox tb, string campo, out double valor)
        {
            valor = 0;
            string texto = tb.Text.Replace(',', '.');
            if (!double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
            {
                MessageBox.Show($"O campo {campo} contém um valor inválido.", "Erro de validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                return false;
            }
            if (!_noteValidator.DoubleIsValid(valor))
            {
                MessageBox.Show($"O campo {campo} deve estar entre 0,0 e 10,0.", "Erro de validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus();
                return false;
            }
            return true;
        }

        // ──────────────────────────────────────────────────────────────
        // Estado inicial / reset completo
        // ──────────────────────────────────────────────────────────────
        private void ResetAll()
        {
            txtNP1.Text   = string.Empty;
            txtNP2.Text   = string.Empty;
            txtPIM.Text   = string.Empty;
            txtExame.Text = string.Empty;

            lblSemestral.Text = "0,0";
            lblFinal.Text     = "0,0";

            SetStatus("Em Andamento", Color.Black);
            SetFinalSectionEnabled(false);
        }

        private void SetFinalSectionEnabled(bool enabled)
        {
            txtExame.Enabled       = enabled;
            btnLimparFinal.Enabled = enabled;
            btnFinal.Enabled       = enabled;
        }

        private void SetStatus(string texto, Color cor)
        {
            lblStatus.Text      = texto;
            lblStatus.ForeColor = cor;
        }

        // ──────────────────────────────────────────────────────────────
        // Eventos dos botões
        // ──────────────────────────────────────────────────────────────

        // Limpar (Semestral) — reseta tudo
        private void BtnLimparSemestral_Click(object? sender, EventArgs e)
        {
            ResetAll();
        }

        // Semestral — calcula MS e define status
        private void BtnSemestral_Click(object? sender, EventArgs e)
        {
            if (!TryGetNota(txtNP1,  "NP1", out double np1))  return;
            if (!TryGetNota(txtNP2,  "NP2", out double np2))  return;
            if (!TryGetNota(txtPIM,  "PIM", out double pim))  return;

            double ms     = GradeCalculator.CalculateSemestralAverage(np1, np2, pim);
            var    status = GradeCalculator.GetSemestralStatus(ms);

            lblSemestral.Text = ms.ToString("0.0", CultureInfo.CurrentCulture);

            if (status == StudentStatus.Aprovado)
            {
                SetStatus("Aprovado", Color.Green);
                SetFinalSectionEnabled(false);
                lblFinal.Text = "0,0";
            }
            else // EmExame
            {
                SetStatus("Em Exame", Color.Orange);
                SetFinalSectionEnabled(true);
            }
        }

        // Limpar (Final) — limpa apenas exame e média final
        private void BtnLimparFinal_Click(object? sender, EventArgs e)
        {
            txtExame.Text = string.Empty;
            lblFinal.Text = "0,0";
            SetStatus(lblStatus.Text, Color.Black); // mantém "Em Exame" mas volta cor preta
        }

        // Final — calcula MF e define status
        private void BtnFinal_Click(object? sender, EventArgs e)
        {
            if (!TryGetNota(txtExame, "Exame", out double exame)) return;

            // Recupera a média semestral já exibida
            string msTexto = lblSemestral.Text.Replace(',', '.');
            if (!double.TryParse(msTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out double ms))
            {
                MessageBox.Show("Calcule primeiro a Média Semestral.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double mf     = GradeCalculator.CalculateFinalAverage(ms, exame);
            var    status = GradeCalculator.GetFinalStatus(mf);

            lblFinal.Text = mf.ToString("0.0", CultureInfo.CurrentCulture);

            if (status == StudentStatus.Aprovado)
                SetStatus("Aprovado", Color.Green);
            else
                SetStatus("Reprovado", Color.Red);
        }
    }
}
