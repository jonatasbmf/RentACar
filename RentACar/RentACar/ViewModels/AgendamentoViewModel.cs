using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using RentACar.Data;
using RentACar.Models;
using RentACar.Services;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        #region PROPRIEDADES
        
        public Agendamento Agendamento { get; set; }

        private string modelo;
        public string Modelo
        {
            get { return Agendamento.Modelo; }
            set { this.Agendamento.Modelo = value; }
        }

        private decimal preco;
        public decimal Preco
        {
            get { return Agendamento.Preco; }
            set { this.Agendamento.Preco = value; }
        }

        public ICommand AgendarCommand { get; set; }

        public string Nome
        {
            get { return Agendamento.Nome; }
            set
            {
                Agendamento.Nome = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }

        }

        public string Fone
        {
            get { return Agendamento.Fone; }
            set
            {
                Agendamento.Fone = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }

        }

        public string Email
        {
            get { return Agendamento.Email; }
            set
            {
                Agendamento.Email = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public DateTime DataAgendamento
        {
            get
            {
                return Agendamento.DataAgendamento;
            }
            set
            {
                Agendamento.DataAgendamento = value;
            }
        }

        public TimeSpan HoraAgendamento
        {
            get
            {
                return Agendamento.HoraAgendamento;
            }
            set
            {
                Agendamento.HoraAgendamento = value;
            }
        }
        #endregion

        #region METODOS
        public async void SalvarAgendamento()
        {
            AgendamentoService agendamentoService = new AgendamentoService();
            await agendamentoService.EnviarAgendamento(this.Agendamento);
        }

        public AgendamentoViewModel(Veiculo veiculo, Usuario usuario)
        {
            this.Agendamento = new Agendamento(usuario.nome, usuario.telefone, usuario.email, veiculo.Nome, veiculo.Preco);

            AgendarCommand = new Command(() =>
            {
                MessagingCenter.Send<Agendamento>(this.Agendamento
                    , "Agendamento");
            }, () =>
              {
                  return !string.IsNullOrEmpty(this.Nome)
                   && !string.IsNullOrEmpty(this.Fone)
                   && !string.IsNullOrEmpty(this.Email);
              });
        }
        #endregion
    }
}
