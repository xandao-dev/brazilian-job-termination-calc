using Calculos_Trabalhistas.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Calculos_Trabalhistas
{
    public partial class PrincipalForm : Form
    {

        public PrincipalForm()
        {
            InitializeComponent();
            this.Size = new Size(637, 547);


        }

        #region VARIAVEIS
        private double SalárioMínimo = Convert.ToDouble(Settings.Default["SalárioMínimo"]); //Valor do salario minimo de 2016
        private double FGTS = (Convert.ToDouble(Settings.Default["FGTS"])) * 0.01; //Taxa do FGTS mensal
        private double FGTS_SJC = (Convert.ToDouble(Settings.Default["FGTS_SJC"])) * 0.01; //Taxa extra do FGTS por Demissao sem Justa Causa
        private double HoraNoturna = (Convert.ToDouble(Settings.Default["HoraNoturna"])) * 0.01; //Taxa adicional da hora noturna(sobre o valor da hora normal)

        private double DoFeBOXValue = 0;
        private double SabBOXValue = 0;
        private double SegSexBOXValue = 0;

        private double XDoFeBOXValue = 0;
        private double XSabBOXValue = 0;
        private double XSegSexBOXValue = 0;

        /*double SegBOXValue = 0;
        double TerBOXValue = 0;
        double QuaBOXValue = 0;
        double QuiBOXValue = 0;
        double SexBOXValue = 0;*/

        private double Meses = 0;
        private double Anos = 0;
        private double Dias = 0;
        private double HET_Segunda_Sabado = 0;
        private double HET_Domingo_Feriado = 0;
        private double ValorTotalHE = 0;
        private double ValorHEMes = 0;
        private double ValorHNMes = 0;
        private double ValorTotalHN = 0;

        private double Remuneração = 0;
        private double SalarioBase = 0;
        private double ValorInsalubridade = 0;
        private double ValorPericulosidade = 0;
        private double ValorMulta477 = 0;
        private double Comissoes = 0;
        private double ValorFeriasProporcionais = 0;
        private double ValorFeriasAtrasadas = 0;
        private double ValorSaldoSalario = 0;
        private double ValorFGTS = 0;
        private double ValorAvisoPrevio = 0;
        private double ValorTotal = 0;
        private double ValorSeguroDesemprego = 0;
        private double Valor13Proporcional = 0;
        private double ValorProDed = 0;
        //private double ValorDSR = 0;
        #endregion

        //Botoes em Geral
        private void button1_Click(object sender, EventArgs e)
        {

            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;

           
            #region Meses ou Dias Trab
            if (choice.Text == "Meses Trabalhados")
            {
                label30.Text = Convert.ToString(Math.Round((Anos * 12 + Meses + Dias / 30),2)); // mostra os meses nos calculos
                label29.Text = "Meses Trabalhados:";
            }
            else if (choice.Text == "Dias Trabalhados")
            {
                double ssd = ((Anos * 360 + Meses * 30 + Dias)*5/ 7);
                double sad = ((Anos * 360 + Meses * 30 + Dias) / 7);
                label30.Text = "Segunda a Sexta: " +Convert.ToString(Math.Round(ssd)) + "e Sábado: " + Convert.ToString(Math.Round(sad)); // mostra os meses nos calculos
                label29.Text = "Dias Trabalhados:";
            }
            #endregion

            VerificarBotao();

            Insalubridade();
            Periculosidade();
            ValorHoraExtra();
            ValorHoraNoturna();
            RemuneraçaoX();

            //
            ProDed();
            SaldoSalario();
            FeriasProporcionais();
            FeriasAtrasadas();
            CalcFGTS();
            Multa477();
            AvisoPrevio();
            SeguroDesemprego();
            _13Proporcional();
            VALORTOTAL();

            #region Controle do tamanho da tela
            if (HEButton.Checked == false && PDButton.Checked == false && InsalubButton.Checked == false && ComissButton.Checked == false && FAtraButton.Checked == false && SDesemButton.Checked == false && PDButton.Checked == false)
            {
                #region Labels ProDed
                if (PDButton.Checked == true)
                {
                    if (label38.Text == "+")
                    {
                        label81.Text = textBox7.Text + ":" + " R$ " + textBox11.Text;
                    }
                    else if (label38.Text == "-")
                    {
                        label81.Text = textBox7.Text + ":" + " R$ -" + textBox11.Text;
                    }

                    if (label43.Text == "+")
                    {
                        label80.Text = textBox9.Text + ":" + " R$ " + textBox8.Text;
                    }
                    else if (label43.Text == "-")
                    {
                        label80.Text = textBox9.Text + ":" + " R$ -" + textBox8.Text;
                    }

                    if (label42.Text == "+")
                    {
                        label77.Text = textBox10.Text + ":" + " R$ " + textBox12.Text;
                    }
                    else if (label42.Text == "-")
                    {
                        label77.Text = textBox10.Text + ":" + " R$ -" + textBox12.Text;
                    }
                }
                else
                {
                    label81.Text = "";
                    label80.Text = "";
                    label77.Text = "";
                }
                #endregion
                this.Size = new Size(640, 716);
                tabControl1.SelectTab(Tabela);
            }
            else
            {
                if (checkBox3.Checked == false)
                {
                    if (HEButton.Checked == true)
                    {
                        this.Size = new Size(637, 622);
                        comboBox3.Location = new Point(366, 180);
                        label31.Location = new Point(35, 183);
                        HEOP.Size = new Size(597, 213);
                        tableLayoutPanel4.Location = new Point(37, 77);
                        tableLayoutPanel2.Visible = false;
                        tableLayoutPanel3.Visible = true;
                        button3.Location = new Point(243, 513);
                        groupBox8.Location = new Point(14, 372);
                        groupBox5.Location = new Point(14, 313);
                        groupBox7.Location = new Point(243, 313);
                        groupBox3.Location = new Point(14, 251);
                        groupBox4.Location = new Point(481, 251);
                        tabControl1.SelectTab(Calculos);
                    }
                    else
                    {
                        this.Size = new Size(637, 404);
                        button3.Location = new Point(243, 295);
                        groupBox8.Location = new Point(14, 152);
                        groupBox5.Location = new Point(14, 93);
                        groupBox7.Location = new Point(243, 93);
                        groupBox3.Location = new Point(14, 31);
                        groupBox4.Location = new Point(481, 31);
                        tabControl1.SelectTab(Calculos);
                    }
                }

                else
                {
                    if (HEButton.Checked == true)
                    {
                        this.Size = new Size(637, 740);
                        comboBox3.Location = new Point(358, 301);
                        label31.Location = new Point(33, 304);
                        HEOP.Size = new Size(597, 336);
                        tableLayoutPanel4.Location = new Point(37, 196);
                        tableLayoutPanel3.Visible = false;
                        tableLayoutPanel2.Location = new Point(37, 48);
                        tableLayoutPanel2.Visible = true;
                        button3.Location = new Point(242, 637);
                        groupBox8.Location = new Point(13, 494);
                        groupBox5.Location = new Point(13, 435);
                        groupBox7.Location = new Point(242, 435);
                        groupBox3.Location = new Point(13, 373);
                        groupBox4.Location = new Point(480, 373);
                        tabControl1.SelectTab(Calculos);
                    }
                    else
                    {
                        this.Size = new Size(637, 404);
                        button3.Location = new Point(243, 295);
                        groupBox8.Location = new Point(14, 152);
                        groupBox5.Location = new Point(14, 93);
                        groupBox7.Location = new Point(243, 93);
                        groupBox3.Location = new Point(14, 31);
                        groupBox4.Location = new Point(481, 31);
                        tabControl1.SelectTab(Calculos);
                    }
                }
            }
            #endregion

            #region nome
            if (checkBox1.Checked == false)
            {
                label24.Visible = false;
                label25.Visible = false;
                label26.Visible = false;
                label66.Visible = false;
                label67.Visible = false;
                label68.Visible = false;
                progressBar4.Visible = false;
                progressBar5.Visible = false;
                progressBar8.Visible = false;
            }
            else
            {
                if(textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
                {
                    label24.Visible = false;
                    label25.Visible = false;
                    label26.Visible = false;
                    label66.Visible = false;
                    label67.Visible = false;
                    label68.Visible = false;
                    progressBar4.Visible = false;
                    progressBar5.Visible = false;
                    progressBar8.Visible = false;
                }
                else
                {
                    label24.Visible = true;
                    label25.Visible = true;
                    label26.Visible = true;
                    label66.Visible = true;
                    label67.Visible = true;
                    label68.Visible = true;
                    progressBar4.Visible = true;
                    progressBar5.Visible = true;
                    progressBar8.Visible = true;
                }
            }

#endregion

            label100.Text = Convert.ToString(Math.Round((Anos * 12 + Meses + Dias / 30),2));

        }//botao "proximo" do principal

        private void button3_Click(object sender, EventArgs e)
        {
            #region Controle do tamanho da tela
            this.Size = new Size(640, 716);
            tabControl1.SelectTab(Tabela);
            #endregion

            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;

            VerificarBotao();

            ValorHoraExtra();
            ValorHoraNoturna();
            Insalubridade();
            Periculosidade();
            RemuneraçaoX();
            //
            ProDed();
            SaldoSalario();
            FeriasProporcionais();
            FeriasAtrasadas();
            CalcFGTS();
            Multa477();
            AvisoPrevio();
            SeguroDesemprego();
            _13Proporcional();
            VALORTOTAL();
            //DSR();

            label100.Text = Convert.ToString(Math.Round((Anos * 12 + Meses + Dias / 30),2));
            
            
            #region Labels ProDed
            if (PDButton.Checked == true)
            {
                if(textBox7.Text == "" && textBox8.Text == "")
                {
                    label81.Visible = false;
                }
                else
                {
                    label81.Visible = true;
                }

                if (textBox9.Text == "" && textBox11.Text == "")
                {
                    label80.Visible = false;
                }
                else
                {
                    label80.Visible = true;
                }

                if (textBox10.Text == "" && textBox12.Text == "")
                {
                    label77.Visible = false;
                }
                else
                {
                    label77.Visible = true;
                }

                if (label38.Text == "+")
                {
                    label81.Text = textBox7.Text + ":" + " R$ " + textBox8.Text;
                }
                else if (label38.Text == "-")
                {
                    label81.Text = textBox7.Text + ":" + " R$ -" + textBox8.Text;
                }

                if (label43.Text == "+")
                {
                    label80.Text = textBox9.Text + ":"+ " R$ " + textBox11.Text;
                }    
                else if (label43.Text == "-")
                {
                    label80.Text = textBox9.Text + ":" + " R$ -" + textBox11.Text;
                }

                if (label42.Text == "+")
                {
                    label77.Text = textBox10.Text + ":" + " R$ " + textBox12.Text;
                }
                else if (label42.Text == "-")
                {
                    label77.Text = textBox10.Text + ":" + " R$ -" + textBox12.Text;
                }
            }
            else
            {
                label81.Text = "";
                label80.Text = "";
                label77.Text = "";
            }
            #endregion

        }//botao proximo dos calculos

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.Size = new Size(637, 547);
                botton1.Location = new Point(233, 438);
                DireitoClient.Location = new Point(13, 292);
                Requisitos.Location = new Point(13, 138);
            }
            else
            {
                Requisitos.Location = new Point(13, 49);
                DireitoClient.Location = new Point(13, 203);
                botton1.Location = new Point(233, 349);
                this.Size = new Size(637, 455);
            }

            tabControl1.SelectTab(Principal);
        }//botao "voltar" dos calculos para principal

        private void Limpar_Click(object sender, EventArgs e)
        {
            ResetarForm.ResetAllControls(ClientDetail);
            ResetarForm.ResetAllControls(DireitoClient);
            Button13Prop.Enabled = true;
            Button13Prop.Checked = false;
            FPropButton.Checked = false;
            FPropButton.Enabled = true;
            HEButton.Checked = false;
            InsalubButton.Checked = false;
            PericuButton.Checked = false;
            ComissButton.Checked = false;
            FAtraButton.Checked = false;
            ButtonSS.Checked = false;
            SDesemButton.Checked = false;
            Multa477Button.Checked = false;
            PDButton.Checked = false;
            FGTSButton.Checked = false;
            label81.Text = "";
            label80.Text = "";
            label77.Text = "";
            qutHeDFLabel.Text = "0";
            qutHeSSLabel.Text = "0";
            SBBOX.Text = "0"; comboBox1.Text = "Pedido de Demissão"; comboBox2.Text = "NÃO"; comboBox2.Enabled = true;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("NÃO");
            comboBox2.Items.Add("Trabalhado");
            comboBox2.Items.Add("Indenizado Pelo Empregado");
            S1.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S2.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S3.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);  S5.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S6.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S7.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S8.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S9.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);
            E1.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E2.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E3.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E5.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E6.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E7.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E8.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E9.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);
            I1.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I2.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I3.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);  I5.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I6.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I7.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I8.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I9.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);
            DoFeBOX.Text = "0"; comboBox3.Text = "50%"; comboBox5.Text = "10%"; numericUpDown1.Value = 16; comboBox4.Text = "Primeira"; numericUpDown2.Value = 1; textBox6.Text = "0"; textBox7.Text = ""; textBox9.Text = ""; textBox10.Text = ""; comboBox6.Text = "Provento"; comboBox7.Text = "Provento"; comboBox8.Text = "Dedução"; textBox8.Text = "0"; textBox11.Text = "0"; textBox12.Text = "0";
        }//botao que reseta o form

        private void Opçoes_Click(object sender, EventArgs e)
        {
            this.Size = new Size(311, 417);
            tabControl1.SelectTab(OpçoesTab);
        }//botao q vai do principal para as opçoes

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.Size = new Size(637, 547);
                botton1.Location = new Point(233, 438);
                DireitoClient.Location = new Point(13, 292);
                Requisitos.Location = new Point(13, 138);
            }
            else
            {
                Requisitos.Location = new Point(13, 49);
                DireitoClient.Location = new Point(13, 203);
                botton1.Location = new Point(233, 349);
                this.Size = new Size(637, 455);
            }

            tabControl1.SelectTab(Principal);
        }//botao nas opçoes que volta para a aba Principal

        private void button1_Click_1(object sender, EventArgs e)
        {
            Settings.Default["SalárioMínimo"] = vsalario.Text;
            Settings.Default["FGTS"] = vFGTS.Text;
            Settings.Default["FGTS_SJC"] = vFGTSSJC.Text;
            Settings.Default["HoraNoturna"] = vAHN.Text;
            Settings.Default["Detalhes"] = checkBox1.Checked;
            Settings.Default["HN"] = checkBox2.Checked;
            Settings.Default.Save();
            MessageBox.Show("Configurações Salvas!");
        }//atualizar dados opçao

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ClientDetail.Visible = true;
            }
            else
            {
                ClientDetail.Visible = false;
            }
        }//botao de opção detalhes do cliente

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked == false)
            {
                if (HEButton.Checked == true)
                {
                    this.Size = new Size(637, 622);
                    comboBox3.Location = new Point(366, 180);
                    label31.Location = new Point(35, 183);
                    HEOP.Size = new Size(597, 213);
                    tableLayoutPanel4.Location = new Point(37, 77);
                    tableLayoutPanel2.Visible = false;
                    tableLayoutPanel3.Visible = true;
                    button3.Location = new Point(243, 513);
                    groupBox8.Location = new Point(14, 372);
                    groupBox5.Location = new Point(14, 313);
                    groupBox7.Location = new Point(243, 313);
                    groupBox3.Location = new Point(14, 251);
                    groupBox4.Location = new Point(481, 251);
                }
                else
                {
                    this.Size = new Size(637, 404);
                    button3.Location = new Point(243, 295);
                    groupBox8.Location = new Point(14, 152);
                    groupBox5.Location = new Point(14, 93);
                    groupBox7.Location = new Point(243, 93);
                    groupBox3.Location = new Point(14, 31);
                    groupBox4.Location = new Point(481, 31);
                }
            }

            else
            {
                if (HEButton.Checked == true)
                {
                    this.Size = new Size(637, 740);
                    comboBox3.Location = new Point(358, 301);
                    label31.Location = new Point(33, 304);
                    HEOP.Size = new Size(597, 336);
                    tableLayoutPanel4.Location = new Point(37, 196);
                    tableLayoutPanel3.Visible = false;
                    tableLayoutPanel2.Location = new Point(37, 48);
                    tableLayoutPanel2.Visible = true;
                    button3.Location = new Point(242, 637);
                    groupBox8.Location = new Point(13, 494);
                    groupBox5.Location = new Point(13, 435);
                    groupBox7.Location = new Point(242, 435);
                    groupBox3.Location = new Point(13, 373);
                    groupBox4.Location = new Point(480, 373);
                }
                else
                {
                    this.Size = new Size(637, 404);
                    button3.Location = new Point(243, 295);
                    groupBox8.Location = new Point(14, 152);
                    groupBox5.Location = new Point(14, 93);
                    groupBox7.Location = new Point(243, 93);
                    groupBox3.Location = new Point(14, 31);
                    groupBox4.Location = new Point(481, 31);
                }
            }
        }//alternador de segunda a sexta

        private void button4_Click(object sender, EventArgs e)
        {
            label81.Text = "";
            label80.Text = "";
            label77.Text = "";
            qutHeDFLabel.Text = "0";
            qutHeSSLabel.Text = "0";
            SegSexBOX.Text = "";SabBOX.Text = "";DoFeBOX.Text = ""; SegBOX.Text = ""; TerBOX.Text = ""; QuaBOX.Text = ""; QuiBOX.Text = ""; SexBOX.Text = "";
            checkBox3.Checked = false;
            S1.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S2.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S3.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);  S5.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S6.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S7.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S8.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); S9.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);
            E1.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E2.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E3.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);  E5.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E6.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E7.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E8.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); E9.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);
            I1.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I2.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I3.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);  I5.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I6.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I7.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I8.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0); I9.Value = new DateTime(2016, 10, 10, 0, 0, 0, 0);
            comboBox3.Text = "50%"; comboBox5.Text = "10%"; numericUpDown1.Value = 16; comboBox4.Text = "Primeira"; numericUpDown2.Value = 1; textBox6.Text = "0"; textBox7.Text = ""; textBox9.Text = ""; textBox10.Text = ""; comboBox6.Text = "Provento"; comboBox7.Text = "Provento"; comboBox8.Text = "Dedução"; textBox8.Text = "0"; textBox11.Text = "0"; textBox12.Text = "0";
        }//botao limpar dos calculos

        private void Imprimir_Click_1(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }//botao imprimir

        private void button5_Click_1(object sender, EventArgs e)
        {
            label81.Text = "";
            label80.Text = "";
            label77.Text = "";
            qutHeDFLabel.Text = "0";
            qutHeSSLabel.Text = "0";

            if (HEButton.Checked == false && PDButton.Checked == false && InsalubButton.Checked == false && ComissButton.Checked == false && FAtraButton.Checked == false && SDesemButton.Checked == false)
            {
                if (checkBox1.Checked == true)
                    this.Size = new Size(637, 547);
                else
                    this.Size = new Size(637, 455);

                tabControl1.SelectTab(Principal);
            }
            else
            {
                if (checkBox3.Checked == false)
                {
                    if (HEButton.Checked == true)
                    {
                        this.Size = new Size(637, 622);
                        comboBox3.Location = new Point(366, 180);
                        label31.Location = new Point(35, 183);
                        HEOP.Size = new Size(597, 213);
                        tableLayoutPanel4.Location = new Point(37, 77);
                        tableLayoutPanel2.Visible = false;
                        tableLayoutPanel3.Visible = true;
                        button3.Location = new Point(243, 513);
                        groupBox8.Location = new Point(14, 372);
                        groupBox5.Location = new Point(14, 313);
                        groupBox7.Location = new Point(243, 313);
                        groupBox3.Location = new Point(14, 251);
                        groupBox4.Location = new Point(481, 251);
                        tabControl1.SelectTab(Calculos);
                    }
                    else
                    {
                        this.Size = new Size(637, 404);
                        button3.Location = new Point(243, 295);
                        groupBox8.Location = new Point(14, 152);
                        groupBox5.Location = new Point(14, 93);
                        groupBox7.Location = new Point(243, 93);
                        groupBox3.Location = new Point(14, 31);
                        groupBox4.Location = new Point(481, 31);
                        tabControl1.SelectTab(Calculos);
                    }
                }

                else
                {
                    if (HEButton.Checked == true)
                    {
                        this.Size = new Size(637, 740);
                        comboBox3.Location = new Point(358, 301);
                        label31.Location = new Point(33, 304);
                        HEOP.Size = new Size(597, 336);
                        tableLayoutPanel4.Location = new Point(37, 196);
                        tableLayoutPanel3.Visible = false;
                        tableLayoutPanel2.Location = new Point(37, 48);
                        tableLayoutPanel2.Visible = true;
                        button3.Location = new Point(242, 637);
                        groupBox8.Location = new Point(13, 494);
                        groupBox5.Location = new Point(13, 435);
                        groupBox7.Location = new Point(242, 435);
                        groupBox3.Location = new Point(13, 373);
                        groupBox4.Location = new Point(480, 373);
                        tabControl1.SelectTab(Calculos);
                    }
                    else
                    {
                        this.Size = new Size(637, 404);
                        button3.Location = new Point(243, 295);
                        groupBox8.Location = new Point(14, 152);
                        groupBox5.Location = new Point(14, 93);
                        groupBox7.Location = new Point(243, 93);
                        groupBox3.Location = new Point(14, 31);
                        groupBox4.Location = new Point(481, 31);
                        tabControl1.SelectTab(Calculos);
                    }
                }
            }
        }//botao voltar da Tabela

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(SBBOX.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                SBBOX.Text = "0";
            }
            
        }//textbox do salario base

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region Direitos dos Clientes
        private void HEButton_CheckedChanged(object sender, EventArgs e)
        {
            if (HEButton.Checked == true)
            {
                HEButton.BackColor = Color.Lime;
                HEOP.Enabled = true;
                HEOP.Visible = true;
            }
            else
            {
                HEButton.BackColor = Color.Salmon;
                HEOP.Enabled = false;
                HEOP.Visible = false;
            }
        }

        private void InsalubButton_CheckedChanged(object sender, EventArgs e)
        {

            if (InsalubButton.Checked == true)
            {
               
                groupBox4.Enabled = true;
                InsalubButton.BackColor = Color.Lime;
            }
            else
            {
                
                groupBox4.Enabled = false;
                InsalubButton.BackColor = Color.Salmon;
            }
        }

        private void PericuButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PericuButton.Checked == true)
            {
                
                PericuButton.BackColor = Color.Lime;
            }
            else
            {
                
                PericuButton.BackColor = Color.Salmon;
            }
        }

        private void ComissButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ComissButton.Checked == true)
            {
                groupBox7.Enabled = true;
                ComissButton.BackColor = Color.Lime;
            }
            else
            {
                groupBox7.Enabled = false;
                ComissButton.BackColor = Color.Salmon;
            }
        }

        private void FPropButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FPropButton.Checked == true)
                FPropButton.BackColor = Color.Lime;
            else
                FPropButton.BackColor = Color.Salmon;
        }

        private void FAtraButton_CheckedChanged(object sender, EventArgs e)
        {
          

            if (FAtraButton.Checked == true)
            {
                groupBox5.Enabled = true;
                FAtraButton.BackColor = Color.Lime;
            }
            else
            {
                groupBox5.Enabled = false;
                FAtraButton.BackColor = Color.Salmon;
            }
        }

        private void Button13Prop_CheckedChanged(object sender, EventArgs e)
        {
            if (Button13Prop.Checked == true)
                Button13Prop.BackColor = Color.Lime;
            else
                Button13Prop.BackColor = Color.Salmon;
        }

        private void ButtonSS_CheckedChanged(object sender, EventArgs e)
        {
            if (ButtonSS.Checked == true)
            {
                
                ButtonSS.BackColor = Color.Lime;
            }
            else
            {
                
                ButtonSS.BackColor = Color.Salmon;
            }
        }

        private void SDesemButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SDesemButton.Checked == true)
            {
                groupBox3.Enabled = true;
                SDesemButton.BackColor = Color.Lime;
            }
            else
            {
                groupBox3.Enabled = false;
                SDesemButton.BackColor = Color.Salmon;
            }
        }

        private void Multa477Button_CheckedChanged(object sender, EventArgs e)
        {
            if (Multa477Button.Checked == true)
                Multa477Button.BackColor = Color.Lime;
            else
                Multa477Button.BackColor = Color.Salmon;
        }

        private void FGTSButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FGTSButton.Checked == true)
                FGTSButton.BackColor = Color.Lime;
            else
                FGTSButton.BackColor = Color.Salmon;
        }

        private void PDButton_CheckedChanged(object sender, EventArgs e)
        {
            if (PDButton.Checked == true)
            {
                groupBox8.Enabled = true;
                PDButton.BackColor = Color.Lime;
            }
            else
            {
                groupBox8.Enabled = false;
                PDButton.BackColor = Color.Salmon;
            }
        }


        #endregion
        private void button7_Click(object sender, EventArgs e)
        {
            this.Size = new Size(311, 417);
            tabControl1.SelectTab(OpçoesTab);
        }//botao q da base dos calculso q volta para opçoes

        private void button6_Click(object sender, EventArgs e)
        {
            var CalcXXX = Path.Combine(Path.GetTempPath(), "Calculos.txt");
            File.WriteAllText(CalcXXX, Calculos_Trabalhistas.Properties.Resources.Calculos);
            Process.Start(CalcXXX);

            //var local = Calculos_Trabalhistas.Properties.Resources.Calculos;
            //Process.Start("notepad.exe" , local);
            //C:\Users\System32\Desktop\Alexandre\Programaçao\C#\Projetos\Calculos Trabalhistas\Calculos Trabalhistas
            //this.Size = new Size(600, 600);
            //tabControl1.SelectTab(BaseCalculos);
        }//botao q vai para base dos calculos

        private void vsalario_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(vsalario.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                vsalario.Text = Convert.ToString(Settings.Default["SalárioMínimo"]);
            }
        }//textbox do salario nas opçoes

        private void PrincipalForm_Load(object sender, EventArgs e)
        {


            vFGTS.Text = Convert.ToString(Settings.Default["FGTS"]);
            vFGTSSJC.Text = Convert.ToString(Settings.Default["FGTS_SJC"]);
            vAHN.Text = Convert.ToString(Settings.Default["HoraNoturna"]);
            vsalario.Text = Convert.ToString(Settings.Default["SalárioMínimo"]);
            checkBox1.Checked = Convert.ToBoolean(Settings.Default["Detalhes"]);
            checkBox2.Checked = Convert.ToBoolean(Settings.Default["HN"]);

            if (checkBox1.Checked == true)
            {
                this.Size = new Size(637, 547);
                botton1.Location = new Point(233, 438);
                DireitoClient.Location = new Point(13, 292);
                Requisitos.Location = new Point(13, 138);
            }
            else
            {
                Requisitos.Location = new Point(13, 49);
                DireitoClient.Location = new Point(13, 203);
                botton1.Location = new Point(233, 349);
                this.Size = new Size(637, 455);
            }

            VerificarBotao();

        }//CONFIGURAÇOES INICIAIS QUANDO ABRE O FORM

        private void OpçoesTab_Click(object sender, EventArgs e)
        {

        }

        private void vFGTS_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(vFGTS.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                vFGTS.Text = Convert.ToString(Settings.Default["FGTS"]);
            }
        }//textbox do fgts nas opçoes

        private void vFGTSSJC_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(vFGTSSJC.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                vFGTSSJC.Text = Convert.ToString(Settings.Default["FGTS_SJC"]);
            }
        }//textbox do fgts sjc nas opçoes

        private void vAHN_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(vAHN.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                vAHN.Text = Convert.ToString(Settings.Default["HoraNoturna"]);
            }
        }//textbox do adc noturno nas ocçoes

        private void choice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;

            #region Meses ou Dias Trab
            if (choice.Text == "Meses Trabalhados")
            {
                label30.Text = Convert.ToString(Math.Round((Anos * 12 + Meses + Dias / 30),2)); // mostra os meses nos calculos
                label29.Text = "Meses Trabalhados:";
            }
            else if (choice.Text == "Dias Trabalhados")
            {
                double ssd = ((Anos * 360 + Meses * 30 + Dias) * 5 / 7);
                double sad = ((Anos * 360 + Meses * 30 + Dias) / 7);
                label30.Text = "Segunda a Sexta: " + Convert.ToString(Math.Round(ssd)) + " e Sábado: " + Convert.ToString(Math.Round(sad)); // mostra os meses nos calculos
                label29.Text = "Dias Trabalhados:";
            }
            #endregion
        } //escolha entre meses ou dias

        private void dataAdm_ValueChanged(object sender, EventArgs e)
        {
            VerificarBotao();
        }

        private void dataAfast_ValueChanged(object sender, EventArgs e)
        {
            VerificarBotao();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificarBotao();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

       

        #region Nome e telefone
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label66.Text = textBox1.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label67.Text = textBox3.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label68.Text = textBox2.Text;
        }
        #endregion

        private void ValorHoraExtra()
        {
            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;


            double AdcHE = 0;
            if (comboBox3.Text == "50%")
                AdcHE = 0.5;
            else if (comboBox3.Text == "55%")
                AdcHE = 0.55;
            else if (comboBox3.Text == "60%")
                AdcHE = 0.6;
            else if (comboBox3.Text == "65%")
                AdcHE = 0.65;
            else if (comboBox3.Text == "70%")
                AdcHE = 0.7;
            else if (comboBox3.Text == "75%")
                AdcHE = 0.75;
            else if (comboBox3.Text == "80%")
                AdcHE = 0.8;
            else if (comboBox3.Text == "85%")
                AdcHE = 0.85;
            else if (comboBox3.Text == "90%")
                AdcHE = 0.9;
            else if (comboBox3.Text == "95%")
                AdcHE = 0.95;
            else if (comboBox3.Text == "100%")
            { AdcHE = 1; }

            if (SBBOX.Text == "")
            {
                SalarioBase = 0;
            }
            else
            {
                SalarioBase = Convert.ToDouble(SBBOX.Text);
            }

            #region Valores SegSex

            double SaidaSegSex = 0;
            double EntradaSegSex = 0;

            if (S1.Value.Hour == 0 && E1.Value.Hour != 0)
            {
                SaidaSegSex = 24;
            }
            else
            {
                SaidaSegSex = S1.Value.Hour;
            }

            if (E1.Value.Hour == 0 && S1.Value.Hour != 0)
            {
                EntradaSegSex = 24;
            }
            else
            {
                EntradaSegSex = E1.Value.Hour;
            }

            double SegSexEMIN = E1.Value.Minute;
            double SegSexIMIN = I1.Value.Minute;
            double SegSexSMIN = S1.Value.Minute;
            double SegSexE = EntradaSegSex + SegSexEMIN / 60;
            double SegSexI = I1.Value.Hour + SegSexIMIN / 60;
            double SegSexS = SaidaSegSex + SegSexSMIN / 60;
            double SegSexT = SegSexS - SegSexE;
            double SegSexSomaCalculo = 0;

            if (checkBox3.Checked == false)
            {


                double SegSexCALCULO = 0;
                if (SegSexT > 0 && SegSexT < 8)
                {
                    if (SegSexS > 22)
                    {
                        SegSexCALCULO = (SegSexT - (SegSexS - 22) + (SegSexS - 22) * ((double)8 / (double)7)) - SegSexI;
                    }
                    else
                    {
                        SegSexCALCULO = 0;
                    }
                }
                else if (SegSexT >= 8)
                {
                    if (SegSexS > 22)
                    {
                        SegSexCALCULO = (SegSexT - (SegSexS - 22) + (SegSexS - 22) * ((double)8 / (double)7)) - SegSexI;
                    }
                    else if (SegSexS <= 22)
                    {
                        SegSexCALCULO = SegSexT - SegSexI;
                    }
                    else
                    {
                        SegSexCALCULO = 0;
                    }
                }
                else if (SegSexT >= -17 && SegSexT < 0)
                {

                    if (SegSexE >= 22 && SegSexS >= 5)
                    {
                        SegSexCALCULO = (((double)32 / (double)7) + (SegSexS - 5) + (25 - SegSexE) * ((double)8 / (double)7)) - SegSexI;
                    }
                    else if (SegSexE < 22 && SegSexS > 5)
                    {
                        SegSexCALCULO = (8 + (SegSexS - 5) + (22 - SegSexE)) - SegSexI;
                    }
                    else if (SegSexE < 22 && SegSexS <= 5)
                    {
                        SegSexCALCULO = ((22 - SegSexE) + SegSexS * ((double)8 / (double)7) + ((double)16 / (double)7)) - SegSexI;
                    }
                    else
                    {
                        SegSexCALCULO = 0;
                    }
                }
                else
                {
                    SegSexCALCULO = 0;
                }

                if(E2.Value.Hour == 0 && S2.Value.Hour == 0)//verifica se sabado está ou n ativo
                {
                    if(SegSexCALCULO > 8.8)
                    {
                        SegSexSomaCalculo = SegSexCALCULO - 8.8;
                    }
                    else
                    {
                        SegSexSomaCalculo = 0;
                    }
                }
                else
                {
                    if(SegSexCALCULO > 8)
                    {
                        SegSexSomaCalculo = SegSexCALCULO - 8;
                    }
                    else
                    {
                        SegSexSomaCalculo = 0;
                    }
                }

            }
            else
            {
                SegSexSomaCalculo = 0;
            }


            if (SegSexBOX.Text == "")
            {
                SegSexBOXValue = 0;
            }
            else
            {
                if (choice.Text == "Meses Trabalhados")
                {
                    SegSexBOXValue = Convert.ToDouble(SegSexBOX.Text);
                }
                else if (choice.Text == "Dias Trabalhados")
                {
                    SegSexBOXValue = Convert.ToDouble(SegSexBOX.Text)*((double)7/(double)150);
                }
            }

            #endregion

            #region Valores Sab
            double SaidaSab = 0;
            double EntradaSab = 0;

            if (S2.Value.Hour == 0 && E2.Value.Hour != 0)
            {
                SaidaSab = 24;
            }
            else
            {
                SaidaSab = S2.Value.Hour;
            }

            if (E2.Value.Hour == 0 && S2.Value.Hour != 0)
            {
                EntradaSab = 24;
            }
            else
            {
                EntradaSab = E2.Value.Hour;
            }

            double SabEMIN = E2.Value.Minute;
            double SabIMIN = I2.Value.Minute;
            double SabSMIN = S2.Value.Minute;
            double SabE = EntradaSab + SabEMIN / 60;
            double SabI = I2.Value.Hour + SabIMIN / 60;
            double SabS = SaidaSab + SabSMIN / 60;
            double SabT = SabS - SabE;
            double SabSomaCalculo = 0;


                double SabCALCULO = 0;
                if (SabT > 0 && SabT < 8)
                {
                    if (SabS > 22)
                    {
                        SabCALCULO = (SabT - (SabS - 22) + (SabS - 22) * ((double)8 / (double)7)) - SabI;
                    }
                    else
                    {
                        SabCALCULO = SabT - SabI;
                    }
                }
                else if (SabT >= 8)
                {
                    if (SabS > 22)
                    {
                        SabCALCULO = (SabT - (SabS - 22) + (SabS - 22) * ((double)8 / (double)7)) - SabI;
                    }
                    else if (SabS <= 22)
                    {
                        SabCALCULO = SabT - SabI;
                    }
                    else
                    {
                        SabCALCULO = 0;
                    }
                }
                else if (SabT >= -17 && SabT < 0)
                {

                    if (SabE >= 22 && SabS >= 5)
                    {
                        SabCALCULO = (((double)32 / (double)7) + (SabS - 5) + (25 - SabE) * ((double)8 / (double)7)) - SabI;
                    }
                    else if (SabE < 22 && SabS > 5)
                    {
                        SabCALCULO = (8 + (SabS - 5) + (22 - SabE)) - SabI;
                    }
                    else if (SabE < 22 && SabS <= 5)
                    {
                        SabCALCULO = ((22 - SabE) + SabS * ((double)8 / (double)7) + ((double)16 / (double)7)) - SabI;
                    }
                    else
                    {
                        SabCALCULO = 0;
                    }
                }
                else
                {
                    SabCALCULO = 0;
                }

                if (SabCALCULO > 4)
                {
                    SabSomaCalculo = SabCALCULO - 4;
                }
                else
                {
                    SabSomaCalculo = 0;
                }


            if (SabBOX.Text == "")
            {
                SabBOXValue = 0;
            }
            else
            {
                if (choice.Text == "Meses Trabalhados")
                {
                    SabBOXValue = Convert.ToDouble(SabBOX.Text);
                }
                else if (choice.Text == "Dias Trabalhados")
                {
                    SabBOXValue = Convert.ToDouble(SabBOX.Text)*((double)7/(double)30);
                }
            }

            #endregion

            #region Valores DoFe

            double SaidaDoFe = 0;
            double EntradaDoFe = 0;

            if (S3.Value.Hour == 0 && E3.Value.Hour != 0)
            {
                SaidaDoFe = 24;
            }
            else
            {
                SaidaDoFe = S3.Value.Hour;
            }

            if (E3.Value.Hour == 0 && S3.Value.Hour != 0)
            {
                EntradaDoFe = 24;
            }
            else
            {
                EntradaDoFe = E3.Value.Hour;
            }

            double DoFeEMIN = E3.Value.Minute;
            double DoFeIMIN = I3.Value.Minute;
            double DoFeSMIN = S3.Value.Minute;
            double DoFeE = EntradaDoFe + DoFeEMIN / 60;
            double DoFeI = I3.Value.Hour + DoFeIMIN / 60;
            double DoFeS = SaidaDoFe + DoFeSMIN / 60;
            double DoFeT = DoFeS - DoFeE;
            double DoFeSomaCalculo = 0;

                double DoFeCALCULO = 0;
                if (DoFeT > 0 && DoFeT < 8)
                {
                    if (DoFeS > 22)
                    {
                        DoFeCALCULO = (DoFeT - (DoFeS - 22) + (DoFeS - 22) * ((double)8 / (double)7)) - DoFeI;
                    }
                    else
                    {
                        DoFeCALCULO = DoFeT - DoFeI;
                    }
                }
                else if (DoFeT >= 8)
                {
                    if (DoFeS > 22)
                    {
                        DoFeCALCULO = (DoFeT - (DoFeS - 22) + (DoFeS - 22) * ((double)8 /(double) 7)) - DoFeI;
                    }
                    else if (DoFeS <= 22)
                    {
                        DoFeCALCULO = DoFeT - DoFeI;
                    }
                    else
                    {
                        DoFeCALCULO = 0;
                    }
                }
                else if (DoFeT >= -17 && DoFeT < 0)
                {

                    if (DoFeE >= 22 && DoFeS >= 5)
                    {
                        DoFeCALCULO = (((double)32 / (double)7) + (DoFeS - 5) + (25 - DoFeE) * ((double)8 / (double)7)) - DoFeI;
                    }
                    else if (DoFeE < 22 && DoFeS > 5)
                    {
                        DoFeCALCULO = (8 + (DoFeS - 5) + (22 - DoFeE)) - DoFeI;
                    }
                    else if (DoFeE < 22 && DoFeS <= 5)
                    {
                        DoFeCALCULO = ((22 - DoFeE) + DoFeS * ((double)8 / (double)7) + ((double)16 / (double)7)) - DoFeI;
                    }
                    else
                    {
                        DoFeCALCULO = 0;
                    }
                }
                else
                {
                    DoFeCALCULO = 0;
                }

                if (DoFeCALCULO > 0)
                {
                    DoFeSomaCalculo = DoFeCALCULO;
                }
                else
                {
                    DoFeSomaCalculo = 0;
                }


            if (DoFeBOX.Text == "")
            {
                DoFeBOXValue = 0;
            }
            else
            {
                if (choice.Text == "Meses Trabalhados")
                {
                    DoFeBOXValue = Convert.ToDouble(DoFeBOX.Text);
                }
                else if (choice.Text == "Dias Trabalhados")
                {
                    DoFeBOXValue = Convert.ToDouble(DoFeBOX.Text)*((double)7/(double)30);
                }
            }

            #endregion

            #region P/ Atualizar
            /*
            double SegE = E5.Value.Hour + E5.Value.Minute / 60;
            double SegI = I5.Value.Hour + I5.Value.Minute / 60;
            double SegS = S5.Value.Hour + S5.Value.Minute / 60;
            double SegT = SegS - (SegI + SegE);

            double TerE = E6.Value.Hour + E6.Value.Minute / 60;
            double TerI = I6.Value.Hour + I6.Value.Minute / 60;
            double TerS = S6.Value.Hour + S6.Value.Minute / 60;
            double TerT = TerS - (TerI + TerE);

            double QuaE = E7.Value.Hour + E8.Value.Minute / 60;
            double QuaI = I7.Value.Hour + I8.Value.Minute / 60;
            double QuaS = S7.Value.Hour + S8.Value.Minute / 60;
            double QuaT = QuaS - (QuaI + QuaE);

            double QuiE = E8.Value.Hour + E8.Value.Minute / 60;
            double QuiI = I8.Value.Hour + I8.Value.Minute / 60;
            double QuiS = S8.Value.Hour + S8.Value.Minute / 60;
            double QuiT = QuiS - (QuiI + QuiE);

            double SexE = E9.Value.Hour + E9.Value.Minute / 60;
            double SexI = I9.Value.Hour + I9.Value.Minute / 60;
            double SexS = S9.Value.Hour + S9.Value.Minute / 60;
            double SexT = SexS - (SexI + SexE);
            */
            #endregion
            
            HET_Segunda_Sabado = (SegSexBOXValue * SegSexSomaCalculo * ((double)150 / (double)7) + (SabBOXValue * SabSomaCalculo * ((double)30 / (double)7)));
            HET_Domingo_Feriado = DoFeSomaCalculo * DoFeBOXValue * ((double)30 / (double)7);

            if (HEButton.Checked == false)
            {
                qutHeDFLabel.Text = "0";
                qutHeSSLabel.Text = "0";
                ValorHEMes = 0;
                ValorTotalHE = 0;
            }
            else
            {
                qutHeDFLabel.Text = Math.Round(HET_Domingo_Feriado, 2).ToString();
                qutHeSSLabel.Text = Math.Round(HET_Segunda_Sabado, 2).ToString();
                ValorTotalHE = ((((SalarioBase / 220) + (SalarioBase / 220) * AdcHE) * HET_Segunda_Sabado) + ((SalarioBase / 220) * 2 * HET_Domingo_Feriado));

                double YDoFeBoxValue = 0;
                double YSegSexBoxValue = 0;
                if (SegSexBOXValue == 0)
                {
                    YSegSexBoxValue = 1;
                }
                else
                {
                    YSegSexBoxValue = SegSexBOXValue;
                }

                if(DoFeBOXValue == 0)
                {
                    YDoFeBoxValue = 1;
                }
                else
                {
                    YDoFeBoxValue = DoFeBOXValue;
                }

                double vvv1 = (((SalarioBase / 220) + (SalarioBase / 220) * AdcHE) * HET_Segunda_Sabado)/YSegSexBoxValue;
                double vvv2 = ((SalarioBase / 220) * 2 * HET_Domingo_Feriado) / YDoFeBoxValue;

                ValorHEMes = vvv1 + vvv2;
            }
            label45.Text = Math.Round(ValorTotalHE, 2).ToString();

        }

        private void ValorHoraNoturna()
        {
            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;

            if (SBBOX.Text == "")
            {
                SalarioBase = 0;
            }
            else
            {
                SalarioBase = Convert.ToDouble(SBBOX.Text);
            }

            #region Valores SegSex

            double XSaidaSegSex = 0;
            double XEntradaSegSex = 0;

            if (S1.Value.Hour == 0 && E1.Value.Hour != 0)
            {
                XSaidaSegSex = 24;
            }
            else
            {
                XSaidaSegSex = S1.Value.Hour;
            }

            if (E1.Value.Hour == 0 && S1.Value.Hour != 0)
            {
                XEntradaSegSex = 24;
            }
            else
            {
                XEntradaSegSex = E1.Value.Hour;
            }

            double XSegSexEMIN = E1.Value.Minute;
            double XSegSexIMIN = I1.Value.Minute;
            double XSegSexSMIN = S1.Value.Minute;
            double XSegSexE = XEntradaSegSex + XSegSexEMIN / 60;
            double XSegSexI = I1.Value.Hour + XSegSexIMIN / 60;
            double XSegSexS = XSaidaSegSex + XSegSexSMIN / 60;
            double XSegSexT = XSegSexS - XSegSexE;
            double SegSexSomaHN = 0;

            double SegSexHN = 0;
            if (XSegSexT >= 0)
            {
                if (XSegSexS > 22)
                {
                    SegSexHN = (XSegSexS - 22) * ((double)8 / (double)7);
                }
                else
                {
                    SegSexHN = 0;
                }
            }
            else if (XSegSexT >= (-17) && XSegSexT < 0)
            {
                if (XSegSexE > 22 && XSegSexS >= 5)
                {
                    SegSexHN = (25 - XSegSexE) * ((double)8 / (double)7) + ((double)32 / (double)7);
                }
                else if (XSegSexE <= 22 && XSegSexS >= 5)
                {
                    SegSexHN = 8;
                }
                else if (XSegSexE < 22 && XSegSexS <= 5)
                {
                    SegSexHN = (XSegSexS * ((double)8 / (double)7) + ((double)16 / (double)7));
                }
                else
                {
                    SegSexHN = 0;
                }
            }
            else
            {
                SegSexHN = 0;
            }

            SegSexSomaHN = SegSexHN;


            if (SegSexBOX.Text == "")
            {
                XSegSexBOXValue = 0;
            }
            else
            {
                if (choice.Text == "Meses Trabalhados")
                {
                    XSegSexBOXValue = Convert.ToDouble(SegSexBOX.Text);
                }
                else if (choice.Text == "Dias Trabalhados")
                {
                    XSegSexBOXValue = Convert.ToDouble(SegSexBOX.Text) * ((double)7 / (double)150);
                }
            }

            #endregion

            #region Valores Sab

            double XSaidaSab = 0;
            double XEntradaSab = 0;

            if (S2.Value.Hour == 0 && E2.Value.Hour != 0)
            {
                XSaidaSab = 24;
            }
            else
            {
                XSaidaSab = S2.Value.Hour;
            }

            if (E2.Value.Hour == 0 && S2.Value.Hour != 0)
            {
                XEntradaSab = 24;
            }
            else
            {
                XEntradaSab = E2.Value.Hour;
            }

            double XSabEMIN = E2.Value.Minute;
            double XSabIMIN = I2.Value.Minute;
            double XSabSMIN = S2.Value.Minute;
            double XSabE = XEntradaSab + XSabEMIN / 60;
            double XSabI = I2.Value.Hour + XSabIMIN / 60;
            double XSabS = XSaidaSab + XSabSMIN / 60;
            double XSabT = XSabS - XSabE;
            double SabSomaHN = 0;

            double SabHN = 0;
            if (XSabT >= 0)
            {
                if (XSabS > 22)
                {
                    SabHN = (XSabS - 22) * ((double)8 / (double)7);
                }
                else
                {
                    SabHN = 0;
                }
            }
            else if (XSabT >= (-17) && XSabT < 0)
            {
                if (XSabE > 22 && XSabS >= 5)
                {
                    SabHN = (25 - XSabE) * ((double)8 / (double)7) + ((double)32 / (double)7);
                }
                else if (XSabE <= 22 && XSabS >= 5)
                {
                    SabHN = 8;
                }
                else if (XSabE < 22 && XSabS <= 5)
                {
                    SabHN = (XSabS * ((double)8 / (double)7) + ((double)16 / (double)7));
                }
                else
                {
                    SabHN = 0;
                }
            }
            else
            {
                SabHN = 0;
            }

            SabSomaHN = SabHN;


            if (SabBOX.Text == "")
            {
                XSabBOXValue = 0;
            }
            else
            {
                if (choice.Text == "Meses Trabalhados")
                {
                    XSabBOXValue = Convert.ToDouble(SabBOX.Text);
                }
                else if (choice.Text == "Dias Trabalhados")
                {
                    XSabBOXValue = Convert.ToDouble(SabBOX.Text) * ((double)7 / (double)150);
                }
            }

            #endregion

            #region Valores DoFe

            double XSaidaDoFe = 0;
            double XEntradaDoFe = 0;

            if (S3.Value.Hour == 0 && E3.Value.Hour != 0)
            {
                XSaidaDoFe = 24;
            }
            else
            {
                XSaidaDoFe = S3.Value.Hour;
            }

            if (E3.Value.Hour == 0 && S3.Value.Hour != 0)
            {
                XEntradaDoFe = 24;
            }
            else
            {
                XEntradaDoFe = E3.Value.Hour;
            }

            double XDoFeEMIN = E3.Value.Minute;
            double XDoFeIMIN = I3.Value.Minute;
            double XDoFeSMIN = S3.Value.Minute;
            double XDoFeE = XEntradaDoFe + XDoFeEMIN / 60;
            double XDoFeI = I3.Value.Hour + XDoFeIMIN / 60;
            double XDoFeS = XSaidaDoFe + XDoFeSMIN / 60;
            double XDoFeT = XDoFeS - XDoFeE;
            double DoFeSomaHN = 0;

            double DoFeHN = 0;
            if (XDoFeT >= 0)
            {
                if (XDoFeS > 22)
                {
                    DoFeHN = (XDoFeS - 22) * ((double)8 / (double)7);
                }
                else
                {
                    DoFeHN = 0;
                }
            }
            else if (XDoFeT >= (-17) && XDoFeT < 0)
            {
                if (XDoFeE > 22 && XDoFeS >= 5)
                {
                    DoFeHN = (25 - XDoFeE) * ((double)8 / (double)7) + ((double)32 / (double)7);
                }
                else if (XDoFeE <= 22 && XDoFeS >= 5)
                {
                    DoFeHN = 8;
                }
                else if (XDoFeE < 22 && XDoFeS <= 5)
                {
                    DoFeHN = (XDoFeS * ((double)8 / (double)7) + ((double)16 / (double)7));
                }
                else
                {
                    DoFeHN = 0;
                }
            }
            else
            {
                DoFeHN = 0;
            }

            DoFeSomaHN = DoFeHN;


            if (DoFeBOX.Text == "")
            {
                XDoFeBOXValue = 0;
            }
            else
            {
                if (choice.Text == "Meses Trabalhados")
                {
                    XDoFeBOXValue = Convert.ToDouble(DoFeBOX.Text);
                }
                else if (choice.Text == "Dias Trabalhados")
                {
                    XDoFeBOXValue = Convert.ToDouble(DoFeBOX.Text) * ((double)7 / (double)150);
                }
            }

            #endregion

            #region P/ Atualizar
            /*double SegE = E5.Value.Hour + E5.Value.Minute / 60;
            double SegI = I5.Value.Hour + I5.Value.Minute / 60;
            double SegS = S5.Value.Hour + S5.Value.Minute / 60;
            double SegT = SegS - (SegI + SegE);

            double TerE = E6.Value.Hour + E6.Value.Minute / 60;
            double TerI = I6.Value.Hour + I6.Value.Minute / 60;
            double TerS = S6.Value.Hour + S6.Value.Minute / 60;
            double TerT = TerS - (TerI + TerE);

            double QuaE = E7.Value.Hour + E8.Value.Minute / 60;
            double QuaI = I7.Value.Hour + I8.Value.Minute / 60;
            double QuaS = S7.Value.Hour + S8.Value.Minute / 60;
            double QuaT = QuaS - (QuaI + QuaE);

            double QuiE = E8.Value.Hour + E8.Value.Minute / 60;
            double QuiI = I8.Value.Hour + I8.Value.Minute / 60;
            double QuiS = S8.Value.Hour + S8.Value.Minute / 60;
            double QuiT = QuiS - (QuiI + QuiE);

            double SexE = E9.Value.Hour + E9.Value.Minute / 60;
            double SexI = I9.Value.Hour + I9.Value.Minute / 60;
            double SexS = S9.Value.Hour + S9.Value.Minute / 60;
            double SexT = SexS - (SexI + SexE);*/
            #endregion


            if (HEButton.Checked == true && checkBox2.Checked == true)
            {
                ValorTotalHN = SegSexSomaHN * XSegSexBOXValue * ((double)150 / (double)7) * HoraNoturna * (SalarioBase / 220) + SabSomaHN * XSabBOXValue * ((double)30 / (double)7) * HoraNoturna * (SalarioBase / 220) + DoFeSomaHN * XDoFeBOXValue * HoraNoturna * ((double)30 / (double)7) * (SalarioBase / 220);
                double YYDoFeBoxValue = 0;
                double YYSegSexBoxValue = 0;
                double YYSabBoxValue = 0;
                if (SegSexBOXValue == 0)
                {
                    YYSegSexBoxValue = 1;
                }
                else
                {
                    YYSegSexBoxValue = SegSexBOXValue;
                }

                if (DoFeBOXValue == 0)
                {
                    YYDoFeBoxValue = 1;
                }
                else
                {
                    YYDoFeBoxValue = DoFeBOXValue;
                }

                if(SabBOXValue == 0)
                {
                    YYSabBoxValue = 1;
                }
                else
                {
                    YYSabBoxValue = SabBOXValue;
                }

                double vvvv1 = SegSexSomaHN * XSegSexBOXValue * ((double)150 / (double)7) * HoraNoturna * (SalarioBase / 220) / YYSegSexBoxValue;
                double vvvv3 = SabSomaHN * XSabBOXValue * ((double)30 / (double)7) * HoraNoturna * (SalarioBase / 220) / YYSabBoxValue;
                double vvvv2 = DoFeSomaHN * XDoFeBOXValue * HoraNoturna * ((double)30 / (double)7) * (SalarioBase / 220) / YYDoFeBoxValue;
                ValorHNMes = vvvv1 + vvvv2 + vvvv3;
                label78.Text = Math.Round((SegSexSomaHN * XSegSexBOXValue * ((double)150 / (double)7) + SabSomaHN * XSabBOXValue * ((double)30 / (double)7) + DoFeSomaHN * XDoFeBOXValue * ((double)30 / (double)7)), 2).ToString();
                label64.Text = Math.Round(ValorTotalHN, 2).ToString();
            }
            else
            {
                ValorTotalHN = 0;
                ValorHNMes = 0;
                label78.Text = "0";
                label64.Text = "0";
            }
        }

        private double RemuneraçaoX()
        {
            double AnosH = dataAfast.Value.Year - dataAdm.Value.Year;
            double MesesH = dataAfast.Value.Month - dataAdm.Value.Month;
            double DiasH = dataAfast.Value.Day - dataAdm.Value.Day;

            if (SBBOX.Text == "")
            {
                SalarioBase = 0;
            }
            else
            {
                SalarioBase = Convert.ToDouble(SBBOX.Text);
            }

            if (textBox6.Text == "")
            {
                Comissoes = 0;
            }
            else
            {
                Comissoes = Convert.ToDouble(textBox6.Text);
            }
            ValorHoraExtra();
            ValorHoraNoturna();
            Insalubridade();
            Periculosidade();

            Remuneração = SalarioBase + Comissoes + (ValorInsalubridade/( AnosH * 12 + MesesH + DiasH / 30)) + (ValorPericulosidade / (AnosH * 12 + MesesH + DiasH / 30)) + ValorHNMes + ValorHEMes;
            label76.Text = Math.Round(Remuneração,2).ToString();
            return Remuneração;
        }

        private double Insalubridade()
        {
            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;

            #region Checagem INSALUBRIDADE
            if (InsalubButton.Checked == false)
            {
                ValorInsalubridade = 0;
                labelInsalub.Text = "0";
            }
            #endregion

            #region Calculo INSALUBRIDADE
            {
                if (InsalubButton.Checked == true && comboBox5.Text == "10%")
                    ValorInsalubridade = SalárioMínimo * 0.1 * (Meses + Anos * 12 + Dias / 30);
                else if (InsalubButton.Checked == true && comboBox5.Text == "20%")
                    ValorInsalubridade = SalárioMínimo * 0.2 * (Meses + Anos * 12 + Dias / 30);
                else if (InsalubButton.Checked == true && comboBox5.Text == "40%")
                    ValorInsalubridade = SalárioMínimo * 0.4 * (Meses + Anos * 12 + Dias / 30);
                else
                    ValorInsalubridade = 0;
            }//calculo da insalubridade

            labelInsalub.Text = Convert.ToString(Math.Round(ValorInsalubridade, 2));
            #endregion
            return ValorInsalubridade;
        }

        private double Periculosidade()
        {
            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;
            #region Checagem e Contas PERICULOSIDADE
            if (PericuButton.Checked == false)
            {
                ValorPericulosidade = 0;
                labelPericu.Text = "0";
            }
            else
            {
                ValorPericulosidade = SalárioMínimo * 0.3 * (Meses + Anos * 12 + Dias / 30);
            }

            labelPericu.Text = Convert.ToString(Math.Round(ValorPericulosidade, 2));
            #endregion

            return ValorPericulosidade;
        }

        private double Multa477()
        {
            RemuneraçaoX();

            if (Multa477Button.Checked == false)
            {
                label57.Text = "0";
                ValorMulta477 = 0;
            }
            else if (Multa477Button.Checked == true)
            {
                ValorMulta477 = Remuneração;
                label57.Text = Math.Round(ValorMulta477, 2).ToString();
            }
            return ValorMulta477;
        }

        private double FeriasProporcionais()
        {

            if (FPropButton.Checked == true)
            {

                double AnosXXXX = dataAfast.Value.Year - dataAdm.Value.Year;
                Meses = dataAfast.Value.Month - dataAdm.Value.Month;
                Dias = dataAfast.Value.Day - dataAdm.Value.Day;



                double DiasAAA = dataAfast.Value.Day;
                double MesesA = 0;
                double MesesB = 0;

                RemuneraçaoX();

                if (dataAfast.Value.Month == dataAdm.Value.Month)
                {
                    AnosXXXX += 1;
                }

                if (AnosXXXX <= 1)
                {
                    MesesB = dataAfast.Value.Month - dataAdm.Value.Month;
                }
                else if(AnosXXXX > 1)
                {
                    MesesB = dataAfast.Value.Month - 1;
                }


                if (DiasAAA >= 15)
                { MesesA = MesesB + 1; }
                else
                { MesesA = MesesB; }

                if (FPropButton.Checked == true)
                { ValorFeriasProporcionais = (MesesA * Remuneração / 12) + (MesesA * Remuneração / 36); }
                else
                { ValorFeriasProporcionais = 0; }

                label58.Text = Math.Round(ValorFeriasProporcionais, 2).ToString();
            }
            else
            {
                ValorFeriasProporcionais = 0;
                label58.Text = "0";
            }
            return ValorFeriasProporcionais;
        }

        private double FeriasAtrasadas()
        {
            RemuneraçaoX();
            if (FAtraButton.Checked == true)
            {
                ValorFeriasAtrasadas = Remuneração * ((double)8 / (double)3) * Convert.ToDouble(numericUpDown2.Value);
                label59.Text = Math.Round(ValorFeriasAtrasadas, 2).ToString();
            }
            else
            {
                ValorFeriasAtrasadas = 0;
                label59.Text = "0";
            }

            return ValorFeriasAtrasadas;
        }

        private double SaldoSalario()
        {
            double DiasY = dataAfast.Value.Day;
            RemuneraçaoX();
            if(ButtonSS.Checked==true)
            {
                ValorSaldoSalario = (Remuneração / 30) * DiasY;
                label65.Text = Math.Round(ValorSaldoSalario,2).ToString();
            }
            else
            {
                ValorSaldoSalario = 0;
                label65.Text = "0";
            }

            return ValorSaldoSalario;
        }

        private double CalcFGTS()
        {
            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;
            double DiasWWW = dataAfast.Value.Day;
            double DiasXXX = 0;
            if (DiasWWW >= 15)
            { DiasXXX = 1; }
            else if (DiasWWW < 15)
            { DiasXXX = 0; }

            RemuneraçaoX();

            if (FGTSButton.Checked == true)
            {
                if (comboBox1.Text == "Dispensa Sem Justa Causa")
                {
                    ValorFGTS = Remuneração * FGTS * (Anos * 12 + Meses + DiasXXX) + Remuneração * FGTS * (Anos * 12 + Meses + DiasXXX) * FGTS_SJC;
                    label72.Text = Math.Round(ValorFGTS, 2).ToString() + "  ,com 40%.";
                }
                else
                {
                    ValorFGTS = Remuneração * FGTS * (Anos * 12 + Meses + DiasXXX);
                    label72.Text = Math.Round(ValorFGTS, 2).ToString();
                }

            }
            else
            {
                ValorFGTS = 0;
                label72.Text = "0";
            }
            return ValorFGTS;
        }

        private double AvisoPrevio()
        {
            int Anos1 = dataAfast.Value.Year - dataAdm.Value.Year;
            int Meses1 = dataAfast.Value.Month - dataAdm.Value.Month;
            int Dias1 = dataAfast.Value.Day - dataAdm.Value.Day;
            RemuneraçaoX();

            int valor = (Anos1 * 12 + Meses1 + Dias1 / 30);

            int v = 0;
            if ((30 + (Anos1 * 12 + Meses1 + Dias1 / 30) / 12) > 90)
            { v = 90; }
            else
            {
                if (valor >= 12)
                {
                    v = (30 + 3 * (valor / (int)12));
                }
                else
                {
                    v = 30;
                }
            }

            if (comboBox2.Text == "NÃO")
            {
                ValorAvisoPrevio = 0;
            }
            else if (comboBox2.Text == "Trabalhado")
            {
                ValorAvisoPrevio = (Remuneração / 30) * v; ;
            }
            else if (comboBox2.Text == "Indenizado Pelo Empregado")
            {
                ValorAvisoPrevio = - Remuneração;
            }
            else if (comboBox2.Text == "Indenizado Pela Firma")
            {
                ValorAvisoPrevio = (Remuneração / 30) * v; ;
            }

            label63.Text = Math.Round(ValorAvisoPrevio,2).ToString();
            return ValorAvisoPrevio;
        }

        private double SeguroDesemprego()
        {
            RemuneraçaoX();
            double hh = 0;
            double y = 0;

            if (SDesemButton.Checked == true)
            {
                if (comboBox4.Text == "Primeira")
                {

                    if (numericUpDown1.Value >= 12 && numericUpDown1.Value <= 23)
                    {
                        hh = 4;
                    }
                    else if (numericUpDown1.Value > 23)
                    {
                       hh = 5;
                    }

                }
                else if (comboBox4.Text == "Segunda")
                {
                    if (numericUpDown1.Value >= 9 && numericUpDown1.Value <= 11)
                    {
                        hh = 3;
                    }
                    else if (numericUpDown1.Value > 11 && numericUpDown1.Value <= 23)
                    {
                        hh = 4;
                    }
                    else if (numericUpDown1.Value > 23)
                    {
                        hh = 5;
                    }

                }
                else if (comboBox4.Text == "Terceira+")
                {
                    if (numericUpDown1.Value >= 6 && numericUpDown1.Value <= 11)
                    {
                        hh = 3;
                    }
                    else if (numericUpDown1.Value > 11 && numericUpDown1.Value <= 23)
                    {
                        hh = 4;
                    }
                    else if (numericUpDown1.Value > 23)
                    {
                        hh = 5;
                    }

                }

                if (Remuneração <= 1360.7)
                {
                    y = Remuneração * 0.8;
                }
                else if(Remuneração > 1360.7 && Remuneração <=2268.05)
                {
                    y = (Remuneração - 1360.7) * 0.5 + 1088.56;
                }
                else if(Remuneração > 2268.05)
                {
                    y = 1542.24;
                }
                else
                {
                    y = 0;
                }

                if (y < SalárioMínimo)
                {
                    y = SalárioMínimo;
                }

                ValorSeguroDesemprego = hh * y;
                label73.Text = hh.ToString() + " parcelas de R$ " + Math.Round(y,2).ToString();
            }
            else
            {
                y = 0;
                ValorSeguroDesemprego = 0;
                label73.Text = "0";
            };
            return ValorSeguroDesemprego;
        }

        private double _13Proporcional()
        {

            double AnosXXXXX = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;
            double DiasAAA = dataAfast.Value.Day;
            double MesesA = 0;
            double MesesB = 0;

            double v1 = 0;
            double v2 = 0;
            //double t1 = 0;
           // double t2 = 0;

            RemuneraçaoX();

            if(dataAfast.Value.Month == dataAdm.Value.Month)
            {
                AnosXXXXX += 1;
            }

            if (AnosXXXXX <= 1)
            {
                MesesB = dataAfast.Value.Month - dataAdm.Value.Month;
            }
            else if (AnosXXXXX > 1)
            {
                MesesB = dataAfast.Value.Month -1;
            }

            if (DiasAAA >= 15)
            { MesesA = MesesB +1 ; }
            else
            { MesesA = MesesB; }

            if (Button13Prop.Checked == true)
            {
                /*if(Remuneração <= 1556.94)
                {
                    t1 = (MesesA * Remuneração / 24) * 0.08;
                }
                else if(Remuneração > 1556.94 && Remuneração <= 2594.92)
                {
                    t1 = (MesesA * Remuneração / 24) * 0.09;
                }
                else if(Remuneração > 2594.92 && Remuneração <= 5189.82)
                {
                    t1 = (MesesA * Remuneração / 24) * 0.11;
                }
                else if(Remuneração > 5189.82)
                {
                    t1 = 570.88;
                }
    
                if(Remuneração <= 1903.98)
                {
                    t2 = 0;
                }
                else if(Remuneração > 1903.98 && Remuneração <= 2826.65)
                {
                    t2 = (MesesA * Remuneração / 24) * 0.075;
                }
                else if(Remuneração > 2826.65 && Remuneração <= 3751.05)
                {
                    t2 = (MesesA * Remuneração / 24) * 0.15;
                }
                else if(Remuneração > 3751.05 && Remuneração <= 4664.68)
                {
                    t2 = (MesesA * Remuneração / 24) * 0.225;
                }
                else if(Remuneração > 4664.68)
                {
                    t2 = (MesesA * Remuneração / 24) * 0.275;
                }*/

                v1 = (MesesA * Remuneração / 24);
                v2 = (MesesA * Remuneração / 24);

                Valor13Proporcional = v1 + v2;
                label82.Text = "1º = " + Math.Round(v1,2).ToString() + "   2º = " + Math.Round(v2,2).ToString();
            }
            else
            {
                Valor13Proporcional = 0;
                label82.Text = "0";
            }
            return Valor13Proporcional;
        }

        private double VALORTOTAL()
        {

            Insalubridade();
            Periculosidade();
            ValorHoraExtra();
            ValorHoraNoturna();

            //

            ProDed();
            AvisoPrevio();
            SaldoSalario();
            FeriasProporcionais();
            Multa477();
            FeriasAtrasadas();
            CalcFGTS();
            SeguroDesemprego();
            _13Proporcional();

            ValorTotal = ValorInsalubridade + ValorPericulosidade + ValorTotalHE + ValorTotalHN + ValorAvisoPrevio + ValorSaldoSalario + ValorFeriasAtrasadas + ValorFeriasProporcionais + ValorMulta477 + ValorFGTS + ValorSeguroDesemprego + Valor13Proporcional + ValorProDed;
            label70.Text = Math.Round(ValorTotal,2).ToString() + " Reais";
            return ValorTotal;
        }

        /* private double DSR()
        {
            ValorHoraExtra();
            RemuneraçaoX();
            if(checkBox2.Checked == true)
            {
                ValorDSR = ValorHEMes * 5.119*((Remuneração/220)*2) / 24.88;
                label63.Text = Math.Round(ValorDSR,2).ToString();
            }
            else if(checkBox2.Checked ==false)
            {
                label63.Text = "0";
            }
            
            return ValorDSR;
            
        }
        */

        private void VerificarBotao()
        {
            Anos = dataAfast.Value.Year - dataAdm.Value.Year;
            Meses = dataAfast.Value.Month - dataAdm.Value.Month;
            Dias = dataAfast.Value.Day - dataAdm.Value.Day;

            if ((Anos + Meses / 12 + Dias / 360) < 1)
            {
                FAtraButton.Enabled = false;
                FAtraButton.Checked = false;
            }
            else
            {
                FAtraButton.Enabled = true;
            }

            if (comboBox1.Text == "Pedido de Demissão")
            {
                comboBox2.Enabled = true;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("NÃO");
                comboBox2.Items.Add("Trabalhado");
                comboBox2.Items.Add("Indenizado Pelo Empregado");
                Button13Prop.Enabled = true;
                FPropButton.Enabled = true;
                SDesemButton.Enabled = false;
                SDesemButton.Checked = false;
            } 
            else if(comboBox1.Text == "Dispensa Com Justa Causa")
            {
                Button13Prop.Enabled = false;
                Button13Prop.Checked = false;
                comboBox2.Text = "NÃO";
                comboBox2.Items.Clear();
                comboBox2.Enabled = false;
                FPropButton.Checked = false;
                FPropButton.Enabled = false;
                SDesemButton.Enabled = false;
                SDesemButton.Checked = false;


            }
            else if(comboBox1.Text == "Dispensa Sem Justa Causa")
            {
                comboBox2.Enabled = true;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("NÃO");
                comboBox2.Items.Add("Trabalhado");
                comboBox2.Items.Add("Indenizado Pela Firma");
                Button13Prop.Enabled = true;
                FPropButton.Enabled = true;
                SDesemButton.Enabled = true;
            }


        }

        #region Proventos e deduçoes
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text == "Provento")
            {
                label38.Text = "+";
            }
            else
            {
                label38.Text = "-";
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.Text == "Provento")
            {
                label43.Text = "+";
            }
            else if (comboBox7.Text == "Dedução")
            {
                label43.Text = "-";
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.Text == "Provento")
            {
                label42.Text = "+";
            }
            else if (comboBox8.Text == "Dedução")
            {
                label42.Text = "-";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            label81.Text = textBox9.Text + ":";
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            label77.Text = textBox10.Text + ":";
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox8.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                textBox8.Text = "0";
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox11.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                textBox11.Text = "0";
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox12.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                textBox12.Text = "0";
            }
        }

        private void ProDed()
        {
            double v1=0, v2=0, v3 = 0;
            if(PDButton.Checked == true)
            {
                if (textBox8.Text != "")
                {
                    if (label38.Text == "+")
                    {
                        v1 = +Convert.ToDouble(textBox8.Text);
                    }
                    else if (label38.Text == "-")
                    {
                        v1 = -Convert.ToDouble(textBox8.Text);
                    }
                    
                }
                else
                {
                    v1 = 0;
                }

                if (textBox11.Text != "")
                {
                    if (label43.Text == "+")
                    {
                        v2 = +Convert.ToDouble(textBox11.Text);
                    }
                    else if (label43.Text == "-")
                    {
                        v2 = -Convert.ToDouble(textBox11.Text);
                    }
                    
                }
                else
                {
                    v2 = 0;
                }

                if (textBox12.Text != "")
                {
                    if (label42.Text == "+")
                    {
                        v3 = +Convert.ToDouble(textBox12.Text);
                    }
                    else if (label42.Text == "-")
                    {
                        v3 = -Convert.ToDouble(textBox12.Text);
                    }

                    
                }
                else
                {
                    v3 = 0;
                }

                ValorProDed = v1 + v2 + v3;
            }
            else
            {
                ValorProDed = 0;
            }
        }

        #endregion

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "[^0-9]"))
            {
                MessageBox.Show("Digite Apenas Números!");
                textBox6.Text = "0";
            }
        }
    }
}


