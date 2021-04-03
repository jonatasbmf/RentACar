using RentACar.Models;
using RentACar.Services;
using RentACar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentACar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentoUsuarioView : ContentPage
    {
        readonly AgendamentoUsuarioViewModel _viewModel;
        public AgendamentoUsuarioView()
        {
            InitializeComponent();
            _viewModel = new AgendamentoUsuarioViewModel();
            this.BindingContext = _viewModel;

        }
        private async Task ReenvioAgendamento(Agendamento agendamento)
        {
            if (!agendamento.Confirmado)
            {
                var reenvio = await DisplayAlert("Reenviar Agendamento", "Confirma reenvio?", "Sim", "Não");
                if (reenvio)
                {
                    AgendamentoService agendamentoService = new AgendamentoService();
                    await agendamentoService.EnviarAgendamento(agendamento);
                    _viewModel.AtualizarLista();
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Agendamento>(this, "AgedamentoSelecionado",
                async (agendamento) =>
                {
                    await ReenvioAgendamento(agendamento);
                });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento",
                async (agendamento) =>
                {
                    await DisplayAlert("Reenvio", "Reenvio com sucesso!", "OK");
                });

            MessagingCenter.Subscribe<Agendamento>(this, "FalhaAgendamento",
               async (agendamento) =>
               {
                   await DisplayAlert("Reenvio", "Falha no reenvio!", "OK");
               });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Agendamento>(this, "AgedamentoSelecionado");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "FalhaAgendamento");
        }
    }
}