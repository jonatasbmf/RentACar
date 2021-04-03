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
    public partial class ListagemView : ContentPage
    {
        public ListagemViewModel viewModel { get; set; }
        readonly Usuario usuario;
        public ListagemView(Usuario usuario)
        {
            InitializeComponent();
            this.viewModel = new ListagemViewModel();
            this.usuario = usuario;
            this.BindingContext = this.viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Veiculo>(this, "VeiculoSelecionado",
                (veiculo) =>
                {
                    Navigation.PushAsync(new DetalheView(veiculo, this.usuario));
                });

            await this.viewModel.GetVeiculosAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Veiculo>(this, "VeiculoSelecionado");
        }
    }
}
