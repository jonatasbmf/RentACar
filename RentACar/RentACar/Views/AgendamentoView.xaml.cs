using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Models;
using RentACar.ViewModels;
using Xamarin.Forms;

namespace RentACar.Views
{
    public partial class AgendamentoView : ContentPage
    {
        public AgendamentoViewModel ViewModel { get; set; }

        public AgendamentoView(Veiculo veiculo, Usuario usuario)
        {
            InitializeComponent();
            this.ViewModel = new AgendamentoViewModel(veiculo, usuario);
            this.BindingContext = this.ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento",
                async (msg) =>
                {
                    var confirmacao = await DisplayAlert("Confirma Agendamento?", "Confirme os dados e a operação!", "Sim", "Não");
                    if (confirmacao)
                    {
                        this.ViewModel.SalvarAgendamento();
                    }
                });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento",
                async (msg) =>
                {
                    await DisplayAlert("Agendamento", "Agendamento salvo com sucesso!", "OK");
                    await Navigation.PopToRootAsync();
                });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento",
                async (msg) =>
                {
                    await DisplayAlert("Falha no Agendamento", "Não foi possivel concluir o agendamento!", "OK");
                    await Navigation.PopToRootAsync();
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
        }
    }
}
