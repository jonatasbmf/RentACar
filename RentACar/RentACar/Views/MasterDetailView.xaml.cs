using RentACar.Models;
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
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly Usuario usuario;
        public MasterDetailView(Usuario usuario)
        {
            InitializeComponent();
            //this.Detail = new ListagemView();
            this.usuario = usuario;
            this.Master = new MasterView(usuario);
            this.Detail = new NavigationPage(new ListagemView(usuario));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMensagens();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarAssinatura();
        }
        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Usuario>(this, "MeusAgendamentos", (user) =>
            {
                this.Detail = new NavigationPage(new AgendamentoUsuarioView());
                this.IsPresented = false;
            });

            MessagingCenter.Subscribe<Usuario>(this, "NovoAgendamento", (user) =>
            {
                this.Detail = new NavigationPage(new ListagemView(user));
                this.IsPresented = false;
            });
        }
        private void CancelarAssinatura()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "MeusAgendamentos");
            MessagingCenter.Unsubscribe<Usuario>(this, "NovoAgendamento");
        }
    }
}